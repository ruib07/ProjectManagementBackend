using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Entities.Efos;

namespace ProjectManagement.EntityFramework.Configurations
{
    public class RegisterUserEfc : IEntityTypeConfiguration<RegisterUserEfo>
    {
        public void Configure(EntityTypeBuilder<RegisterUserEfo> builder)
        {
            builder.ToTable("RegisterUsers");
            builder.HasKey(property => new { property.RegisterUserId });
            builder.Property(property => property.RegisterUserId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(property => property.UserName).IsRequired().HasMaxLength(25).IsUnicode(false);
            builder.Property(property => property.Password).IsRequired().HasMaxLength(25).IsUnicode(false);
        }
    }
}
