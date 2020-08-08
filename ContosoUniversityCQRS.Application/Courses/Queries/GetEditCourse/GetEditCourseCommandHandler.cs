using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Courses.Queries.GetEditCourse
{
    public class GetEditCourseCommandHandler : IRequestHandler<GetEditCourseCommand, EditCourseVM>
    {
        private readonly ISchoolContext _context;
        private readonly IMapper _mapper;

        public GetEditCourseCommandHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EditCourseVM> Handle(GetEditCourseCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Course), request.ID);

            var course = await _context.Courses
                .AsNoTracking()
                .ProjectTo<EditCourseVM>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.CourseID == request.ID, cancellationToken);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.ID);

            return course;
        }
    }
}
