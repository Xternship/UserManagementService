using Grpc.Core;
using UserManagementService.Data;
using UserManagementService.Models;
namespace UserManagementService.Services;

public class UserManageService : UserService.UserServiceBase
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public UserManageService(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        // Hash password before saving
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            UserName = request.UserName,
            PasswordHash = hashedPassword,
            Role = (Models.UserRole)request.Role,
            IsActive = true // Set user to active by default
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Generate JWT token upon successful creation
        var token = _tokenService.GenerateToken(user);

        return new CreateUserResponse
        {
            Message = "User created successfully",
            Token = token 
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

    public override Task<InviteUserResponse> InviteUser(InviteUserRequest request, ServerCallContext context)
    {
        // Your implementation logic for inviting a user
        return Task.FromResult(new InviteUserResponse
        {
            Message = "Invitation sent successfully"
        });
    }
}
