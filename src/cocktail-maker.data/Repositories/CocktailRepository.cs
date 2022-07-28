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
    public class CocktailRepository : RepositoryBase, IReadWriteRepository<Cocktail, int>
    {
        /// <see cref="CocktailRepository" />
        public CocktailRepository(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }

        /// <inheritdoc />
        public async Task<Cocktail> CreateAsync(Cocktail newEntity, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = await dbContext.Cocktails.AddAsync(newEntity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);

            return result.Entity;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Cocktail entityToDelete, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = dbContext.Cocktails.Remove(entityToDelete);
            await dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);
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

            return query.ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Cocktail> UpdateAsync(Cocktail updatedEntity, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = dbContext.Cocktails.Update(updatedEntity);
            await dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);

            return result.Entity;
        }
    }
}
