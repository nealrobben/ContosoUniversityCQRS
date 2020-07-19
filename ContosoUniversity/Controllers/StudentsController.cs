using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversityCQRS.WebUI.Data;
using System;
using ContosoUniversityCQRS.Application.Students.Queries.GetStudentsOverview;
using ContosoUniversityCQRS.Application.Students.Queries.GetStudentDetails;
using ContosoUniversityCQRS.Application.Students.Commands.CreateStudent;
using ContosoUniversityCQRS.Application.Students.Commands.DeleteStudent;
using ContosoUniversityCQRS.Application.Students.Queries.DeleteConfirmation;
using ContosoUniversityCQRS.Application.Students.Commands.UpdateStudent;
using ContosoUniversityCQRS.Application.Students.Queries.GetUpdateStudent;

namespace ContosoUniversityCQRS.WebUI.Controllers
{
    public class StudentsController : BaseController
    {
        public StudentsController(SchoolContext context) : base(context)
        {
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var result = await Mediator.Send(new GetStudentsOverviewQuery(sortOrder, currentFilter, searchString, pageNumber));
            return View(result);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var result = await Mediator.Send(new GetStudentDetailsQuery(id));
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstName,EnrollmentDate")] CreateStudentCommand command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await Mediator.Send(command);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(command);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var result = await Mediator.Send(new GetUpdateStudentCommand(id));
            return View(result);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost([Bind("StudentID, LastName,FirstName,EnrollmentDate")] UpdateStudentCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }

            return View(command);
        }

        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            var result = await Mediator.Send(new GetDeleteConfirmationCommand(id));

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await Mediator.Send(new DeleteStudentCommand(id));

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}
