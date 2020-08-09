using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;

        public GetInstructorsOverviewQueryHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            List<InstructorVM> instructorVMs = await GetInstructors(cancellationToken);
            List<CourseVM> courseVMs = GetCourses(request, instructors);
            List<EnrollmentVM> enrollments = await GetEnrollments(request, cancellationToken);

            return new InstructorsOverviewVM
            {
                SelectedInstructorID = request.SelectedInstructorID,
                SelectedCourseID = request.SelectedCourseID,
                Instructors = instructorVMs,
                Courses = courseVMs,
                Enrollments = enrollments
            };
        }

        private async Task<List<InstructorVM>> GetInstructors(CancellationToken cancellationToken)
        {
            return await _context.Instructors
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
                  .ProjectTo<InstructorVM>(_mapper.ConfigurationProvider)
                  .ToListAsync(cancellationToken);
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

        private async Task<List<EnrollmentVM>> GetEnrollments(GetInstructorsOverviewQuery request, CancellationToken cancellationToken)
        {
            if (request.SelectedCourseID != null)
            {
                return await _context.Enrollments
                    .AsNoTracking()
                    .Include(x => x.Student)
                    .Where(x => x.CourseID == request.SelectedCourseID.Value)
                    .ProjectTo<EnrollmentVM>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }

            return null;
        }
    }
}
