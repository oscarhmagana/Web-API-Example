using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Bike> Bikes { get; set; }
        // more DBSets can be declared here...

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public Bike getBike(int bikeId)
        {
            return Bikes.Find(bikeId);
        }

        public List<Bike> getBikes()
        {
            return Bikes.ToList();
        }


        public void AddBike(Bike _bike)
        {
            Bikes.Add(_bike);
            SaveChanges();
            return;
        }

    }
}
