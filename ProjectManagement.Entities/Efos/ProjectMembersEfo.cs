using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Entities.Efos
{
    public class ProjectMembersEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectMembersID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberID { get; set; }

        public ProjectsEfo Project { get; set; }
        public UserEfo Member { get; set; }
    }
}
