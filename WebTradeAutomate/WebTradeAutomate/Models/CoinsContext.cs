using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTradeAutomate.Models
{
   /// <summary>
   /// класс необходимый для взаимодействия с базой данных монет
   /// </summary>
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
