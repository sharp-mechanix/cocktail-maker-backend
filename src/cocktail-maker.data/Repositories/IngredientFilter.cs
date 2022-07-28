using CocktailMaker.Data.Interfaces;

namespace CocktailMaker.Data.Repositories
{
    /// <summary>
    ///     Filter for ingredients
    /// </summary>
    public record IngredientFilter : IFilter
    {
        /// <summary>
        ///     Ingredient name
        /// </summary>
        public string? Name { get; init; }
    }
}

