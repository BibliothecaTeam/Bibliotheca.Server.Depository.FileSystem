using System.Collections.Generic;
using System.Threading.Tasks;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Services
{
    public interface IBranchesService
    {
        Task<IList<BranchDto>> GetBranchesAsync(string projectId);

        Task<BranchDto> GetBranchAsync(string projectId, string branchName);

        Task<ActionConfirmation> CreateBranchAsync(BranchDto brach);

        Task<ActionConfirmation> UpdateBranchAsync(string projectId, string branchName, BranchDto branch);
        
        Task<ActionConfirmation> DeleteBranchAsync(string projectId, string branchName);
    }
}