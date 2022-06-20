namespace CocktailMaker.Api.Dto.Requests
{
    /// <summary>
    ///     Cocktail list request
    /// </summary>
    public record GetCocktailListRequestDto : PagedListRequestDto
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