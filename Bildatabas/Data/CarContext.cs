using Bildatabas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bildatabas.Data
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options)
            : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarDealer> CarDealers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<City>(entity =>
            {
                entity.HasIndex(x => x.CityName).IsUnique();
            });
            builder.Entity<Manufacturer>(entity =>
            {
                entity.HasIndex(x => x.ManufacturerName).IsUnique();
            });
        }
    }
}
