using MediatR;

namespace ContosoUniversityCQRS.Application.Courses.Queries.GetEditCourse
{
    public class GetEditCourseCommand : IRequest<EditCourseVM>
    {
        public int? ID { get; set; }

        public GetEditCourseCommand(int? id)
        {
            ID = id;
        }
    }
}
