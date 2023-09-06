using DisorderedOrdersMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisorderedOrdersTests.ModelTests
{
    public class CustomerTests
    {
        [Fact]
        public void CreatedWithoutConstructor()
        {
            var customer = new Customer() { Id = 1, Email = "megan@test.com", IsPreferred = true };

            Assert.IsType<Customer>(customer);
        }
    }
}
