using DisorderedOrdersMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisorderedOrdersTests.ModelTests
{
    public class ProductTests
    {
        [Fact]
        public void CreatedWithoutConstructor()
        {
            var product = new Product() { Id = 1, Name = "Hot Dog", StockQuantity = 10 };

            Assert.IsType<Product>(product);
        }

        [Fact]
        public void InStockReturnsBool()
        {
            var product = new Product() { Id = 1, Name = "Hot Dog", StockQuantity = 10 };

            Assert.True(product.InStock(5));
            Assert.False(product.InStock(15));
        }

        [Fact]
        public void DecreaseQuantityChangesStockQuantity()
        {
            var product = new Product() { Id = 1, Name = "Hot Dog", StockQuantity = 10 };

            product.DecreaseStock(5);

            Assert.Equal(5, product.StockQuantity);
        }
    }
}
