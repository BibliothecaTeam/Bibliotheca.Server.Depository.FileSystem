using FluentBehave;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using System.Net;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Projects
{
    [Feature("ProjectsDelete", "Deleting project")]
    public class ProjectsDelete
    {
        private HttpResponse _response;

        [Scenario("Project should be successfully deleted")]
        public async Task ProjectShouldBeSuccessfullyDeleted()
        {
            await GivenSystemContainsProject("project-to-delete");
            await WhenUserDeletesProject("project-to-delete");
            ThenSystemReturnsStatusCodeOk();
            await ThenProjectNotExists("project-to-delete");
        }

        [Scenario("System have to return proper status code when project not exists")]
        public async Task SystemHaveToReturnProperStatusCodeWhenProjectNotExists()
        {
            GivenSystemNotContainsProjects("project-x");
            await WhenUserDeletesProject("project-x");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when project id not specified")]
        public async Task SystemHaveToReturnProperStatusCodeWhenProjectIdNotSpecified()
        {
            GivenSystemNotContainsProjects("");
            await WhenUserDeletesProject("");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System contains project")]
        private async Task GivenSystemContainsProject(string projectId)
        {
            var projectDto = new ProjectDto
            {
                Id = projectId,
                Name = "Project to delete"
            };

            var projectsClient = new ProjectsClient();
            await projectsClient.PostAsync(projectDto);

            var result = await projectsClient.GetAsync();
            Assert.True(result.Content.Any(x => x.Id == projectId));
        }

        [When("User deletes project")]
        private async Task WhenUserDeletesProject(string projectId)
        {
            var projectsClient = new ProjectsClient();
            _response = await projectsClient.DeleteAsync(projectId);
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then("Project not exists")]
        private async Task ThenProjectNotExists(string projectId)
        {
            var projectsClient = new ProjectsClient();
            var result = await projectsClient.GetAsync();

            Assert.False(result.Content.Any(x => x.Id == projectId));
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

        [Then("System returns status code BadRequest")]
        private void ThenSystemReturnsStatusCodeBadRequest()
        {
            Assert.Equal(HttpStatusCode.BadRequest, _response.StatusCode);
        }
    }
}