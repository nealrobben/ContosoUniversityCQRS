using ContosoUniversityCQRS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Departments.Queries.GetDepartmentsOverview
{
    public class GetDepartmentsOverviewQueryHandler : IRequestHandler<GetDepartmentsOverviewQuery, DepartmentsOverviewVM>
    {
        private readonly ISchoolContext _context;

        public GetDepartmentsOverviewQueryHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<DepartmentsOverviewVM> Handle(GetDepartmentsOverviewQuery request, CancellationToken cancellationToken)
        {
            var departments = _context.Departments
                .Include(d => d.Administrator)
                .AsNoTracking().Select(c => new DepartmentVM
                {
                    DepartmentID = c.DepartmentID,
                    Name = c.Name,
                    Budget = c.Budget,
                    StartDate = c.StartDate,
                    AdministratorName = c.Administrator != null ? c.Administrator.FullName : string.Empty
                });

            var result = await departments.ToListAsync();

            return new DepartmentsOverviewVM(result);
        }
    }
}
