using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.DeleteConfirmation
{
    public class GetDeleteInstructorConfirmationCommandHandler : IRequestHandler<GetDeleteInstructorConfirmationCommand, DeleteInstructorVM>
    {
        private readonly ISchoolContext _context;
        private readonly IMapper _mapper;

        public GetDeleteInstructorConfirmationCommandHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DeleteInstructorVM> Handle(GetDeleteInstructorConfirmationCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Instructor), request.ID);

            var instructor = await _context.Instructors
                .AsNoTracking()
                .ProjectTo<DeleteInstructorVM>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.InstructorID == request.ID, cancellationToken);

            if (instructor == null)
                throw new NotFoundException(nameof(Instructor), request.ID);

            return instructor;
        }
    }
}
