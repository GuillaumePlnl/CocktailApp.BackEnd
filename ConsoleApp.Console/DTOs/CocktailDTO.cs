using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CocktailApp.Console.DTOs
{
    public class RootCocktail
    {
        public RootCocktail()
        {
        }
        public DrinkJson[] drinks { get; set; }
    }

    public class DrinkJson
    {
        public DrinkJson()
        {
        }

        //TBL_UrlPicture
        [JsonPropertyName("idDrink")]
        public string idDrink { get; set; }

        //TBL_Drink
        [JsonPropertyName("strDrink")]
        public string strDrink { get; set; }
        //public object strDrinkAlternate { get; set; }
        //public string strTags { get; set; }
        //public object strVideo { get; set; }

        //TBL_Category
        [JsonPropertyName("strCategory")]
        public string strCategory { get; set; }
        //public string strIBA { get; set; }

        //TBL_alcoholic
        [JsonPropertyName("strAlcoholic")]
        public string strAlcoholic { get; set; }

        //TBL_Glass
        public string strGlass { get; set; }

        //TBL_Drink
        [JsonPropertyName("strInstructions")]
        public string strInstructions { get; set; }
        //public object strInstructionsES { get; set; }
        //public string strInstructionsDE { get; set; }
        //public object strInstructionsFR { get; set; }
        //public string strInstructionsIT { get; set; }
        //public object strInstructionsZHHANS { get; set; }
        //public object strInstructionsZHHANT { get; set; }
        //public string strDrinkThumb { get; set; }

        //TBL_Ingredient  >> designation
        [JsonPropertyName("strIngredient1")]
        public string strIngredient1 { get; set; }
        [JsonPropertyName("strIngredient2")]
        public string strIngredient2 { get; set; }
        [JsonPropertyName("strIngredient3")]
        public string strIngredient3 { get; set; }
        [JsonPropertyName("strIngredient4")]
        public string strIngredient4 { get; set; }
        [JsonPropertyName("strIngredient5")]
        public string strIngredient5 { get; set; }
        [JsonPropertyName("strIngredient6")]
        public string strIngredient6 { get; set; }
        [JsonPropertyName("strIngredient7")]
        public string strIngredient7 { get; set; }
        [JsonPropertyName("strIngredient8")]
        public string strIngredient8 { get; set; }
        [JsonPropertyName("strIngredient9")]
        public string strIngredient9 { get; set; }
        [JsonPropertyName("strIngredient10")]
        public string strIngredient10 { get; set; }
        [JsonPropertyName("strIngredient11")]
        public string strIngredient11 { get; set; }
        [JsonPropertyName("strIngredient12")]
        public string strIngredient12 { get; set; }
        [JsonPropertyName("strIngredient13")]
        public string strIngredient13 { get; set; }
        [JsonPropertyName("strIngredient14")]
        public string strIngredient14 { get; set; }
        [JsonPropertyName("strIngredient15")]
        public string strIngredient15 { get; set; }

        //TBL_Measure  >> quantity
        public string strMeasure1 { get; set; }
        public string strMeasure2 { get; set; }
        public string strMeasure3 { get; set; }
        public string strMeasure4 { get; set; }
        public string strMeasure5 { get; set; }
        public string strMeasure6 { get; set; }
        public string strMeasure7 { get; set; }
        public string strMeasure8 { get; set; }
        public string strMeasure9 { get; set; }
        public string strMeasure10 { get; set; }
        public string strMeasure11 { get; set; }
        public string strMeasure12 { get; set; }
        public string strMeasure13 { get; set; }
        public string strMeasure14 { get; set; }
        public string strMeasure15 { get; set; }

        // UrlPicture
        public string strDrinkThumb { get; set; }
        //public string strImageAttribution { get; set; }
        //public string strCreativeCommonsConfirmed { get; set; }
        //public string dateModified { get; set; }
    }

}
