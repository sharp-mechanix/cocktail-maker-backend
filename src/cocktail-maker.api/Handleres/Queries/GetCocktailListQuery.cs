using CocktailMaker.Api.Dto.Responses;
using CocktailMaker.Api.Handlers.Queries.Primitives;

namespace CocktailMaker.Api.Handlers.Queries
{
    /// <summary>
    ///     Query to list cocktails
    /// </summary>
    public record GetCocktailListQuery : PagedListQuery<CocktailListItemDto>
    {
        /// <summary>
        ///     Search by name
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        ///     Search by ingredient
        /// </summary>
        public string? Ingredient { get; init; }
    }
}