namespace ReadNest.Dtos;

public record CreateBorrowedInfoDto(
    Guid Id,
    Guid BookId,
    string LenderName,
    DateOnly BorrowedDate,
    DateOnly DueDate
);
