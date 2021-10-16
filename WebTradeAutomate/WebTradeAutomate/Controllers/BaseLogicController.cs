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
      /// <summary>
      /// в дальнейшем можно переделать эту базу для информации на дисплее автомата каждого пользователя, если потребуется
      /// </summary>
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
      /// <summary>
      /// получение общего номера напитка
      /// </summary>
      /// <returns></returns>
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
      /// <summary>
      /// добавление номера напитка
      /// </summary>
      /// <param name="item"></param>
      /// <returns></returns>
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
      /// <summary>
      /// добавление монет к общей сумме
      /// </summary>
      /// <param name="cost"></param>
      /// <returns></returns>
      [HttpPut("putcoin/{cost}")]
      public async Task<ActionResult<string>> PutCoin(int cost)
      {
         BaseLogic bl = db.BaseLogics.First(x => x.Id == 1);

         if (cost == 1)
         {
            bl.CoinCount1++;
            bl.CoinSumm += cost;
         }
         if (cost == 2)
         {
            bl.CoinCount2++;
            bl.CoinSumm += cost;
         }
         if (cost == 5)
         {
            bl.CoinCount3++;
            bl.CoinSumm += cost;
         }
         if (cost == 10)
         {
            bl.CoinCount4++;
            bl.CoinSumm += cost;
         }
         if (bl.CoinSumm == 0)
         {
            bl.CoinCount1 = bl.CoinCount2 = bl.CoinCount3 = bl.CoinCount4 = 0;
         }

         db.Update(bl);
         await db.SaveChangesAsync();
         return Ok(bl);
      }

      /// <summary>
      /// метод отвечающий за выдачу сдачи
      /// </summary>
      /// <returns></returns>

      [HttpPut("deliveryofmoney")]
      public async Task<ActionResult<string>>DeliveryOfMoney()
      {
         BaseLogic bl = db.BaseLogics.First(x => x.Id == 1);

         bl.ItemName = "000";
         bl.CoinSumm = 0;

         db.Update(bl);
         await db.SaveChangesAsync();
         return Ok(bl);
      }
      /// <summary>
      /// метод отвечающий за подсчет сдачи
      /// </summary>
      /// <param name="cost"></param>
      /// <returns></returns>
      [HttpPut("putmathcoin/{cost}")]
      public async Task<ActionResult<string>> PutMathCoin(int cost)
      {
         BaseLogic bl = db.BaseLogics.First(x => x.Id == 1);

         if (bl.CoinSumm == 0)
         {
            bl.CoinCount1 = bl.CoinCount2 = bl.CoinCount3 = bl.CoinCount4 = 0;
         }

         var temp = cost;
         if (temp >= 10)
         {
            temp = cost % 10;
            bl.CoinCount4 = bl.CoinCount4 - (cost- temp) / 10;

         }
         if (temp >= 5)
         {
            bl.CoinCount3 = bl.CoinCount3 - (temp - (temp % 5)) / 5;
            temp = temp % 5;
         }
         if (temp >= 2)
         {
            bl.CoinCount2 = bl.CoinCount2 - (temp - (temp % 2)) / 2;
            temp = temp % 2;
         }
         if (temp >= 1)
         {
            bl.CoinCount1 = bl.CoinCount1 - temp;
         }

         bl.CoinSumm = bl.CoinSumm - cost;
         db.Update(bl);
         await db.SaveChangesAsync();
         return Ok(bl);
      }
      /// <summary>
      /// получение информации о балансе
      /// </summary>
      /// <returns></returns>
      [HttpGet("getcoin")]
      public async Task<ActionResult<string>> GetCoin()
      {
         BaseLogic bl = db.BaseLogics.First(x => x.Id == 1);
         if (int.Parse(bl.ItemName) <= 0)
         {
            bl.CoinSumm=0;
         }
         if (bl.CoinSumm == 0)
         {
            bl.CoinCount1 = bl.CoinCount2 = bl.CoinCount3 = bl.CoinCount4 = 0;
            db.Update(bl);
            await db.SaveChangesAsync();
         }

         return Ok(bl);
      }
   }
}
