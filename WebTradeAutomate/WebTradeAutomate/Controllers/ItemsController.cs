using Microsoft.AspNetCore.Mvc;
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

   }
}
