using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Data.Entities;
using CocktailMaker.Data.Interfaces;

namespace CocktailMaker.Data.Repositories
{
    /// <summary>
    ///     Implementatioin for <see cref="IWriteRepository{TEntity, TId}" />
    /// </summary>
    public class MeasureRepository : IWriteRepository<Measure, int>
    {
        private readonly AppDbContext _dbContext;

        /// <see cref="MeasureRepository"/>
        public MeasureRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Measure> CreateAsync(Measure newEntity, CancellationToken cancellationToken)
        {
            using var t = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = await _dbContext.Measures.AddAsync(newEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);

            return result.Entity;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Measure entityToDelete, CancellationToken cancellationToken)
        {
            using var t = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = _dbContext.Measures.Remove(entityToDelete);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Measure> UpdateAsync(Measure updatedEntity, CancellationToken cancellationToken)
        {
            using var t = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = _dbContext.Measures.Update(updatedEntity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);

            return result.Entity;
        }
    }
}

