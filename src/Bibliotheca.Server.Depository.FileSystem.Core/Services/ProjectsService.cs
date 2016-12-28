using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using Bibliotheca.Server.Depository.FileSystem.Core.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly ILogger _logger;

        public ProjectsService(IFileSystemService fileSystemService, ILoggerFactory loggerFactory)
        {
            _fileSystemService = fileSystemService;
            _logger = loggerFactory.CreateLogger<ProjectsService>();
        }

        public async Task<IList<ProjectDto>> GetProjectsAsync()
        {
            var projectIds = await _fileSystemService.GetProjectsIdsAsync();
            projectIds = projectIds.OrderBy(x => x).ToList();

            var projectDtos = new List<ProjectDto>();
            foreach (var projectId in projectIds)
            {
                try
                {
                    var configurationFile = await _fileSystemService.ReadTextAsync(projectId, "configuration.json");
                    var projectDto = JsonConvert.DeserializeObject<ProjectDto>(configurationFile);

                    projectDto.Id = projectId;
                    projectDtos.Add(projectDto);
                }
                catch (FileNotFoundException)
                {
                    _logger.LogWarning($"Project '{projectId}' doesn't have configuration file.");
                }
            }

            return projectDtos;
        }

        public async Task<ProjectDto> GetProjectAsync(string projectId)
        {
            var projectIds = await _fileSystemService.GetProjectsIdsAsync();
            if (!projectIds.Contains(projectId))
            {
                throw new ProjectNotFoundException($"Project '{projectId}' not found.");
            }

            try
            {
                var configurationFile = await _fileSystemService.ReadTextAsync(projectId, "configuration.json");
                var projectDto = JsonConvert.DeserializeObject<ProjectDto>(configurationFile);

                projectDto.Id = projectId;
                return projectDto;
            }
            catch (FileNotFoundException)
            {
                _logger.LogWarning($"Project '{projectId}' doesn't have configuration file.");
                throw new ConfigurationFileNotFoundException($"Configuration file for project '{projectId}' not found.");
            }
        }

        public async Task CreateProjectAsync(ProjectDto project)
        {
            if (string.IsNullOrWhiteSpace(project.Id))
            {
                throw new ProjectIdNotSpecifiedException();
            }

            var projectIds = await _fileSystemService.GetProjectsIdsAsync();
            if (projectIds.Contains(project.Id))
            {
                throw new ProjectAlreadyExistsException($"Project with id '{project.Id}' already exists.");
            }

            await _fileSystemService.CreateFolderAsync(project.Id);
            var serializedProject = JsonConvert.SerializeObject(project);

            await _fileSystemService.WriteTextAsync(project.Id, "configuration.json", serializedProject);
        }

        public async Task UpdateProjectAsync(string projectId, ProjectDto project)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ProjectIdNotSpecifiedException();
            }

            var projectIds = await _fileSystemService.GetProjectsIdsAsync();
            if (!projectIds.Contains(projectId))
            {
                throw new ProjectNotFoundException($"Project '{projectId}' not found.");
            }

            await _fileSystemService.CreateFolderAsync(projectId);

            project.Id = projectId;
            var serializedProject = JsonConvert.SerializeObject(project);

            await _fileSystemService.WriteTextAsync(projectId, "configuration.json", serializedProject);
        }

        public async Task DeleteProjectAsync(string projectId)
        {
            var projectIds = await _fileSystemService.GetProjectsIdsAsync();
            if (!projectIds.Contains(projectId))
            {
                throw new ProjectNotFoundException($"Project '{projectId}' not found.");
            }

            await _fileSystemService.DeleteFolderAsync(projectId);
        }
    }
}