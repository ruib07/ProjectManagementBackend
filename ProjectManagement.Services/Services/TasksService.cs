using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities.Efos;
using ProjectManagement.EntityFramework;

namespace ProjectManagement.Services.Services
{
    public interface ITasksService
    {
        Task<List<TasksEfo>> GetAllTasksAsync();
        Task<TasksEfo> GetTaskByIdAsync(int taskId);
        Task<TasksEfo> GetTaskByNameAsync(string taskName);
        Task<TasksEfo> SendTaskAsync(TasksEfo task);
        Task<TasksEfo> UpdateTaskAsync(int taskId, TasksEfo updateTask);
        Task DeleteTaskAsync(int taskId);
    }

    public class TasksService : ITasksService
    {
        private readonly PManagementDbContext _context;

        public TasksService(PManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<TasksEfo>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TasksEfo> GetTaskByIdAsync(int taskId)
        {
            TasksEfo? task = await _context.Tasks.AsNoTracking()
                .FirstOrDefaultAsync(t => t.TaskID == taskId);

            if (task == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return task;
        }

        public async Task<TasksEfo> GetTaskByNameAsync(string taskName)
        {
            TasksEfo? task = await _context.Tasks.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Name == taskName);

            if (task == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return task;
        }

        public async Task<TasksEfo> SendTaskAsync(TasksEfo task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<TasksEfo> UpdateTaskAsync(int taskId, TasksEfo updateTask)
        {
            try
            {
                TasksEfo? task = await _context.Tasks.FindAsync(taskId);

                if (task == null)
                {
                    throw new Exception("Entity doesn´t exist in the database");
                }

                task.Name = updateTask.Name;
                task.Description = updateTask.Description;
                task.CreationDate = updateTask.CreationDate;
                task.ExpirationDate = updateTask.ExpirationDate;
                task.Status = updateTask.Status;

                await _context.SaveChangesAsync();

                return task;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating task: {ex.Message}");
            }
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            TasksEfo? task = await _context.Tasks.FirstOrDefaultAsync(
                t => t.TaskID == taskId );

            if (task == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
