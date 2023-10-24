using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities.Efos;
using ProjectManagement.EntityFramework;

namespace ProjectManagement.Services.Services
{
    public interface ICommentsService
    {
        Task<List<CommentsEfo>> GetAllCommentsAsync();
        Task<CommentsEfo> GetCommentByIdAsync(int commentId);
        Task<CommentsEfo> SendCommentAsync(CommentsEfo comment);
        Task<CommentsEfo> UpdateCommentAsync(int commentId, CommentsEfo updateComment);
        Task DeleteCommentAsync(int commentId);
    }

    public class CommentsService : ICommentsService
    {
        private readonly PManagementDbContext _context;

        public CommentsService(PManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<CommentsEfo>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<CommentsEfo> GetCommentByIdAsync(int commentId)
        {
            CommentsEfo? comment = await _context.Comments.AsNoTracking()
                .FirstOrDefaultAsync(c => c.CommentID == commentId);

            if (comment == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return comment;
        }

        public async Task<CommentsEfo> SendCommentAsync(CommentsEfo comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<CommentsEfo> UpdateCommentAsync(int commentId, CommentsEfo updateComment)
        {
            try
            {
                CommentsEfo? comment = await _context.Comments.FindAsync(commentId);

                if (comment == null)
                {
                    throw new Exception("Entity doesn´t exist in the database");
                }

                comment.Comment = updateComment.Comment;
                comment.CreationDate = updateComment.CreationDate;

                await _context.SaveChangesAsync();

                return comment;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating comment: {ex.Message}");
            }
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            CommentsEfo? comment = await _context.Comments.FirstOrDefaultAsync(
                c => c.CommentID == commentId);

            if (comment == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
