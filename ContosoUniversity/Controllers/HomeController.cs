using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversityCQRS.WebUI.Models;
using ContosoUniversityCQRS.WebUI.Data;
using System.Threading.Tasks;
using System.Linq;
using ContosoUniversityCQRS.WebUI.Models.SchoolViewModels;
using Microsoft.EntityFrameworkCore;

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
            IQueryable<EnrollmentDateGroup> data =
                from student in _context.Students
                group student by student.EnrollmentDate into dateGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
