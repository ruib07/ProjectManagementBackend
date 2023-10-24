using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Entities.Efos
{
    public class StageEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StageID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime ConclusionDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }

        public ProjectsEfo Project { get; set; }
    }
}
