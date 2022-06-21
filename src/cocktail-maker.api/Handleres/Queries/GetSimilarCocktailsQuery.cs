using CocktailMaker.Api.Dto.Responses;
using MediatR;

namespace CocktailMaker.Api.Handleres.Queries
{
    public record GetSimilarCocktailsQuery : IRequest<SimilarCocktailsDto>
    {
        public int Id { get; init; }
    }
}

