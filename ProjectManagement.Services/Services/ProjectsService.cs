using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities.Efos;
using ProjectManagement.EntityFramework;

namespace ProjectManagement.Services.Services
{
    public interface IProjectService
    {
        Task<List<ProjectsEfo>> GetAllProjectsAsync();
        Task<ProjectsEfo> GetProjectByIdAsync(int projectId);
        Task<ProjectsEfo> GetProjectByNameAsync(string projectName);
        Task<ProjectsEfo> SendProjectAsync(ProjectsEfo project);
        Task<ProjectsEfo> UpdateProjectAsync(int projectId, ProjectsEfo updateProject);
        Task DeleteProjectAsync(int projectId);
    }

    public class ProjectsService : IProjectService
    {
        private readonly PManagementDbContext _context;

        public ProjectsService(PManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectsEfo>> GetAllProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<ProjectsEfo> GetProjectByIdAsync(int projectId)
        {
            ProjectsEfo? project = await _context.Projects.AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjectID == projectId);

            if (project == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return project;
        }

        public async Task<ProjectsEfo> GetProjectByNameAsync(string projectName)
        {
            ProjectsEfo? project = await _context.Projects.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name == projectName);

            if (project == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return project;
        }

        public async Task<ProjectsEfo> SendProjectAsync(ProjectsEfo project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<ProjectsEfo> UpdateProjectAsync(int projectId, ProjectsEfo updateProject)
        {
            try
            {
                ProjectsEfo? project = await _context.Projects.FindAsync(projectId);

                if (project == null)
                {
                    throw new Exception("Entity doesn´t exist in the database");
                }

                project.Name = updateProject.Name;
                project.Description = updateProject.Description;
                project.InitiationDate = updateProject.InitiationDate;
                project.DeadLine = updateProject.DeadLine;

                await _context.SaveChangesAsync();

                return project;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating project: {ex.Message}");
            }
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            ProjectsEfo? project = await _context.Projects.FirstOrDefaultAsync(
                p => p.ProjectID == projectId);

            if (project == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
