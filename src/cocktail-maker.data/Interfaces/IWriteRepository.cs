using System.Threading;
using System.Threading.Tasks;

namespace CocktailMaker.Data.Interfaces
{
    /// <summary>
    ///     Repository with ability to write entities to data storage
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TId">Identifier type</typeparam>
    public interface IWriteRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        /// <summary>
        ///     Creates given entity in a storage
        /// </summary>
        Task<TEntity> CreateAsync(TEntity newEntity, CancellationToken cancellationToken);

        /// <summary>
        ///     Updates given entity in a storage
        /// </summary>
        Task<TEntity> UpdateAsync(TEntity updatedEntity, CancellationToken cancellationToken);

        /// <summary>
        ///     Deletes given entity in a storage
        /// </summary>
        Task DeleteAsync(TEntity entityToDelete, CancellationToken cancellationToken);
    }
}

