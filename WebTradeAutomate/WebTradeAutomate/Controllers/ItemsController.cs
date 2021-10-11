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
   public class ItemsController : ControllerBase
   {
      ItemsContext db;
      public ItemsController(ItemsContext context)
      {
         db = context;
         if (!db.Items.Any())
         {
            db.Items.Add(new Item { Name = "water", cost=20,quantity=15 });
            db.Items.Add(new Item { Name = "icetee", cost=50,quantity=17 });
            db.SaveChanges();
         }
      }

      [HttpGet]
      public async Task<ActionResult<IEnumerable<Item>>> Get()
      {
         return await db.Items.ToListAsync();
      }

      // GET api/users/5
      [HttpGet("{id}")]
      public async Task<ActionResult<Item>> Get(int id)
      {
         Item item = await db.Items.FirstOrDefaultAsync(x => x.Id == id);
         if (item == null)
            return NotFound();
         return new ObjectResult(item);
      }






      // POST api/users
      [HttpPost]
      public async Task<ActionResult<Item>> Post(Item item)
      {
         if (item == null)
         {
            return BadRequest();
         }

         db.Items.Add(item);
         await db.SaveChangesAsync();
         return Ok(item);
      }

      // PUT api/users/
      [HttpPut]
      public async Task<ActionResult<Item>> Put(Item item)
      {
         if (item == null)
         {
            return BadRequest();
         }
         if (!db.Items.Any(x => x.Id == item.Id))
         {
            return NotFound();
         }

         db.Update(item);
         await db.SaveChangesAsync();
         return Ok(item);
      }

      // DELETE api/users/5
      [HttpDelete("{id}")]
      public async Task<ActionResult<Item>> Delete(int id)
      {
         Item item = db.Items.FirstOrDefault(x => x.Id == id);
         if (item == null)
         {
            return NotFound();
         }
         db.Items.Remove(item);
         await db.SaveChangesAsync();
         return Ok(item);
      }
   }
}
