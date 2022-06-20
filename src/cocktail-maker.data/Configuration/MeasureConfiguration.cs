using CocktailMaker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMaker.Data.Configuration
{
    /// <summary>
    ///     Configuration of measures table
    /// </summary>
	public class MeasureConfiguration : IEntityTypeConfiguration<Measure>
    {
        public void Configure(EntityTypeBuilder<Measure> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.HasOne(_ => _.Cocktail)
                .WithMany(_ => _.Measures)
                .HasForeignKey(_ => _.CocktailId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(_ => _.Ingredient)
                .WithOne()
                .HasForeignKey<Measure>(_ => _.IngredientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
