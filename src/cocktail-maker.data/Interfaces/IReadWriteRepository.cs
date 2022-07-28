namespace CocktailMaker.Data.Interfaces
{
    /// <summary>
    ///     Combined read/write repository interface
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TId">Identifier type</typeparam>
    public interface IReadWriteRepository<TEntity, TId> : IReadRepository<TEntity, TId>, IWriteRepository<TEntity, TId>
        where TEntity : IEntity<TId>
    {
    }
}

