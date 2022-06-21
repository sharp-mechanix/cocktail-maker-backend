using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Api.Dto.Responses;
using CocktailMaker.Api.Interfaces;
using MediatR;

namespace CocktailMaker.Api.Handlers.Queries
{
    /// <summary>
    ///     Handler for <see cref="GetCocktailOfTheDayQuery" />
    /// </summary>
    public class GetCocktailOfTheDayQueryHandler : IRequestHandler<GetCocktailOfTheDayQuery, CocktailDto>
    {
        private readonly ICocktailOfTheDayService _cotdService;

        public GetCocktailOfTheDayQueryHandler(ICocktailOfTheDayService cotdService)
        {
            _cotdService = cotdService;
        }

        /// <inheritdoc />
        public async Task<CocktailDto> Handle(GetCocktailOfTheDayQuery request, CancellationToken cancellationToken)
        {
            var cotd = await _cotdService.GetCocktailOfTheDayAsync(cancellationToken);

            return new CocktailDto
            {
                Id = cotd.Id,
                Category = cotd.Category,
                Glass = cotd.Glass,
                IbaCategory = cotd.IbaCategory,
                Name = cotd.Name,
                Instructions = cotd.Instructions,
                Measures = cotd.Measures.Select(m => new MeasureDto
                {
                    IngredientName = m.Ingredient.Name,
                    Measure = m.Value
                }).ToImmutableArray()
            };
        }
    }
}
