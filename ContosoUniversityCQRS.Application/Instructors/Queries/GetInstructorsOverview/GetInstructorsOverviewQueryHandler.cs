using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsOverview
{
    public class GetInstructorsOverviewQueryHandler : IRequestHandler<GetInstructorsOverviewQuery, InstructorsOverviewVM>
    {
        private readonly ISchoolContext _context;

        public GetInstructorsOverviewQueryHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<InstructorsOverviewVM> Handle(GetInstructorsOverviewQuery request, CancellationToken cancellationToken)
        {
            var instructors = await _context.Instructors
                  .Include(i => i.OfficeAssignment)
                  .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Enrollments)
                            .ThenInclude(i => i.Student)
                  .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Department)
                  .AsNoTracking()
                  .OrderBy(i => i.LastName)
                  .ToListAsync();

            List<InstructorVM> instructorVMs = GetInstructors(instructors);
            List<CourseVM> courseVMs = GetCourses(request, instructors);
            List<EnrollmentVM> enrollments = await GetEnrollments(request);

            return new InstructorsOverviewVM
            {
                SelectedInstructorID = request.SelectedInstructorID,
                SelectedCourseID = request.SelectedCourseID,
                Instructors = instructorVMs,
                Courses = courseVMs,
                Enrollments = enrollments
            };
        }

        private static List<InstructorVM> GetInstructors(List<Instructor> instructors)
        {
            return instructors.Select(c => new InstructorVM
            {
                InstructorID = c.ID,
                FirstName = c.FirstMidName,
                LastName = c.LastName,
                HireDate = c.HireDate,
                OfficeLocation = c.OfficeAssignment?.Location,
                CourseAssignments = c.CourseAssignments
                    .Select(x => new CourseAssignmentVM 
                        {
                            CourseID = x.CourseID,
                            CourseTitle = x.Course.Title
                        }).ToList()
            }).ToList();
        }

        private static List<CourseVM> GetCourses(GetInstructorsOverviewQuery request, List<Instructor> instructors)
        {
            List<CourseVM> courseVMs = null;

            if (request.SelectedInstructorID != null)
            {
                Instructor instructor = instructors.Where(
                    i => i.ID == request.SelectedInstructorID.Value).Single();
                courseVMs = instructor.CourseAssignments.Select(c => new CourseVM
                {
                    CourseID = c.CourseID,
                    Title = c.Course.Title,
                    DepartmentName = c.Course.Department.Name
                }).ToList();
            }

            return courseVMs;
        }

        private async Task<List<EnrollmentVM>> GetEnrollments(GetInstructorsOverviewQuery request)
        {
            List<EnrollmentVM> enrollments = null;

            if (request.SelectedCourseID != null)
            {
                enrollments = await _context.Enrollments
                    .Include(x => x.Student)
                    .Where(x => x.CourseID == request.SelectedCourseID.Value)
                    .Select(x => new EnrollmentVM
                    {
                        StudentName = x.Student.FullName,
                        StudentGrade = x.Grade
                    })
                    .ToListAsync();
            }

            return enrollments;
        }
    }
}
