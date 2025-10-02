using ReadNest.Entities;
using ReadNest.Dtos;

namespace ReadNest.Mapping;

public static class ReadingLogMapping
{
    public static ReadingLog ToEntity(this CreateReadingLogDto readingLog, int pageRead)
    {
        return new ReadingLog()
        {
            Date = readingLog.Date,
            BookId = readingLog.BookId,
            PagesRead = pageRead
        };
    }

    public static ReadingLogDto ToDto(this ReadingLog readingLog)
    {
        return new(
            readingLog.Date,
            readingLog.Book!.BookId,
            readingLog.Book!.Title,
            readingLog.PagesRead
        );
    }
}
