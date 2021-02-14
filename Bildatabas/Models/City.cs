using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using StringLengthAttribute = System.ComponentModel.DataAnnotations.StringLengthAttribute;

namespace Bildatabas.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please fill in a name")]
        [StringLength(20)]
        public string CityName { get; set; }
    }
}
