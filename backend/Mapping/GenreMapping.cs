using ReadNest.Dtos;
using ReadNest.Entities;

namespace ReadNest.Mapping;

public static class BookGenreMapping
{
    public static BookGenre ToEntity(this CreateBookGenreDto genre)
    {
        return new BookGenre()
        {
            Name = genre.Name
        };
    }

    public static BookGenreDto ToDto(this BookGenre genre)
    {
        return new(
            genre.GenreId,
            genre.Name
        );
    }
}
