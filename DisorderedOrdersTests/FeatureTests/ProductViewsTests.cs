using DisorderedOrdersMVC.DataAccess;
using DisorderedOrdersMVC.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisorderedOrdersTests.FeatureTests
{
    [Collection("Feature Tests")]
    public class ProductViewsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProductViewsTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void IndexShowsAllAvailableProducts()
        {
            var client = _factory.CreateClient();
            var context = GetDbContext();

            var hotdog = new Product() { Name = "Hot Dog", StockQuantity = 100, Price = 100 };
            var notHotdog = new Product() { Name = "Not a Hot Dog", StockQuantity = 50, Price = 250 };
            var dietcoke = new Product() { Name = "Diet Coke", StockQuantity = 0, Price = 500 };
            var soda = new Product() { Name = "Soda", StockQuantity = 200, Price = 325 };
            context.Products.AddRange(hotdog, notHotdog, dietcoke, soda);
            context.SaveChanges();

            var response = await client.GetAsync("/Products");
            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("Hot Dog", html);
            Assert.Contains("Not a Hot Dog", html);
            Assert.Contains("Soda", html);
            Assert.DoesNotContain("Diet Coke", html);

        }

        private DisorderedOrdersContext GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DisorderedOrdersContext>();
            optionsBuilder.UseInMemoryDatabase("TestDatabase");

            var context = new DisorderedOrdersContext(optionsBuilder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Products.RemoveRange(context.Products);
            context.Customers.RemoveRange(context.Customers);

            return context;
        }
    }
}
