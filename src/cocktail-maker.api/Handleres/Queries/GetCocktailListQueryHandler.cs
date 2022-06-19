using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Api.Dto.Responses;
using MediatR;

namespace CocktailMaker.Api.Handlers.Queries
{
    /// <summary>
    ///     Handler for <see cref="GetCocktailListQuery" />
    /// </summary>
    public class GetCocktailListQueryHandler : IRequestHandler<GetCocktailListQuery, PagedResponseDto<CocktailDto>>
    {
        /// <inheritdoc />
        public Task<PagedResponseDto<CocktailDto>> Handle(GetCocktailListQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}