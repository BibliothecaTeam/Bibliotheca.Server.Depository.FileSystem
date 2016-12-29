using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Bibliotheca.Server.Depository.FileSystem.Core.Parameters;
using Microsoft.Extensions.Options;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly ApplicationParameters _applicationParameters;

        public FileSystemService(IOptions<ApplicationParameters> applicationParameters)
        {
            _applicationParameters = applicationParameters.Value;
        }

        public async Task<IList<string>> GetProjectsIdsAsync()
        {
            return await GetFoldersAsync(string.Empty);
        }

        public async Task<IList<string>> GetBranchesNamesAsync(string projectId)
        {
            return await GetFoldersAsync(projectId);
        }

        public async Task<bool> IsFileExistsAsync(string projectId, string branchName, string fileUri)
        {
            var path = Path.Combine(_applicationParameters.ProjectsUrl, projectId, branchName, fileUri);
            return await FileExistsAsync(path);
        }

        public async Task DeleteFileAsync(string projectId, string branchName, string fileUri)
        {
            var path = Path.Combine(_applicationParameters.ProjectsUrl, projectId, branchName, fileUri);
            await DeleteFileAsync(path);
        }

        public async Task<IList<string>> GetFoldersAsync(string projectId)
        {
            return await GetFoldersAsync(projectId, string.Empty, string.Empty);
        }

        public async Task<IList<string>> GetFoldersAsync(string projectId, string branchName)
        {
            return await GetFoldersAsync(projectId, branchName, string.Empty);
        }

        public async Task<IList<string>> GetFoldersAsync(string projectId, string branchName, string path)
        {
            var folderPath = Path.Combine(_applicationParameters.ProjectsUrl, projectId, branchName, path);
            var directories = await GetDirectoriesAsync(folderPath);

            var projectIds = new List<string>();
            foreach (var directory in directories)
            {
                var directoryName = Path.GetFileName(directory);
                projectIds.Add(directoryName);
            }

            return projectIds;
        }

        public async Task<string> ReadTextAsync(string projectId, string fileUri)
        {
            return await ReadTextAsync(projectId, string.Empty, fileUri);
        }

        public async Task<string> ReadTextAsync(string projectId, string branchName, string fileUri)
        {
            string path = GetPathToFile(projectId, fileUri);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            var fileString = await ReadAllTextAsync(path);
            return fileString;
        }

        public async Task<byte[]> ReadBinaryAsync(string projectId, string branchName, string fileUri)
        {
            string path = GetPathToFile(projectId, fileUri);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            var fileString = await ReadAllBinaryAsync(path);
            return fileString;
        }

        public async Task WriteTextAsync(string projectId, string fileUri, string contents)
        {
            await WriteTextAsync(projectId, string.Empty, fileUri, contents);
        }

        public async Task WriteTextAsync(string projectId, string branchName, string fileUri, string contents)
        {
            string path = GetPathToFile(projectId, branchName, fileUri);
            await WriteAllTextAsync(path, contents);
        }

        public async Task WriteBinaryAsync(string projectId, string branchName, string fileUri, byte[] contents)
        {
            string path = GetPathToFile(projectId, branchName, fileUri);
            await WriteAllBinaryAsync(path, contents);
        }

        public async Task CreateFolderAsync(string projectId)
        {
            await CreateFolderAsync(projectId, string.Empty, string.Empty);
        }

        public async Task CreateFolderAsync(string projectId, string branchName)
        {
            await CreateFolderAsync(projectId, branchName, string.Empty);
        }

        public async Task CreateFolderAsync(string projectId, string branchName, string path)
        {
            string[] paths = path.Split('/');
            var directoryPath = Path.Combine(_applicationParameters.ProjectsUrl, projectId, branchName);

            foreach (var item in paths)
            {
                directoryPath = Path.Combine(directoryPath, item);
                if (!Directory.Exists(directoryPath))
                {
                    await CreateDirectoryAsync(directoryPath);
                }
            }
        }

        public async Task DeleteFolderAsync(string projectId)
        {
            await DeleteFolderAsync(projectId, string.Empty, string.Empty);
        }

        public async Task DeleteFolderAsync(string projectId, string branchName)
        {
            await DeleteFolderAsync(projectId, branchName, string.Empty);
        }

        public async Task DeleteFolderAsync(string projectId, string branchName, string path)
        {
            var directoryPath = Path.Combine(_applicationParameters.ProjectsUrl, projectId, branchName, path);
            await DeleteDirectoryAsync(directoryPath);
        }

        private string GetPathToFile(string projectId, string fileUri)
        {
            return GetPathToFile(projectId, string.Empty, fileUri);
        }

        private string GetPathToFile(string projectId, string branchName, string fileUri)
        {
            string path = Path.Combine(_applicationParameters.ProjectsUrl, projectId, branchName, fileUri);
            return path;
        }

        private async Task<DirectoryInfo> CreateDirectoryAsync(string path)
        {
            return await Task.Run(() =>
            {
                return Directory.CreateDirectory(path);
            });
        }

        private async Task DeleteDirectoryAsync(string path)
        {
            await Task.Run(() =>
            {
                Directory.Delete(path, true);
            });
        }

        private async Task<string[]> GetDirectoriesAsync(string path)
        {
            return await Task.Run(() =>
            {
                return Directory.GetDirectories(path);
            });
        }

        private async Task<string> ReadAllTextAsync(string path)
        {
            return await Task.Run(() =>
            {
                return File.ReadAllText(path);
            });
        }

        private async Task<byte[]> ReadAllBinaryAsync(string path)
        {
            return await Task.Run(() =>
            {
                return File.ReadAllBytes(path);
            });
        }

        private async Task WriteAllTextAsync(string path, string contents)
        {
            await Task.Run(() =>
            {
                File.WriteAllText(path, contents);
            });
        }

        private async Task WriteAllBinaryAsync(string path, byte[] contents)
        {
            await Task.Run(() =>
            {
                File.WriteAllBytes(path, contents);
            });
        }

        private async Task<bool> FileExistsAsync(string path)
        {
            return await Task.Run(() =>
            {
                return File.Exists(path);
            });
        }

        private async Task DeleteFileAsync(string path)
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });
        }
    }
}