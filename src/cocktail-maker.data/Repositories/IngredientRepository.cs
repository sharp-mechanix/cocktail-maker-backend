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
    ///     Implementation for <see cref="IReadWriteRepository{TEntity, TId}" />
    /// </summary>
    public class IngredientRepository : IReadWriteRepository<Ingredient, int>
    {
        private readonly AppDbContext _dbContext;

        /// <see cref="IngredientRepository" />
        public IngredientRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Ingredient> CreateAsync(Ingredient newEntity, CancellationToken cancellationToken)
        {
            using var t = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = await _dbContext.Ingredients.AddAsync(newEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);

            return result.Entity;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Ingredient entityToDelete, CancellationToken cancellationToken)
        {
            using var t = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = _dbContext.Ingredients.Remove(entityToDelete);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);
        }

        /// <inheritdoc />
        public Task<Ingredient> GetByIdAsync(int id, CancellationToken cancellationToken)
            => _dbContext.Ingredients.FirstAsync(c => c.Id == id, cancellationToken);

        /// <inheritdoc />
        public Task<List<Ingredient>> ListAsync(IFilter? filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Ingredients
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
            using var t = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = _dbContext.Ingredients.Update(updatedEntity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);

            return result.Entity;
        }
    }
}

