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
        [HttpPost]
        public IActionResult Index(string name)
        {
            List<Customer> customers = this.context.SearchCustomer(!string.IsNullOrEmpty(name)?name:"").ToList();
            return View(customers);
        }
        [HttpPost]
        public IActionResult Insert(Customer customer)
        {
            Customer model = new Customer()
            {
                Name = customer.Name,
                Country = customer.Country
            };
            this.context.InsertCustomer(model);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            Customer model = new Customer()
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Country = customer.Country
            };
            this.context.UpdateCustomer(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            this.context.DeleteCustomer(customer.CustomerId);
            return RedirectToAction("Index");
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