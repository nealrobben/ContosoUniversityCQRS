using MediatR;

namespace ContosoUniversityCQRS.Application.Courses.Queries.GetCoursesOverview
{
    public class GetCoursesOverviewQuery : IRequest<CoursesOverviewVM>
    {
    }
}
