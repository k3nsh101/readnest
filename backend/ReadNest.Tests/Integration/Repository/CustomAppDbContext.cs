using Microsoft.EntityFrameworkCore;
using ReadNest.Data;

namespace ReadNest.Tests.Integration.Repo;

public class CustomAppDbContext
{
    public static AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }
}
