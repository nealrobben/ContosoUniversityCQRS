using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoUniversityCQRS.Application.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommand : IRequest
    {
        public int ID { get; set; }

        public DeleteStudentCommand(int id)
        {
            ID = id;
        }
    }
}
