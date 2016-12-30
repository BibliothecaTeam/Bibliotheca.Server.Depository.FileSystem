using FluentBehave;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using System.Net;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Projects
{
    [Feature("ProjectsPost", "Creating a new project")]
    public class ProjectsPost
    {
        private HttpResponse<ProjectDto> _response;
        private const string _baseAddress = "http://localhost/api/projects";

        [Scenario("Project should be successfully added")]
        public async Task ProjectShouldBeSuccessfullyAdded()
        {
            try
            {
                await GivenSystemDoesNotContainsProject("new-project-a");
                await WhenUserAddsProject("new-project-a");
                ThenSystemReturnsStatusCodeCreated();
                await ThenProjectExists("new-project-a");
            }
            finally
            {
                var projectsClient = new RestClient<ProjectDto>(_baseAddress);
                await projectsClient.DeleteAsync("new-project-a");
            }
        }

        [Scenario("System have to return proper status code when project exists")]
        public async Task SystemHaveToReturnProperStatusCodeWhenProjectExists()
        {
            await GivenSystemContainsProjects("project-a");
            await WhenUserAddsProject("project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Scenario("Project without id cannot be added")]
        public async Task ProjectWithoutIdCannotBeAdded()
        {
            await GivenSystemDoesNotContainsProject("new-project-b");
            await WhenUserAddsProject("");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System does not contains project")]
        private async Task GivenSystemDoesNotContainsProject(string projectId)
        {
            var projectsClient = new RestClient<ProjectDto>(_baseAddress);
            await projectsClient.DeleteAsync(projectId);

            var result = await projectsClient.GetAsync();
            Assert.False(result.Content.Any(x => x.Id == projectId));
        }

        [When("User adds project")]
        private async Task WhenUserAddsProject(string projectId)
        {
            var projectDto = new ProjectDto
            {
                Id = projectId
            };

            var projectsClient = new RestClient<ProjectDto>(_baseAddress);
            _response = await projectsClient.PostAsync(projectDto);
        }

        [Then("System returns status code Created")]
        private void ThenSystemReturnsStatusCodeCreated()
        {
            Assert.Equal(HttpStatusCode.Created, _response.StatusCode);
        }

        [Then("Project exists")]
        private async Task ThenProjectExists(string projectId)
        {
            var projectsClient = new RestClient<ProjectDto>(_baseAddress);
            var result = await projectsClient.GetAsync();

            Assert.True(result.Content.Any(x => x.Id == projectId));
        }

        [Given("System contains projects")]
        private async Task GivenSystemContainsProjects(string projectId)
        {
            var projectsClient = new RestClient<ProjectDto>(_baseAddress);
            var result = await projectsClient.GetAsync();

            Assert.True(result.Content.Any(x => x.Id == projectId));
        }

        [Then("System returns status code BadRequest")]
        private void ThenSystemReturnsStatusCodeBadRequest()
        {
            Assert.Equal(HttpStatusCode.BadRequest, _response.StatusCode);
        }
    }
}