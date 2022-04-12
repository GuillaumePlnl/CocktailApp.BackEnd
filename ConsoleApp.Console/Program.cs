using Newtonsoft.Json;
using CocktailApp.Console.DTOs;
using System.Threading;

namespace CocktailApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            RootCocktail newCocktails = new RootCocktail();
            // Call to API trough letters from A to Z : https://www.thecocktaildb.com/api/json/v1/1/search.php?f=a
            for (char i = 'a'; i <= 'z'; i++)
            {
                string jsonExtractFromUrl = Helper.ExtractUrlCocktail(i.ToString());
                RootCocktail deserializedRawData = JsonConvert.DeserializeObject<RootCocktail>(jsonExtractFromUrl);

                newCocktails.drinks = deserializedRawData.drinks;
                if(newCocktails.drinks != null)
                {
                    Helper.PutDataInDb(newCocktails);
                }

                Thread.Sleep(500);
            }
        }
    }

}



