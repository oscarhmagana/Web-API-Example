using WebAPI.Controllers;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;


namespace WebAPI.Tests
{
    public class AyudaControllerTest
    {
        AyudaController _controller;
        DatabaseContext _context;
        string connString;

        public AyudaControllerTest()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            connString = config["ConnectionStrings:MySQLConnection"];

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlServer(connString)
                    .Options;

            _context = new DatabaseContext(options);
            _controller = new AyudaController(_context);
        }


        [Fact]
        public void TestGet()
        {
            string msg = "Hello World!";

            // Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(msg, result);
        }


        [Fact]
        public void TestGetBike()
        {
            // Arrange
            string brand = "Bianchi";

            // Act
            Bike result = _controller.GetBike(2);            

            // Assert
            Assert.NotNull(result);
            Assert.Equal(brand, result.Brand);
        }


        [Fact]
        public void TestGetBikes()
        {
            // Act
            var result = _controller.GetBikes();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Count);
        }


        [Fact]
        public void TestAddBike()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlServer(connString)
                    .Options;
            var bike = new Bike()
            {
                Type = "BMX",
                WheelSize = 20,
                FrameSize = 20,
                Color = "chrome",
                Brand = "Dyno"
            };

            // Act
            _controller.AddBike(bike);

            // Use a separate instance of the context to verify data
            using (var context = new DatabaseContext(options))
            {
                var count = context.getBikes().Count;

                // Assert            
                Assert.Equal(11, count);
            }
        }

        [Fact]
        public async void TestPutBike()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlServer(connString)
                    .Options;
            int bikeID = 12;
            var bike = new Bike()
            {
                BikeID = bikeID,
                Type = "Road",
                WheelSize = 700,
                FrameSize = 56,
                Color = "Black",
                Brand = "Specialized"
            };

            // Act
            await _controller.PutBike(bikeID, bike);

            // Use a separate instance of the context to verify data
            using (var context = new DatabaseContext(options))
            {
                var updatedBike = context.getBike(bikeID);

                // Assert            
                Assert.Equal(bike.Type, updatedBike.Type);
                Assert.Equal(bike.Brand, updatedBike.Brand);
            }
        }


        [Fact]
        public async void TestDeleteBike()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlServer(connString)
                    .Options;
            int bikeID = 11;

            // Act
            await _controller.DeleteBike(bikeID);

            // Use a separate instance of the context to verify data
            using (var context = new DatabaseContext(options))
            {
                var bike = context.getBike(bikeID);

                // Assert     
                Assert.Null(bike);                
            }
        }

    }
}
