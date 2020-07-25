using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversityCQRS.Application.Departments.Queries.GetDepartmentsOverview;
using ContosoUniversityCQRS.Application.Departments.Queries.GetDepartmentDetails;
using ContosoUniversityCQRS.Application.Departments.Commands.DeleteDepartment;
using ContosoUniversityCQRS.Application.Departments.Queries.DeleteConfirmation;
using ContosoUniversityCQRS.Application.Departments.Commands.CreateDepartment;
using ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsLookup;
using ContosoUniversityCQRS.Application.Departments.Queries.GetUpdateDepartment;
using ContosoUniversityCQRS.Application.Departments.Commands.UpdateDepartment;

namespace ContosoUniversityCQRS.WebUI.Controllers
{
    public class DepartmentsController : BaseController
    {
        public DepartmentsController()
        {
        }

        public async Task<IActionResult> Index()
        {
            var result = await Mediator.Send(new GetDepartmentsOverviewQuery());
            return View(result);
        }

        public async Task<IActionResult> Details(int? id)
        {
            {
                var result = await Mediator.Send(new GetDepartmentDetailsQuery(id));
                return View(result);
            }
        }

        public async Task<IActionResult> Create()
        {
            var result = await Mediator.Send(new GetInstructorLookupCommand());

            ViewData["InstructorID"] = new SelectList(result, "ID", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentID,Name,Budget,StartDate,InstructorID")] CreateDepartmentCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception)
            {
                var result = await Mediator.Send(new GetInstructorLookupCommand());
                ViewData["InstructorID"] = new SelectList(result, "ID", "FullName", command.InstructorID);

                return View(new Domain.Entities.Department
                {
                    Name = command.Name,
                    Budget = command.Budget,
                    StartDate = command.StartDate,
                    InstructorID = command.InstructorID
                });
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var result = await Mediator.Send(new GetUpdateDepartmentCommand(id));

            var instructors = await Mediator.Send(new GetInstructorLookupCommand());
            ViewData["InstructorID"] = new SelectList(instructors, "ID", "FullName", result.InstructorID);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateDepartmentCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var vm = new UpdateDepartmentVM
                {
                    DepartmentID = command.DepartmentID.Value,
                    Name = command.Name,
                    Budget = command.Budget,
                    StartDate = command.StartDate,
                    InstructorID = command.InstructorID,
                    RowVersion = command.RowVersion
                };

                var exceptionEntry = ex.Entries.Single();
                var clientValues = (Models.Department)exceptionEntry.Entity;
                var databaseEntry = exceptionEntry.GetDatabaseValues();
                if (databaseEntry == null)
                {
                    ModelState.AddModelError(string.Empty,
                        "Unable to save changes. The department was deleted by another user.");
                }
                else
                {
                    var databaseValues = (Models.Department)databaseEntry.ToObject();

                    if (databaseValues.Name != clientValues.Name)
                    {
                        ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}");
                    }
                    if (databaseValues.Budget != clientValues.Budget)
                    {
                        ModelState.AddModelError("Budget", $"Current value: {databaseValues.Budget:c}");
                    }
                    if (databaseValues.StartDate != clientValues.StartDate)
                    {
                        ModelState.AddModelError("StartDate", $"Current value: {databaseValues.StartDate:d}");
                    }
                    if (databaseValues.InstructorID != clientValues.InstructorID)
                    {
                        //Models.Instructor databaseInstructor = await _context.Instructors.FirstOrDefaultAsync(i => i.ID == databaseValues.InstructorID);
                        //ModelState.AddModelError("InstructorID", $"Current value: {databaseInstructor?.FullName}");
                    }

                    ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The "
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again. Otherwise click the Back to List hyperlink.");
                    vm.RowVersion = (byte[])databaseValues.RowVersion;
                    ModelState.Remove("RowVersion");
                }

                var instructorsLookup = await Mediator.Send(new GetInstructorLookupCommand());
                ViewData["InstructorID"] = new SelectList(instructorsLookup, "ID", "FullName", command.InstructorID);

                return View(vm);
            }

            ////-----------------------------
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var departmentToUpdate = await _context.Departments.Include(i => i.Administrator).FirstOrDefaultAsync(m => m.DepartmentID == id);

            //if (departmentToUpdate == null)
            //{
            //    Models.Department deletedDepartment = new Models.Department();
            //    await TryUpdateModelAsync(deletedDepartment);
            //    ModelState.AddModelError(string.Empty,
            //        "Unable to save changes. The department was deleted by another user.");

            //    var instructorsList = await Mediator.Send(new GetInstructorLookupCommand());
            //    ViewData["InstructorID"] = new SelectList(instructorsList, "ID", "FullName", deletedDepartment.InstructorID);
            //    return View(deletedDepartment);
            //}

            //_context.Entry(departmentToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            //if (await TryUpdateModelAsync<Models.Department>(
            //    departmentToUpdate,
            //    "",
            //    s => s.Name, s => s.StartDate, s => s.Budget, s => s.InstructorID))
            //{
            //    try
            //    {
            //        await _context.SaveChangesAsync();
            //        return RedirectToAction(nameof(Index));
            //    }
            //    catch (DbUpdateConcurrencyException ex)
            //    {
            //        var exceptionEntry = ex.Entries.Single();
            //        var clientValues = (Models.Department)exceptionEntry.Entity;
            //        var databaseEntry = exceptionEntry.GetDatabaseValues();
            //        if (databaseEntry == null)
            //        {
            //            ModelState.AddModelError(string.Empty,
            //                "Unable to save changes. The department was deleted by another user.");
            //        }
            //        else
            //        {
            //            var databaseValues = (Models.Department)databaseEntry.ToObject();

            //            if (databaseValues.Name != clientValues.Name)
            //            {
            //                ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}");
            //            }
            //            if (databaseValues.Budget != clientValues.Budget)
            //            {
            //                ModelState.AddModelError("Budget", $"Current value: {databaseValues.Budget:c}");
            //            }
            //            if (databaseValues.StartDate != clientValues.StartDate)
            //            {
            //                ModelState.AddModelError("StartDate", $"Current value: {databaseValues.StartDate:d}");
            //            }
            //            if (databaseValues.InstructorID != clientValues.InstructorID)
            //            {
            //                Models.Instructor databaseInstructor = await _context.Instructors.FirstOrDefaultAsync(i => i.ID == databaseValues.InstructorID);
            //                ModelState.AddModelError("InstructorID", $"Current value: {databaseInstructor?.FullName}");
            //            }

            //            ModelState.AddModelError(string.Empty, "The record you attempted to edit "
            //                    + "was modified by another user after you got the original value. The "
            //                    + "edit operation was canceled and the current values in the database "
            //                    + "have been displayed. If you still want to edit this record, click "
            //                    + "the Save button again. Otherwise click the Back to List hyperlink.");
            //            departmentToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
            //            ModelState.Remove("RowVersion");
            //        }
            //    }
            //}

            //var instructors = await Mediator.Send(new GetInstructorLookupCommand());
            //ViewData["InstructorID"] = new SelectList(instructors, "ID", "FullName", departmentToUpdate.InstructorID);
            //return View(departmentToUpdate);
        }

        public async Task<IActionResult> Delete(int? id, bool? concurrencyError)
        {
            var result = await Mediator.Send(new GetDeleteDepartmentConfirmationCommand(id));

            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ConcurrencyErrorMessage"] = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int DepartmentID)
        {
            try
            {
                await Mediator.Send(new DeleteDepartmentCommand(DepartmentID));
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { concurrencyError = true, id = DepartmentID });
            }
        }
    }
}
