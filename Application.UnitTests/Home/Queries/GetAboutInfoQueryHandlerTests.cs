using ContosoUniversityCQRS.Application.Home.Queries.GetAboutInfo;
using ContosoUniversityCQRS.Domain.Entities;
using ContosoUniversityCQRS.Persistence;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Home.Queries
{
    public class GetAboutInfoQueryHandlerTests
    {
        private readonly SchoolContext _context;

        public GetAboutInfoQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new SchoolContext(options);

            _context.Database.EnsureCreated();

            _context.Add(new Student
            {
                FirstMidName = "Student 1",
                LastName = "Lastname",
                EnrollmentDate = new DateTime(2020, 2, 15)
            });

            _context.Add(new Student
            {
                FirstMidName = "Student 2",
                LastName = "Lastname",
                EnrollmentDate = new DateTime(2020, 2, 15)
            });

            _context.Add(new Student
            {
                FirstMidName = "Student 3",
                LastName = "Lastname",
                EnrollmentDate = new DateTime(2020, 2, 20)
            });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAboutInfoTest()
        {
            var sut = new GetAboutInfoQueryHandler(_context);
            var result = await sut.Handle(new GetAboutInfoQuery(), CancellationToken.None);

            result.ShouldBeOfType<AboutInfoVM>();

            result.Items.Count.ShouldBe(2);
        }
    }
}
