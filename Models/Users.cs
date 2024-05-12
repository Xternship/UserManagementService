using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementService.Models
{
    [Table("Users")]
    public class User
    {
        public int UserId {get;set;}
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; }
        public string? PasswordHash { get; set; } // Store hashed password instead of plain text
        public string? RefreshToken { get; set; } // Optional for refresh token functionality
        public bool IsActive { get; set; } // Track user account status


        public User() { }
       
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
