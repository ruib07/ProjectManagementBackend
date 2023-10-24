using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Entities.Efos;

namespace ProjectManagement.EntityFramework.Configurations
{
    public class CommentsEfc : IEntityTypeConfiguration<CommentsEfo>
    {
        public void Configure(EntityTypeBuilder<CommentsEfo> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(property => new { property.CommentID });
            builder.Property(property => property.CommentID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.Comment).IsRequired().HasMaxLength(100).IsUnicode(false);
            builder.Property(property => property.CreationDate).IsRequired();
            builder.Property(property => property.UserID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.TaskID).IsRequired(false).ValueGeneratedOnAdd();
            builder.Property(property => property.ProjectID).IsRequired(false).ValueGeneratedOnAdd();

            builder.HasOne(property => property.User)
                .WithMany()
                .HasForeignKey(property => property.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(property => property.Task)
                .WithMany()
                .HasForeignKey(property => property.TaskID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(property => property.Project)
                .WithMany()
                .HasForeignKey(property => property.ProjectID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
