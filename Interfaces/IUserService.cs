using UserManagementService.Models;

namespace UserManagementService.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByUsernameAsync(string username);
    }
}
