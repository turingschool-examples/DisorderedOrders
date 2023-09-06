using DisorderedOrdersMVC.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace DisorderedOrdersMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DisorderedOrdersContext _context;

        public ProductsController(DisorderedOrdersContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Where(p => p.StockQuantity > 0);

            return View(products);
        }
    }
}
