using CocktailApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cocktail.WebApi.Repositories
{
    public class CocktailRepository : ICocktailRepository
    {
        private readonly CocktailsDbContext _context;

        public CocktailRepository(CocktailsDbContext context)
        {
            _context = context;
            Console.WriteLine("Ctr CocktailRepository");
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredients()
        {
            if (_context == null || _context.Ingredients == null)
            {
                throw new ArgumentNullException("Context or Ingredients Null");
            }
            var ret = await _context.Ingredients.OrderBy(i => i.IngredientName).ToListAsync();
            Console.WriteLine("Ctr de GetAllIngredients : " + ret.Count().ToString() + " elements");
            return ret;
        }
        public async Task<IEnumerable<Glass>> GetAllGlasses()
        {
            if (_context == null || _context.Glasses == null)
            {
                throw new ArgumentNullException("Context or Ingredients Null");
            }
            var ret = new List<Glass>();
            ret = await _context.Glasses.OrderBy(i => i.GlassName).ToListAsync();
            return ret;
        }
        public async Task<IEnumerable<Alcoholic>> GetAllAlcoholics()
        {
            if (_context == null || _context.Alcoholics == null)
            {
                throw new ArgumentNullException("Context or Ingredients Null");
            }
            var ret = new List<Alcoholic>();
            ret = await _context.Alcoholics.OrderBy(i => i.AlcoholicName).ToListAsync();
            return ret;
        }

        public async Task<IEnumerable<DrinkFull>> GetAllDrinks()
        {
            if (_context.Drinks == null)
            {
                throw new ArgumentNullException("Context or Ingredients Null");
            }

            var drinks = await _context.Drinks.Include(d => d.Category).Include(d => d.Alcoholic).Include(d => d.Glass)
                                            .ToListAsync();
            var rep = new List<DrinkFull> { };
                foreach(Drink d in drinks)
                {
                    rep.Add(new DrinkFull
                    {
                        AlcoholicName = d.Alcoholic.AlcoholicName,
                        CategoryName = d.Category.CategoryName,
                        DrinkName = d.DrinkName,
                        GlassName = d.Glass.GlassName,
                        Instruction = d.Instruction,
                        UrlPicture = d.UrlPicture,
                        PkId = d.PkId,
                    });
                }
            Console.WriteLine("Ctr de GetAllDrinks : " + rep.Count().ToString() + " elements");
            return rep;
        }

        public async Task<IEnumerable<Drink>> GetDrinkByIngredientId(string strIngrIdList)
        {
            // passage des Guid du front en une string, et reconversion de cette string en guids
            List<string> drinkList = strIngrIdList.Split("&Ids=").ToList();

            List<Guid> ListeIdDrink = await _context.Measures.Where(m => drinkList.Contains(m.IngredientPkId.ToString()))
                                                .Select(m => m.DrinkPkId).ToListAsync();
            Console.WriteLine("Ctr de GetDrinkByIngredientId : " + strIngrIdList);
            return await _context.Drinks.Where(d => ListeIdDrink.Contains(d.PkId)).OrderBy(n => n.DrinkName).ToListAsync();
        }

        public async Task<DrinkFull> GetDetailledDrink(string drinkPkId)
        {
            Console.WriteLine("Ctr GetDetailledDrink.");

            //var glass = _context.Glasses.Where(i => i.PkId == a.FkGlass).Select(j => j.GlassName).FirstOrDefault();
            //var category = _context.Categories.Where(i => i.PkId == a.FkCategory)
            //                                    .Select(j => j.CategoryName).FirstOrDefault();
            //var alcoholic = _context.Alcoholics.Where(i => i.PkId == a.FkAlcoholic)
            //                                    .Select(j => j.AlcoholicName).FirstOrDefault();
            //var quantity = _context.Measures.Where(i => i.DrinkPkId == Guid.Parse(drinkId))
            //                                    .Select(j => j.Quantity).ToList();
            //var ingredients = _context.Measures.Where(i => i.DrinkPkId == Guid.Parse(drinkId))
            //                                    .Select(j => j.Ingredient).Select(k => k.IngredientName).ToList();

            var detailledDrink = await _context.Drinks.Include(d => d.Glass).Include(d => d.Alcoholic)
                                    .Include(d => d.Category).FirstOrDefaultAsync(d => d.PkId == Guid.Parse(drinkPkId));
            // "Where" not needed on top request because can include the filter in firstOrDefault. 
            //var detailledDrink = await _context.Drinks.Where(d => d.PkId == Guid.Parse(drinkId))
            //                        .Include(d => d.Alcoholic).Include(d => d.Category).FirstOrDefaultAsync();

            var detailledMeasures = await _context.Measures.Where(m => m.DrinkPkId == Guid.Parse(drinkPkId)).Include(m => m.Ingredient)
                        .Include(m => m.Drink).ToListAsync();

            //var queryIngredientQuantity = (from Measure in _context.Measures
            //            join Ingredient in _context.Ingredients
            //                            on Measure.IngredientPkId
            //                            equals Ingredient.PkId
            //            join Drink in _context.Drinks
            //                            on Measure.DrinkPkId
            //                            equals Drink.PkId
            //            select new IngredientFull { IngredientName = Ingredient.IngredientName,
            //                                        Quantity = Measure.Quantity,
            //                                        FK_IdDrink = Measure.DrinkPkId,
            //                                        DrinkName = Drink.DrinkName
            //            }).ToList();

            var drinkFull = new DrinkFull
            {
                AlcoholicName = detailledDrink.Alcoholic.AlcoholicName,
                CategoryName = detailledDrink.Category.CategoryName,
                DrinkName = detailledDrink.DrinkName,
                GlassName = detailledDrink.Glass.GlassName,
                Instruction = detailledDrink.Instruction,
                UrlPicture = detailledDrink.UrlPicture,
                PkId = detailledDrink.PkId,
                IngredientsQuantities = new List<IngredientFull> ()
            };

            foreach(var dm in detailledMeasures)
            {
                var ingredientToAdd = new IngredientFull()
                {
                    DrinkName = drinkFull.DrinkName,
                    FK_IdDrink = drinkFull.PkId,
                    IngredientName = dm.Ingredient.IngredientName,
                    Quantity = dm.Quantity
                };
                drinkFull.IngredientsQuantities.Add(ingredientToAdd);
            }

            return drinkFull;
        }

        public async Task<IEnumerable<DrinkFull>> GetAllCocktails()
        {
            var cocktails = await _context.Drinks.Select(a => new DrinkFull
            {
                PkId = a.PkId,
                UrlPicture = a.UrlPicture,
                DrinkName = a.DrinkName,
                CategoryName = a.Category.CategoryName,
            }
                                    ).OrderBy(e => Guid.NewGuid()).Take(10).ToListAsync();
            Console.WriteLine(DateTime.Now + " : Ctr de GetAllCocktails. " + cocktails.Count() + " cocktails trouvés. ");
            return cocktails;
        }

        public async Task<IEnumerable<DrinkFull>> GetDrinkBySearchName(string searchString)
        {
            List<DrinkFull> drinks = new();
            if (String.IsNullOrEmpty(searchString))
            {
                drinks = await _context.Drinks.Select(b => new DrinkFull
                {
                    PkId = b.PkId,
                    UrlPicture = b.UrlPicture,
                    DrinkName = b.DrinkName,
                    CategoryName = b.Category.CategoryName,
                }).ToListAsync();
            }
            else
            {
                drinks = await _context.Drinks.Where(a => a.DrinkName.StartsWith(searchString)).Select(b => new DrinkFull
                {
                    PkId = b.PkId,
                    UrlPicture = b.UrlPicture,
                    DrinkName = b.DrinkName,
                    CategoryName = b.Category.CategoryName,
                }).ToListAsync();
            }

            Console.WriteLine(DateTime.Now + " : Ctr de GetDrinkBySearchName. Cocktail : " + drinks.FirstOrDefault().DrinkName);
            return drinks;
        }

        public async Task CreateDrink(Drink drinkJson)
        {
            //// si ingredients non fournis dans la requete (donc inexistants), création de nouveaux Guid
            //if(Regex.IsMatch(drinkJson.FkGlass.ToString(),"[0-9A-Fa-f]{8}-([0-9A-Fa-f]{4}-){3}[0-9A-Fa-f]{12}") != true)
            //{
            //    drinkJson.FkGlass = Guid.NewGuid();

            //}
            //if (Regex.IsMatch(drinkJson.FkCategory.ToString(),"[0-9A-Fa-f]{8}-([0-9A-Fa-f]{4}-){3}[0-9A-Fa-f]{12}") != true)
            //{
            //    drinkJson.FkCategory = Guid.NewGuid();
            //}
            //if (Regex.IsMatch(drinkJson.FkAlcoholic.ToString(),"[0-9A-Fa-f]{8}-([0-9A-Fa-f]{4}-){3}[0-9A-Fa-f]{12}") != true)
            //{
            //    drinkJson.FkAlcoholic = Guid.NewGuid();
            //}
            Drink newDrink = drinkJson;
            //    new()
            //{
            //    PkId = drinkJson.PkId,
            //    DrinkName = drinkJson.DrinkName,
            //    Alcoholic = drinkJson.Alcoholic,
            //    Category = drinkJson.Category,
            //    Glass = drinkJson.Glass,
            //    Instruction = drinkJson.Instruction,
            //    Measures = drinkJson.Measures,
            //    UrlPicture = drinkJson.UrlPicture,

            //    FkAlcoholic = drinkJson.FkAlcoholic,
            //    FkCategory = drinkJson.FkCategory,
            //    FkGlass = drinkJson.FkGlass,
            //};

            _context.Drinks.Add(newDrink);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException("Saving in Db failed for some reason.");
            }
            return;
        }
    }
}
