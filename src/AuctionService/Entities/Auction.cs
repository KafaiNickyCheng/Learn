using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService.Entities; // namespace to physical location

public class Auction
{
    public Guid id { get; set; } // primary key
    public int ReservePrice { get; set; } = 0; // initial value.
    public string Seller { get; set; }
    public string Winner { get; set; }
    public int? SoldAmount { get; set; }
    public int? CurrentHighBid { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // this should be use as UtcNow instead of Now for the suggestion of PostgresSQL 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime AuctionEnd { get; set; }
    public Status Status { get; set; } // related properties
    public Item Item { get; set; } // related properties

    // but in order to put the relation between Enities this need to be defined
}