using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.CourseAssignment
{
    public class UpdateCourseAssignmentsCommandHandler : IRequestHandler<UpdateCourseAssignmentsCommand>
    {
        private readonly ISchoolContext _context;

        public UpdateCourseAssignmentsCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCourseAssignmentsCommand request, CancellationToken cancellationToken)
        {
            if (request.InstructorID == null)
                throw new NotFoundException(nameof(Instructor), request.InstructorID);

            var instructor = await _context.Instructors.Include(x => x.CourseAssignments).Where(x => x.ID == request.InstructorID).FirstOrDefaultAsync();

            if(instructor == null)
                throw new NotFoundException(nameof(Instructor), request.InstructorID);

            if (request.SelectedCourses == null)
            {
                instructor.CourseAssignments = new List<Domain.Entities.CourseAssignment>();
            }

            var selectedCoursesHS = new HashSet<string>(request.SelectedCourses);
            var instructorCourses = new HashSet<int>
                (instructor.CourseAssignments.Select(c => c.CourseID));

            foreach (var course in _context.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructor.CourseAssignments.Add(new Domain.Entities.CourseAssignment { InstructorID = instructor.ID, CourseID = course.CourseID });
                    }
                }
                else
                {

                    if (instructorCourses.Contains(course.CourseID))
                    {
                        Domain.Entities.CourseAssignment courseToRemove = instructor.CourseAssignments.FirstOrDefault(i => i.CourseID == course.CourseID);
                        _context.CourseAssignments.Remove(courseToRemove);
                    }
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
