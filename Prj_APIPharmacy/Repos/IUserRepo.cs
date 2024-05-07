using Api.Enums;
using Api.Models;

namespace Api.Repos;

public interface IUserRepo
{
    Task<bool> SaveAsync(User user);
    Task<User?> FindByUserNameOrEmailAsync(string usernameOrEmail);
    Task<IEnumerable<User>> AllAsync();
    Task<IEnumerable<User>> AllAsync(UserRole role);

    Task<bool> DeleteAsync(User user);
    Task<User?> GetUserByIdAsync(Guid id);
    Task<bool> UpdateAsync(User user);
}
