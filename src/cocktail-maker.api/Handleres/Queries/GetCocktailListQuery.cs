using CocktailMaker.Api.Dto.Responses;
using MediatR;

namespace CocktailMaker.Api.Handlers.Queries
{
    /// <summary>
    ///     Query to list cocktails
    public record GetCocktailListQuery : IRequest<PagedResponseDto<CocktailDto>>
    {
    }
}