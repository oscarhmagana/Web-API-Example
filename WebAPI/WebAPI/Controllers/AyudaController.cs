using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AyudaController : ControllerBase
    {
        private readonly DatabaseContext _database;

        public AyudaController(DatabaseContext context)
        {
            _database = context;
        }


        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            return "Hello World!";
        }


        // GET: api/Ayuda/bike
        [HttpGet("{bikeid}")]        
        public Bike GetBike(int bikeID)
        {            
            var bike = _database.getBike(bikeID);

            return bike;
        }


        // GET: api/Ayuda/bikes
        [HttpGet("bikes")]        
        public List<Bike> GetBikes()
        {
            return _database.getBikes();
        }


        // POST: api/Ayuda/bike
        [HttpPost("bike")]        
        public IActionResult AddBike(Bike bike)
        {
            _database.AddBike(bike);
            return Ok(bike);
        }


        // PUT: api/Ayuda/bike
        [HttpPut("{bikeid}")]               
        public async Task<IActionResult> PutBike(int bikeID, Bike bike)
        {
            if (bikeID != bike.BikeID)
            {
                return BadRequest();
            }

            _database.Entry(bike).State = EntityState.Modified;
            await _database.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Ayuda/bike
        [HttpDelete("{bikeid}")]        
        public async Task<IActionResult> DeleteBike(int bikeID)
        {            
            var bike = _database.getBike(bikeID);

            if (bike == null)
            {
                return NotFound();
            }

            _database.Bikes.Remove(bike);
            await _database.SaveChangesAsync();

            return NoContent();
        }
    }
}
