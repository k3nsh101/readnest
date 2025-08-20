using ReadNest.Dtos;
using ReadNest.Entities;

namespace ReadNest.Mapping;

public static class UserMapping
{
    public static User ToEntity(this CreateUserDto user)
    {
        return new User()
        {
            Name = user.Name
        };
    }

    public static UserDto ToDto(this User user)
    {
        return new(
            user.UserId,
            user.Name
        );
    }
}
