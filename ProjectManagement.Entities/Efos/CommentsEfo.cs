using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Entities.Efos
{
    public class CommentsEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }
        public string Comment {  get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? TaskID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ProjectID { get; set; }

        public UserEfo User { get; set; }
        public TasksEfo Task { get; set; }
        public ProjectsEfo Project { get; set; }
    }
}
