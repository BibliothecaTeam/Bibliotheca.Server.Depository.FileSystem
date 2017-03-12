using FluentBehave;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using System.Threading.Tasks;
using System.Net;
using Xunit;
using Bibliotheca.Server.Depository.FileSystem.Core.DataTransferObjects;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Projects
{
    [Feature("ProjectsGetById", "Project details")]
    public class ProjectsGetById
    {
        private HttpResponse<ProjectDto> _response;
        private const string _baseAddress = "http://localhost/api/projects";

        [Scenario("Project details must be available")]
        public async Task ProjectDetailsMustBeAvailable()
        {
            GivenSystemContainsProject("project-a");
            await WhenUserWantsToSeeDetailsOfProject("project-a");
            ThenSystemReturnsStatusCodeOk();
            ThenProjectNameIsEqualTo("Project A");
            ThenDescriptionIsEqualTo("Test project A");
            ThenDefaultBranchIsEqaulTo("Latest");
            ThenGroupIsEqualTo("Group A");
            ThenVisibleBranchesContainsOnly("Release 1.0", "Latest");
            ThenTagsContainsOnly("TagC", "TagB", "TagA");
        }

        [Scenario("System have to return proper status code during getting project details when project not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringGettingProjectDetailsWhenProjectNotExists()
        {
            GivenSystemNotContainsProjects("project-x");
            await WhenUserWantsToSeeDetailsOfProject("project-x");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Given("System contains project")]
        private void GivenSystemContainsProject(string projectId)
        {
        }

        [When("User wants to see details of project")]
        private async Task WhenUserWantsToSeeDetailsOfProject(string projectId)
        {
            var projectsClient = new RestClient<ProjectDto>(_baseAddress);
            _response = await projectsClient.GetByIdAsync(projectId);
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then("Project name is equal to")]
        private void ThenProjectNameIsEqualTo(string projectName)
        {
            Assert.Equal(projectName, _response.Content.Name);
        }

        [Then("Description is equal to")]
        private void ThenDescriptionIsEqualTo(string description)
        {
            Assert.Equal(description, _response.Content.Description);
        }

        [Then("Default branch is eqaul to")]
        private void ThenDefaultBranchIsEqaulTo(string defaultBranch)
        {
            Assert.Equal(defaultBranch, _response.Content.DefaultBranch);
        }

        [Then("Group is equal to")]
        private void ThenGroupIsEqualTo(string group)
        {
            Assert.Equal(group, _response.Content.Group);
        }

        [Then("Visible branches contains only")]
        private void ThenVisibleBranchesContainsOnly(string branch1, string branch2)
        {
            Assert.Equal(2, _response.Content.VisibleBranches.Count);
            Assert.True(_response.Content.VisibleBranches.Contains(branch1));
            Assert.True(_response.Content.VisibleBranches.Contains(branch2));
        }

        [Then("Tags contains only")]
        private void ThenTagsContainsOnly(string tag1, string tag2, string tag3)
        {
            Assert.Equal(3, _response.Content.Tags.Count);
            Assert.True(_response.Content.Tags.Contains(tag1));
            Assert.True(_response.Content.Tags.Contains(tag2));
            Assert.True(_response.Content.Tags.Contains(tag3));
        }

        [Given("System not contains projects")]
        private void GivenSystemNotContainsProjects(string projectId)
        {
        }

        [Then("System returns status code NotFound")]
        private void ThenSystemReturnsStatusCodeNotFound()
        {
            Assert.Equal(HttpStatusCode.NotFound, _response.StatusCode);
        }
    }
}