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
            var paths = await GetDirectoriesAsync(_applicationParameters.ProjectsUrl);

            var projectIds = new List<string>();
            foreach(var path in paths)
            {
                var directoryName = Path.GetFileName(path);
                projectIds.Add(directoryName);
            }

            return projectIds;
        }

        public async Task<string> ReadTextAsync(string projectId, string fileUri)
        {
            string path = GetPathToFile(projectId, fileUri);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            var fileString = await ReadAllTextAsync(path);
            return fileString;
        }

        public async Task WriteTextAsync(string projectId, string fileUri, string contents)
        {
            string path = GetPathToFile(projectId, fileUri);
            await WriteAllTextAsync(path, contents);
        }

        public async Task CreateFolderAsync(string path)
        {
            var directoryPath = Path.Combine(_applicationParameters.ProjectsUrl, path);
            await CreateDirectoryAsync(directoryPath);
        }

        public async Task DeleteFolderAsync(string path)
        {
            var directoryPath = Path.Combine(_applicationParameters.ProjectsUrl, path);
            await DeleteDirectoryAsync(directoryPath);
        }

        private string GetPathToFile(string projectId, string fileUri)
        {
            string path = Path.Combine(_applicationParameters.ProjectsUrl, projectId, fileUri);
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

        private async Task WriteAllTextAsync(string path, string contents)
        {
            await Task.Run(() =>
            {
                File.WriteAllText(path, contents);
            });
        }
    }
}