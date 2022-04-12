using System;
using System.Net;
using CocktailApp.Console.DTOs;
using CocktailApp.Infrastructure;
using System.Linq;
using System.Collections.Generic;

namespace CocktailApp.Console
{
    public static class Helper
    {
        static Dictionary<string, DrinkJson> doublonsDictionnary = new();
        static Dictionary<string, Ingredient> ingredientDictionnary = new();
        static Dictionary<string, Drink> drinkDictionnary = new();

        public static void PutDataInDb(RootCocktail cocktailArray)
        {
            // Db instance to fill with deserialized values (ETL)
            CocktailsDbContext _DbCocktail = new();

            foreach (DrinkJson d in cocktailArray.drinks)
            {
                Guid alcoholicsGuid = Guid.NewGuid();
                Guid glassesGuid = Guid.NewGuid();
                Guid categoriesGuid = Guid.NewGuid();

                if(! doublonsDictionnary.Any(e => e.Value.strAlcoholic.ToLower() == d.strAlcoholic.ToLower()))
                {
                    _DbCocktail.Alcoholics.Add(new Alcoholic()
                    {
                        PkId = alcoholicsGuid,
                        AlcoholicName = d.strAlcoholic,
                    });
                    _DbCocktail.SaveChanges();
                }

                if (! doublonsDictionnary.Any(e => e.Value.strGlass.ToLower() == d.strGlass.ToLower()))
                {
                    _DbCocktail.Glasses.Add(new Glass()
                    {
                        PkId = glassesGuid,
                        GlassName = d.strGlass,
                    });
                    _DbCocktail.SaveChanges();
                }

                if (! doublonsDictionnary.Any(e => e.Value.strCategory.ToLower() == d.strCategory.ToLower()))
                {
                    _DbCocktail.Categories.Add(new Category()
                    {
                        PkId = categoriesGuid,
                        CategoryName = d.strCategory,
                    });
                    _DbCocktail.SaveChanges();
                }

                // Si la Db contient les éléments de la table strAlcoholic,
                // remplacement de la FK_Alcoholic générée du Drink par la PK existante
                if (doublonsDictionnary.Any(e => e.Value.strAlcoholic.ToLower() == d.strAlcoholic.ToLower()))
                {
                    alcoholicsGuid = Guid.Parse(_DbCocktail.Alcoholics.Where(g => g.AlcoholicName.ToLower() 
                                                       == d.strAlcoholic.ToLower()).Select(c => c.PkId).First().ToString());
                };

                // Si la Db contient les éléments de la table strCategory,
                // remplacement de la FK_Category générée du Drink par la PK existante
                if (doublonsDictionnary.Any(e => e.Value.strCategory.ToLower() == d.strCategory.ToLower()))
                {
                    categoriesGuid = Guid.Parse(_DbCocktail.Categories.Where(g => g.CategoryName.ToLower()
                                                       == d.strCategory.ToLower()).Select(c => c.PkId).First().ToString());
                };

                // Si la Db contient les éléments de la table strGlass,
                // remplacement de la FK_Glass générée du Drink par la PK existante
                if (doublonsDictionnary.Any(e => e.Value.strGlass.ToLower() == d.strGlass.ToLower()))
                {
                    glassesGuid = Guid.Parse(_DbCocktail.Glasses.Where(g => g.GlassName.ToLower() 
                                                       == d.strGlass.ToLower()).Select(c => c.PkId).First().ToString());
                };

                // Création de l'objet désérialisé et proprement lié aux tables SQL
                Guid drinkId = Guid.NewGuid();
                Drink newDrink = new Drink()
                {
                    PkId = drinkId,
                    DrinkName = d.strDrink,
                    UrlPicture = d.strDrinkThumb,
                    Instruction = d.strInstructions,
                    IdSource = d.idDrink,

                    FkAlcoholic = alcoholicsGuid,
                    FkCategory = categoriesGuid,
                    FkGlass = glassesGuid,
                };

                _DbCocktail.Add(newDrink);
                _DbCocktail.SaveChanges();
                drinkDictionnary.Add(newDrink.DrinkName, newDrink);

                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient1, d.strMeasure1);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient2, d.strMeasure2);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient3, d.strMeasure3);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient4, d.strMeasure4);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient5, d.strMeasure5);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient6, d.strMeasure6);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient7, d.strMeasure7);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient8, d.strMeasure8);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient9, d.strMeasure9);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient10, d.strMeasure10);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient11, d.strMeasure11);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient12, d.strMeasure12);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient13, d.strMeasure13);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient14, d.strMeasure14);
                AddstrIngredientAndstrMesure(_DbCocktail, drinkId, d.strIngredient15, d.strMeasure15);
                //if (d.strIngredient1 != null) _DbCocktail.Ingredients.Add(new Ingredient() { PkId = IngredientId1, IngredientName = d.strIngredient1 }); _DbCocktail.SaveChanges();
                //if (d.strMeasure1 != null) _DbCocktail.Measures.Add(new Measure() { DrinkPkId = drinkId, IngredientPkId = IngredientId1, Quantity = d.strMeasure1 }); _DbCocktail.SaveChanges();

                doublonsDictionnary.Add(d.strDrink, d);
                System.Console.WriteLine("Drink added. Name : {0} Id : {1}", d.strDrink, d.idDrink);
            }
        }

        public static void AddstrIngredientAndstrMesure(CocktailsDbContext _DbCocktail, Guid drinkId, 
                                                            string ingredientPassed, string measurePassed)
        {
            Ingredient ingredientForMeasure;
            Guid guid;
            string quantity = (measurePassed == null) ? "" : measurePassed;

            // Gestion des exceptions et valeurs nulles
            if (String.IsNullOrEmpty(ingredientPassed) || (ingredientPassed == "\n")) return;

            // Si l'ingredient existe
            if (ingredientDictionnary.TryGetValue(ingredientPassed.ToLower(), out Ingredient ExistingIngredient))
            {
                guid = ExistingIngredient.PkId;
            }
            // Si l'ingredient n'existe pas
            else
            {
                guid = Guid.NewGuid();
                ingredientForMeasure = new Ingredient()
                {
                    PkId = guid,
                    IngredientName = ingredientPassed,
                };

                _DbCocktail.Ingredients.Add(ingredientForMeasure);
                _DbCocktail.SaveChanges();
                ingredientDictionnary.Add(ingredientPassed.ToLower(), ingredientForMeasure);
            }

            Measure newMeasure = new Measure()
            {
                PK_Id = Guid.NewGuid(),
                DrinkPkId = drinkId,
                IngredientPkId = guid,
                Quantity = quantity
            };

            _DbCocktail.Measures.Add(newMeasure);
            _DbCocktail.SaveChanges();
        }

        public static string ExtractUrlCocktail(string letter)
        {
            using WebClient webClient = new System.Net.WebClient();

            WebClient n = new();
            var json = n.DownloadString("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=" + letter.ToString());
            string valueOriginal = Convert.ToString(json);

            System.Console.WriteLine(json);
            return valueOriginal;
        }

        public static void Insert<T>(List<T> items) where T : class
        {
            using (var context = new CocktailsDbContext())
            {
                foreach (var item in items)
                {
                    context.Set<T>().Add(item);
                }

                context.SaveChanges();
            }
        }
    }
}



