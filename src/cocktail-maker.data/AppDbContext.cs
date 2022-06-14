using CocktailMaker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.NameTranslation;

namespace CocktailMaker.Data
{
	/// <summary>
	///		Application database context
	/// </summary>
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		/// <summary>
        ///		Cocktails
        /// </summary>
		public DbSet<Cocktail> Cocktails { get; set; }

		/// <summary>
        ///		Ingredients
        /// </summary>
		public DbSet<Ingredient> Ingredients { get; set; }

		/// <summary>
        ///		Measures
        /// </summary>
		public DbSet<Measure> Measures { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mapper = new NpgsqlSnakeCaseNameTranslator();
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var storeObjectId = StoreObjectIdentifier.Table(entity.GetTableName(), entity.GetSchema());
                foreach (var property in entity.GetProperties())
                {
                    // Проставляем имя поля по умолчанию (snake_case)
                    property.SetColumnName(mapper.TranslateMemberName(property.GetColumnName(storeObjectId)));
                }
            }
        }
    }
}
