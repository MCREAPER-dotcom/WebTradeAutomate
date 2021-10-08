using Microsoft.EntityFrameworkCore;

namespace WebTradeAutomate.Models
{
   public class ItemsContext : DbContext
   {
      public DbSet<Item> Items{ get; set; }
      public ItemsContext(DbContextOptions<ItemsContext> options)
          : base(options)
      {
         Database.EnsureCreated();
      }
   }
}
