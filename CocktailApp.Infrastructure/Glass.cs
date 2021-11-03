using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

#nullable disable

namespace CocktailApp.Infrastructure
{
    [DataContract]
    public partial class Glass
    {
        [Key]
        [DataMember]
        public Guid PkId { get; set; }
        [DataMember]
        public string GlassName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Drink> Drinks { get; set; }

    }

}
