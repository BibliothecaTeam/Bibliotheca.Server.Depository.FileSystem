using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Services
{
    public interface IFileSystemService
    {
        Task<IList<string>> GetProjectsIdsAsync();
        
        Task<string> ReadTextAsync(string projectId, string fileUri);

        Task WriteTextAsync(string projectId, string fileUri, string contents);

        Task CreateFolderAsync(string path);

        Task DeleteFolderAsync(string path);

        //Task<string[]> GetBranchesPathsAsync(string projectId);
        //Task<string> ReadAllTextAsync(string projectId, string branchName, string fileUri);
        //Task<byte[]> ReadAllBytesAsync(string projectId, string branchName, string fileUri);
    }
}