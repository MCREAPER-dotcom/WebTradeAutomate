using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTradeAutomate.Models;
namespace WebTradeAutomate.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class BaseLogicController : ControllerBase
   {
      private BaseLogicContext db;

      public BaseLogicController(BaseLogicContext context)
      {
         db = context;
         if (!db.BaseLogics.Any())
         {
            db.BaseLogics.Add(new BaseLogic {ItemName = "000",CoinSumm = 0,CoinCount1 = 0,CoinCount2 = 0,CoinCount3 = 0,CoinCount4 = 0});
            db.SaveChanges();
         }
      }

      [HttpGet("getitem")]
      public ActionResult<string> GetItem()
      {
         BaseLogic bl = db.BaseLogics.First(x => x.Id == 1);
         if (int.Parse(bl.ItemName) == 0 || int.Parse(bl.ItemName) > 999)
         {
            bl.ItemName = "000";
         }
         return Ok(bl);
      }

      [HttpPut("putitem/{item}")]
      public async Task<ActionResult<string>> PutItem(string item)
      {
         BaseLogic bl = db.BaseLogics.First(x => x.Id == 1);
         var nameitem = $"{item}";
         if(bl.ItemName!="000")
            bl.ItemName = bl.ItemName + nameitem;
         else
            bl.ItemName = nameitem;

         if (int.Parse(bl.ItemName) <= 0 || int.Parse(bl.ItemName) > 999)
         {
            bl.ItemName = "000";
            db.Update(bl);
            await db.SaveChangesAsync();
            return Ok(bl);
         }
         db.Update(bl);
         await db.SaveChangesAsync();
         return Ok(bl);
      }
      [HttpPut("putcoin/{cost}")]
      public async Task<ActionResult<string>> PutItem(int cost)
      {
         BaseLogic bl = db.BaseLogics.First(x => x.Id == 1);

         if (cost == 1)
            bl.CoinCount1++;
         if (cost == 2)
            bl.CoinCount2++;
         if (cost == 5)
            bl.CoinCount3++;
         if (cost == 10)
            bl.CoinCount4++;
         bl.CoinSumm += cost;
         if (bl.CoinSumm == 0)
         {
            bl.CoinCount1 = bl.CoinCount2 = bl.CoinCount3 = bl.CoinCount4 = 0;
         }

         db.Update(bl);
         await db.SaveChangesAsync();
         return Ok(bl);
      }
      [HttpPut("takecoin")]
      public async Task<ActionResult<string>>TakeItem()
      {
         BaseLogic bl = db.BaseLogics.First(x => x.Id == 1);
         if (bl.CoinSumm == 0)
         {
            bl.CoinCount1 = bl.CoinCount2 = bl.CoinCount3 = bl.CoinCount4 = 0;
         }

         bl.CoinSumm = 0;
         db.Update(bl);
         await db.SaveChangesAsync();
         return Ok(bl);
      }
      [HttpGet("getcoin")]
      public ActionResult<string> GetCoin()
      {
         BaseLogic bl = db.BaseLogics.First(x => x.Id == 1);
         if (int.Parse(bl.ItemName) <= 0)
         {
            bl.CoinSumm=0;
         }
         if (bl.CoinSumm == 0)
         {
            bl.CoinCount1 = bl.CoinCount2 = bl.CoinCount3 = bl.CoinCount4 = 0;
         }

         return Ok(bl);
      }
   }
}
