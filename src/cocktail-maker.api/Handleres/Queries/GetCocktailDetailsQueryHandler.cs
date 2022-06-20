using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Api.Dto.Responses;
using CocktailMaker.Data.Entities;
using CocktailMaker.Data.Interfaces;
using MediatR;

namespace CocktailMaker.Api.Handleres.Queries
{
    /// <summary>
    ///     Handler for <see cref="GetCocktailDetailsQuery"/>
    /// </summary>
    public class GetCocktailDetailsQueryHandler : IRequestHandler<GetCocktailDetailsQuery, CocktailDto>
    {
        private readonly IReadRepository<Cocktail, int> _repository;

        public GetCocktailDetailsQueryHandler(IReadRepository<Cocktail, int> repository)
        {
            _repository = repository;
        }

        public async Task<CocktailDto> Handle(GetCocktailDetailsQuery request, CancellationToken cancellationToken)
        {
            var cocktail = await _repository.GetByIdAsync(request.Id, cancellationToken);

            return new CocktailDto
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                Category = cocktail.Category,
                IbaCategory = cocktail.IbaCategory,
                Glass = cocktail.Glass,
                Instructions = cocktail.Instructions,
                Measures = cocktail.Measures.Select(m => new MeasureDto
                {
                    IngredientName = m.Ingredient.Name,
                    Measure = m.Value
                }).ToImmutableArray()
            };
        }
    }
}
