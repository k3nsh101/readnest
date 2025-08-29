using Microsoft.EntityFrameworkCore;
using ReadNest.Entities;

namespace ReadNest.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReadingLog>().HasKey(r => new { r.Date, r.BookId });
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookGenre> BookGenres => Set<BookGenre>();
    public DbSet<BorrowedInfo> BorrowedInfo => Set<BorrowedInfo>();
    public DbSet<LoanedInfo> LoanedInfo => Set<LoanedInfo>();
    public DbSet<ReadingLog> ReadingLog => Set<ReadingLog>();
}
