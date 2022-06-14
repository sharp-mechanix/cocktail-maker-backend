using CocktailMaker.Common.Settings;
using CocktailMaker.Data;
using CocktailMaker.Grabber.Interfaces;
using CocktailMaker.Grabber.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CocktailMaker.Grabber
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var dbSettings = hostContext.Configuration.GetSection("Database").Get<DatabaseSettings>();

                    services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseNpgsql(dbSettings.ConnectionString, opts =>
                        {
                            opts.MigrationsHistoryTable("__schema_migrations");
                        });
                    });

                    services.AddSingleton<IGrabberService, GrabberService>();
                    services.AddHostedService<Worker>();
                });
    }
}

