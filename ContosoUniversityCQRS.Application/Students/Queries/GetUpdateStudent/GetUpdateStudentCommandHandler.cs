using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Students.Queries.GetUpdateStudent
{
    public class GetUpdateStudentCommandHandler : IRequestHandler<GetUpdateStudentCommand, UpdateStudentVM>
    {
        private readonly ISchoolContext _context;
        private readonly IMapper _mapper;

        public GetUpdateStudentCommandHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateStudentVM> Handle(GetUpdateStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Student), request.ID);

            var student = await _context.Students
                .AsNoTracking()
                .ProjectTo<UpdateStudentVM>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.StudentID == request.ID, cancellationToken);

            if (student == null)
                throw new NotFoundException(nameof(Student), request.ID);

            return student;
        }
    }
}
