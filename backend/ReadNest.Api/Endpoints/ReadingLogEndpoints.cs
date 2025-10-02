using ReadNest.Repositories;
using ReadNest.Dtos;
using ReadNest.Mapping;
using ReadNest.Entities;

namespace ReadNest.Endpoints;

public static class ReadingLogEndpoints
{
    public static void MapReadingLogEndpoints(this IEndpointRouteBuilder app)
    {
        var readingLogGroup = app.MapGroup("reading-logs");

        readingLogGroup.MapGet("/", async (IReadingLogRepository repo) =>
        {
            var readingLog = await repo.GetAllReadingLogs();
            return readingLog.Select(ReadingLogMapping.ToDto);
        });

        readingLogGroup.MapGet("/{date}", async (string date, IReadingLogRepository repo) =>
        {
            if (!DateOnly.TryParse(date, out var parsedDate))
                return Results.BadRequest("Invalid date format. Use yyyy-MM-dd");

            var readingLog = await repo.GetReadingLogByDate(parsedDate);

            return readingLog is null ? Results.NotFound() : Results.Ok(readingLog);
        });

        readingLogGroup.MapPost("/", async (CreateReadingLogDto newReadingLog, IReadingLogRepository logRepo, IBookRepository bookRepo) =>
        {
            var book = await bookRepo.GetBookById(newReadingLog.BookId);
            if (book == null)
            {
                return Results.BadRequest();

            }

            var totalPagesRead = book.PagesRead;
            var pagesRead = newReadingLog.CurrentPage - totalPagesRead;
            ReadingLog createdReadinglog = await logRepo.AddReadingLog(newReadingLog.ToEntity(pagesRead));

            return Results.Created();
        });

        readingLogGroup.MapPatch("/", async (UpdateReadingLogDto updatedReadingLog, IReadingLogRepository repo) =>
        {
            var success = await repo.UpdateReadingLog(updatedReadingLog);

            return success ? Results.NoContent() : Results.NotFound();
        });

        readingLogGroup.MapDelete("/", async (DateOnly date, Guid bookId, IReadingLogRepository repo) =>
        {
            var success = await repo.DeleteReadingLog(date, bookId);
            return success ? Results.NoContent() : Results.NotFound();
        });
    }
}
