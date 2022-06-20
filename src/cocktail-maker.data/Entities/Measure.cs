using CocktailMaker.Data.Interfaces;

namespace CocktailMaker.Data.Entities
{
    /// <summary>
    ///		Ingredient measure (how much do you need of it)
    /// </summary>
    public class Measure : IEntity<int>
    {
        /// <summary>
        ///		ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///		Cocktail to apply measure
        /// </summary>
        public Cocktail Cocktail { get; set; }

        /// <summary>
        ///		Cocktail database Id
        /// </summary>
        public int CocktailId { get; set; }

        /// <summary>
        ///		Ingredient to measure
        /// </summary>
        public Ingredient Ingredient { get; set; }

        /// <summary>
        ///		Ingredient database Id
        /// </summary>
        public int IngredientId { get; set; }

        /// <summary>
        ///		Measure value
        /// </summary>
        public string Value { get; set; }
    }
}
