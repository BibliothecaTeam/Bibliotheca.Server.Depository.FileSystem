using FluentBehave;
using System;
using System.Threading.Tasks;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using Xunit;
using System.Linq;
using System.Net;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Branches
{
    [Feature("BranchesPost", "Creating a new branch")]
    public class BranchesPost
    {
        private HttpResponse<BranchDto> _response;

        [Scenario("Branch should be successfully added")]
        public async Task BranchShouldBeSuccessfullyAdded()
        {
            try
            {
                await GivenSystemNotContainsBranchInProject("new-branch", "project-a");
                await WhenUserAddsBranchToProject("new-branch", "project-a");
                ThenSystemReturnsStatusCodeCreated();
                await ThenBranchExistsInProject("new-branch", "project-a");
            }
            finally
            {
                var httpClient = new HttpClient<BranchDto>($"http://localhost/api/projects/project-a/branches");
                await httpClient.DeleteAsync("new-branch");
            }
        }

        [Scenario("System have to return proper status code when project not exists")]
        public async Task SystemHaveToReturnProperStatusCodeWhenProjectNotExists()
        {
            await GivenSystemNotContainsProject("project-not-exists");
            await WhenUserAddsBranchToProject("new-branch", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when branch name not specified")]
        public async Task SystemHaveToReturnProperStatusCodeWhenBranchNameNotSpecified()
        {
            await GivenSystemContainsProject("project-a");
            await WhenUserAddsBranchToProject("", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Scenario("System have to return proper status code when project id not specified")]
        public async Task SystemHaveToReturnProperStatusCodeWhenProjectIdNotSpecified()
        {
            await GivenSystemNotContainsBranchInProject("new-branch", "project-a");
            await WhenUserAddsBranchToProject("new-branch", "");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Given("System not contains branch in project")]
        private async Task GivenSystemNotContainsBranchInProject(string branchName, string projectId)
        {
            var httpClient = new HttpClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");

            var result = await httpClient.GetAsync();
            Assert.False(result.Content.Any(x => x.Name == branchName));
        }

        [When("User adds branch to project")]
        private async Task WhenUserAddsBranchToProject(string branchName, string projectId)
        {
            var httpClient = new HttpClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");

            var branchDto = new BranchDto
            {
                Name = branchName,
                MkDocsYaml = "site_name: ProejctA\ndocs_dir: 'docs'\n"
            };

            _response = await httpClient.PostAsync(branchDto);
        }

        [Then("System returns status code Created")]
        private void ThenSystemReturnsStatusCodeCreated()
        {
            Assert.Equal(HttpStatusCode.Created, _response.StatusCode);
        }

        [Then("Branch exists in project")]
        private async Task ThenBranchExistsInProject(string branchName, string projectId)
        {
            var httpClient = new HttpClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");

            var result = await httpClient.GetAsync();
            Assert.True(result.Content.Any(x => x.Name == branchName));
        }

        [Given("System not contains project")]
        private async Task GivenSystemNotContainsProject(string projectId)
        {
            var httpClient = new HttpClient<ProjectDto>($"http://localhost/api/projects");

            var result = await httpClient.GetAsync();
            Assert.False(result.Content.Any(x => x.Id == projectId));
        }

        [Then("System returns status code NotFound")]
        private void ThenSystemReturnsStatusCodeNotFound()
        {
            Assert.Equal(HttpStatusCode.NotFound, _response.StatusCode);
        }

        [Given("System contains project")]
        private async Task GivenSystemContainsProject(string projectId)
        {
            var httpClient = new HttpClient<ProjectDto>($"http://localhost/api/projects");

            var result = await httpClient.GetAsync();
            Assert.True(result.Content.Any(x => x.Id == projectId));
        }

        [Then("System returns status code BadRequest")]
        private void ThenSystemReturnsStatusCodeBadRequest()
        {
            Assert.Equal(HttpStatusCode.BadRequest, _response.StatusCode);
        }
    }
}