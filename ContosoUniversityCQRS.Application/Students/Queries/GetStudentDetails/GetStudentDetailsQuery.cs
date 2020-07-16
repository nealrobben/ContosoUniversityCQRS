using MediatR;

namespace ContosoUniversityCQRS.Application.Students.Queries.GetStudentDetails
{
    public class GetStudentDetailsQuery : IRequest<StudentDetailsVM>
    {
        public int? ID { get; set; }

        public GetStudentDetailsQuery(int? id)
        {
            ID = id;
        }
    }
}
