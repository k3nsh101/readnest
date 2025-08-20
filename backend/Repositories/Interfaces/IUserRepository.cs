using ReadNest.Entities;

namespace ReadNest.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllUsers();
    Task<User> AddUser(User user);
    Task<User?> UpdateUser(User updatedUser);
    Task<bool> DeleteUser(int id);
}
