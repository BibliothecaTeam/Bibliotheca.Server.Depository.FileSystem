using System.Threading.Tasks;
using Bibliotheca.Server.Depository.Abstractions.Exceptions;
using Bibliotheca.Server.Depository.FileSystem.Core.Services;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Validators
{
    public class CommonValidator : ICommonValidator
    {
        private readonly IFileSystemService _fileSystemService;

        public CommonValidator(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        public void ProjectIdShouldBeSpecified(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ProjectIdNotSpecifiedException();
            }
        }

        public void BranchNameShouldBeSpecified(string branchName)
        {
            if (string.IsNullOrWhiteSpace(branchName))
            {
                throw new BranchNameNotSpecifiedException();
            }
        }

        public void DocumentUriShouldBeSpecified(string fileUri)
        {
            if (string.IsNullOrWhiteSpace(fileUri))
            {
                throw new DocumentUriNotSpecifiedException();
            }
        }

        public async Task BranchHaveToExists(string projectId, string branchName)
        {
            BranchNameShouldBeSpecified(branchName);

            var branchesNames = await _fileSystemService.GetBranchesNamesAsync(projectId);
            if (!branchesNames.Contains(branchName))
            {
                throw new BranchNotFoundException($"Branch '{branchName}' in project '{projectId}' not found.");
            }
        }

        public async Task ProjectHaveToExists(string projectId)
        {
            ProjectIdShouldBeSpecified(projectId);

            var projectIds = await _fileSystemService.GetProjectsIdsAsync();
            if (!projectIds.Contains(projectId))
            {
                throw new ProjectNotFoundException($"Project '{projectId}' not found.");
            }
        }

        public async Task DocumentHaveToExists(string projectId, string branchName, string fileUri)
        {
            DocumentUriShouldBeSpecified(fileUri);

            bool fileExists = await _fileSystemService.IsFileExistsAsync(projectId, branchName, fileUri);
            if (!fileExists)
            {
                throw new DocumentNotFoundException($"Document '{fileUri}' not exists in branch '{branchName}' in project '{projectId}'.");
            }
        }

        public async Task DocumentShouldNotExists(string projectId, string branchName, string fileUri)
        {
            bool fileExists = await _fileSystemService.IsFileExistsAsync(projectId, branchName, fileUri);
            if (fileExists)
            {
                throw new DocumentAlreadyExistsException($"Document '{fileUri}' already exists in branch '{branchName}' in project '{projectId}'.");
            }
        }

        public async Task BranchShouldNotExists(string projectId, string branchName)
        {
            var branchesNames = await _fileSystemService.GetBranchesNamesAsync(projectId);
            if (branchesNames.Contains(branchName))
            {
                throw new BranchAlreadyExistsException($"Branch '{branchName}' in project '{projectId}' already exists.");
            }
        }

        public async Task ProjectShouldNotExists(string projectId)
        {
            var projectIds = await _fileSystemService.GetProjectsIdsAsync();
            if (projectIds.Contains(projectId))
            {
                throw new ProjectAlreadyExistsException($"Project with id '{projectId}' already exists.");
            }
        }
    }
}