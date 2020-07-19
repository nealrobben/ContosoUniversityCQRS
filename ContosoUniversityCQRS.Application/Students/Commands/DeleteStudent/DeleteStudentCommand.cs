using MediatR;

namespace ContosoUniversityCQRS.Application.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommand : IRequest
    {
        public int ID { get; set; }

        public DeleteStudentCommand(int id)
        {
            ID = id;
        }
    }
}
