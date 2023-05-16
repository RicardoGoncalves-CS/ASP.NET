using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // Default routing logic used by MVC:
        // /[Controller]/[ActionName]/[Parameters]

        // The routing format is set in the Program.cs:
        // app.MapControllerRoute();
        
        // When any URL segments are not provided, it default to the "Home" controller and "Index" method:
        // name: "default",
        // pattern: "{controller=Home}/{action=Index}/{id?}");
        
        // Index method is the default method called on a controller if a method isn't specified

        // GET: /HelloWorld/
        public string Index()
        {
            return "This is my default action";
        }

        // GET: /HelloWorld/Welcome/
        public string Welcome()
        {
            return "This is the Welcome action method";
        }

        // It's possible to pass parameters through the URL

        // For example, in the method below we can use:
        // /HelloWorld/Welcome?name=Rick&numtimes=4
        // Output: Hello Rick, NumTimes is: 4

        // HtmlEncoder.Default.Encode is used to protect the app from malicious input, such as through JavaScript

        // GET: /HelloWorld/WelcomeParameters/
        public string WelcomeParameters(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }

        // Other method that take parameters

        // In the following example the third URL segment matches the route parameter id
        // /HelloWorld/WelcomeWithId/3?name=Rick
        // Output: Hello Rick, ID: 3

        // GET: /HelloWorld/WelcomeWithId
        public string WelcomeWithId(string name, int ID = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        }
    }
}
