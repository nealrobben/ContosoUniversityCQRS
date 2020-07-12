using ContosoUniversityCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ContosoUniversityCQRS.Persistence.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasIndex(e => e.InstructorID);

            builder.Property(e => e.DepartmentID).HasColumnName("DepartmentID");

            builder.Property(e => e.Budget).HasColumnType("money");

            builder.Property(e => e.InstructorID).HasColumnName("InstructorID");

            builder.Property(e => e.Name).HasMaxLength(50);

            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            builder.HasOne(d => d.Instructor)
                .WithMany(p => p.Departments)
                .HasForeignKey(d => d.InstructorID);
        }
    }
}
