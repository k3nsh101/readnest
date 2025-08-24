using Microsoft.EntityFrameworkCore;
using ReadNest.Entities;

namespace ReadNest.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookGenre> BookGenres => Set<BookGenre>();
    public DbSet<BorrowedInfo> BorrowedInfo => Set<BorrowedInfo>();
}
