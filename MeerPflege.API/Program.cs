using MeerPflege.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                 var roleManager = services.GetService<RoleManager<IdentityRole>>();
                var roles = typeof(Domain.Role).GetFields();
                foreach (var prop in roles)
                {
                    if (!roleManager.RoleExistsAsync(prop.Name).Result)
                    {
                        _ = roleManager.CreateAsync(new IdentityRole(prop.Name)).Result;
                    }
                }
            }
            catch(Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "error on auto migration in program main");
            }
            
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
