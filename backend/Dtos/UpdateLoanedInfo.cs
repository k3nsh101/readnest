namespace ReadNest.Dtos;

public record UpdatedLoanedInfoDto(
    DateOnly LoanedDate,
    DateOnly DueDate,
    DateOnly? ReturnedDate
);
