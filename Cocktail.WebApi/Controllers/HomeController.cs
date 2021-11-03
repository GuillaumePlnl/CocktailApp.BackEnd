using Cocktail.WebApi.Repositories;
using CocktailApp.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail.WebApi.Controllers
{
    public class Test
    {
        public static List<string> listOfIngredients = new() { "Gin" };
        public static List<string> idOfIngredients = new() { "613b4cdf-5f9f-4f55-bc18-08179e33d83d" };
        public string DrinkName { get; set; } = "Gin Squirt";
    }
    [ApiController]
    [Route("cocktail/[controller]")]
    public class HomeController : ControllerBase
    {

        private ICocktailRepository _repo { get; set; }
        public HomeController(ICocktailRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("allDrinks/")]
        public async Task<IEnumerable<DrinkFull>> GetAllDrinks()
        {
            return await _repo.GetAllDrinks();
        }

        //etape 1
        [HttpGet("allIngredients/")]
        public async Task<IEnumerable<Ingredient>> GetAllIngredients()
        {
            return await _repo.GetAllIngredients();
        }

        //etape 2
        [HttpGet("GetDrinkByIngredientsId/{ids}")]
        public async Task<IEnumerable<Drink>> GetDrinkByIngredientsId(string ids)
        {
            return await _repo.GetDrinkByIngredientId(ids);
        }

        //etape 3
        [HttpGet("getDetailledDrink/{drinkId}")]
        public async Task<ActionResult> GetDetailledDrink(string drinkId)
        {
            return Ok(await _repo.GetDetailledDrink(drinkId));
        }

        [HttpGet("GetAllCocktails/")]
        public async Task<IEnumerable<DrinkFull>> GetAllCocktails()
        {
            return await _repo.GetAllCocktails();
        }
        ////etape 3b - Details d'ingrédients du cocktail (intégré dans detailled drink)
        //[HttpGet("GetDrinkComplement/{drinkId}")]
        //public async Task<IEnumerable<Ingredient>> GetDrinkComplement(string drinkId)
        //{
        //    return await _repo.GetDrinkComplement(drinkId);
        //}

        [HttpGet("GetDrinkBySearchName/{searchString}")]

        public async Task<IEnumerable<DrinkFull>> GetDrinkBySearchName(string searchString)
        {
            if (String.IsNullOrEmpty(searchString)) searchString = "";
            return await _repo.GetDrinkBySearchName(searchString);
        }

    }

}
