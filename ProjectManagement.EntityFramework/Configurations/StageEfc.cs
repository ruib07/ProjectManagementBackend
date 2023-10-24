using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Entities.Efos;

namespace ProjectManagement.EntityFramework.Configurations
{
    public class StageEfc : IEntityTypeConfiguration<StageEfo>
    {
        public void Configure(EntityTypeBuilder<StageEfo> builder)
        {
            builder.ToTable("Stages");
            builder.HasKey(property => new { property.StageID });
            builder.Property(property => property.StageID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.Name).IsRequired().HasMaxLength(30).IsUnicode(false);
            builder.Property(property => property.ConclusionDate).IsRequired();
            builder.Property(property => property.ProjectID).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(property => property.Project)
                .WithMany()
                .HasForeignKey(property => property.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
