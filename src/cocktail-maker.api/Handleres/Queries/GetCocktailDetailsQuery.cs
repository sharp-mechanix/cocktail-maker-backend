using CocktailMaker.Api.Dto.Responses;
using MediatR;

namespace CocktailMaker.Api.Handleres.Queries
{
    /// <summary>
    ///     Query to get cocktail details
    /// </summary>
    public record GetCocktailDetailsQuery : IRequest<CocktailDto>
    {
        /// <summary>
        ///     Cocktail ID
        /// </summary>
        public int Id { get; init; }
    }
}
