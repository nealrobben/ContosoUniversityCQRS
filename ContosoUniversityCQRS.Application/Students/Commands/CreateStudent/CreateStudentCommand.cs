using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoUniversityCQRS.Application.Students.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
