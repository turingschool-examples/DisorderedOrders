using DisorderedOrdersMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisorderedOrdersTests.ServiceTests
{
    public class PayPalProcessorTests
    {
        [Fact]
        public void CanProcessPayments()
        {
            var processor = new PayPalProcessor();

            Assert.True(processor.ProcessPayment(30));
        }

        [Fact]
        public void CanProcessRefunds()
        {
            var processor = new PayPalProcessor();

            Assert.True(processor.ProcessRefund(30));
        }
    }
}
