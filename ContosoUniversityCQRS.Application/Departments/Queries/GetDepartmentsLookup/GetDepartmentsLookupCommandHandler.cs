using ContosoUniversityCQRS.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversityCQRS.Application.Departments.Queries.GetDepartmentsLookup
{
    public class GetDepartmentsLookupCommandHandler : IRequestHandler<GetDepartmentsLookupCommand, List<DepartmentLookupVM>>
    {
        private readonly ISchoolContext _context;

        public GetDepartmentsLookupCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<List<DepartmentLookupVM>> Handle(GetDepartmentsLookupCommand request, CancellationToken cancellationToken)
        {
            return await _context.Departments
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new DepartmentLookupVM
                {
                    DepartmentID = x.DepartmentID,
                    Name = x.Name
                })
                .ToListAsync();
        }
    }
}
