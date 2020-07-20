using MediatR;

namespace ContosoUniversityCQRS.Application.Courses.Queries.DeleteConfirmation
{
    public class GetDeleteCourseConfirmationCommand : IRequest<DeleteCourseVM>
    {
        public int? ID { get; set; }

        public GetDeleteCourseConfirmationCommand(int? id)
        {
            ID = id;
        }
    }
}
