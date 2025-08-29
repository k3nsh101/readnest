using ReadNest.Entities;
using ReadNest.Dtos;

namespace ReadNest.Mapping;

public static class LoanedInfoMapping
{
    public static LoanedInfo ToEntity(this CreateLoanedInfoDto loanedInfo)
    {
        return new LoanedInfo()
        {
            Id = new Guid(),
            BookId = loanedInfo.BookId,
            BorrowerName = loanedInfo.BorrowerName,
            LoarnedDate = loanedInfo.LoanedDate,
            DueDate = loanedInfo.DueDate
        };
    }

    public static LoanedInfoDto ToDto(this LoanedInfo loanedInfo)
    {
        return new(
            loanedInfo.Id,
            loanedInfo.Book!.Title,
            loanedInfo.BorrowerName,
            loanedInfo.LoarnedDate,
            loanedInfo.DueDate,
            loanedInfo.ReturnedDate
        );
    }
}
