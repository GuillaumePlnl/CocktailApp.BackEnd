using CocktailApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail.WebApi.Repositories
{
    public interface ICocktailRepository
    {
        // Get all ingredients to list (1st screen)
        public Task<IEnumerable<CocktailApp.Infrastructure.Ingredient>> GetAllIngredients();

        public Task<IEnumerable<DrinkFull>> GetAllDrinks();
        public Task<IEnumerable<DrinkFull>> GetDrinkBySearchName(string searchString);

        // Detail drink
        public Task<IEnumerable<Drink>> GetDrinkByIngredientId(string strIngrList);

        // Get all drinks corresponding with selected ingredients
        public Task<DrinkFull> GetDetailledDrink(string drinkId);

        //public Task<IEnumerable<Ingredient>> GetDrinkComplement(string drinkId);

        public Task<IEnumerable<DrinkFull>> GetAllCocktails();


    }
}
