using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Api.Dto.Responses;
using MediatR;

namespace CocktailMaker.Api.Handlers.Queries
{
    /// <summary>
    ///     Handler for <see cref="GetCocktailOfTheDayQuery" />
    /// </summary>
    public class GetCocktailOfTheDayQueryHandler : IRequestHandler<GetCocktailOfTheDayQuery, CocktailDto>
    {
        /// <inheritdoc />
        public Task<CocktailDto> Handle(GetCocktailOfTheDayQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}