using ContosoUniversityCQRS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversityCQRS.Application.Students.Queries.GetStudentsOverview
{
    public class GetStudentsOverviewQueryHandler : IRequestHandler<GetStudentsOverviewQuery, StudentsOverviewVM>
    {
        private readonly ISchoolContext _context;
        private const int _pageSize = 3;

        public GetStudentsOverviewQueryHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<StudentsOverviewVM> Handle(GetStudentsOverviewQuery request, CancellationToken cancellationToken)
        {
            var result = new StudentsOverviewVM(null); //TODO: pass correct list

            result.CurrentSort = request.SortOrder;
            result.NameSortParm = String.IsNullOrEmpty(request.SortOrder) ? "name_desc" : "";
            result.DateSortParm = request.SortOrder == "Date" ? "date_desc" : "Date";

            if (request.SearchString != null)
            {
                result.PageNumber = 1;
            }
            else
            {
                result.CurrentFilter = request.SearchString;
            }

            var students = from s in _context.Students
                           select s;
            if (!string.IsNullOrEmpty(request.SearchString))
            {
                students = students.Where(s => s.LastName.Contains(request.SearchString)
                                       || s.FirstMidName.Contains(request.SearchString));
            }

            switch (result.CurrentSort)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            result.TotalPages = await students.CountAsync();
            var items = await students.AsNoTracking().Skip((result.PageNumber - 1) * _pageSize)
                .Take(_pageSize).ToListAsync();

            foreach(var student in items)
            {
                result.Students.Add(new StudentVM
                {
                    FirstName = student.FirstMidName,
                    LastName = student.LastName,
                    EnrollmentDate = student.EnrollmentDate
                });
            }

            throw new NotImplementedException();
        }
    }
}
