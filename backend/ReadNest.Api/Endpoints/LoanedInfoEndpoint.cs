using ReadNest.Repositories;
using ReadNest.Dtos;
using ReadNest.Mapping;
using ReadNest.Entities;

namespace ReadNest.Endpoints;

public static class LoanedInfoEndpoints
{
    public static void MapLoanedInfoEndpoints(this IEndpointRouteBuilder app)
    {
        var loanedInfoGroup = app.MapGroup("loaned");

        loanedInfoGroup.MapGet("/", async (ILoanedInfoRepository repo) =>
        {
            var loanedInfo = await repo.GetAllLoanedInfo();
            return loanedInfo.Select(LoanedInfoMapping.ToDto);
        });

        loanedInfoGroup.MapPost("/", async (CreateLoanedInfoDto newLoanedInfo, ILoanedInfoRepository repo) =>
        {
            LoanedInfo createdLoanedInfo = await repo.AddLoanedInfo(newLoanedInfo.ToEntity());
            var loanedInfo = createdLoanedInfo.ToDto();

            return Results.Created($"/{loanedInfo.Id}", loanedInfo);
        });

        loanedInfoGroup.MapPut("/{id}", async (Guid id, UpdatedLoanedInfoDto updatedLoanededInfo, ILoanedInfoRepository repo) =>
        {
            var success = await repo.UpdateLoanedInfo(id, updatedLoanededInfo);

            return success ? Results.NoContent() : Results.NotFound();
        });

        loanedInfoGroup.MapDelete("/{id}", async (Guid id, ILoanedInfoRepository repo) =>
        {
            var success = await repo.DeleteLoanedInfo(id);
            return success ? Results.NoContent() : Results.NotFound();
        });

        loanedInfoGroup.MapPatch("/{id}/returned", async (Guid id, ILoanedInfoRepository repo) =>
        {
            var success = await repo.MarkReturned(id);
            return success ? Results.NoContent() : Results.NotFound();
        });
    }
}
