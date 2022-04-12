using Cocktail.WebApi.DTOs;
using Cocktail.WebApi.Helpers;
using Cocktail.WebApi.Repositories;
using CocktailApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    [Route("cocktail/")]
    //[Authorize]
    public class HomeController : ControllerBase
    {
        private ICocktailRepository _repo { get; set; }
        public HomeController(ICocktailRepository repo)
        {
            _repo = repo;
        }

        [AllowAnonymous]
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
        [Helpers.Authorize]
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

        [HttpPost("/createDrink")]
        public async Task<IActionResult> CreateDrink([Bind(include:"id,dn,url,inst,fkcat,fkglas,fkalc,meas")][FromBody]DrinkDTO drinkDTO)
        {
            /* attributes exemple
            {
                "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "dn": "bazooka",
                "url": "https://tipsybartender.com/static/47103fd9ec5b8f5edc761c307c839953/aa61c/Bazooka-Joe-Shooters.jpg",
                "inst": "Try by yourself",
                "fkcat": "4dd298d5-8bde-4485-aebe-bcfd82cd81e0",
                "fkglas": "22dc688e-63de-4bc2-9ccd-04a99f311507",
                "fkalc": "063ee82e-7181-4203-8bf2-a4583aaaf2dd",
                "meas": [
                "Vodka&=1/3", "martini&=1/3", "coco&=1/3"
                ]
            }
            */

            if (drinkDTO == null) return BadRequest();

            drinkDTO.id = Guid.Parse("00000000" + Guid.NewGuid().ToString().Substring(8));
            
            var measCollection = new Collection<Measure>() { };

            foreach (string i in drinkDTO.meas)
            {
                Guid newIngredientId = Guid.Parse("00000000" + Guid.NewGuid().ToString()[8..]);
                string[] j = i.Split("&=");
                measCollection.Add( 
                    new Measure() 
                    { 
                        DrinkPkId = drinkDTO.id,
                        Ingredient = new Ingredient() { IngredientName = j[0],/*Measures = ,*/ PkId = newIngredientId },
                        IngredientPkId = newIngredientId,
                        PK_Id = Guid.Parse("00000000" + Guid.NewGuid().ToString()[8..]),
                        Quantity = j[1],
                    });
            }
            Drink drink = new()
            {
                PkId = drinkDTO.id,
                DrinkName = "Custom Drink : " + drinkDTO.dn,
                FkAlcoholic = Guid.Parse(drinkDTO.fkalc),
                FkCategory = Guid.Parse(drinkDTO.fkcat),
                FkGlass = Guid.Parse(drinkDTO.fkglas),
                Instruction = drinkDTO.inst,
                UrlPicture = drinkDTO.url,
                Measures = measCollection,
            };

            try
            {
                await _repo.CreateDrink(drink);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }

}
