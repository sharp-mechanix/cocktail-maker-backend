﻿using System.Collections.Generic;
using CocktailMaker.Common.Enums;
using CocktailMaker.Data.Interfaces;

namespace CocktailMaker.Data.Entities
{
    /// <summary>
    ///     Cocktail entity
    /// </summary>
    public class Cocktail : IEntity<int>
    {
        /// <summary>
        ///     Cocktail ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     ID in CocktailDb
        /// </summary>
        public int CocktailDbId { get; set; }

        /// <summary>
        ///     Cocktail name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Cocktail category
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        ///     Is cocktail alcoholic
        /// </summary>
        public Alcoholic IsAlcoholic { get; set; }

        /// <summary>
        ///     IBA category
        /// </summary>
        public string IbaCategory { get; set; }

        /// <summary>
        ///     Cocktail glass
        /// </summary>
        public Glass Glass { get; set; }

        /// <summary>
        ///     Instructions to make cocktail
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        ///     Cocktail ingredients with measures
        /// </summary>
        public List<Measure> Measures { get; set; }
    }
}
