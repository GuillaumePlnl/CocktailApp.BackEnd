using CocktailApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail.WebApi.Repositories
{
    public class CocktailRepository : ICocktailRepository
    {
        private readonly CocktailsDbContext _context;

        public CocktailRepository(CocktailsDbContext context)
        {
            _context = context;

            Console.WriteLine("Ctr de CocktailRepository");
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

        public async Task<DrinkFull> GetDetailledDrink(string drinkId)
        {

            //Drink a = _context.Drinks.Where(m => Guid.Parse(drinkId) == m.PkId)
            //                         .FirstOrDefault();
            Console.WriteLine("Ctr de GetDetailledDrink. retour cocktail : " /*+ a.Select(d => d.DrinkName)*/);


            //var glass = _context.Glasses.Where(i => i.PkId == a.FkGlass).Select(j => j.GlassName).FirstOrDefault();
            //var category = _context.Categories.Where(i => i.PkId == a.FkCategory)
            //                                    .Select(j => j.CategoryName).FirstOrDefault();
            //var alcoholic = _context.Alcoholics.Where(i => i.PkId == a.FkAlcoholic)
            //                                    .Select(j => j.AlcoholicName).FirstOrDefault();
            //var quantity = _context.Measures.Where(i => i.DrinkPkId == Guid.Parse(drinkId))
            //                                    .Select(j => j.Quantity).ToList();
            //var ingredients = _context.Measures.Where(i => i.DrinkPkId == Guid.Parse(drinkId))
            //                                    .Select(j => j.Ingredient).Select(k => k.IngredientName).ToList();
            var c = _context.Measures.Where(m => m.DrinkPkId == Guid.Parse(drinkId)).Include(m => m.Ingredient).Include(m => m.Drink).ToList();
            var d = _context.Drinks.Include(d => d.Glass).Include(d => d.Alcoholic)
                                    .Include(d => d.Category).FirstOrDefault(d => d.PkId == Guid.Parse(drinkId));


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


            //Measure drinkComplement = GetDrinkComplement(drinkId);
            var rep = new DrinkFull
            {
                AlcoholicName = d.Alcoholic.AlcoholicName,
                CategoryName = d.Category.CategoryName,
                DrinkName = d.DrinkName,
                GlassName = d.Glass.GlassName,
                Instruction = d.Instruction,
                UrlPicture = d.UrlPicture,
                PkId = d.PkId,
                IngredientsQuantities = new List<IngredientFull> (),
            };

            foreach(var measure in c)
            {
                var ingredientToAdd = new IngredientFull()
                {
                    DrinkName = rep.DrinkName,
                    FK_IdDrink = rep.PkId,
                    IngredientName = measure.Ingredient.IngredientName,
                    Quantity = measure.Quantity
                };
                rep.IngredientsQuantities.Add(ingredientToAdd);
            }


            //List<DrinkFull> resp2 = new() { rep };
            return rep;
        }

        public async Task<IEnumerable<DrinkFull>> GetAllCocktails()
        {
            var a = await _context.Drinks.Select(a => new DrinkFull
                                        {
                                            PkId = a.PkId,
                                            UrlPicture = a.UrlPicture,
                                            DrinkName = a.DrinkName,
                                            CategoryName = a.Category.CategoryName,
                                        }
                                    ).OrderBy(e => Guid.NewGuid()).Take(10).ToListAsync();
            Console.WriteLine(DateTime.Now + " : Ctr de GetAllCocktails. " + a.Count() + " cocktails trouvés. ");

            return a;
        }

        public async Task<IEnumerable<DrinkFull>> GetDrinkBySearchName(string searchString)
        {
            List<DrinkFull> a = new();
            if (String.IsNullOrEmpty(searchString))
            {
                a = await _context.Drinks.Select(b => new DrinkFull
                {
                    PkId = b.PkId,
                    UrlPicture = b.UrlPicture,
                    DrinkName = b.DrinkName,
                    CategoryName = b.Category.CategoryName,
                }).ToListAsync();
            }
            else
            {
                a = await _context.Drinks.Where(a => a.DrinkName.StartsWith(searchString)).Select(b => new DrinkFull
                {
                    PkId = b.PkId,
                    UrlPicture = b.UrlPicture,
                    DrinkName = b.DrinkName,
                    CategoryName = b.Category.CategoryName,
                }).ToListAsync();
            }

            Console.WriteLine(DateTime.Now + " : Ctr de GetDrinkBySearchName. Cocktail : " + a.FirstOrDefault().DrinkName);

            return a;
        }
    }
}
