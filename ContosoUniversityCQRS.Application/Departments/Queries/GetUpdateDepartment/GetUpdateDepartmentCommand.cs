using MediatR;

namespace ContosoUniversityCQRS.Application.Departments.Queries.GetUpdateDepartment
{
    public class GetUpdateDepartmentCommand : IRequest<UpdateDepartmentVM>
    {
        public int? ID { get; set; }

        public GetUpdateDepartmentCommand(int?  id)
        {
            ID = id;
        }
    }
}
