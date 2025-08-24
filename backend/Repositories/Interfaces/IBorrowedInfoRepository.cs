using ReadNest.Entities;
using ReadNest.Dtos;

namespace ReadNest.Repositories;

public interface IBorrowedInfoRepository
{
    Task<List<BorrowedInfo>> GetAllBorrowedInfo();
    Task<BorrowedInfo> AddBorrowedInfo(BorrowedInfo borrowedInfo);
    Task<bool> UpdateBorrowedInfo(Guid id, UpdatedBorrowedInfoDto updatedBorrowedInfo);
    Task<bool> MarkReturned(Guid id);
    Task<bool> DeleteBorrowedInfo(Guid id);
}
