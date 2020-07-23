using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversityCQRS.WebUI.Data;
using ContosoUniversityCQRS.WebUI.Models;
using ContosoUniversityCQRS.WebUI.Models.SchoolViewModels;
using System.Collections.Generic;
using System;
using ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorDetails;
using ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsOverview;
using ContosoUniversityCQRS.Application.Instructors.Commands.DeleteInstructor;
using ContosoUniversityCQRS.Application.Instructors.Queries.DeleteConfirmation;
using ContosoUniversityCQRS.Application.Instructors.Queries.GetCreateInstructor;
using ContosoUniversityCQRS.Application.Instructors.Commands.CreateInstructor;
using ContosoUniversityCQRS.Application.Instructors.Queries.GetUpdateInstructor;
using ContosoUniversityCQRS.Application.Courses.Queries.GetCourseList;

namespace ContosoUniversityCQRS.WebUI.Controllers
{
    public class InstructorsController : BaseController
    {
        public InstructorsController(SchoolContext context) : base(context)
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
        public async Task<IActionResult> Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructorToUpdate = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Instructor>(
                instructorToUpdate,
                "",
                i => i.FirstMidName, i => i.LastName, i => i.HireDate, i => i.OfficeAssignment))
            {
                if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment?.Location))
                {
                    instructorToUpdate.OfficeAssignment = null;
                }
                UpdateInstructorCourses(selectedCourses, instructorToUpdate);
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
            UpdateInstructorCourses(selectedCourses, instructorToUpdate);
            await PopulateAssignedCourseData(instructorToUpdate.ID);
            return View(instructorToUpdate);
        }

        private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.CourseAssignments = new List<CourseAssignment>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.CourseAssignments.Select(c => c.Course.CourseID));
            foreach (var course in _context.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.CourseAssignments.Add(new CourseAssignment { InstructorID = instructorToUpdate.ID, CourseID = course.CourseID });
                    }
                }
                else
                {

                    if (instructorCourses.Contains(course.CourseID))
                    {
                        CourseAssignment courseToRemove = instructorToUpdate.CourseAssignments.FirstOrDefault(i => i.CourseID == course.CourseID);
                        _context.Remove(courseToRemove);
                    }
                }
            }
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
