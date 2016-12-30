using FluentBehave;
using System.Threading.Tasks;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using System.Linq;
using Xunit;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using System.Net;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Projects
{
    [Feature("ProjectsPut", "Updating existing project")]
    public class ProjectsPut
    {
        private HttpResponse _response;
        private const string _baseAddress = "http://localhost/api/projects";

        [Scenario("Project should be successfully modified")]
        public async Task ProjectShouldBeSuccessfullyModified()
        {
            try
            {
                await GivenSystemContainsProjectWithName("updated-project-a", "Project Name");
                await WhenUserUpdatesProjectWithNewName("updated-project-a", "New Project Name");
                ThenSystemReturnsStatusCodeOk();
                await ThenProjectHasName("updated-project-a", "New Project Name");
            }
            finally
            {
                var projectsClient = new RestClient<ProjectDto>(_baseAddress);
                await projectsClient.DeleteAsync("updated-project-a");
            }
        }

        [Scenario("System have to return proper status code during updating project when project not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringUpdatingProjectWhenProjectNotExists()
        {
            await GivenSystemDoesNotContainsProject("updated-project-b");
            await WhenUserUpdatesProjectWithNewName("updated-project-b", "New Project Name");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during updating project when project id was not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringUpdatingProjectWhenProjectIdWasNotSpecified()
        {
            await GivenSystemDoesNotContainsProject("updated-project-c");
            await WhenUserUpdatesProjectWithNewName("", "New Project Name");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System contains project with name")]
        private async Task GivenSystemContainsProjectWithName(string projectId, string projectName)
        {
            var projectsClient = new RestClient<ProjectDto>(_baseAddress);
            var projectDto = new ProjectDto
            {
                Id = projectId,
                Name = projectName
            };
            await projectsClient.PostAsync(projectDto);

            var result = await projectsClient.GetByIdAsync(projectId);

            Assert.NotNull(result.Content);
            Assert.Equal(projectName, result.Content.Name);
        }

        [When("User updates project with new name")]
        private async Task WhenUserUpdatesProjectWithNewName(string projectId, string projectName)
        {
            var projectsClient = new RestClient<ProjectDto>(_baseAddress);

            var projectDto = new ProjectDto
            {
                Id = projectId,
                Name = projectName
            };

            _response = await projectsClient.PutAsync(projectId, projectDto);
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then("Project has name")]
        private async Task ThenProjectHasName(string projectId, string projectName)
        {
            var projectsClient = new RestClient<ProjectDto>(_baseAddress);
            var result = await projectsClient.GetByIdAsync(projectId);

            Assert.NotNull(result.Content);
            Assert.Equal(projectName, result.Content.Name);
        }

        [Given("System does not contains project")]
        private async Task GivenSystemDoesNotContainsProject(string projectId)
        {
            var projectsClient = new RestClient<ProjectDto>(_baseAddress);

            var result = await projectsClient.GetAsync();
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
    }
}