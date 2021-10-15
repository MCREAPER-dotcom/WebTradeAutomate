using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebTradeAutomate.Models;

namespace WebTradeAutomate
{
   public class Startup
   {
      public void ConfigureServices(IServiceCollection services)
      {
         string con = "Server=(localdb)\\mssqllocaldb;" +
            "Database=itemsdatabase;" +
            "Trusted_Connection=True;";
         // устанавливаем контекст данных
         services.AddDbContext<ItemsContext>(options => options.UseSqlServer(con));
         string coin = "Server=(localdb)\\mssqllocaldb;" +
            "Database=coinsdata;" +
            "Trusted_Connection=True;";
         // устанавливаем контекст данных
         services.AddDbContext<CoinsContext>(options => options.UseSqlServer(coin));
         string baselogicbd= "Server=(localdb)\\mssqllocaldb;" +
                       "Database=baselogicbd;" +
                       "Trusted_Connection=True;";
         // устанавливаем контекст данных
         services.AddDbContext<BaseLogicContext>(options => options.UseSqlServer(baselogicbd));

         services.AddControllers(); // используем контроллеры без представлений
      }

      public void Configure(IApplicationBuilder app)
      {
         app.UseDeveloperExceptionPage();

         app.UseDefaultFiles();
         app.UseStaticFiles();

         app.UseRouting();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}