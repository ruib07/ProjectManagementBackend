using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities.Efos;
using ProjectManagement.EntityFramework.Configurations;

namespace ProjectManagement.EntityFramework
{
    public class PManagementDbContext : DbContext
    {
        public PManagementDbContext(DbContextOptions<PManagementDbContext> options) : base(options) { }

        public DbSet<RegisterUserEfo> RegisterUsers { get; set; }
        public DbSet<UserEfo> Users { get; set; }
        public DbSet<ProjectsEfo> Projects { get; set; }
        public DbSet<ProjectMembersEfo> ProjectMembers { get; set; }
        public DbSet<TasksEfo> Tasks { get; set; }
        public DbSet<CommentsEfo> Comments { get; set; }
        public DbSet<StageEfo> Stages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegisterUserEfc());
            modelBuilder.ApplyConfiguration(new UserEfc());
            modelBuilder.ApplyConfiguration(new ProjectsEfc());
            modelBuilder.ApplyConfiguration(new ProjectMembersEfc());
            modelBuilder.ApplyConfiguration(new TasksEfc());
            modelBuilder.ApplyConfiguration(new CommentsEfc());
            modelBuilder.ApplyConfiguration(new StageEfc());
        }
    }
}
