using ContosoUniversityCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoUniversityCQRS.Persistence.Configurations
{
    public class CourseAssignmentConfiguration : IEntityTypeConfiguration<CourseAssignment>
    {
        public void Configure(EntityTypeBuilder<CourseAssignment> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
