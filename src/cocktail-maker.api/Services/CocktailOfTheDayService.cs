using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Api.Interfaces;
using CocktailMaker.Data;
using CocktailMaker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailMaker.Api.Services
{
    /// <summary>
    ///     Implementation of <see cref="ICocktailOfTheDayService"/>
    /// </summary>
    public class CocktailOfTheDayService : ICocktailOfTheDayService
    {
        // Cocktail of the day
        private Cocktail? _cotd;

        // Time of last update
        private DateTime _updatedOn;

        // Service scope provider to get DB context
        private readonly IServiceScopeFactory _scopeFactory;

        /// <see cref="CocktailOfTheDayService"/>
        public CocktailOfTheDayService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        /// <inheritdoc />
        public async Task<Cocktail> GetCocktailOfTheDayAsync(CancellationToken cancellationToken)
        {
            if (_cotd is null || _updatedOn.Date < DateTime.UtcNow.Date)
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var rng = new Random();

                var newCotd = await dbContext.Cocktails.OrderBy(_ => rng.NextDouble()).FirstAsync(cancellationToken);

                _cotd = newCotd;
                _updatedOn = DateTime.UtcNow.Date;
            }

            return _cotd;
        }
    }
}
