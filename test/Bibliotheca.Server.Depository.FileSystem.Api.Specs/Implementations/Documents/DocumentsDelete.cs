using FluentBehave;
using Xunit;
using System.Threading.Tasks;
using System.IO;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using System.Net;
using System.Linq;
using Bibliotheca.Server.Depository.FileSystem.Core.DataTransferObjects;

namespace test
{
    [Feature("DocumentsDelete", "Deleting document")]
    public class DocumentsDelete
    {
        private HttpResponse<DocumentDto> _response;

        [Scenario("Document should be successfully deleted")]
        public async Task DocumentShouldBeSuccessfullyDeleted()
        {
            try
            {
                await GivenSystemContainsDocumentInBranchInProject("docs/to-delete.md", "Latest", "project-a");
                await WhenUserDeletesDocumentFromBranchInProject("docs/to-delete.md", "Latest", "project-a");
                ThenSystemReturnsStatusCodeOk();
                await ThenDocumentNotExistsInBranchInProject("docs/to-delete.md", "Latest", "project-a");
            }
            finally
            {
                var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/project-a/branches/Latest/documents");
                var postResult = await httpClient.DeleteAsync("docs+to-delete.md");
            }
        }

        [Scenario("System have to return proper status code during deleting document when project not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringDeletingDocumentWhenProjectNotExists()
        {
            await GivenSystemNotContainsProject("project-not-exists");
            await WhenUserDeletesDocumentFromBranchInProject("docs/index.md", "Latest", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during deleting document when branch not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringDeletingDocumentWhenBranchNotExists()
        {
            await GivenSystemNotContainsBranchInProject("branch-not-exists", "project-a");
            await WhenUserDeletesDocumentFromBranchInProject("docs/index.md", "branch-not-exists", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during deleting document when document not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringDeletingDocumentWhenDocumentNotExists()
        {
            await GivenSystemNotContainsDocumentInBranchInProject("docs/index-not-exists.md", "Latest", "project-a");
            await WhenUserDeletesDocumentFromBranchInProject("docs/index-not-exists.md", "Latest", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during deleting document when project id not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringDeletingDocumentWhenProjectIdNotSpecified()
        {
            try
            {
                await GivenSystemContainsDocumentInBranchInProject("docs/to-delete.md", "Latest", "project-a");
                await WhenUserDeletesDocumentFromBranchInProject("docs/to-delete.md", "Latest", "");
                ThenSystemReturnsStatusCodeNotFound();
            }
            finally
            {
                var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/project-a/branches/Latest/documents");
                var postResult = await httpClient.DeleteAsync("docs+to-delete.md");
            }
        }

        [Scenario("System have to return proper status code during deleting document when branch name not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringDeletingDocumentWhenBranchNameNotSpecified()
        {
            try
            {
                await GivenSystemContainsDocumentInBranchInProject("docs/to-delete.md", "Latest", "project-a");
                await WhenUserDeletesDocumentFromBranchInProject("docs/to-delete.md", "", "project-a");
                ThenSystemReturnsStatusCodeNotFound();
            }
            finally
            {
                var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/project-a/branches/Latest/documents");
                var postResult = await httpClient.DeleteAsync("docs+to-delete.md");
            }
        }

        [Scenario("System have to return proper status code during deleting document when document uri not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringDeletingDocumentWhenDocumentUriNotSpecified()
        {
            try
            {
                await GivenSystemContainsDocumentInBranchInProject("docs/to-delete.md", "Latest", "project-a");
                await WhenUserDeletesDocumentFromBranchInProject("", "Latest", "project-a");
                ThenSystemReturnsStatusCodeBadRequest();
            }
            finally
            {
                var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/project-a/branches/Latest/documents");
                var postResult = await httpClient.DeleteAsync("docs+to-delete.md");
            }
        }

        [Given("System contains document in branch in project")]
        private async Task GivenSystemContainsDocumentInBranchInProject(string documentName, string branchName, string projectId)
        {
            var documentDto = new DocumentDto
            {
                Name = Path.GetFileName(documentName),
                Uri = documentName,
                Content = GetBytes("test")
            };

            var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/{projectId}/branches/{branchName}/documents");
            var postResult = await httpClient.PostAsync(documentDto);
            Assert.Equal(HttpStatusCode.Created, postResult.StatusCode);
        }

        [When("User deletes document from branch in project")]
        private async Task WhenUserDeletesDocumentFromBranchInProject(string documentName, string branchName, string projectId)
        {
            var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/{projectId}/branches/{branchName}/documents");

            var encodedDocumentName = documentName.Replace("/", "+");
            _response = await httpClient.DeleteAsync(encodedDocumentName);
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then("Document not exists in branch in project")]
        private async Task ThenDocumentNotExistsInBranchInProject(string documentName, string branchName, string projectId)
        {
            var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/{projectId}/branches/{branchName}/documents");

            var encodedDocumentName = documentName.Replace("/", "+");
            var response = await httpClient.GetByIdAsync(encodedDocumentName);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}