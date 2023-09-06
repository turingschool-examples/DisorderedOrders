using DisorderedOrdersMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisorderedOrdersTests.ServiceTests
{
    public class BitcoinProcessorTests
    {
        [Fact]
        public void CanProcessPayments()
        {
            var processor = new BitcoinProcessor();

            Assert.True(processor.ProcessPayment(30));
        }
    }
}
