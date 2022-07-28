using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Data.Entities;
using CocktailMaker.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailMaker.Data.Repositories
{
    /// <summary>
    ///     Implementatioin for <see cref="IWriteRepository{TEntity, TId}" />
    /// </summary>
    public class MeasureRepository : RepositoryBase, IWriteRepository<Measure, int>
    {
        public MeasureRepository(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }

        /// <inheritdoc />
        public async Task<Measure> CreateAsync(Measure newEntity, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = await dbContext.Measures.AddAsync(newEntity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);

            return result.Entity;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Measure entityToDelete, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = dbContext.Measures.Remove(entityToDelete);
            await dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Measure> UpdateAsync(Measure updatedEntity, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDbContext(scope);

            using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            var result = dbContext.Measures.Update(updatedEntity);
            await dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);

            return result.Entity;
        }
    }
}

