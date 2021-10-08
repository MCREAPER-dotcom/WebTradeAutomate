using Microsoft.EntityFrameworkCore;
namespace WebTradeAutomate.Models
{
   public class InfoTradeContext : DbContext
   {
      public DbSet<InfoTrade> InfoTrades { get; set; }
      public InfoTradeContext(DbContextOptions<InfoTradeContext> options)
          : base(options)
      {
         Database.EnsureCreated();
      }
   }
}
