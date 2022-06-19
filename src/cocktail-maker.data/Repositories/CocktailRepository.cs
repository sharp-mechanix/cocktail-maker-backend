using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Data.Entities;
using CocktailMaker.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailMaker.Data.Repositories
{
    /// <summary>
    ///     Repository for cocktails
    /// </summary>
	public class CocktailRepository : IReadRepository<Cocktail, int>
	{
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <see cref="CocktailRepository" />
		public CocktailRepository(IServiceScopeFactory serviceScopeFactory)
		{
            _serviceScopeFactory = serviceScopeFactory;
		}

        /// <inheritdoc />
        public async Task<Cocktail> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            var result = await dbContext.Cocktails
                .Include(c => c.Measures)
                .ThenInclude(m => m.Ingredient)
                .FirstAsync(c => c.Id == id, cancellationToken);
            return result;
        }

        /// <inheritdoc />
        public Task<List<Cocktail>> ListAsync(IFilter? filter, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            var query = dbContext.Cocktails
                .Include(c => c.Measures)
                .ThenInclude(m => m.Ingredient)
                .AsQueryable();
            if (filter is null)
            {
                return query.ToListAsync(cancellationToken);
            }

            var cocktailFilter = (CocktailFilter)filter;
            if (!string.IsNullOrEmpty(cocktailFilter.Name))
            {
                query = query.Where(c => c.Name.Contains(cocktailFilter.Name, StringComparison.InvariantCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(cocktailFilter.Ingredient))
            {
                query = query.Where(c => c.Measures
                    .Any(m => m.Ingredient.Name.Contains(cocktailFilter.Ingredient, StringComparison.InvariantCultureIgnoreCase)));
            }

            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns database context
        /// </summary>
        private AppDbContext GetDbContext(IServiceScope scope)
            => scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }
}

