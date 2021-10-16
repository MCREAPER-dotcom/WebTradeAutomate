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
   public class CoinsController : ControllerBase
   {
      /// <summary>
      /// база денег, в случае необходимости можно будет вводить другую валюту и менять стоимость валюты
      /// </summary>
      CoinsContext db;
      public CoinsController(CoinsContext context)
      {
         db = context;
         if (!db.Coins.Any())
         {
            db.Coins.Add(new Coin { Name = "1", Cost = 1, IsUsed = true});
            db.Coins.Add(new Coin { Name = "2", Cost = 2, IsUsed = true});
            db.Coins.Add(new Coin { Name = "5", Cost = 5, IsUsed = true});
            db.Coins.Add(new Coin { Name = "10", Cost = 10, IsUsed = true});
            db.SaveChanges();
         }
      }

      [HttpGet]
      public async Task<ActionResult<IEnumerable<Coin>>> Get()
      {
         return await db.Coins.ToListAsync();
      }

      /// <summary>
      /// обработка запроса Get(id)
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet("{id}")]
      public async Task<ActionResult<Coin>> Get(int id)
      {
         Coin coin = await db.Coins.FirstOrDefaultAsync(x => x.Id == id);
         if (coin == null)
            return NotFound();
         return new ObjectResult(coin);
      }






      /// <summary>
      /// обработка запроса POST
      /// </summary>
      /// <param name="coin"></param>
      /// <returns></returns>
      [HttpPost]
      public async Task<ActionResult<Coin>> Post(Coin coin)
      {
         if (coin == null)
         {
            return BadRequest();
         }

         db.Coins.Add(coin);
         await db.SaveChangesAsync();
         return Ok(coin);
      }
      /// <summary>
      /// обработка запроса PUT
      /// </summary>
      /// <param name="coin"></param>
      /// <returns></returns>
      [HttpPut]
      public async Task<ActionResult<Coin>> Put(Coin coin)
      {
         if (coin == null)
         {
            return BadRequest();
         }
         if (!db.Coins.Any(x => x.Id == coin.Id))
         {
            return NotFound();
         }

         db.Update(coin);
         await db.SaveChangesAsync();
         return Ok(coin);
      }

      /// <summary>
      /// обработка запроса  DELETE(id)
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpDelete("{id}")]
      public async Task<ActionResult<Coin>> Delete(int id)
      {
         Coin coin = db.Coins.FirstOrDefault(x => x.Id == id);
         if (coin == null)
         {
            return NotFound();
         }
         db.Coins.Remove(coin);
         await db.SaveChangesAsync();
         return Ok(coin);
      }
   }
}
