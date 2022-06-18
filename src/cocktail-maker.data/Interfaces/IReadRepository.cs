using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CocktailMaker.Data.Interfaces
{
    /// <summary>
    ///     Read-only repository
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="D">Identifier type</typeparam>
    public interface IReadRepository<T, D> where T : IEntity<D>
    {
        /// <summary>
        ///     Get one record by identifier
        Task<T> GetByIdAsync(D id, CancellationToken cancellationToken);

        /// <summary>
        ///     List filtered records
        /// </summary>
        Task<List<T>> ListAsync(IFilter filter, CancellationToken cancellationToken);
    }
}