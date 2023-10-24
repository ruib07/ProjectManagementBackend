using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Entities.Efos;

namespace ProjectManagement.EntityFramework.Configurations
{
    public class ProjectMembersEfc : IEntityTypeConfiguration<ProjectMembersEfo>
    {
        public void Configure(EntityTypeBuilder<ProjectMembersEfo> builder)
        {
            builder.ToTable("ProjectMembers");
            builder.HasKey(property => new { property.ProjectMembersID });
            builder.Property(property => property.ProjectMembersID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.ProjectID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.MemberID).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(property => property.Project)
                .WithMany()
                .HasForeignKey(property => property.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(property => property.Member)
                .WithMany()
                .HasForeignKey(property => property.MemberID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
