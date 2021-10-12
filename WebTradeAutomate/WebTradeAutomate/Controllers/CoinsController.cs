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
      CoinsContext db;
      public CoinsController(CoinsContext context)
      {
         db = context;
         if (!db.Coins.Any())
         {
            db.Coins.Add(new Coin { Name = "1",Cost=1});
            db.Coins.Add(new Coin { Name = "2", Cost = 2 });
            db.Coins.Add(new Coin { Name = "5", Cost = 5 });
            db.Coins.Add(new Coin { Name = "10", Cost = 10 });
            db.SaveChanges();
         }
      }

      [HttpGet]
      public async Task<ActionResult<IEnumerable<Coin>>> Get()
      {
         return await db.Coins.ToListAsync();
      }

      // GET api/users/5
      [HttpGet("{id}")]
      public async Task<ActionResult<Coin>> Get(int id)
      {
         Coin coin = await db.Coins.FirstOrDefaultAsync(x => x.Id == id);
         if (coin == null)
            return NotFound();
         return new ObjectResult(coin);
      }






      // POST api/users
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

      // PUT api/users/
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

      // DELETE api/users/5
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
