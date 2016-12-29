using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using Bibliotheca.Server.Depository.FileSystem.Core.Exceptions;
using Bibliotheca.Server.Depository.FileSystem.Core.Validators;
using Microsoft.Extensions.Logging;
using YamlDotNet.Serialization;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Services
{
    public class BranchesService : IBranchesService
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly ICommonValidator _commonValidator;
        private readonly ILogger _logger;

        public BranchesService(IFileSystemService fileSystemService, ICommonValidator commonValidator, ILoggerFactory loggerFactory)
        {
            _fileSystemService = fileSystemService;
            _commonValidator = commonValidator;
            _logger = loggerFactory.CreateLogger<BranchesService>();
        }

        public async Task<IList<BranchDto>> GetBranchesAsync(string projectId)
        {
            await _commonValidator.ProjectHaveToExists(projectId);

            var branchesNames = await _fileSystemService.GetBranchesNamesAsync(projectId);
            var branches = new List<BranchDto>();

            foreach (var branchName in branchesNames)
            {
                try
                {
                    string mkDocsYaml = await _fileSystemService.ReadTextAsync(projectId, branchName, "mkdocs.yml");

                    var branch = new BranchDto
                    {
                        Name = branchName,
                        MkDocsYaml = mkDocsYaml
                    };

                    branches.Add(branch);
                }
                catch (MkDocsFileNotFoundException)
                {
                    _logger.LogWarning($"Branch '{branchName}' in project '{projectId}' doesn't have mkdocs file.");
                }
            }

            return branches;
        }

        public async Task<BranchDto> GetBranchAsync(string projectId, string branchName)
        {
            await _commonValidator.ProjectHaveToExists(projectId);
            await _commonValidator.BranchHaveToExists(projectId, branchName);

            try
            {
                string mkDocsYaml = await _fileSystemService.ReadTextAsync(projectId, branchName, "mkdocs.yml");
                var branch = new BranchDto
                {
                    Name = branchName,
                    MkDocsYaml = mkDocsYaml
                };

                return branch;
            }
            catch (MkDocsFileNotFoundException)
            {
                _logger.LogWarning($"Branch '{branchName}' in project '{projectId}' doesn't have mkdocs file.");
                throw new MkDocsFileNotFoundException($"MkDocs configuration file for branch '{branchName}' in project '{projectId}' not found.");
            }
        }

        public async Task CreateBranchAsync(string projectId, BranchDto branch)
        {
            await _commonValidator.ProjectHaveToExists(projectId);

            _commonValidator.BranchNameShouldBeSpecified(branch.Name);
            await _commonValidator.BranchShouldNotExists(projectId, branch.Name);

            if (!IsYamlFileCorrect(branch.MkDocsYaml))
            {
                throw new MkDocsFileIsIncorrectException($"MkDocs file is empty or has incorrect format.");
            }

            await _fileSystemService.CreateFolderAsync(projectId, branch.Name);
            await _fileSystemService.WriteTextAsync(projectId, branch.Name, "mkdocs.yml", branch.MkDocsYaml);
        }

        public async Task UpdateBranchAsync(string projectId, string branchName, BranchDto branch)
        {
            await _commonValidator.ProjectHaveToExists(projectId);
            await _commonValidator.BranchHaveToExists(projectId, branchName);

            if(!IsYamlFileCorrect(branch.MkDocsYaml))
            {
                throw new MkDocsFileIsIncorrectException($"MkDocs file is empty or has incorrect format.");
            }

            await _fileSystemService.WriteTextAsync(projectId, branchName, "mkdocs.yml", branch.MkDocsYaml);
        }

        public async Task DeleteBranchAsync(string projectId, string branchName)
        {
            await _commonValidator.ProjectHaveToExists(projectId);
            await _commonValidator.BranchHaveToExists(projectId, branchName);

            await _fileSystemService.DeleteFolderAsync(projectId, branchName);
        }

        private bool IsYamlFileCorrect(string mkdocsFile)
        {
            if (string.IsNullOrWhiteSpace(mkdocsFile))
            {
                return false;
            }

            try
            {
                var deserializer = new Deserializer();
                var branchConfiguration = deserializer.Deserialize<Dictionary<object, object>>(mkdocsFile);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}