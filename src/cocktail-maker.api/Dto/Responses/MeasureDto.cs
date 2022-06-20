namespace CocktailMaker.Api.Dto.Responses
{
    /// <summary>
    ///     Represents ingredient and its measure
    /// </summary>
    public record MeasureDto
    {
        /// <summary>
        ///     Ingredient name
        /// </summary>
        public string IngredientName { get; init; } = "";

        /// <summary>
        ///     Measure of ingredient
        /// </summary>
        public string Measure { get; set; } = "";
    }
}
