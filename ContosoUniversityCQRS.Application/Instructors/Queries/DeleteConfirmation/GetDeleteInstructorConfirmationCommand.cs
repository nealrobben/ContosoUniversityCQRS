using MediatR;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.DeleteConfirmation
{
    public class GetDeleteInstructorConfirmationCommand : IRequest<DeleteInstructorVM>
    {
        public int? ID { get; set; }

        public GetDeleteInstructorConfirmationCommand(int? id)
        {
            ID = id;
        }
    }
}
