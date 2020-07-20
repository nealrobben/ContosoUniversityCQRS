using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Departments.Queries.DeleteConfirmation
{
    public class GetDeleteDepartmentConfirmationCommandHandler : IRequestHandler<GetDeleteDepartmentConfirmationCommand, DeleteDepartmentVM>
    {
        private readonly ISchoolContext _context;

        public GetDeleteDepartmentConfirmationCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<DeleteDepartmentVM> Handle(GetDeleteDepartmentConfirmationCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Department), request.ID);

            var department = await _context.Departments
                .Include(d => d.Administrator).AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartmentID == request.ID, cancellationToken);

            if (department == null)
                throw new NotFoundException(nameof(Department), request.ID);

            return new DeleteDepartmentVM
            {
                DepartmentID = department.DepartmentID,
                Name = department.Name,
                Budget = department.Budget,
                StartDate = department.StartDate,
                RowVersion = department.RowVersion,
                AdministratorName = department.Administrator.FullName
            };
        }
    }
}
