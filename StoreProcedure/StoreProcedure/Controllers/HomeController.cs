using Microsoft.AspNetCore.Mvc;
using StoreProcedure.Models;
using System.Diagnostics;

namespace StoreProcedure.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpDbContext context;
        public HomeController(ILogger<HomeController> logger, SpDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            List<Customer> customers = this.context.SearchCustomer("").ToList();
            return View(customers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}