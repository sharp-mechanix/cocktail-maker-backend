using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Api.Dto.Responses;
using CocktailMaker.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailMaker.Api.Handleres.Queries
{
    public class GetSimilarCocktailsQueryHandler : IRequestHandler<GetSimilarCocktailsQuery, SimilarCocktailsDto>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public GetSimilarCocktailsQueryHandler(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<SimilarCocktailsDto> Handle(GetSimilarCocktailsQuery request, CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var cocktail = await dbContext.Cocktails.FirstAsync(c => c.Id == request.Id, cancellationToken);
            var ingredientIds = cocktail.Measures.Take(3).Select(m => m.IngredientId).ToList();

            var similar = await dbContext.Cocktails
                .OrderByDescending(c => c.Measures
                    .Select(m => m.IngredientId)
                    .Intersect(ingredientIds).Count())
                .ToListAsync(cancellationToken);

            return new SimilarCocktailsDto
            {
                SimilarCocktails = similar
                    .Take(3)
                    .Select(c => new CocktailListItemDto(c.Id, c.Name))
                    .ToImmutableArray()
            };
        }
    }
}
