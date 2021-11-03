using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CocktailApp.Infrastructure
{
    public partial class Drink
    {
        public Drink()
        {
            Measures = new HashSet<Measure>();
        }
        [Key]
        public Guid PkId { get; set; }
        public string DrinkName { get; set; }
        public string UrlPicture { get; set; }
        public string Instruction { get; set; }
        public string IdSource { get; set; }
        // Foreign Keys
        [ForeignKey("FK_Category")]
        public Guid FkCategory { get; set; }
        public Category Category { get; set; }

        [ForeignKey("FK_Glass")]
        public Guid FkGlass { get; set; }
        public Glass Glass { get; set; }

        [ForeignKey("FK_Alcoholic")]
        public Guid FkAlcoholic { get; set; }
        public Alcoholic Alcoholic { get; set; }

        public virtual ICollection<Measure> Measures { get; set; }
    }

}
