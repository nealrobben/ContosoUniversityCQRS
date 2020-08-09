using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversityCQRS.WebUI.Models;
using System;
using ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorDetails;
using ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsOverview;
using ContosoUniversityCQRS.Application.Instructors.Commands.DeleteInstructor;
using ContosoUniversityCQRS.Application.Instructors.Queries.DeleteConfirmation;
using ContosoUniversityCQRS.Application.Instructors.Queries.GetCreateInstructor;
using ContosoUniversityCQRS.Application.Instructors.Commands.CreateInstructor;
using ContosoUniversityCQRS.Application.Instructors.Queries.GetUpdateInstructor;
using ContosoUniversityCQRS.Application.Courses.Queries.GetCourseList;
using ContosoUniversityCQRS.Application.CourseAssignment;
using ContosoUniversityCQRS.Application.Instructors.Commands.UpdateInstructor;
using ContosoUniversityCQRS.Domain.Entities;
using ContosoUniversityCQRS.Application.CourseAssignment.Commands;

namespace ContosoUniversityCQRS.WebUI.Controllers
{
    public class InstructorsController : BaseController
    {
        public InstructorsController()
        {
        }

        public async Task<IActionResult> Index(int? id, int? courseID)
        {
            var result = await Mediator.Send(new GetInstructorsOverviewQuery(id,courseID));
            return View(result);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var result = await Mediator.Send(new GetInstructorDetailsQuery(id));
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,HireDate,LastName")] CreateInstructorCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(new CreateInstructorVM
                {
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    HireDate = command.HireDate
                });
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var result = await Mediator.Send(new GetUpdateInstructorCommand(id));

            await PopulateAssignedCourseData(result.InstructorID);

            return View(result);
        }

        private async Task PopulateAssignedCourseData(int instructorID)
        {
            ViewData["Courses"] = await Mediator.Send(new GetCourseListCommand(instructorID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateInstructorCommand command, string[] selectedCourses)
        {
            try
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");

                await PopulateAssignedCourseData(command.InstructorID.Value);
                return View(new UpdateInstructorVM 
                {
                    InstructorID = command.InstructorID.Value,
                    LastName = command.LastName,
                    FirstName = command.FirstName,
                    HireDate = command.HireDate,
                    OfficeLocation = command.OfficeLocation
                });
            }
        }

        private async Task UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
        {
            await Mediator.Send(new UpdateCourseAssignmentsCommand(instructorToUpdate.ID, selectedCourses));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var result = await Mediator.Send(new GetDeleteInstructorConfirmationCommand(id));
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Mediator.Send(new DeleteInstructorCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
