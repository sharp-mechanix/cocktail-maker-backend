namespace CocktailMaker.Data.Interfaces
{
    /// <summary>
    ///     Entity with identifier
    /// </summary>
    /// <typeparam name="T">Identifier type</typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        ///    Identifier
        /// </summary>
        T Id { get; set; }
    }
}