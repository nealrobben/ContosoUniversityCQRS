using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversityCQRS.WebUI.Models;
using ContosoUniversityCQRS.WebUI.Data;
using System.Threading.Tasks;
using ContosoUniversityCQRS.Application.Home.Queries.GetAboutInfo;

namespace ContosoUniversityCQRS.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController( SchoolContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            var result = await Mediator.Send(new GetAboutInfoQuery());
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
