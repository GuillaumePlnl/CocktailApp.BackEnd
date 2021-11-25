using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail.WebApi.DTOs
{
    public class DrinkDTO
    {
        public Guid id { get; set; }
        public string dn { get; set; }
        public string url { get; set; }
        public string inst { get; set; }
        public string fkcat { get; set; }
        public string fkglas { get; set; }
        public string fkalc { get; set; }
        public string[] meas { get; set; }
    }
}
