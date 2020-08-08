using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;

        public GetDepartmentsOverviewQueryHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DepartmentsOverviewVM> Handle(GetDepartmentsOverviewQuery request, CancellationToken cancellationToken)
        {
            var departments = await _context.Departments
                .Include(d => d.Administrator)
                .AsNoTracking()
                .ProjectTo<DepartmentVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new DepartmentsOverviewVM(departments);
        }
    }
}
