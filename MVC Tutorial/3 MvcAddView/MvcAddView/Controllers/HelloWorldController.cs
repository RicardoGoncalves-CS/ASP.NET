using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcAddView.Controllers
{
    public class HelloWorldController : Controller
    {
        // The Index method below:
        // Calls the controller's View method
        // Uses a view template to generate an HTML response

        // Controller methods:
        // Are referred to as "action methods"
        // Generally return an IActionResult or a class derived from ActionResult

        // When the view name isn't specified, the default View is returned
        // The default view has the same name as the action method, Index in this example
        // The view template /Views/HelloWorld/Index.cshtml is used

        // GET: /HelloWorld/
        public IActionResult Index()
        {
            return View();
        }


        // The View template generates a dynamic response, which means that appropriate data must be passed
        // from the controller to the view to generate the response.
        // This is done by having the controller put the dynamic data (parameters) that the view template needs
        // in a ViewData dictionary. The view template can then access the dynamic data.

        // The ViewData dictionary is a dynamic object that has no defined properties until something is added.
        // The MVC model binding system automatically maps the named parameters (name and numTimes)
        // from the query string to parameters in the method:

        // GET: /HelloWorld/Welcome/
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }
}
