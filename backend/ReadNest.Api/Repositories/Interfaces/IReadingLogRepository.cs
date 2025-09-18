using ReadNest.Entities;
using ReadNest.Dtos;

namespace ReadNest.Repositories;

public interface IReadingLogRepository
{
    Task<List<ReadingLog>> GetAllReadingLogs();
    Task<ReadingLog?> GetReadingLogByDate(DateOnly date);
    Task<ReadingLog> AddReadingLog(ReadingLog readingLog);
    Task<bool> UpdateReadingLog(UpdateReadingLogDto updatedReadingLog);
    Task<bool> DeleteReadingLog(DateOnly date, Guid bookId);
}
