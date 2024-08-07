using DnsClient;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using ZstdSharp.Unsafe;

namespace SearchService;

[ApiController]
[Route("api/Search")]
public class SearchService : ControllerBase
{
    // [HttpGet]
    // public async Task<ActionResult<List<Item>>> SearchItems(string searchTerm)
    // {
    //     var query = DB.Find<Item>(); // the linq query of mongoDb

    //     query.Sort(x => x.Ascending(a => a.Make));

    //     if (!String.IsNullOrEmpty(searchTerm))
    //     {
    //         query.Match(Search.Full, searchTerm).SortByTextScore();
    //         // search by the entire string
    //     }

    //     var result = await query.ExecuteAsync();

    //     return result;
    // }

    // if the result is so huge we should implemented pagination
    
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery]SearchParams searchParams) // binding SearchParams
    {
        var query = DB.PagedSearch<Item, Item>(); // pagination

        if (!String.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
            // search by the entire all columns
        }

        // add Sorting // by the Orderby params
        query = searchParams.OrderBy switch
        {
            "make" => query.Sort(x => x.Ascending(a => a.Make)),
            "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
            _ => query.Sort(x => x.Ascending(a => a.AuctionEnd)) // default sorting
        };

        // add filtering // by the Filterby params
        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
            "endingSoon" => query.Match(x => x.AuctionEnd < DateTime.UtcNow.AddHours(6) 
                && x.AuctionEnd > DateTime.UtcNow),
            _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow)
        };

        // check the searchParams
        if (!String.IsNullOrEmpty(searchParams.Seller))
        {
            query.Match(x => x.Seller == searchParams.Seller);
        }

        if (!String.IsNullOrEmpty(searchParams.Winner))
        {
            query.Match(x => x.Winner == searchParams.Winner);
        }

        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        var result = await query.ExecuteAsync();

        // return result; // instead of returning the results
        // return a Ok

        return Ok(new 
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });

    }
}