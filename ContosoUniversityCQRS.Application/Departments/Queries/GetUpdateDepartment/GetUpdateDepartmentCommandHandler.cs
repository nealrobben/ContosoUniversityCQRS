using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Departments.Queries.GetUpdateDepartment
{
    public class GetUpdateDepartmentCommandHandler : IRequestHandler<GetUpdateDepartmentCommand, UpdateDepartmentVM>
    {
        private readonly ISchoolContext _context;
        private readonly IMapper _mapper;

        public GetUpdateDepartmentCommandHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateDepartmentVM> Handle(GetUpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Department), request.ID);

            var department = await _context.Departments
                .Include(i => i.Administrator)
                .AsNoTracking()
                .ProjectTo<UpdateDepartmentVM>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.DepartmentID == request.ID, cancellationToken);

            if(department == null)
                throw new NotFoundException(nameof(Department), request.ID);

            return department;
        }
    }
}
