using System.ComponentModel.DataAnnotations;

namespace AuctionService.DTOs;

public class CreateAuctionDto
{
    // the properties that you want to take from the user

    // this is from the item properties
    [Required]
    public string Make { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    public int Mileage { get; set; }
    [Required]
    public string ImageUrl { get; set; }

    // this is from the Auction properties
    [Required]
    public int ReservePrice { get; set; }
    [Required]
    public DateTime AuctionEnd { get; set; }
}