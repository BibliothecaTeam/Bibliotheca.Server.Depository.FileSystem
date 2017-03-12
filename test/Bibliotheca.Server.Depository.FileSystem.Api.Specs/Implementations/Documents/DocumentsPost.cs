using FluentBehave;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using Xunit;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Bibliotheca.Server.Depository.FileSystem.Core.DataTransferObjects;

namespace test
{
    [Feature("DocumentsPost", "Creating a new documents")]
    public class DocumentsPost
    {
        private HttpResponse<DocumentDto> _response;

        [Scenario("Documents should be successfully added")]
        public async Task DocumentsShouldBeSuccessfullyAdded()
        {
            try
            {
                await GivenSystemNotContainsDocumentInBranchInProject("docs/new-document.md", "Latest", "project-a");
                await WhenUserAddsDocumentToBranchInProject("docs/new-document.md", "Latest", "project-a");
                ThenSystemReturnsStatusCodeCreated();
                await ThenNewDocumentExistsInBranchInProject("docs/new-document.md", "Latest", "project-a");
            }
            finally
            {
                var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/project-a/branches/Latest/documents");
                var postResult = await httpClient.DeleteAsync("docs+new-document.md");
            }
        }

        [Scenario("System have to return proper status code during adding document when project not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringAddingDocumentWhenProjectNotExists()
        {
            await GivenSystemNotContainsProject("project-not-exists");
            await WhenUserAddsDocumentToBranchInProject("docs/new-document.md", "Latest", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during adding document when branch not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringAddingDocumentWhenBranchNotExists()
        {
            await GivenSystemNotContainsBranchInProject("branch-not-exists", "project-a");
            await WhenUserAddsDocumentToBranchInProject("docs/new-document.md", "branch-not-exists", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during adding document when project id not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringAddingDocumentWhenProjectIdNotSpecified()
        {
            await GivenSystemNotContainsDocumentInBranchInProject("docs/new-document.md", "Latest", "project-a");
            await WhenUserAddsDocumentToBranchInProject("docs/new-document.md", "Latest", "");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during adding document when branch name not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringAddingDocumentWhenBranchNameNotSpecified()
        {
            await GivenSystemNotContainsDocumentInBranchInProject("docs/new-document.md", "Latest", "project-a");
            await WhenUserAddsDocumentToBranchInProject("docs/new-document.md", "", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during adding document when document uri not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringAddingDocumentWhenDocumentUriNotSpecified()
        {
            await GivenSystemNotContainsDocumentInBranchInProject("docs/new-document.md", "Latest", "project-a");
            await WhenUserAddsDocumentToBranchInProject("", "Latest", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System not contains document in branch in project")]
        private async Task GivenSystemNotContainsDocumentInBranchInProject(string documentName, string branchName, string projectId)
        {
            var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/{projectId}/branches/{branchName}/documents");

            var encodedDocumentName = documentName.Replace("/", "+");
            var response = await httpClient.GetByIdAsync(encodedDocumentName);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [When("User adds document to branch in project")]
        private async Task WhenUserAddsDocumentToBranchInProject(string documentName, string branchName, string projectId)
        {
            var encodedDocumentName = documentName.Replace("/", "+");

            var documentDto = new DocumentDto
            {
                Name = Path.GetFileName(documentName),
                Uri = encodedDocumentName,
                Content = GetBytes("test")
            };

            var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/{projectId}/branches/{branchName}/documents");
            _response = await httpClient.PostAsync(documentDto);
        }

        [Then("System returns status code Created")]
        private void ThenSystemReturnsStatusCodeCreated()
        {
            Assert.Equal(HttpStatusCode.Created, _response.StatusCode);
        }

        [Then("New document exists in branch in project")]
        private async Task ThenNewDocumentExistsInBranchInProject(string documentName, string branchName, string projectId)
        {
            var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/{projectId}/branches/{branchName}/documents");

            var encodedDocumentName = documentName.Replace("/", "+");
            var response = await httpClient.GetByIdAsync(encodedDocumentName);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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

        [Then("System returns status code BadRequest")]
        private void ThenSystemReturnsStatusCodeBadRequest()
        {
            Assert.Equal(HttpStatusCode.BadRequest, _response.StatusCode);
        }

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}