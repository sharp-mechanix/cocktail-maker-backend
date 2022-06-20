namespace CocktailMaker.Api.Dto.Requests
{
    /// <summary>
    ///     Base list request with paging
    public record PagedListRequestDto
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