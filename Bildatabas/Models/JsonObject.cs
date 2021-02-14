using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bildatabas.Models
{
//    {
//"Year":2014,
//"EngineType":0,
//"ManufacturerName":"Mazda",
//"Model":"6 Skyactiv",
//"Price":30000.0,
//"Dealer":{
//"Name":"Star Motors",
//"City":"Skövds"
//}
//},
    public class JsonObject
    {
        public int Year { get; set; }
        public int EngineType { get; set; }
        public string ManufacturerName { get; set; }
        public string  Model { get; set; }
        public float Price { get; set; }
        public dealer Dealer { get; set; }

        public class dealer
        {
            public string Name { get; set; }
            public string City { get; set; }
        }

    }
}
