using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebTradeAutomate.Models
{
   public class DataBaseContext : DbContext
   {
      public DbSet<BaseLogic> BaseLogics { get; set; }
      public DbSet<Item> Items { get; set; }
      public DbSet<Coin> Coins { get; set; }
      public DataBaseContext(DbContextOptions<DataBaseContext> options)
         : base(options)
      {
         //Database.EnsureDeleted();
         Database.EnsureCreated();
      }
   }
}
