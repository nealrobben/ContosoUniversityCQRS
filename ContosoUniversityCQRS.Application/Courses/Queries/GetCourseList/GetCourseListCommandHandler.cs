using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Courses.Queries.GetCourseList
{
    public class GetCourseListCommandHandler : IRequestHandler<GetCourseListCommand, List<CourseSelectionVM>>
    {
        private readonly ISchoolContext _context;
        private readonly IMapper _mapper;

        public GetCourseListCommandHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CourseSelectionVM>> Handle(GetCourseListCommand request, CancellationToken cancellationToken)
        {
            var courses = await _context.Courses
                .AsNoTracking()
                .ProjectTo<CourseSelectionVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var courseIDsForInstructor = await _context.CourseAssignments
                .Where(x => x.InstructorID == request.InstructorID)
                .Select(x => x.CourseID).ToListAsync();

            foreach(var course in courses)
            {
                course.Assigned = courseIDsForInstructor.Contains(course.CourseID);
            }

            return courses;
        }
    }
}
