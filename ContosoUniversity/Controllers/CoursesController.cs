using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversityCQRS.WebUI.Data;
using ContosoUniversityCQRS.WebUI.Models;
using ContosoUniversityCQRS.Application.Courses.Queries.GetCoursesOverview;
using ContosoUniversityCQRS.Application.Courses.Queries.GetCourseDetails;
using ContosoUniversityCQRS.Application.Courses.Commands.DeleteCourse;
using ContosoUniversityCQRS.Application.Courses.Queries.DeleteConfirmation;
using ContosoUniversityCQRS.Application.Courses.Commands.CreateCourse;
using ContosoUniversityCQRS.Application.Departments.Queries.GetDepartmentsLookup;
using ContosoUniversityCQRS.Application.Courses.Queries.GetEditCourse;

namespace ContosoUniversityCQRS.WebUI.Controllers
{
    public class CoursesController : BaseController
    {
        public CoursesController(SchoolContext context) : base(context)
        {
        }

        public async Task<IActionResult> Index()
        {
            var result = await Mediator.Send(new GetCoursesOverviewQuery());
            return View(result);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var result = await Mediator.Send(new GetCourseDetailsQuery(id));
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,Credits,DepartmentID,Title")] CreateCourseCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception)
            {
                await PopulateDepartmentsDropDownList(command.DepartmentID);

                return View(new Domain.Entities.Course
                {
                    CourseID = command.CourseID,
                    Title = command.Title,
                    Credits = command.Credits,
                    DepartmentID = command.DepartmentID
                });
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var result = await Mediator.Send(new GetEditCourseCommand(id));

            await PopulateDepartmentsDropDownList(result.DepartmentID);

            return View(result);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
                return NotFound();

            var courseToUpdate = await _context.Courses
                .FirstOrDefaultAsync(c => c.CourseID == id);

            if (await TryUpdateModelAsync<Course>(courseToUpdate,
                "",
                c => c.Credits, c => c.DepartmentID, c => c.Title))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }

            await PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID);

            return View(courseToUpdate);
        }

        private async Task PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var result = await Mediator.Send(new GetDepartmentsLookupCommand());
            ViewBag.DepartmentID = new SelectList(result, "DepartmentID", "Name", selectedDepartment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var result = await Mediator.Send(new GetDeleteCourseConfirmationCommand(id));
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Mediator.Send(new DeleteCourseCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
