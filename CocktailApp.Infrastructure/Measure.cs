using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CocktailApp.Infrastructure
{
    public partial class Measure
    {
        [Key]
        public Guid PK_Id { get; set; }

        // TODO Quantité
        public string Quantity { get; set; }

        [ForeignKey("FK_IdIngredient")]
        public Guid IngredientPkId { get; set; }
        public Ingredient Ingredient { get; set; }

        [ForeignKey("FK_IdDrink")]
        public Guid DrinkPkId { get; set; }
        public Drink Drink { get; set; }

    }
}
