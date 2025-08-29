using ReadNest.Entities;
using ReadNest.Dtos;

namespace ReadNest.Mapping;

public static class BorrowedInfoMapping
{
    public static BorrowedInfo ToEntity(this CreateBorrowedInfoDto borrowedInfo)
    {
        return new BorrowedInfo()
        {
            Id = new Guid(),
            BookId = borrowedInfo.BookId,
            LenderName = borrowedInfo.LenderName,
            BorrowedDate = borrowedInfo.BorrowedDate,
            DueDate = borrowedInfo.DueDate
        };
    }

    public static BorrowedInfoDto ToDto(this BorrowedInfo borrowedInfo)
    {
        return new(
            borrowedInfo.Id,
            borrowedInfo.Book!.Title,
            borrowedInfo.LenderName,
            borrowedInfo.BorrowedDate,
            borrowedInfo.DueDate,
            borrowedInfo.ReturnedDate
        );
    }
}
