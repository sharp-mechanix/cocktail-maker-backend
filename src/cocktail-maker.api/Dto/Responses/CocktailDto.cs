using System.Collections.Immutable;
using CocktailMaker.Common.Enums;

namespace CocktailMaker.Api.Dto.Responses
{
    /// <summary>
    ///     DTO representing cocktail
    /// </summary>
    public record CocktailDto
    {
        /// <summary>
        ///     Cocktail ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Cocktail name
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        ///     Cocktail category
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        ///     IBA category
        /// </summary>
        public string IbaCategory { get; set; } = "";

        /// <summary>
        ///     Cocktail glass
        /// </summary>
        public Glass Glass { get; set; }

        /// <summary>
        ///     Instructions to make cocktail
        /// </summary>
        public string Instructions { get; set; } = "";

        /// <summary>
        ///     Measures of ingredients
        /// </summary>
        public ImmutableArray<MeasureDto> Measures { get; init; } = ImmutableArray<MeasureDto>.Empty;
    }
}
