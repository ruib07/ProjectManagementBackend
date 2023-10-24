using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities.Efos;
using ProjectManagement.EntityFramework;

namespace ProjectManagement.Services.Services
{
    public interface IStagesService
    {
        Task<List<StageEfo>> GetAllStagesAsync();
        Task<StageEfo> GetStageByIdAsync(int stageId);
        Task<StageEfo> GetStageByNameAsync(string stageName);
        Task<StageEfo> SendStageAsync(StageEfo stage);
        Task<StageEfo> UpdateStageAsync(int stageId, StageEfo updateStage);
        Task DeleteStageAsync(int stageId);
    }

    public class StagesService : IStagesService
    {
        private readonly PManagementDbContext _context;

        public StagesService(PManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<StageEfo>> GetAllStagesAsync()
        {
            return await _context.Stages.ToListAsync();
        }

        public async Task<StageEfo> GetStageByIdAsync(int stageId)
        {
            StageEfo? stage = await _context.Stages.AsNoTracking()
                .FirstOrDefaultAsync(s => s.StageID == stageId);

            if (stage == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return stage;
        }

        public async Task<StageEfo> GetStageByNameAsync(string stageName)
        {
            StageEfo? stage = await _context.Stages.AsNoTracking()
                .FirstOrDefaultAsync(s => s.Name == stageName);

            if (stage == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return stage;
        }

        public async Task<StageEfo> SendStageAsync(StageEfo stage)
        {
            await _context.Stages.AddAsync(stage);
            await _context.SaveChangesAsync();

            return stage;
        }

        public async Task<StageEfo> UpdateStageAsync(int stageId, StageEfo updateStage)
        {
            try
            {
                StageEfo? stage = await _context.Stages.FindAsync(stageId);

                if (stage == null)
                {
                    throw new Exception("Entity doesn´t exist in the database");
                }

                stage.Name = updateStage.Name;
                stage.ConclusionDate = updateStage.ConclusionDate;

                await _context.SaveChangesAsync();

                return stage;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating stage: {ex.Message}");
            }
        }

        public async Task DeleteStageAsync(int stageId)
        {
            StageEfo? stage = await _context.Stages.FirstOrDefaultAsync(
                s => s.StageID == stageId);

            if (stage == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            _context.Stages.Remove(stage);
            await _context.SaveChangesAsync();
        }
    }
}
