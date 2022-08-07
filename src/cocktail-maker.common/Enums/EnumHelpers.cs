using System;

namespace CocktailMaker.Common.Enums
{
    /// <summary>
    ///     Helpers to convert strings to enums
    /// </summary>
    public static class EnumHelpers
    {
        /// <summary>
        ///     Converts "Alcoholic" field string to <see cref="Alcoholic" />
        /// </summary>
        public static Alcoholic ToAlcoholicEnum(this string source)
            => source switch
            {
                "yes" => Alcoholic.Alcoholic,
                "no" => Alcoholic.NonAlcoholic,
                _ => Alcoholic.Optional
            };

        /// <summary>
        ///     Converts "Glass" field string to <see cref="Glass" />
        /// </summary>
        /// <exception cref="ArgumentException">If glass string is not present in enum</exception>
        public static Glass ToGlassEnum(this string source)
            => source.ToLowerInvariant() switch
            {
                "highball glass" => Glass.HighballGlass,
                "cocktail glass" => Glass.CocktailGlass,
                "old-fashioned glass" => Glass.OldFashionedGlass,
                "whiskey glass" => Glass.WhiskeyGlass,
                "collins glass" => Glass.CollinsGlass,
                "pousse cafe glass" => Glass.PousseCafeGlass,
                "champagne glass" => Glass.ChampagneGlass,
                "whiskey sour glass" => Glass.WhiskeySourGlass,
                "cordial glass" => Glass.CordialGlass,
                "brandy sniffer" => Glass.BrandySnifter,
                "white wine glass" => Glass.WhiteWineGlass,
                "nick and nora glass" => Glass.NickAndNoraGlass,
                "hurricane glass" => Glass.HurricaneGlass,
                "coffee mug" => Glass.CoffeeMug,
                "shot glass" => Glass.ShotGlass,
                "jar" => Glass.Jar,
                "irish coffee cup" => Glass.IrishCoffeeCup,
                "punch bowl" => Glass.PunchBowl,
                "pitcher" => Glass.Pitcher,
                "pint glass" => Glass.PintGlass,
                "copper mug" => Glass.CopperMug,
                "wine glass" => Glass.WineGlass,
                "beer mug" => Glass.BeerMug,
                "margarita/coupette glass" => Glass.MargaritaCoupetteGlass,
                "beer pilsner" => Glass.BeerPilsner,
                "parfait glass" => Glass.ParfaitGlass,
                "mason jar" => Glass.MasonJar,
                "margarita glass" => Glass.MargaritaGlass,
                "martini glass" => Glass.MartiniGlass,
                "balloon glass" => Glass.BalloonGlass,
                "coupe glass" => Glass.CoupeGlass,
                _ => throw new ArgumentException("Unknown type of glass"),
            };

        /// <summary>
        ///     Converts "Category" field string to <see cref="Category" />
        /// </summary>
        public static Category ToCategoryEnum(this string source)
            => source.ToLowerInvariant() switch
            {
                "ordinary drink" => Category.Ordinary,
                "cocktail" => Category.Cocktail,
                "shake" => Category.Shake,
                "cocoa" => Category.Cocoa,
                "shot" => Category.Shot,
                "coffee / tea" => Category.CoffeeOrTea,
                "homemade liqueur" => Category.HomemadeLiquor,
                "punch / party drink" => Category.Punch,
                "beer" => Category.Beer,
                "soft drink" => Category.Soft,
                _ => Category.Other,
            };
    }
}

