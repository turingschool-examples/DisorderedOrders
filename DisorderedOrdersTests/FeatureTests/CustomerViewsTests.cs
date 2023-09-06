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
    public class CustomerViewsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CustomerViewsTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void IndexShowsAllCustomers()
        {
            var client = _factory.CreateClient();
            var context = GetDbContext();

            var megan = new Customer() { Email = "megan@test.com", IsPreferred = true };
            var molly = new Customer() { Email = "molly@test.com", IsPreferred = false };
            context.Customers.AddRange(megan, molly);
            context.SaveChanges();

            var response = await client.GetAsync("/Customers");
            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("megan@test.com", html);
            Assert.Contains("molly@test.com", html);
            Assert.Contains("Create Order", html);
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
