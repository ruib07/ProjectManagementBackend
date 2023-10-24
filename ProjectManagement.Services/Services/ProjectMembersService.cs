using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities.Efos;
using ProjectManagement.EntityFramework;

namespace ProjectManagement.Services.Services
{
    public interface IProjectMembersService
    {
        Task<List<ProjectMembersEfo>> GetAllProjectMembersAsync();
        Task<ProjectMembersEfo> GetProjectMembersByIdAsync(int projectMembersId);
        Task DeleteProjectMembersAsync(int projectMembersId);
    }

    public class ProjectMembersService : IProjectMembersService
    {
        private readonly PManagementDbContext _context;

        public ProjectMembersService(PManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectMembersEfo>> GetAllProjectMembersAsync()
        {
            return await _context.ProjectMembers.ToListAsync();
        }

        public async Task<ProjectMembersEfo> GetProjectMembersByIdAsync(int projectMembersId)
        {
            ProjectMembersEfo? projectMembers = await _context.ProjectMembers.AsNoTracking()
                .FirstOrDefaultAsync(pm => pm.ProjectMembersID == projectMembersId);

            if (projectMembers == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return projectMembers;
        }

        public async Task DeleteProjectMembersAsync(int projectMembersId)
        {
            ProjectMembersEfo? projectMembers = await _context.ProjectMembers.FirstOrDefaultAsync(
                pm => pm.ProjectID == projectMembersId);

            if (projectMembers == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            _context.ProjectMembers.Remove(projectMembers);
            await _context.SaveChangesAsync();
        }
    }
}
