using FluentBehave;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using Bibliotheca.Server.Depository.FileSystem.Core.DataTransferObjects;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Branches
{
    [Feature("BranchesGet", "Branches list")]
    public class BranchesGet
    {
        private HttpResponse<IList<BranchDto>> _response;

        [Scenario("List of branches must be available")]
        public async Task ListOfBranchesMustBeAvailable()
        {
            await GivenSystemContainsProjectWithBranches("project-a", "Latest", "Release 1.0");
            await WhenUserWantsToSeeAllBranchesFromProject("project-a");
            ThenSystemReturnsStatusCodeOk();
            await ThenSystemReturnsBranchesFromProject("Latest", "Release 1.0", "project-a");
            ThenBranchesAreSortedAlpabetically();
        }

        [Scenario("List must contains only branches with correct configuration")]
        public async Task ListMustContainsOnlyBranchesWithCorrectConfiguration()
        {
            await GivenSystemContainsProjectWithBranches("project-a", "Latest", "Release 1.0");
            GivenBranchInProjectDoesNotHaveConfigurationFile("empty-branch", "project-a");
            await WhenUserWantsToSeeAllBranchesFromProject("project-a");
            ThenSystemReturnsStatusCodeOk();
            await ThenSystemReturnsBranchesFromProjectWithout("empty-branch", "project-a");
        }

        [Scenario("System have to return proper status code during getting list of branches when project not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringGettingListOfBranchesWhenProjectNotExists()
        {
            await GivenSystemNotContainsProject("project-not-exists");
            await WhenUserWantsToSeeAllBranchesFromProject("project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during getting list of branches when project id not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringGettingListOfBranchesWhenProjectIdNotSpecified()
        {
            await GivenSystemContainsProjectWithBranches("project-a", "Latest", "Release 1.0");
            await WhenUserWantsToSeeAllBranchesFromProject("");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Given("System contains project with branches")]
        private async Task GivenSystemContainsProjectWithBranches(string projectId, string branch1, string branch2)
        {
            var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");

            var result = await httpClient.GetAsync();
            Assert.True(result.Content.Count == 2);
            Assert.True(result.Content.Any(x => x.Name == branch1));
            Assert.True(result.Content.Any(x => x.Name == branch2));
        }

        [When("User wants to see all branches from project")]
        private async Task WhenUserWantsToSeeAllBranchesFromProject(string projectId)
        {
            var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");
            _response = await httpClient.GetAsync();
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then("System returns branches from project")]
        private async Task ThenSystemReturnsBranchesFromProject(string branch1, string branch2, string projectId)
        {
            var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");

            var result = await httpClient.GetAsync();
            Assert.True(result.Content.Count == 2);
            Assert.True(result.Content.Any(x => x.Name == branch1));
            Assert.True(result.Content.Any(x => x.Name == branch2));
        }

        [Then("Branches are sorted alpabetically")]
        private void ThenBranchesAreSortedAlpabetically()
        {
            for(int i = 1; i < _response.Content.Count; ++i)
            {
                Assert.True(_response.Content[i -1].Name.CompareTo(_response.Content[i].Name) == -1);
            }
        }

        [Given("Branch in project does not have configuration file")]
        private void GivenBranchInProjectDoesNotHaveConfigurationFile(string branchName, string projectId)
        {
        }

        [Then("System returns branches from project without")]
        private async Task ThenSystemReturnsBranchesFromProjectWithout(string branchName, string projectId)
        {
            var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");

            var result = await httpClient.GetAsync();
            Assert.False(result.Content.Any(x => x.Name == branchName));
        }

        [Given("System not contains project")]
        private async Task GivenSystemNotContainsProject(string projectId)
        {
            var httpClient = new RestClient<ProjectDto>($"http://localhost/api/projects");

            var result = await httpClient.GetAsync();
            Assert.False(result.Content.Any(x => x.Name == projectId));
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
    }
}