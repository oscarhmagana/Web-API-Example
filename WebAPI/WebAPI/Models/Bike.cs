using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Models
{
    public class Bike
    {
        public int BikeID { get; set; }
        public string Type { get; set; }
        public decimal WheelSize { get; set; }
        public decimal FrameSize { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }

        public static implicit operator Bike(Task<ActionResult<Bike>> v)
        {
            throw new NotImplementedException();
        }
    }
    

}
