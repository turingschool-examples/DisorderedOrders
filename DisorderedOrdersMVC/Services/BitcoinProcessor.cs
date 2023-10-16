namespace DisorderedOrdersMVC.Services
{
    public class BitcoinProcessor : IPaymentProcessor, IRefundProcessor
    {
        public bool ProcessPayment(int amount)
        {
            // payment processing. This would be an API call to a crypto service that would create a payment transaction.
            
            return true;
        }

        public bool ProcessRefund(int amount)
        {
            throw new NotImplementedException("Bitcoin users would never rethink a decision.  Refunding is not necessary.");
        }
    }
}
