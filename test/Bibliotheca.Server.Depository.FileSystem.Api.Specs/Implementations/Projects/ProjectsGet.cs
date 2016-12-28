using FluentBehave;
using System;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using System.Collections.Generic;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using System.Linq;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Projects
{
    [Feature("ProjectsGet", "Projects list")]
    public class ProjectsGet
    {
        private HttpResponse<IList<ProjectDto>> _response;

        [Scenario("List of projects must be available")]
        public async Task ListOfProjectsMustBeAvailable()
        {
            GivenSystemContainsProjects();
            await WhenUserWantsToSeeAllProjects();
            ThenSystemReturnsStatusCodeOk();
            ThenSystemReturnsAllAvailableProjects();
            ThenProjectsAreSortedAlpabetically();
        }

        [Given("System contains projects")]
        private void GivenSystemContainsProjects()
        {
        }

        [When("User wants to see all projects")]
        private async Task WhenUserWantsToSeeAllProjects()
        {
            var projectsClient = new ProjectsClient();
            _response = await projectsClient.GetAsync();
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then("System returns all available projects")]
        private void ThenSystemReturnsAllAvailableProjects()
        {
            Assert.True(_response.Content.Any(x => x.Id == "project-a"));
            Assert.True(_response.Content.Any(x => x.Id == "project-b"));
            Assert.True(_response.Content.Any(x => x.Id == "project-c"));
            Assert.True(_response.Content.Any(x => x.Id == "project-d"));
        }

        [Then("Projects are sorted alpabetically")]
        private void ThenProjectsAreSortedAlpabetically()
        {
            for(int i = 1; i < _response.Content.Count; ++i)
            {
                Assert.True(_response.Content[i -1].Id.CompareTo(_response.Content[i].Id) == -1);
            }
        }
    }
}