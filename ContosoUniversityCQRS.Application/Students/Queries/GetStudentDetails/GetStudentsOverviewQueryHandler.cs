using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Students.Queries.GetStudentDetails
{
    public class GetStudentsOverviewQueryHandler : IRequestHandler<GetStudentDetailsQuery, StudentDetailsVM>
    {
        private readonly ISchoolContext _context;

        public GetStudentsOverviewQueryHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<StudentDetailsVM> Handle(GetStudentDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Student), request.ID);

            var student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == request.ID);

            if (student == null)
                throw new NotFoundException(nameof(Student), request.ID);

            var result = new StudentDetailsVM
            {
                StudentID = student.ID,
                FirstName = student.FirstMidName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate
            };

            foreach(var enrollment in student.Enrollments)
            {
                result.Enrollments.Add(new EnrollmentVM
                {
                    CourseTitle = enrollment.Course.Title,
                    Grade = enrollment.Grade
                });
            }

            return result;
        }
    }
}
