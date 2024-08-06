using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions options) : base(options)
    {
    //                      <This provides the table in database to be able to queryable>
    }

    // DbContext need to tell for each table names
    public DbSet<Auction> Auctions { get; set; }
}

