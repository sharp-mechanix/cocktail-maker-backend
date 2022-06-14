using CocktailMaker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMaker.Data.Configuration
{
    /// <summary>
    ///     Configuration for cocktails table
    /// </summary>
    public class CocktailConfiguration : IEntityTypeConfiguration<Cocktail>
    {
        public void Configure(EntityTypeBuilder<Cocktail> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.HasMany(_ => _.Measures)
                .WithOne(_ => _.Cocktail)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

