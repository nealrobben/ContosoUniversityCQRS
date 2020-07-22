using ContosoUniversityCQRS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsLookup
{
    public class GetInstructorLookupCommandHandler : IRequestHandler<GetInstructorLookupCommand, List<InstructorLookupVM>>
    {
        private readonly ISchoolContext _context;

        public GetInstructorLookupCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<List<InstructorLookupVM>> Handle(GetInstructorLookupCommand request, CancellationToken cancellationToken)
        {
            return await _context.Instructors
                .AsNoTracking()
                .OrderBy(x => x.LastName)
                .Select(x => new InstructorLookupVM
                {
                    ID = x.ID,
                    FullName = x.FullName
                })
                .ToListAsync();
        }
    }
}
