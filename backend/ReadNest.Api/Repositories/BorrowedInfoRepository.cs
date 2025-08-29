using Microsoft.EntityFrameworkCore;
using ReadNest.Data;
using ReadNest.Entities;
using ReadNest.Dtos;

namespace ReadNest.Repositories;

public class BorrowedInfoRepository : IBorrowedInfoRepository
{
    readonly AppDbContext _appDbContext;

    public BorrowedInfoRepository(AppDbContext context)
    {
        _appDbContext = context;
    }

    public async Task<List<BorrowedInfo>> GetAllBorrowedInfo()
    {
        return await _appDbContext.BorrowedInfo.Include(b => b.Book).ToListAsync();
    }

    public async Task<BorrowedInfo> AddBorrowedInfo(BorrowedInfo newBorrowedInfo)
    {
        _appDbContext.BorrowedInfo.Add(newBorrowedInfo);
        await _appDbContext.SaveChangesAsync();

        return await _appDbContext.BorrowedInfo
              .Include(b => b.Book)
              .FirstAsync(b => b.Id == newBorrowedInfo.Id);
    }

    public async Task<bool> UpdateBorrowedInfo(Guid id, UpdatedBorrowedInfoDto updatedBorrowedInfo)
    {
        var borrowedInfo = await _appDbContext.BorrowedInfo.FindAsync(id);

        if (borrowedInfo == null)
        {
            return false;
        }

        borrowedInfo.BorrowedDate = updatedBorrowedInfo.BorrowedDate;
        borrowedInfo.DueDate = updatedBorrowedInfo.DueDate;
        borrowedInfo.ReturnedDate = updatedBorrowedInfo.ReturnedDate;

        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkReturned(Guid id)
    {
        var borrowedInfo = await _appDbContext.BorrowedInfo.FindAsync(id);

        if (borrowedInfo == null)
        {
            return false;
        }

        borrowedInfo.ReturnedDate = DateOnly.FromDateTime(DateTime.Now);

        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteBorrowedInfo(Guid id)
    {
        var borrowedInfo = await _appDbContext.BorrowedInfo.FindAsync(id);

        if (borrowedInfo == null)
        {
            return false;
        }

        _appDbContext.BorrowedInfo.Remove(borrowedInfo);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}
