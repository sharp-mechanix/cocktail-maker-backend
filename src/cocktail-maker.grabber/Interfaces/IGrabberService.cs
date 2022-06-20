using System.Threading;
using System.Threading.Tasks;

namespace CocktailMaker.Grabber.Interfaces
{
    /// <summary>
    ///     Interface for cocktail grabber
    /// </summary>
    public interface IGrabberService
    {
        /// <summary>
        ///     Gets all cocktails data
        /// </summary>
        Task GetCocktailDataAsync(CancellationToken cancellationToken);
    }
}
