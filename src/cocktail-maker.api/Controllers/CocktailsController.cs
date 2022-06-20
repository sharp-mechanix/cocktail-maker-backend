using System;
using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Api.Dto.Requests;
using CocktailMaker.Api.Dto.Responses;
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

        [HttpGet("cotd")]
        public Task<CocktailDto> GetCocktailOfTheDay()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets cocktails by filters (paged)
        /// </summary>
        [HttpGet]
        public async Task<PagedResponseDto<CocktailListItemDto>> GetCocktailList([FromQuery] GetCocktailListRequestDto request, CancellationToken cancellationToken)
        {
            var query = new GetCocktailListQuery
            {
                Take = request.Take,
                Skip = request.Skip,
                Name = request.Name,
                Ingredient = request.Ingredient
            };

            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpGet("{id}")]
        public Task<CocktailDto> GetCocktailDetails(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/similar")]
        public Task<SimilarCocktailsDto> GetSimilarCocktails(int id)
        {
            throw new NotImplementedException();
        }
    }
}