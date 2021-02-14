using System.Collections.Generic;

namespace Bildatabas.Models
{
    public class Engine
    {
        public int Id { get; set; }
        public int EngineReferenceNumber { get; set; }
        public string EngineType { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}