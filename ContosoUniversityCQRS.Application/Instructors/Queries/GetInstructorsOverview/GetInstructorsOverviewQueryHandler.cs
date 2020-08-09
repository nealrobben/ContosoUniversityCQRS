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
            List<InstructorVM> instructorVMs = await GetInstructors(cancellationToken);
            List<CourseVM> courseVMs = await GetCourses(request, cancellationToken);
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

        private async Task<List<CourseVM>> GetCourses(GetInstructorsOverviewQuery request, CancellationToken cancellationToken)
        {
            if (request.SelectedInstructorID != null)
            {
                return await _context.CourseAssignments
                    .Where(x => x.InstructorID == request.SelectedInstructorID.Value)
                    .AsNoTracking()
                    .ProjectTo<CourseVM>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }

            return null;
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
