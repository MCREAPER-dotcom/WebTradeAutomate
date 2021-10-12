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
         // ������������� �������� ������
         services.AddDbContext<ItemsContext>(options => options.UseSqlServer(con));
         string coin = "Server=(localdb)\\mssqllocaldb;" +
            "Database=coinsdatabase;" +
            "Trusted_Connection=True;";
         // ������������� �������� ������
         services.AddDbContext<CoinsContext>(options => options.UseSqlServer(coin));
         services.AddControllers(); // ���������� ����������� ��� �������������
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