using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeService.Infrastructure.Database;

namespace RecipeService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;

            builder.Services.AddSwagger();
            builder.Services.AddControllers();
            builder.Services.AddAuthentication(configuration);
            builder.Services.AddAuthorization();
            builder.Services.AddServices();
            builder.Services.AddValidation();
            //builder.Services.AddEntityFramework(configuration);
            builder.Services.AddDbContext<DatabaseContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            app.MapControllers();
            app.UseSwagger(app);

            app.UseMiddlewares();
            app.UseAuthentication();
            app.UseAuthorization();


            /* using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                db.Database.Migrate();
            } */
            app.Run();

        }
    }
}
