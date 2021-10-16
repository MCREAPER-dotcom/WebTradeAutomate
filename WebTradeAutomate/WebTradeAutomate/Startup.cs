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
         string con = "Data Source=databaseTradeA.db";
         // ������������� �������� ������
         services.AddDbContext<DataBaseContext>(options => options.UseSqlite(con));

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