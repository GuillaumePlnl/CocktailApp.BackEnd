using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CocktailApp.Infrastructure
{
    public partial class DrinkFull
    {
        public DrinkFull()
        {
        }

        public Guid PkId { get; set; }
        public string DrinkName { get; set; }
        public string UrlPicture { get; set; }
        public string Instruction { get; set; }

        public string GlassName { get; set; }
        public string AlcoholicName { get; set; }
        public string CategoryName { get; set; }

        public List<IngredientFull> IngredientsQuantities { get; set; }
    }

}
