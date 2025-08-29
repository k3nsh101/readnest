using ReadNest.Repositories;
using ReadNest.Dtos;
using ReadNest.Mapping;
using ReadNest.Entities;

namespace ReadNest.Endpoints;

public static class BorrowedInfoEndpoints
{
    public static void MapBorrowedInfoEndpoints(this IEndpointRouteBuilder app)
    {
        var borrowedInfoGroup = app.MapGroup("borrowed");

        borrowedInfoGroup.MapGet("/", async (IBorrowedInfoRepository repo) =>
        {
            var borrowedInfo = await repo.GetAllBorrowedInfo();
            return borrowedInfo.Select(BorrowedInfoMapping.ToDto);
        });

        borrowedInfoGroup.MapPost("/", async (CreateBorrowedInfoDto newBorrowedInfo, IBorrowedInfoRepository repo) =>
        {
            BorrowedInfo createdBorrowedInfo = await repo.AddBorrowedInfo(newBorrowedInfo.ToEntity());
            var borrowedInfo = createdBorrowedInfo.ToDto();

            return Results.Created($"/{borrowedInfo.Id}", borrowedInfo);
        });

        borrowedInfoGroup.MapPut("/{id}", async (Guid id, UpdatedBorrowedInfoDto updatedBorrowedInfo, IBorrowedInfoRepository repo) =>
        {
            var success = await repo.UpdateBorrowedInfo(id, updatedBorrowedInfo);

            return success ? Results.NoContent() : Results.NotFound();
        });

        borrowedInfoGroup.MapDelete("/{id}", async (Guid id, IBorrowedInfoRepository repo) =>
        {
            var success = await repo.DeleteBorrowedInfo(id);
            return success ? Results.NoContent() : Results.NotFound();
        });

        borrowedInfoGroup.MapPatch("/{id}/returned", async (Guid id, IBorrowedInfoRepository repo) =>
        {
            var success = await repo.MarkReturned(id);
            return success ? Results.NoContent() : Results.NotFound();
        });
    }
}
