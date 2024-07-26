using System.Diagnostics;
using Microsoft.AspNetCore.Mvc; // to extends with `: Controller` and IActionResult
using MvcMovie.Models;

// to organize HomeController to MvcMovie.Controllers
namespace MvcMovie.Controllers
{
    // Controller: A base class for an MVC controller with view support
    public class HomeController : Controller
    {
        // ILogger is used for loggin purpose
        // define private _logger to be override when the HomeController
        // Constructor is called
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
