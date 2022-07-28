using Microsoft.Extensions.DependencyInjection;

namespace CocktailMaker.Data.Repositories
{
    /// <summary>
    ///     Base class for repositories with common functions
    /// </summary>
    public class RepositoryBase
    {
        protected readonly IServiceScopeFactory _serviceScopeFactory;

        /// <see cref="RepositoryBase" />
        protected RepositoryBase(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        ///     Returns database context
        /// </summary>
        protected virtual AppDbContext GetDbContext(IServiceScope scope)
            => scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }
}
