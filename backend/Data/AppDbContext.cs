using Microsoft.EntityFrameworkCore;
using ReadNest.Entities;

namespace ReadNest.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookGenre> BookGenres => Set<BookGenre>();
}
