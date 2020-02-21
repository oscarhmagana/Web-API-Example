using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class BikeDbContext : DbContext
    {

        public DbSet<Bike> Bikes { get; set; }

        public BikeDbContext(DbContextOptions<BikeDbContext> options) : base(options)
        {
        }
    }
}
