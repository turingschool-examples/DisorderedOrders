namespace DisorderedOrdersMVC.Services
{
    public interface IPaymentProcessor
    {
        public bool ProcessPayment(int amount);
        public bool ProcessRefund(int amount);
    }
}
