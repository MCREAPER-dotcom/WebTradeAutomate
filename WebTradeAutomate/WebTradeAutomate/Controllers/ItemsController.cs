using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTradeAutomate.Models;

namespace WebTradeAutomate.Controllers
{
   /// <summary>
   /// контроллер товаров отвечающий за сообщение с сайтом
   /// </summary>
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
            db.Items.Add(new Item { Name = "water", Code = 152, cost=20,quantity=15 });
            db.Items.Add(new Item { Name = "icetee", Code = 561, cost=50,quantity=17 });
            db.SaveChanges();
         }
      }
      /// <summary>
      /// обработка запроса Get
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public async Task<ActionResult<IEnumerable<Item>>> Get()
      {
         return await db.Items.ToListAsync();
      }
      /// <summary>
      /// обработка запроса Get(id)
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet("{id}")]
      public async Task<ActionResult<Item>> Get(int id)
      {
         Item item = await db.Items.FirstOrDefaultAsync(x => x.Id == id);
         if (item == null)
            return NotFound();
         return new ObjectResult(item);
      }






      /// <summary>
      /// обработка запроса POST
      /// </summary>
      /// <param name="item"></param>
      /// <returns></returns>
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

      /// <summary>
      /// обработка запроса PUT
      /// </summary>
      /// <param name="item"></param>
      /// <returns></returns>
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

      /// <summary>
      /// обработка запроса  DELETE(id)
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
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
