namespace ReadNest.Dtos;

public record BookDto(
    int BookId,
    string Name,
    string Author,
    int TotalPages,
    int? Rating,
    ReadStatus Status,
    string? Remarks,
    BookGenreDto Genre,
    UserDto Owner
);
