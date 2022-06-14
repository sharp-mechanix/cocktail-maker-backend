using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CocktailDbAPI;
using CocktailDbAPI.Models.Drink;
using CocktailMaker.Common.Math;
using CocktailMaker.Data;
using CocktailMaker.Grabber.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using App = CocktailMaker.Data.Entities;

namespace CocktailMaker.Grabber.Modules
{
	/// <summary>
    ///		Cocktail grabber root service
    /// </summary>
	public class GrabberService : IGrabberService
	{
        private readonly IServiceScopeFactory _scopeFactory;
		private readonly ILogger<GrabberService> _logger;
        private readonly CocktailAPI _apiClient;

        public GrabberService(IServiceScopeFactory scopeFactory, ILogger<GrabberService> logger)
		{
			_logger = logger;
            _scopeFactory = scopeFactory;

            _apiClient = new CocktailAPI();
		}

		/// <inheritdoc />
        public async Task GetCocktailDataAsync(CancellationToken cancellationToken)
        {
			await GetIngredientsAsync(cancellationToken);
			await GetCocktailsAsync(cancellationToken);			
        }

        /// <summary>
        ///     Gets data about ingredients
        /// </summary>
        private async Task GetIngredientsAsync(CancellationToken cancellationToken)
        {
            var ingredientNames = await _apiClient.GetIngredientsFiltersAsync();
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            foreach (var name in ingredientNames)
            {
                var ingredient = (await _apiClient.GetIngredientsByNameAsync(name.Ingredient)).FirstOrDefault();
                if (ingredient is not null && !await IsIngredientExistsInDb(ingredient.IngredientId, dbContext))
                {
                    var newIngredient = new App.Ingredient
                    {
                        Name = ingredient.IngredientName,
                        Description = ingredient.Description,
                        Type = ingredient.Type,
                        CocktailDbId = Convert.ToInt32(ingredient.IngredientId),
                        IsAlcohol = ingredient.Alcohol.Contains("yes", StringComparison.InvariantCultureIgnoreCase),
                        ABV = ingredient.ABV is not null ? Convert.ToInt32(ingredient.ABV) : null,
                    };

                    using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

                    await dbContext.Ingredients.AddAsync(newIngredient, cancellationToken);

                    await dbContext.SaveChangesAsync(cancellationToken);
                    await t.CommitAsync(cancellationToken);
                }
            }
        }

        /// <summary>
        ///     If the ingredient is already imported
        /// </summary>
        private static Task<bool> IsIngredientExistsInDb(string cocktailDbId, AppDbContext dbContext)
            => dbContext.Ingredients.AnyAsync(i => i.CocktailDbId == Convert.ToInt32(cocktailDbId));

        /// <summary>
        ///     Gets data about cocktails
        /// </summary>
        private async Task GetCocktailsAsync(CancellationToken cancellationToken)
        {
            var categories = await _apiClient.GetCategoryFiltersAsync();
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            foreach (var category in categories)
            {
                var drinks = await _apiClient.GetDrinkSummariesByCategoryAsync(category.Category);
                foreach (var drink in drinks)
                {
                    if (await dbContext.Cocktails.AnyAsync(c => c.CocktailDbId == Convert.ToInt32(drink.DrinkId), cancellationToken))
                    {
                        continue;
                    }

                    var drinkDetails = await _apiClient.GetDrinkByIdAsync(drink.DrinkId);

                    var cocktail = new App.Cocktail
                    {
                        CocktailDbId = Convert.ToInt32(drinkDetails.DrinkId),
                        Name = drinkDetails.DrinkName,
                        Category = drinkDetails.Category,
                        IbaCategory = drinkDetails.IBA,
                        Glass = drinkDetails.Glass,
                        IsAlcoholic = drinkDetails.Alcoholic.Contains("yes", StringComparison.InvariantCultureIgnoreCase),
                        Instructions = drinkDetails.Instructions
                    };

                    using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

                    cocktail = (await dbContext.Cocktails.AddAsync(cocktail, cancellationToken)).Entity;
                    await dbContext.SaveChangesAsync(cancellationToken);

                    await t.CommitAsync(cancellationToken);

                    await GetMeasures(dbContext, cocktail, drinkDetails, cancellationToken);

                }
            }
        }

        /// <summary>
        ///     Parses measure from cocktail
        /// </summary>
        private static async Task GetMeasures(AppDbContext dbContext, App.Cocktail cocktail, Drink drink, CancellationToken cancellationToken)
        {
            var ingredients = await ConvertIngredientsToArrayAsync(dbContext, drink, cancellationToken);
            var measures = ConvertMeasuresToArray(drink);

            if (ingredients.Length < measures.Length)
            {
                throw new Exception("Something went wrong while parsing measures");
            }

            var dbMeasures = new List<App.Measure>();
            for (var i = 0; i < ingredients.Length; i++)
            {
                var measure = new App.Measure
                {
                    CocktailId = cocktail.Id,
                    IngredientId = ingredients[i].Id,
                    Unit = measures[i].Unit,
                    Value = measures[i].Value
                };

                dbMeasures.Add(measure);
            }

            using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            await dbContext.Measures.AddRangeAsync(dbMeasures, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            await t.CommitAsync(cancellationToken);
        }

        /// <summary>
        ///     Converts ingredient fields to list
        /// </summary>
        private static async Task<App.Ingredient[]> ConvertIngredientsToArrayAsync(AppDbContext dbContext, Drink drink, CancellationToken cancellationToken)
        {
            var result = new App.Ingredient[]
            {
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient1, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient2, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient3, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient4, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient5, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient6, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient7, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient8, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient9, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient10, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient11, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient12, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient13, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient14, cancellationToken),
                await GetOrCreateIngredientAsync(dbContext, drink.Ingredient15, cancellationToken),
            };

            return result.Where(i => i is not null).ToArray();
        }

        private static async Task<App.Ingredient> GetOrCreateIngredientAsync(AppDbContext dbContext, string ingredientName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(ingredientName))
            {
                return null;
            }

            var ingredient = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Name == ingredientName, cancellationToken);

            if (ingredient is not null)
            {
                return ingredient;
            }
            else
            {
                var newIngredient = new App.Ingredient
                {
                    Name = ingredientName
                };

                using var t = await dbContext.Database.BeginTransactionAsync(cancellationToken);

                var result = await dbContext.Ingredients.AddAsync(newIngredient);
                await dbContext.SaveChangesAsync(cancellationToken);

                await t.CommitAsync(cancellationToken);

                return result.Entity;
            }
        }

        /// <summary>
        ///     Converts measure fields to list
        /// </summary>
        private static UnitValuePair[] ConvertMeasuresToArray(Drink drink)
        {
            var result = new UnitValuePair[]
            {
                drink.Measure1 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure1) : null,
                drink.Measure2 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure2) : null,
                drink.Measure3 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure3) : null,
                drink.Measure4 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure4) : null,
                drink.Measure5 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure5) : null,
                drink.Measure6 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure6) : null,
                drink.Measure7 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure7) : null,
                drink.Measure8 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure8) : null,
                drink.Measure9 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure9) : null,
                drink.Measure10 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure10) : null,
                drink.Measure11 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure11) : null,
                drink.Measure12 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure12) : null,
                drink.Measure13 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure13) : null,
                drink.Measure14 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure14) : null,
                drink.Measure15 is not null ? Fraction.ConvertFractionToUnitValuePair(drink.Measure15) : null
            };

            return result.Where(m => m is not null).ToArray();
        }
    }
}

