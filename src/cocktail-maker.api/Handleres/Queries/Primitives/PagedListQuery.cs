using CocktailMaker.Api.Dto.Responses;
using MediatR;

namespace CocktailMaker.Api.Handlers.Queries.Primitives
{
    /// <summary>
    ///     Base paged list query
    /// </summary>
    public record PagedListQuery<TListItem> : IRequest<PagedResponseDto<TListItem>> where TListItem : class
    {
        /// <summary>
        ///     Page size
        /// </summary>
        public int Take { get; init; }

        /// <summary>
        ///     How many records to skip (Skip = PageSize * (PageNumber - 1))
        /// </summary>
        public int Skip { get; init; }
    }
}