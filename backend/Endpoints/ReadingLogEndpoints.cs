using ReadNest.Repositories;
using ReadNest.Dtos;
using ReadNest.Mapping;
using ReadNest.Entities;

namespace ReadNest.Endpoints;

public static class ReadingLogEndpoints
{
    public static void MapReadingLogEndpoints(this IEndpointRouteBuilder app)
    {
        var readingLogGroup = app.MapGroup("reading");

        readingLogGroup.MapGet("/", async (IReadingLogRepository repo) =>
        {
            var readingLog = await repo.GetAllReadingLogs();
            return readingLog.Select(ReadingLogMapping.ToDto);
        });

        readingLogGroup.MapPost("/", async (CreateReadingLogDto newReadingLog, IReadingLogRepository repo) =>
        {
            ReadingLog createdReadinglog = await repo.AddReadingLog(newReadingLog.ToEntity());

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
