using MediatR;

namespace ContosoUniversityCQRS.Application.Departments.Queries.GetDepartmentsOverview
{
    public class GetDepartmentsOverviewQuery : IRequest<DepartmentsOverviewVM>
    {
    }
}
