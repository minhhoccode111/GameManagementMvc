using System.Diagnostics;
using Microsoft.AspNetCore.Mvc; // to extends with `: Controller` and IActionResult
using MvcMovie.Models;

// to organize HomeController to MvcMovie.Controllers
namespace MvcMovie.Controllers
{
    // Controller: A base class for an MVC controller with view support
    public class HomeController : Controller
    {
        // ILogger is used for loggin purpose, which will be injected into the controller with dependency injection
        // define private _logger to be override when the HomeController
        // Constructor is called
        private readonly ILogger<HomeController> _logger;

        // mark as readonly so that I cannot be changed after initializing
        public HomeController(ILogger<HomeController> logger)
        {
            // just to log warning and errors
            _logger = logger;
        }

        /*
           Defines a contract that represents the result of an action method
           Returning views: return View(); or return View("MyView");
           Returning JSON data: return Json(myData);
           Redirecting to another action: return RedirectToAction("Index", "Home");
           Handling errors: return NotFound(); or return BadRequest();
           */
        public IActionResult Index()
        {
            return View();
        }

        /*
           the [ResponseCache] attribute in this context is used to explicitly disable caching for the Error action to guarantee that the most up-to-date error information is always displayed to the user.
           */
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
