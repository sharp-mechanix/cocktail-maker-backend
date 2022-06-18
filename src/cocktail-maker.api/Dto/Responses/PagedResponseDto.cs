using System.Collections.Immutable;

namespace CocktailMaker.Api.Dto.Responses
{
    public record PagedResponseDto<T> where T : class
    {
        public int TotalCount { get; init; }

        public ImmutableArray<T> Items { get; set; }
    }
}