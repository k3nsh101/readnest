using ReadNest.Entities;
using ReadNest.Dtos;

namespace ReadNest.Repositories;

public interface ILoanedInfoRepository
{
    Task<List<LoanedInfo>> GetAllLoanedInfo();
    Task<LoanedInfo> AddLoanedInfo(LoanedInfo loanedInfo);
    Task<bool> UpdateLoanedInfo(Guid id, UpdatedLoanedInfoDto updatedLoanedInfo);
    Task<bool> MarkReturned(Guid id);
    Task<bool> DeleteLoanedInfo(Guid id);
}
