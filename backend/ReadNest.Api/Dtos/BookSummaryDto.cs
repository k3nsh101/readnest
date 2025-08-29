namespace ReadNest.Dtos;

public record BookSummaryDto(
    Guid BookId,
    string Title,
    ReadStatus Status,
    BookGenreDto Genre,
    bool Owned,
    string? CoverUrl
);
