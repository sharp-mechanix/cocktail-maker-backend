using CocktailMaker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMaker.Data.Configuration
{
    /// <summary>
    ///     Configuration for ingredients table
    /// </summary>
	public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
	{
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(_ => _.Id);
        }
    }
}

