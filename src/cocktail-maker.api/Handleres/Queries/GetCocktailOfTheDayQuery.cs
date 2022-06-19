using CocktailMaker.Api.Dto.Responses;
using MediatR;

namespace CocktailMaker.Api.Handlers.Queries
{
    /// <summary>
    ///     Query to get cocktail of the day
    /// </summary>
    public record GetCocktailOfTheDayQuery : IRequest<CocktailDto> {}
}