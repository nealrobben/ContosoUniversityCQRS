using MediatR;

namespace ContosoUniversityCQRS.Application.Departments.Queries.DeleteConfirmation
{
    public class GetDeleteDepartmentConfirmationCommand : IRequest<DeleteDepartmentVM>
    {
        public int? ID { get; set; }

        public GetDeleteDepartmentConfirmationCommand(int? id)
        {
            ID = id;
        }
    }
}
