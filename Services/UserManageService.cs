using Grpc.Core;
using UserManagementService.Data;
using UserManagementService.Interfaces;
using Microsoft.EntityFrameworkCore;
using UserManagementService.Models;
using System.Threading.Tasks;
using EmailService.Interfaces;

namespace UserManagementService.Services
{
    public class UserManageService : UserService.UserServiceBase
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public UserManageService(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var isAdmin = CheckIfCurrentUserIsAdmin();

            if (!isAdmin)
            {
                return new CreateUserResponse
                {
                    Message = "Only admins can register new users."
                };
            }

            var password = GenerateRandomPassword();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = hashedPassword,
                Role = (Models.UserRole)request.Role,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Send the password via email
            await _emailService.SendPasswordEmailAsync(user.Email, password);

            return new CreateUserResponse
            {
                Message = "User created successfully"
            };
        }

        private bool CheckIfCurrentUserIsAdmin()
        {
            // Implement actual admin check logic here
            return true;
        }

        public override async Task<UpdateUserRoleResponse> UpdateUserRole(UpdateUserRoleRequest request, ServerCallContext context)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user == null)
            {
                return new UpdateUserRoleResponse
                {
                    Message = "User not found"
                };
            }

            user.Role = (Models.UserRole)request.NewRole;
            await _context.SaveChangesAsync();

            return new UpdateUserRoleResponse
            {
                Message = "User role updated successfully"
            };
        }

        private string GenerateRandomPassword()
        {
            // Implement a method to generate a secure random password
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
