using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Data.Entities;

namespace CocktailMaker.Api.Interfaces
{
    /// <summary>
    ///     Interface fot "Cocktail of the day" service
    /// </summary>
    public interface ICocktailOfTheDayService
    {
        /// <summary>
        ///     Gets cocktail of the day
        /// <remarks>Returns current or finds another)</remarks>
        /// </summary>
        Task<Cocktail> GetCocktailOfTheDayAsync(CancellationToken cancellationToken);
    }
}
