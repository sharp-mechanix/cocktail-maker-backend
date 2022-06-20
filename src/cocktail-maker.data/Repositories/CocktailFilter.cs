using CocktailMaker.Data.Interfaces;

namespace CocktailMaker.Data.Repositories
{
    /// <summary>
    ///     Filter for cocktails
    /// </summary>
    public record CocktailFilter : IFilter
    {
        /// <summary>
        ///     Filter by name
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        ///     Filter by ingredient
        /// </summary>
        public string? Ingredient { get; init; }
    }
}
