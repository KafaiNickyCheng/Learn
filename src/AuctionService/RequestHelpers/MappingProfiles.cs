using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // create a map 
        // as Auction Table a start point
        // to shape like the AuctionDTO end point
        // as also includes the Item table inside of that AuctionDTO
        // because AuctionDTO have some properties that come from the Item Enities
        CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item);
        // tell the Mapping again to map Item into the AuctionDTO
        CreateMap<Item, AuctionDto>();

        // so this tell that the CreateAuctionDto will go into the Auction Enities
        CreateMap<CreateAuctionDto, Auction>()
            .ForMember(d => d.Item, o=> o.MapFrom(s => s));

        // 
        CreateMap<CreateAuctionDto, Item>();

    }
}