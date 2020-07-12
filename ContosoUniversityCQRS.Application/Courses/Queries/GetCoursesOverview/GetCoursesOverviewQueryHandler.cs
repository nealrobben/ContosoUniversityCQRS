using ContosoUniversityCQRS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Courses.Queries.GetCoursesOverview
{
    public class GetCoursesOverviewQueryHandler : IRequestHandler<GetCoursesOverviewQuery, CoursesOverviewVM>
    {
        private readonly ISchoolContext _context;

        public GetCoursesOverviewQueryHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<CoursesOverviewVM> Handle(GetCoursesOverviewQuery request, CancellationToken cancellationToken)
        {
            var courses = _context.Courses
                .Include(c => c.Department)
                .AsNoTracking().Select(c => new CourseVM
                {
                    CourseID = c.CourseID,
                    Title = c.Title,
                    Credits = c.Credits,
                    DepartmentName = c.Department.Name
                });

            var result = await courses.ToListAsync();

            return new CoursesOverviewVM(result);
        }
    }
}
