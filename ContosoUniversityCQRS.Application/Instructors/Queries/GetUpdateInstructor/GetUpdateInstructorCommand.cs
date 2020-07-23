using MediatR;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetUpdateInstructor
{
    public class GetUpdateInstructorCommand : IRequest<UpdateInstructorVM>
    {
        public int? ID { get; set; }

        public GetUpdateInstructorCommand(int? id)
        {
            ID = id;
        }
    }
}
