using Microsoft.EntityFrameworkCore;
using ReadNest.Data;
using ReadNest.Entities;
using ReadNest.Dtos;

namespace ReadNest.Repositories;

public class ReadingLogRepository : IReadingLogRepository
{
    readonly AppDbContext _appDbContext;

    public ReadingLogRepository(AppDbContext context)
    {
        _appDbContext = context;
    }

    public async Task<List<ReadingLog>> GetAllReadingLogs()
    {
        return await _appDbContext.ReadingLog.Include(b => b.Book).ToListAsync();
    }

    public async Task<ReadingLog> AddReadingLog(ReadingLog newReadingLog)
    {
        var book = await _appDbContext.Books.FindAsync(newReadingLog.BookId);

        _appDbContext.ReadingLog.Add(newReadingLog);
        book!.PagesRead += newReadingLog.PagesRead;

        await _appDbContext.SaveChangesAsync();

        return await _appDbContext.ReadingLog
              .Include(log => log.Book)
              .FirstAsync(log => log.Date == newReadingLog.Date && log.BookId == newReadingLog.BookId);
    }

    public async Task<bool> UpdateReadingLog(UpdateReadingLogDto updatedReadingLog)
    {
        var readingLog = await _appDbContext.ReadingLog.FindAsync([updatedReadingLog.Date, updatedReadingLog.BookId]);
        var book = await _appDbContext.Books.FindAsync(updatedReadingLog.BookId);

        if (readingLog == null)
        {
            return false;
        }

        int currentReadPageCount = book!.PagesRead;
        int currentLogPageCount = readingLog.PagesRead;
        int newReadPageCount = updatedReadingLog.PagesRead;

        if (currentReadPageCount - currentLogPageCount + updatedReadingLog.PagesRead > book!.TotalPages)
        {
            return false;
        }

        readingLog.PagesRead = updatedReadingLog.PagesRead;
        book!.PagesRead = currentReadPageCount - currentLogPageCount + newReadPageCount;

        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteReadingLog(DateOnly date, Guid bookId)
    {
        var readingLog = await _appDbContext.ReadingLog.FindAsync([date, bookId]);

        if (readingLog == null)
        {
            return false;
        }

        var book = await _appDbContext.Books.FindAsync(bookId);
        book!.PagesRead -= readingLog.PagesRead;

        _appDbContext.ReadingLog.Remove(readingLog);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}
