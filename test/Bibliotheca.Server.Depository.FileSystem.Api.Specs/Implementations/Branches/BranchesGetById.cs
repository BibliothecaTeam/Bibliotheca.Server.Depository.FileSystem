using FluentBehave;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System.Net;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Branches
{
    [Feature("BranchesGetById", "Branch details")]
    public class BranchesGetById
    {
        private HttpResponse<BranchDto> _response;

        [Scenario("Branch details must be available")]
        public async Task BranchDetailsMustBeAvailable()
        {
            await GivenSystemContainsProjectWithBranch("project-a", "Latest");
            await WhenUserWantsToSeeDetailsOfBranchFromProject("Latest", "project-a");
            ThenSystemReturnsStatusCodeOk();
            ThenBranchNameIsEqualTo("Latest");
            ThenConfigurationInformationIsAvailable();
        }

        [Scenario("System have to return proper status code when branch not exists")]
        public async Task SystemHaveToReturnProperStatusCodeWhenBranchNotExists()
        {
            await GivenSystemNotContainsBranchInProject("branch-not-exists", "project-a");
            await WhenUserWantsToSeeDetailsOfBranchFromProject("branch-not-exists", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when project not exists")]
        public async Task SystemHaveToReturnProperStatusCodeWhenProjectNotExists()
        {
            await GivenSystemNotContainsProject("project-not-exists");
            await WhenUserWantsToSeeDetailsOfBranchFromProject("branch-not-exists", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when project id not specified")]
        public async Task SystemHaveToReturnProperStatusCodeWhenProjectIdNotSpecified()
        {
            await GivenSystemContainsProjectWithBranch("project-a", "Latest");
            await WhenUserWantsToSeeDetailsOfBranchFromProject("Latest", "");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Given("System contains project with branch")]
        private async Task GivenSystemContainsProjectWithBranch(string projectId, string branchName)
        {
            var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");

            var result = await httpClient.GetAsync();
            Assert.True(result.Content.Any(x => x.Name == branchName));
        }

        [When("User wants to see details of branch from project")]
        private async Task WhenUserWantsToSeeDetailsOfBranchFromProject(string branchName, string projectId)
        {
            var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");
            _response = await httpClient.GetByIdAsync(branchName);
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then("Branch name is equal to")]
        private void ThenBranchNameIsEqualTo(string branchName)
        {
            Assert.Equal(branchName, _response.Content.Name);
        }

        [Then("Configuration information is available")]
        private void ThenConfigurationInformationIsAvailable()
        {
            Assert.False(string.IsNullOrWhiteSpace(_response.Content.Name));
        }

        [Given("System not contains branch in project")]
        private async Task GivenSystemNotContainsBranchInProject(string branchName, string projectId)
        {
            var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");

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
            var httpClient = new RestClient<ProjectDto>($"http://localhost/api/projects");

            var result = await httpClient.GetAsync();
            Assert.False(result.Content.Any(x => x.Id == projectId));
        }

        [Then("System returns status code BadRequest")]
        private void ThenSystemReturnsStatusCodeBadRequest()
        {
            Assert.Equal(HttpStatusCode.BadRequest, _response.StatusCode);
        }
    }
}