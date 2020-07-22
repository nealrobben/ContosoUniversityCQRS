using MediatR;
using System.Collections.Generic;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsLookup
{
    public class GetInstructorLookupCommand : IRequest<List<InstructorLookupVM>>
    {
    }
}
