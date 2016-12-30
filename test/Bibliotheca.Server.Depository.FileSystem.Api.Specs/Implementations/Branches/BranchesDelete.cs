using FluentBehave;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using System.Net;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Branches
{
    [Feature("BranchesDelete", "Deleting branch")]
    public class BranchesDelete
    {
        private HttpResponse<BranchDto> _response;

        [Scenario("Branch should be successfully deleted")]
        public async Task BranchShouldBeSuccessfullyDeleted()
        {
            try
            {
                await GivenSystemContainsProjectWithBranch("project-a", "branch-to-delete");
                await WhenUserDeletesBranchFromProject("branch-to-delete", "project-a");
                ThenSystemReturnsStatusCodeOk();
                await ThenBrachInProjectNotExists("branch-to-delete", "project-a");
            }
            finally
            {
                var httpClient = new HttpClient<BranchDto>($"http://localhost/api/projects/project-a/branches");
                await httpClient.DeleteAsync("branch-to-delete");
            }
        }

        [Scenario("System have to return proper status code when branch not exists")]
        public async Task SystemHaveToReturnProperStatusCodeWhenBranchNotExists()
        {
            await GivenSystemNotContainsBranchInProject("branch-not-exists", "project-a");
            await WhenUserDeletesBranchFromProject("branch-not-exists", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when project not exists")]
        public async Task SystemHaveToReturnProperStatusCodeWhenProjectNotExists()
        {
            await GivenSystemNotContainsProject("project-not-exists");
            await WhenUserDeletesBranchFromProject("branch-not-exists", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when branch name not specified")]
        public async Task SystemHaveToReturnProperStatusCodeWhenBranchNameNotSpecified()
        {
            await GivenSystemContainsProject("project-a");
            await WhenUserDeletesBranchFromProject("", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Scenario("System have to return proper status code when project id not specified")]
        public async Task SystemHaveToReturnProperStatusCodeWhenProjectIdNotSpecified()
        {
            try
            {
                await GivenSystemContainsProjectWithBranch("project-a", "branch-to-delete");
                await WhenUserDeletesBranchFromProject("branch-to-delete", "");
                ThenSystemReturnsStatusCodeNotFound();
            }
            finally
            {
                var httpClient = new HttpClient<BranchDto>($"http://localhost/api/projects/project-a/branches");
                await httpClient.DeleteAsync("branch-to-delete");
            }
        }

        [Given("System contains project with branch")]
        private async Task GivenSystemContainsProjectWithBranch(string projectId, string branchName)
        {
            var branchDto = new BranchDto
            {
                Name = branchName,
                MkDocsYaml = "site_name: ProejctA\ndocs_dir: 'docs'\n"
            };

            var httpClient = new HttpClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");
            var postResult = await httpClient.PostAsync(branchDto);
            Assert.Equal(HttpStatusCode.Created, postResult.StatusCode);

            var listResult = await httpClient.GetAsync();
            Assert.True(listResult.Content.Any(x => x.Name == branchName));
        }

        [When("User deletes branch from project")]
        private async Task WhenUserDeletesBranchFromProject(string branchName, string projectId)
        {
            var httpClient = new HttpClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");
            _response = await httpClient.DeleteAsync(branchName);
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then("Brach in project not exists")]
        private async Task ThenBrachInProjectNotExists(string branchName, string projectId)
        {
            var httpClient = new HttpClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");

            var result = await httpClient.GetAsync();
            Assert.False(result.Content.Any(x => x.Name == branchName));
        }

        [Given("System not contains branch in project")]
        private async Task GivenSystemNotContainsBranchInProject(string branchName, string projectId)
        {
            var httpClient = new HttpClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");

            var result = await httpClient.GetAsync();
            Assert.False(result.Content.Any(x => x.Name == branchName));
        }

        [Then("System returns status code NotFound")]
        private void ThenSystemReturnsStatusCodeNotFound()
        {
            Assert.Equal(HttpStatusCode.NotFound, _response.StatusCode);
        }

        [Given("System not contains project")]
        private async Task GivenSystemNotContainsProject(string projectId)
        {
            var httpClient = new HttpClient<ProjectDto>($"http://localhost/api/projects");

            var result = await httpClient.GetAsync();
            Assert.False(result.Content.Any(x => x.Id == projectId));
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