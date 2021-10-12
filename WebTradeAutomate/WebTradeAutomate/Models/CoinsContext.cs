using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTradeAutomate.Models
{
   public class CoinsContext : DbContext
   {
      public DbSet<Coin> Coins { get; set; }
      public CoinsContext(DbContextOptions<CoinsContext> options)
          : base(options)
      {
         Database.EnsureCreated();
      }
   }
}
