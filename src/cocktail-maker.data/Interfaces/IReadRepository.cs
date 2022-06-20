using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CocktailMaker.Data.Interfaces
{
    /// <summary>
    ///     Read-only repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TId">Identifier type</typeparam>
    public interface IReadRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        /// <summary>
        ///     Get one record by identifier
        Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken);

        /// <summary>
        ///     List filtered records
        /// </summary>
        Task<List<TEntity>> ListAsync(IFilter? filter, CancellationToken cancellationToken);
    }
}
