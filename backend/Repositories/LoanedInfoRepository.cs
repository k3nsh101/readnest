using Microsoft.EntityFrameworkCore;
using ReadNest.Data;
using ReadNest.Entities;
using ReadNest.Dtos;

namespace ReadNest.Repositories;

public class LoanedInfoRepository : ILoanedInfoRepository
{
    readonly AppDbContext _appDbContext;

    public LoanedInfoRepository(AppDbContext context)
    {
        _appDbContext = context;
    }

    public async Task<List<LoanedInfo>> GetAllLoanedInfo()
    {
        return await _appDbContext.LoanedInfo.Include(b => b.Book).ToListAsync();
    }

    public async Task<LoanedInfo> AddLoanedInfo(LoanedInfo newLoanedInfo)
    {
        _appDbContext.LoanedInfo.Add(newLoanedInfo);
        await _appDbContext.SaveChangesAsync();

        return await _appDbContext.LoanedInfo
              .Include(b => b.Book)
              .FirstAsync(b => b.Id == newLoanedInfo.Id);
    }

    public async Task<bool> UpdateLoanedInfo(Guid id, UpdatedLoanedInfoDto updatedLoanedInfo)
    {
        var loanedInfo = await _appDbContext.LoanedInfo.FindAsync(id);

        if (loanedInfo == null)
        {
            return false;
        }

        loanedInfo.LoarnedDate = updatedLoanedInfo.LoanedDate;
        loanedInfo.DueDate = updatedLoanedInfo.DueDate;
        loanedInfo.ReturnedDate = updatedLoanedInfo.ReturnedDate;

        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkReturned(Guid id)
    {
        var loanedInfo = await _appDbContext.LoanedInfo.FindAsync(id);

        if (loanedInfo == null)
        {
            return false;
        }

        loanedInfo.ReturnedDate = DateOnly.FromDateTime(DateTime.Now);

        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteLoanedInfo(Guid id)
    {
        var loanedInfo = await _appDbContext.LoanedInfo.FindAsync(id);

        if (loanedInfo == null)
        {
            return false;
        }

        _appDbContext.LoanedInfo.Remove(loanedInfo);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}
