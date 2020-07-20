﻿using ContosoUniversityCQRS.Application.Common.Exceptions;
using ContosoUniversityCQRS.Application.Common.Interfaces;
using ContosoUniversityCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.Courses.Queries.DeleteConfirmation
{
    public class GetDeleteCourseConfirmationCommandHandler : IRequestHandler<GetDeleteCourseConfirmationCommand, DeleteCourseVM>
    {
        private readonly ISchoolContext _context;

        public GetDeleteCourseConfirmationCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<DeleteCourseVM> Handle(GetDeleteCourseConfirmationCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
                throw new NotFoundException(nameof(Course), request.ID);

            var course = await _context.Courses
                .Include(c => c.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CourseID == request.ID);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.ID);

            return new DeleteCourseVM
            {
                CourseID = course.CourseID,
                Title = course.Title,
                Credits = course.Credits,
                DepartmentName = course.Department.Name
            };
        }
    }
}
