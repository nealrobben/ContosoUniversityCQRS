using MediatR;

namespace ContosoUniversityCQRS.Application.Home.Queries.GetAboutInfo
{
    public class GetAboutInfoQuery : IRequest<AboutInfoVM>
    {
    }
}
