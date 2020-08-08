using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;

        public GetInstructorLookupCommandHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<InstructorLookupVM>> Handle(GetInstructorLookupCommand request, CancellationToken cancellationToken)
        {
            return await _context.Instructors
                .AsNoTracking()
                .OrderBy(x => x.LastName)
                .ProjectTo<InstructorLookupVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
