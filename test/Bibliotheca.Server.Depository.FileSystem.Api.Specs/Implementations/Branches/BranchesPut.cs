using FluentBehave;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using System.Linq;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Branches
{
    [Feature("BranchesPut", "Updating existing branch")]
    public class BranchesPut
    {
        private HttpResponse<BranchDto> _response;

        [Scenario("Branch should be successfully modified")]
        public async Task BranchShouldBeSuccessfullyModified()
        {
            try
            {
                await GivenSystemContainsBranchInProject("updated-branch", "project-a");
                await WhenUserUpdatesBranchInProject("updated-branch", "project-a");
                ThenSystemReturnsStatusCodeOk();
            }
            finally
            {
                var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/project-a/branches");
                await httpClient.DeleteAsync("updated-branch");
            }
        }

        [Scenario("System have to return proper status code during updating branch when project not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringUpdatingBranchWhenProjectNotExists()
        {
            await GivenSystemNotContainsProject("project-not-exists");
            await WhenUserUpdatesBranchInProject("updated-branch", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during updating branch when branch name not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringUpdatingBranchWhenBranchNameNotSpecified()
        {
            try
            {
                await GivenSystemContainsBranchInProject("updated-branch", "project-a");
                await WhenUserUpdatesBranchInProject("", "project-a");
                ThenSystemReturnsStatusCodeBadRequest();
            }
            finally
            {
                var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/project-a/branches");
                await httpClient.DeleteAsync("updated-branch");
            }
        }

        [Scenario("System have to return proper status code during updating branch when project id not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringUpdatingBranchWhenProjectIdNotSpecified()
        {
            await GivenSystemNotContainsBranchInProject("not-exists-branch", "project-a");
            await WhenUserUpdatesBranchInProject("not-exists-branch", "");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Given("System contains branch in project")]
        private async Task GivenSystemContainsBranchInProject(string branchName, string projectId)
        {
            var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");
            var branchDto = new BranchDto
            {
                Name = branchName,
                MkDocsYaml = "site_name: ProejctA\ndocs_dir: 'docs'\n"
            };

            var response = await httpClient.PostAsync(branchDto);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [When("User updates branch in project")]
        private async Task WhenUserUpdatesBranchInProject(string branchName, string projectId)
        {
            var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");
            var branchDto = new BranchDto
            {
                Name = branchName,
                MkDocsYaml = "site_name: ProejctA\ndocs_dir: 'docs'\n"
            };

            _response = await httpClient.PutAsync(branchName, branchDto);
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }

        [Given("System not contains project")]
        private async Task GivenSystemNotContainsProject(string projectId)
        {
            var httpClient = new RestClient<ProjectDto>($"http://localhost/api/projects");

            var result = await httpClient.GetAsync();
            Assert.False(result.Content.Any(x => x.Id == projectId));
        }

        [Then("System returns status code NotFound")]
        private void ThenSystemReturnsStatusCodeNotFound()
        {
            Assert.Equal(HttpStatusCode.NotFound, _response.StatusCode);
        }

        [Then("System returns status code BadRequest")]
        private void ThenSystemReturnsStatusCodeBadRequest()
        {
            Assert.Equal(HttpStatusCode.BadRequest, _response.StatusCode);
        }

        [Given("System not contains branch in project")]
        private async Task GivenSystemNotContainsBranchInProject(string branchName, string projectId)
        {
            var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");

            var result = await httpClient.GetAsync();
            Assert.False(result.Content.Any(x => x.Name == branchName));
        }
    }
}