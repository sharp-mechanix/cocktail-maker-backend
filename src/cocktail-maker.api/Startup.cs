using CocktailMaker.Api.Interfaces;
using CocktailMaker.Api.Services;
using CocktailMaker.Common.Settings;
using CocktailMaker.Data;
using CocktailMaker.Data.Entities;
using CocktailMaker.Data.Interfaces;
using CocktailMaker.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CocktailMaker.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "cocktail_maker.api", Version = "v1" });
            });

            var dbSettings = Configuration.GetSection("Database").Get<DatabaseSettings>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(dbSettings.ConnectionString, opts =>
                {
                    opts.MigrationsHistoryTable("__schema_migrations");
                });
            });

            services.AddScoped<IReadRepository<Cocktail, int>, CocktailRepository>();
            services.AddSingleton<ICocktailOfTheDayService, CocktailOfTheDayService>();

            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "cocktail_maker.api v1"));
            }

            dbContext.Database.Migrate();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

