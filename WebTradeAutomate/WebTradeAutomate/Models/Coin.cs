using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTradeAutomate.Models
{
   /// <summary>
   /// класс представления монет (добавлен в случае необходимости расширения) 
   /// </summary>
   public class Coin
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public int Cost { get; set; }
      public bool IsUsed { get; set; }
   }
}
