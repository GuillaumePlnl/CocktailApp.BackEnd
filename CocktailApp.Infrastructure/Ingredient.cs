using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CocktailApp.Infrastructure
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            Measures = new HashSet<Measure>();
        }
        [Key]
        public Guid PkId { get; set; }
        // TODO tableau d'ingredients
        public string IngredientName { get; set; }

        public virtual ICollection<Measure> Measures { get; set; }
    }
}
