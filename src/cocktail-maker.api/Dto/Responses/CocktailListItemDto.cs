namespace CocktailMaker.Api.Dto.Responses
{
    /// <summary>
    ///     DTO for cocktail list item
    /// </summary>
    public record CocktailListItemDto
    {
        /// <see cref="CocktailListItemDto" />
        public CocktailListItemDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        ///     Cocktail ID
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     Cocktail name
        /// </summary>
        public string Name { get; }
    }
}
