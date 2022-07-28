using CocktailMaker.Common.Settings;
using CocktailMaker.Data;
using CocktailMaker.Data.Entities;
using CocktailMaker.Data.Interfaces;
using CocktailMaker.Data.Repositories;
using CocktailMaker.Grabber.Interfaces;
using CocktailMaker.Grabber.Services;
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

                    services.AddScoped<IReadWriteRepository<Ingredient, int>, IngredientRepository>();
                    services.AddScoped<IWriteRepository<Measure, int>, MeasureRepository>();
                    services.AddScoped<IReadWriteRepository<Cocktail, int>, CocktailRepository>();

                    services.AddSingleton<IGrabberService, GrabberService>();
                    services.AddHostedService<Worker>();
                });
    }
}
