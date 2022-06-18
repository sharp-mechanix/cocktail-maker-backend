using System.Collections.Immutable;

namespace CocktailMaker.Api.Dto.Responses
{
    public record SimilarCocktailsDto
    {
        public ImmutableArray<CocktailListItemDto> SimilarCocktails { get; init; }
    }
}