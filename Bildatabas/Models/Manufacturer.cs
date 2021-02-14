using ServiceStack.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using StringLengthAttribute = System.ComponentModel.DataAnnotations.StringLengthAttribute;

namespace Bildatabas.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please fill in a name")]
        [StringLength(10)]
        
        public string ManufacturerName { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}