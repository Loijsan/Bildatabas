using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bildatabas.Models
{
    public class Car
    {
        public int Id { get; set; }
        //ToDo: rätta
        
        [Required(ErrorMessage = "All fields must be filled")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        [Required(ErrorMessage = "All fields must be filled")]
        [StringLength(10)]
        public string CarModel { get; set; }
        
        [Required(ErrorMessage = "All fields must be filled")]
        public int EngineId { get; set; }
        public Engine Engine { get; set; }

        [Required(ErrorMessage = "All fields must be filled")]
        public int ProductionYear { get; set; }

        [Required(ErrorMessage = "All fields must be filled")]
        public float CarPrice { get; set; }

        [Required(ErrorMessage = "All fields must be filled")]
        public int CardealerId { get; set; }
        public CarDealer CarDealer { get; set; }

    }
}
