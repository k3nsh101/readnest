namespace ReadNest.Dtos;

public record ReadingLogDto(
    DateOnly Date,
    Guid BookId,
    string Title,
    int PagesRead
);
