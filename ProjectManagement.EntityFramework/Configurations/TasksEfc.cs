using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Entities.Efos;

namespace ProjectManagement.EntityFramework.Configurations
{
    public class TasksEfc : IEntityTypeConfiguration<TasksEfo>
    {
        public void Configure(EntityTypeBuilder<TasksEfo> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(property => new { property.TaskID });
            builder.Property(property => property.TaskID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.Name).IsRequired().HasMaxLength(30).IsUnicode(false);
            builder.Property(property => property.Description).IsRequired().HasMaxLength(150).IsUnicode(false);
            builder.Property(property => property.CreationDate).IsRequired();
            builder.Property(property => property.ExpirationDate).IsRequired();
            builder.Property(property => property.Status).IsRequired();
            builder.Property(property => property.ProjectID).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(property => property.Project)
                .WithMany()
                .HasForeignKey(property => property.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
