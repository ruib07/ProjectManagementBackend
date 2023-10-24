using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Entities.Efos
{
    public class TasksEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Status { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }

        public ProjectsEfo Project { get; set; }
    }
}
