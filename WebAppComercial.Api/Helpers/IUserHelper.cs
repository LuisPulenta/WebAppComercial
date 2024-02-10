using Microsoft.AspNetCore.Identity;
using WebAppComercial.Shared.DTOs;
using WebAppComercial.Shared.Entities;

namespace WebAppComercial.Api.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(User user, string roleName);
        Task RemoveUserFromRoleAsync(User user, string roleName);
        Task<bool> IsUserInRoleAsync(User user, string roleName);
        Task<SignInResult> LoginAsync(LoginDTO model);
        Task LogoutAsync();
        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<User> GetUserAsync(Guid userId);
        Task<IdentityResult> DeleteUserAsync(User user);
    }
}
