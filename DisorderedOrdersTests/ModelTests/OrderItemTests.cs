using DisorderedOrdersMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisorderedOrdersTests.ModelTests
{
    public class OrderItemTests
    {
        [Fact]
        public void CreatedWithoutConstructor()
        {
            var product = new Product() { Id = 1, Price = 350, Name = "Hot Dog", StockQuantity = 10 };
            var orderItem = new OrderItem() { Id = 1,  Quantity = 5, Item = product };

            Assert.IsType<OrderItem>(orderItem);
        }
    }
}
