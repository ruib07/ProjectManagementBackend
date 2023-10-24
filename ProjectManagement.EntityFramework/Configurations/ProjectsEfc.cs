using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Entities.Efos;

namespace ProjectManagement.EntityFramework.Configurations
{
    public class ProjectsEfc : IEntityTypeConfiguration<ProjectsEfo>
    {
        public void Configure(EntityTypeBuilder<ProjectsEfo> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(property => new { property.ProjectID });
            builder.Property(property => property.ProjectID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.Name).IsRequired().HasMaxLength(30).IsUnicode(false);
            builder.Property(property => property.Description).IsRequired().HasMaxLength(150).IsUnicode(false);
            builder.Property(property => property.InitiationDate).IsRequired();
            builder.Property(property => property.DeadLine).IsRequired();
            builder.Property(property => property.ManagerID).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(property => property.Manager)
                .WithMany()
                .HasForeignKey(property => property.ManagerID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
