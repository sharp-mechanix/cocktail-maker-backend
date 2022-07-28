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
    ///     Implementation for <see cref="IReadWriteRepository{TEntity, TId}"/>
    /// </summary>
    public class IngredientRepository : RepositoryBase, IReadWriteRepository<Ingredient, int>
    {
        /// <see cref="IngredientRepository" />
        public IngredientRepository(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }

        /// <inheritdoc />
        public async Task<Ingredient> CreateAsync(Ingredient newEntity, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = await dbContext.Ingredients.AddAsync(newEntity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);

            return result.Entity;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Ingredient entityToDelete, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = dbContext.Ingredients.Remove(entityToDelete);
            await dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Ingredient> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            var result = await dbContext.Ingredients
                .FirstAsync(c => c.Id == id, cancellationToken);

            return result;
        }

        /// <inheritdoc />
        public Task<List<Ingredient>> ListAsync(IFilter? filter, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            var query = dbContext.Ingredients
                .AsQueryable();

            if (filter is null)
            {
                return query.ToListAsync(cancellationToken);
            }

            var ingredientFilter = (IngredientFilter)filter;
            if (!string.IsNullOrEmpty(ingredientFilter.Name))
            {
                query = query.Where(c => c.Name.Contains(ingredientFilter.Name, StringComparison.InvariantCultureIgnoreCase));
            }

            return query.ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Ingredient> UpdateAsync(Ingredient updatedEntity, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = dbContext.Ingredients.Update(updatedEntity);
            await dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);

            return result.Entity;
        }
    }
}

