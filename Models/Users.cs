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
        public string? PasswordHash { get; set; } 
        public string? RefreshToken { get; set; }
        public bool IsActive { get; set; } 


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
