using Bildatabas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Bildatabas.Data;

namespace Bildatabas.Data
{
    public class DbInitializer
    {
        /// <summary>
        /// This method fills in the engine types in the database
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(CarContext context)
        {
            context.Database.EnsureCreated();
            if (context.Engines.Any())
            {
                return;
            }
            var engines = new Engine[]
            {
                new Engine{ EngineType = "Petrol", EngineReferenceNumber = 0},
                new Engine{ EngineType = "Diesel", EngineReferenceNumber = 1},
                new Engine{ EngineType = "Electric", EngineReferenceNumber = 2}
            };
            foreach (var engine in engines)
            {
                context.Engines.Add(engine);
            }
            context.SaveChanges();

        }
        /// <summary>
        /// This method reads from the Json file and fills the database with the data in it.
        /// </summary>
        /// <param name="context"></param>
        public static void JsonData(CarContext context)
        {
            string JsonString = File.ReadAllText(@"Data\FillData.json");
            List<JsonObject> jsonObjects = JsonConvert.DeserializeObject<List<JsonObject>>(JsonString);

            foreach (var jObject in jsonObjects)
            {
                // Creates the city
                City city = context.Cities.FirstOrDefault(x => x.CityName == jObject.Dealer.City);
                if (city is null)
                {
                    city = new City
                    {
                        CityName = jObject.Dealer.City.Substring(0, Math.Min(jObject.Dealer.City.Length, 20))
                    };
                    context.Cities.Add(city);
                    context.SaveChanges();
                }
                // Then the cardealer, with a connection to the city that was created, or existed
                CarDealer dealer = context.CarDealers.FirstOrDefault(x => x.CarDealerName == jObject.Dealer.Name && x.CityId == city.Id);
                if (dealer is null)
                {
                        dealer = new CarDealer
                        {
                            CarDealerName = jObject.Dealer.Name.Substring(0, Math.Min(jObject.Dealer.Name.Length, 50)),
                            City = city
                        };
                        context.CarDealers.Add(dealer);
                        context.SaveChanges();
                };
                // Then the manufacturer
                Manufacturer manuFacturer = context.Manufacturers.FirstOrDefault(x => x.ManufacturerName == jObject.ManufacturerName);
                if (manuFacturer is null)
                {
                    manuFacturer = new Manufacturer
                    {
                        ManufacturerName = jObject.ManufacturerName.Substring(0, Math.Min(jObject.ManufacturerName.Length, 10))
                    };
                    context.Manufacturers.Add(manuFacturer);
                    context.SaveChanges();
                };
                // And, lastly, the car that it will always create
                Car car = new Car
                {
                    Manufacturer = manuFacturer,
                    CarModel = jObject.Model.Substring(0, Math.Min(jObject.Model.Length, 10)),
                    Engine = context.Engines.FirstOrDefault(x => x.EngineReferenceNumber == jObject.EngineType),
                    ProductionYear = jObject.Year,
                    CarPrice = jObject.Price,
                    CarDealer = dealer
                };
                context.Cars.Add(car);
                context.SaveChanges();
            }
        }
    }
}
