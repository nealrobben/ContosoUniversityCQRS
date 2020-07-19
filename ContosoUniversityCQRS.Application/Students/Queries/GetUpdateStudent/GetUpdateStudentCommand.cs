using MediatR;

namespace ContosoUniversityCQRS.Application.Students.Queries.GetUpdateStudent
{
    public class GetUpdateStudentCommand : IRequest<UpdateStudentVM>
    {
        public int? ID { get; set; }

        public GetUpdateStudentCommand(int? id)
        {
            ID = id;
        }
    }
}
