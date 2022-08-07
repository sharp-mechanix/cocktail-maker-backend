using CocktailMaker.Data.Interfaces;

namespace CocktailMaker.Data.Entities
{
    public class Ingredient : IEntity<int>
    {
        /// <summary>
        ///     ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     CocktailDB ID
        /// </summary>
        public int? CocktailDbId { get; set; }

        /// <summary>
        ///     Ingredient name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Ingredient description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Ingredient type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     Whether ingredient has alcohol or not
        /// </summary>
        public bool IsAlcohol { get; set; }

        /// <summary>
        ///     Volume percentage of alcohol
        /// </summary>
        public double ABV { get; set; }
    }
}
