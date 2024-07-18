using Grpc.Core;
using UserManagementService.Data;
using UserManagementService.Models;
namespace UserManagementService.Services;

public class UserManageService : UserService.UserServiceBase 
{
    private readonly AppDbContext _context;
 

    public UserManageService(AppDbContext context)
    {
        _context = context;
      
    }

    public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            UserName = request.UserName,
            PasswordHash = hashedPassword,
            Role = (Models.UserRole)request.Role,
            IsActive = true 
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

  

        return new CreateUserResponse
        {
            Message = "User created successfully",
           
        };
    }

    public override Task<UpdateUserRoleResponse> UpdateUserRole(UpdateUserRoleRequest request, ServerCallContext context)
    {
        // Your implementation logic for updating user role
        return Task.FromResult(new UpdateUserRoleResponse
        {
            Message = "User role updated successfully"
        });
    }

   
}
