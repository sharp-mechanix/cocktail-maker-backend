using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Api.Dto.Responses;
using CocktailMaker.Data.Entities;
using CocktailMaker.Data.Interfaces;
using CocktailMaker.Data.Repositories;
using MediatR;

namespace CocktailMaker.Api.Handlers.Queries
{
    /// <summary>
    ///     Handler for <see cref="GetCocktailListQuery" />
    /// </summary>
    public class GetCocktailListQueryHandler : IRequestHandler<GetCocktailListQuery, PagedResponseDto<CocktailListItemDto>>
    {
        private readonly IReadRepository<Cocktail, int> _repository;

        public GetCocktailListQueryHandler(IReadRepository<Cocktail, int> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<PagedResponseDto<CocktailListItemDto>> Handle(GetCocktailListQuery request, CancellationToken cancellationToken)
        {
            var filter = new CocktailFilter
            {
                Name = request.Name,
                Ingredient = request.Ingredient
            };

            var result = await _repository.ListAsync(filter, cancellationToken);

            return new PagedResponseDto<CocktailListItemDto>
            {
                TotalCount = result.Count,
                Items = result.Skip(request.Skip).Take(request.Take).Select(c => new CocktailListItemDto(c.Id, c.Name)).ToImmutableArray()
            };
        }
    }
}
