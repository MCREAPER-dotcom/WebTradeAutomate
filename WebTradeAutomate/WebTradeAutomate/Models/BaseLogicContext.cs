using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebTradeAutomate.Models
{
   public class BaseLogicContext : DbContext
   {
      public DbSet<BaseLogic> BaseLogics { get; set; }
      public BaseLogicContext(DbContextOptions<BaseLogicContext> options)
         : base(options)
      {
         Database.EnsureCreated();
      }
   }
}
