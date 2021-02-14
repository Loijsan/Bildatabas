using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bildatabas.Models
{
    public class CarDealer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please fill in a name")]
        [StringLength(50)]
        public string CarDealerName { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
