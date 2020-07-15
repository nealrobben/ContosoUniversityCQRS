﻿using MediatR;

namespace ContosoUniversityCQRS.Application.Departments.Queries.GetDepartmentDetails
{
    public class GetDepartmentDetailsQuery : IRequest<DepartmentDetailVM>
    {
        public int? ID { get; set; }

        public GetDepartmentDetailsQuery(int? id)
        {
            ID = id;
        }
    }
}
