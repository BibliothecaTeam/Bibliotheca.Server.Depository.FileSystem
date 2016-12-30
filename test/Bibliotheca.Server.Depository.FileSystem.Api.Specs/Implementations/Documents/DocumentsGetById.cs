using FluentBehave;
using System.Threading.Tasks;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using Xunit;
using System.Net;
using System.Linq;

namespace test
{
    [Feature("DocumentsGetById", "Documents details")]
    public class DocumentsGetById
    {
        private HttpResponse<DocumentDto> _response;

        [Scenario("Documents details must be available")]
        public async Task DocumentsDetailsMustBeAvailable()
        {
            await GivenSystemContainsDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            await WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            ThenSystemReturnsStatusCodeOk();
            ThenDocumentUriIsEqualTo("docs/index.md");
            ThenFileContentIsAvailable();
            ThenFileTypeIsEqual("text/markdown");
        }

        [Scenario("System have to return proper status code during getting document when project not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringGettingDocumentWhenProjectNotExists()
        {
            await GivenSystemNotContainsProject("project-not-exists");
            await WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("docs/index.md", "Latest", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during getting document when branch not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringGettingDocumentWhenBranchNotExists()
        {
            await GivenSystemNotContainsBranchInProject("branch-not-exists", "project-a");
            await WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("docs/index.md", "branch-not-exists", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during getting document when document not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringGettingDocumentWhenDocumentNotExists()
        {
            await GivenSystemNotContainsDocumentInBranchInProject("docs/index-not-exists.md", "Latest", "project-a");
            await WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("docs/index-not-exists.md", "Latest", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during getting document when project id not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringGettingDocumentWhenProjectIdNotSpecified()
        {
            await GivenSystemContainsDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            await WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("docs/index.md", "Latest", "");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during getting document when branch name not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringGettingDocumentWhenBranchNameNotSpecified()
        {
            await GivenSystemContainsDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            await WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("docs/index.md", "", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during getting document when document uri not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringGettingDocumentWhenDocumentUriNotSpecified()
        {
            await GivenSystemContainsDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            await WhenUserWantsToSeeDetailsOfDocumentInBranchInProject("", "Latest", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System contains document in branch in project")]
        private async Task GivenSystemContainsDocumentInBranchInProject(string documentName, string branchName, string projectId)
        {
            var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/{projectId}/branches/{branchName}/documents");

            var encodedDocumentName = documentName.Replace("/", "+");
            var response = await httpClient.GetByIdAsync(encodedDocumentName);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [When("User wants to see details of document in branch in project")]
        private async Task WhenUserWantsToSeeDetailsOfDocumentInBranchInProject(string documentName, string branchName, string projectId)
        {
            var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/{projectId}/branches/{branchName}/documents");

            var encodedDocumentName = documentName.Replace("/", "+");
            _response = await httpClient.GetByIdAsync(encodedDocumentName);
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then("Document uri is equal to")]
        private void ThenDocumentUriIsEqualTo(string documentUri)
        {
            Assert.Equal(documentUri, _response.Content.Uri);
        }

        [Then("File content is available")]
        private void ThenFileContentIsAvailable()
        {
            Assert.True(_response.Content.Content.Length > 0);
        }

        [Then("File type is equal")]
        private void ThenFileTypeIsEqual(string fileType)
        {
            Assert.Equal(fileType, _response.Content.ConentType);
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

        [Given("System not contains branch in project")]
        private async Task GivenSystemNotContainsBranchInProject(string branchName, string projectId)
        {
            var httpClient = new RestClient<BranchDto>($"http://localhost/api/projects/{projectId}/branches");

            var result = await httpClient.GetAsync();
            Assert.False(result.Content.Any(x => x.Name == branchName));
        }

        [Given("System not contains document in branch in project")]
        private async Task GivenSystemNotContainsDocumentInBranchInProject(string documentName, string branchName, string projectId)
        {
            var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/{projectId}/branches/{branchName}/documents");

            var encodedDocumentName = documentName.Replace("/", "+");
            var response = await httpClient.GetByIdAsync(encodedDocumentName);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Then("System returns status code BadRequest")]
        private void ThenSystemReturnsStatusCodeBadRequest()
        {
            Assert.Equal(HttpStatusCode.BadRequest, _response.StatusCode);
        }
    }
}