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

        public GetUpdateDepartmentCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<UpdateDepartmentVM> Handle(GetUpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Department), request.ID);

            var department = await _context.Departments
                .Include(i => i.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartmentID == request.ID);

            if(department == null)
                throw new NotFoundException(nameof(Department), request.ID);

            return new UpdateDepartmentVM
            {
                 DepartmentID = department.DepartmentID,
                 Name = department.Name,
                 Budget = department.Budget,
                 StartDate = department.StartDate,
                 RowVersion = department.RowVersion,
                 InstructorID = department.Administrator.ID
            };
        }
    }
}
