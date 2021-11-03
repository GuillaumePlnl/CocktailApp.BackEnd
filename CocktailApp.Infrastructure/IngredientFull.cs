using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailApp.Infrastructure
{
    public class IngredientFull
    {
        public string IngredientName { get; set; }
        public string Quantity { get; set; }
        public Guid FK_IdDrink { get; set; }
        public string DrinkName { get; set; }
    }
}