using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Entities.Efos
{
    public class UserEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
        public string Function {  get; set; } = string.Empty;
        public string ImageURL {  get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegisterUserId { get; set; }

        public RegisterUserEfo RegisterUser { get; set; }
    }
}
