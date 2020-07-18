using MediatR;

namespace ContosoUniversityCQRS.Application.Students.Queries.DeleteConfirmation
{
    public class GetDeleteConfirmationCommand : IRequest<DeleteStudentVM>
    {
        public int? ID { get; set; }

        public GetDeleteConfirmationCommand(int? id)
        {
            ID = id;
        }
    }
}
