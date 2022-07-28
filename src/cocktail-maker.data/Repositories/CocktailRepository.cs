using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Data.Entities;
using CocktailMaker.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CocktailMaker.Data.Repositories
{
    /// <summary>
    ///     Repository for cocktails
    /// </summary>
    public class CocktailRepository : IReadWriteRepository<Cocktail, int>
    {
        private readonly AppDbContext _dbContext;

        /// <see cref="CocktailRepository" />
        public CocktailRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Cocktail> CreateAsync(Cocktail newEntity, CancellationToken cancellationToken)
        {
            using var t = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = await _dbContext.Cocktails.AddAsync(newEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);

            return result.Entity;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Cocktail entityToDelete, CancellationToken cancellationToken)
        {
            using var t = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = _dbContext.Cocktails.Remove(entityToDelete);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);
        }

        /// <inheritdoc />
        public Task<Cocktail> GetByIdAsync(int id, CancellationToken cancellationToken)
            => _dbContext.Cocktails
                .Include(c => c.Measures)
                .ThenInclude(m => m.Ingredient)
                .FirstAsync(c => c.Id == id, cancellationToken);

        /// <inheritdoc />
        public Task<List<Cocktail>> ListAsync(IFilter? filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Cocktails
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

            return query.ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Cocktail> UpdateAsync(Cocktail updatedEntity, CancellationToken cancellationToken)
        {
            using var t = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = _dbContext.Cocktails.Update(updatedEntity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);

            return result.Entity;
        }
    }
}
