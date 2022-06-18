using System.Threading.Tasks;
using CocktailMaker.Api.Dto.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMaker.Api.Controllers
{
    [Route("api/cocktails")]
    public class CocktailsController : ControllerBase
    {
        [HttpGet("cotd")]
        public Task<CocktailDto> GetCocktailOfTheDay();

        [HttpGet]
        public Task<PagedResponseDto<CocktailListItemDto>> GetCocktailList();

        [HttpGet("{id}")]
        public Task<CocktailDto> GetCocktailDetails(int id);

        [HttpGet("{id}/similar")]
        public Task<SimilarCocktailsDto> GetSimilarCocktails(int id);
    }
}