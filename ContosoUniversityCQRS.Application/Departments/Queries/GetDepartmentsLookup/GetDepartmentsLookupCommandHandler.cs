using ContosoUniversityCQRS.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ContosoUniversityCQRS.Application.Departments.Queries.GetDepartmentsLookup
{
    public class GetDepartmentsLookupCommandHandler : IRequestHandler<GetDepartmentsLookupCommand, List<DepartmentLookupVM>>
    {
        private readonly ISchoolContext _context;
        private readonly IMapper _mapper;

        public GetDepartmentsLookupCommandHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DepartmentLookupVM>> Handle(GetDepartmentsLookupCommand request, CancellationToken cancellationToken)
        {
            return await _context.Departments
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ProjectTo<DepartmentLookupVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
