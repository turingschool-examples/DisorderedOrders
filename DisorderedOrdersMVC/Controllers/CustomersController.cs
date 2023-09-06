using DisorderedOrdersMVC.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace DisorderedOrdersMVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly DisorderedOrdersContext _context;

        public CustomersController(DisorderedOrdersContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customers = _context.Customers;

            return View(customers);
        }
    }
}
