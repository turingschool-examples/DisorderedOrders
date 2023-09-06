using DisorderedOrdersMVC.DataAccess;
using DisorderedOrdersMVC.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisorderedOrdersTests.FeatureTests
{
    [Collection("Feature Tests")]
    public class OrderViewsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public OrderViewsTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void NewHasFormWithAvailableProducts()
        {
            var client = _factory.CreateClient();
            var context = GetDbContext();

            var megan = new Customer() { Email = "megan@test.com", IsPreferred = true };
            var hotdog = new Product() { Name = "Hot Dog", StockQuantity = 100, Price = 100 };
            var dietcoke = new Product() { Name = "Diet Coke", StockQuantity = 0, Price = 500 };
            var soda = new Product() { Name = "Soda", StockQuantity = 200, Price = 325 };
            context.Customers.Add(megan);
            context.Products.AddRange(hotdog, dietcoke, soda);
            context.SaveChanges();

            var response = await client.GetAsync($"/Orders/new/{megan.Id}");
            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("form", html);
            Assert.Contains("Hot Dog", html);
            Assert.Contains("Soda", html);
            Assert.DoesNotContain("Diet Coke", html);
        }

        [Fact]
        public async void ShowIncludesProductsAndTotal()
        {
            var client = _factory.CreateClient();
            var context = GetDbContext();

            var megan = new Customer() { Email = "megan@test.com", IsPreferred = true };
            var hotdog = new Product() { Name = "Hot Dog", StockQuantity = 100, Price = 100 };
            var soda = new Product() { Name = "Soda", StockQuantity = 200, Price = 325 };
            var dietcoke = new Product() { Name = "Diet Coke", StockQuantity = 10, Price = 500 };
            var orderItem1 = new OrderItem() { Item = hotdog, Quantity = 2 };
            var orderItem2 = new OrderItem() { Item = soda, Quantity = 1 };
            context.Customers.Add(megan);
            context.Products.AddRange(hotdog, soda);
            var order = new Order() { Customer = megan, Items = new List<OrderItem> { orderItem1, orderItem2 } };
            context.Orders.Add(order);
            context.SaveChanges();

            var response = await client.GetAsync($"/Orders/{order.Id}");
            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("525", html);
            Assert.Contains("Hot Dog", html);
            Assert.Contains("Soda", html);
            Assert.DoesNotContain("Diet Coke", html);
        }

        [Fact]
        public async void PostCreatesNewOrder()
        {
            var client = _factory.CreateClient();
            var context = GetDbContext();

            var megan = new Customer() { Email = "megan@test.com", IsPreferred = true };
            var hotdog = new Product() { Name = "Hot Dog", StockQuantity = 100, Price = 100 };
            var soda = new Product() { Name = "Soda", StockQuantity = 200, Price = 325 };
            context.Customers.Add(megan);
            context.Products.AddRange(hotdog, soda);
            context.SaveChanges();

            var formData = new Dictionary<string, string>
            {
                { "CustomerId", $"{megan.Id}" },
                { "Hot Dog", "2" },
                { "Soda", "1" },
                { "paymentType", "creditcard" }
            };

            var response = await client.PostAsync("/orders", new FormUrlEncodedContent(formData));
            var html = await response.Content.ReadAsStringAsync();

            var orderCount = context.Orders.Count();

            Assert.Contains("Total: 525", html);
            Assert.Equal(1, orderCount);
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
