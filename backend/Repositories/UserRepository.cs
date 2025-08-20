using Microsoft.EntityFrameworkCore;
using ReadNest.Data;
using ReadNest.Entities;

namespace ReadNest.Repositories;

public class UserRepository : IUserRepository
{
    readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext context)
    {
        _appDbContext = context;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _appDbContext.Users.ToListAsync();
    }

    public async Task<User> AddUser(User newUser)
    {
        _appDbContext.Users.Add(newUser);
        await _appDbContext.SaveChangesAsync();
        return newUser;
    }

    public async Task<User?> UpdateUser(User updatedUser)
    {
        var user = await _appDbContext.Users.FindAsync(updatedUser.UserId);

        if (user == null)
        {
            return null;
        }

        user.Name = updatedUser.Name;

        await _appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _appDbContext.Users.FindAsync(id);

        if (user == null)
        {
            return false;
        }

        _appDbContext.Users.Remove(user);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}
