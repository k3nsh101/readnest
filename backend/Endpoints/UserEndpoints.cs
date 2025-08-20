using ReadNest.Repositories;
using ReadNest.Entities;
using ReadNest.Dtos;
using ReadNest.Mapping;

namespace ReadNest.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/users", async (IUserRepository repo) =>
        {
            var users = await repo.GetAllUsers();

            return Results.Ok(users.Select(UserMapping.ToDto));
        });

        app.MapPost("/users", async (CreateUserDto newUser, IUserRepository repo) =>
        {
            User user = newUser.ToEntity();
            var createdUser = await repo.AddUser(user);
            UserDto userDto = createdUser.ToDto();

            return Results.Created($"/users/{userDto.UserId}", userDto);
        });

        app.MapPut("/users", async (UpdateUserDto changedUser, IUserRepository repo) =>
        {
            User user = new()
            {
                UserId = changedUser.UserId,
                Name = changedUser.Name
            };

            var updatedUser = await repo.UpdateUser(user);
            return updatedUser is not null ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/users/{id}", async (int id, IUserRepository repo) =>
        {
            var success = await repo.DeleteUser(id);
            return success ? Results.NoContent() : Results.NotFound();
        });
    }
}
