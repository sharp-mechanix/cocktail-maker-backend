using System;
using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Api.Dto.Requests;
using CocktailMaker.Api.Dto.Responses;
using CocktailMaker.Api.Handleres.Queries;
using CocktailMaker.Api.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMaker.Api.Controllers
{
    [Route("api/cocktails")]
    public class CocktailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CocktailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Gets cocktail of the day
        /// </summary>
        [HttpGet("cotd")]
        public Task<CocktailDto> GetCocktailOfTheDay(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets cocktails by filters (paged)
        /// </summary>
        [HttpGet]
        public Task<PagedResponseDto<CocktailListItemDto>> GetCocktailList([FromQuery] GetCocktailListRequestDto request, CancellationToken cancellationToken)
        {
            var query = new GetCocktailListQuery
            {
                Take = request.Take,
                Skip = request.Skip,
                Name = request.Name,
                Ingredient = request.Ingredient
            };

            return _mediator.Send(query, cancellationToken);
        }

        /// <summary>
        ///     Gets cocktail details by ID
        /// </summary>
        [HttpGet("{id}")]
        public Task<CocktailDto> GetCocktailDetails(int id, CancellationToken cancellationToken)
        {
            var query = new GetCocktailDetailsQuery
            {
                Id = id
            };

            return _mediator.Send(query, cancellationToken);
        }

        /// <summary>
        ///     Get similar cocktails
        /// </summary>
        [HttpGet("{id}/similar")]
        public Task<SimilarCocktailsDto> GetSimilarCocktails(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
