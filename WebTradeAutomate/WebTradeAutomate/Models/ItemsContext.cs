using Microsoft.EntityFrameworkCore;

namespace WebTradeAutomate.Models
{
   /// <summary>
   /// класс необходимый для взаимодействия с базой данных товаров
   /// </summary>
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
