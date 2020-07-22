using MediatR;
using System.Collections.Generic;

namespace ContosoUniversityCQRS.Application.Departments.Queries.GetDepartmentsLookup
{
    public class GetDepartmentsLookupCommand : IRequest<List<DepartmentLookupVM>>
    {
    }
}
