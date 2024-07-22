using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementService.Models
{
    [Table("Users")]
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
    }

    public enum UserRole
    {
        ADMIN,
        INTERN,
        EVALUATOR,
        MENTOR,
        MANAGEMENT
    }
}