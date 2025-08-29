namespace ReadNest.Dtos;

public record UpdatedBorrowedInfoDto(
    DateOnly BorrowedDate,
    DateOnly DueDate,
    DateOnly? ReturnedDate
);
