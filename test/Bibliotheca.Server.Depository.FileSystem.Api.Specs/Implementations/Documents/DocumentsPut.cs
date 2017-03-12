using FluentBehave;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using System.IO;
using System.Linq;
using Bibliotheca.Server.Depository.FileSystem.Core.DataTransferObjects;

namespace test
{
    [Feature("DocumentsPut", "Updating existing document")]
    public class DocumentsPut
    {
        private HttpResponse<DocumentDto> _response;

        [Scenario("Document should be successfully modified")]
        public async Task DocumentShouldBeSuccessfullyModified()
        {
            try
            {
                await GivenSystemContainsDocumentInBranchInProject("docs/updated-index.md", "Latest", "project-a");
                await WhenUserUpdatesDocumentInBranchInProject("docs/updated-index.md", "Latest", "project-a");
                ThenSystemReturnsStatusCodeOk();
            }
            finally
            {
                var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/project-a/branches/Latest/documents");
                var postResult = await httpClient.DeleteAsync("docs+updated-index.md");
            }
        }

        [Scenario("System have to return proper status code during updating document when project not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringUpdatingDocumentWhenProjectNotExists()
        {
            await GivenSystemNotContainsProject("project-not-exists");
            await WhenUserUpdatesDocumentInBranchInProject("docs/index.md", "Latest", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during updating document when branch not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringUpdatingDocumentWhenBranchNotExists()
        {
            await GivenSystemNotContainsBranchInProject("branch-not-exists", "project-a");
            await WhenUserUpdatesDocumentInBranchInProject("docs/index.md", "branch-not-exists", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during updating document when document not exists")]
        public async Task SystemHaveToReturnProperStatusCodeDuringUpdatingDocumentWhenDocumentNotExists()
        {
            await GivenSystemNotContainsDocumentInBranchInProject("docs/index-not-exists.md", "Latest", "project-a");
            await WhenUserUpdatesDocumentInBranchInProject("docs/index-not-exists.md", "Latest", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code during updating document when project id not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringUpdatingDocumentWhenProjectIdNotSpecified()
        {
            try
            {
                await GivenSystemContainsDocumentInBranchInProject("docs/updated-index.md", "Latest", "project-a");
                await WhenUserUpdatesDocumentInBranchInProject("docs/updated-index.md", "Latest", "");
                ThenSystemReturnsStatusCodeNotFound();
            }
            finally
            {
                var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/project-a/branches/Latest/documents");
                var postResult = await httpClient.DeleteAsync("docs+updated-index.md");
            }
        }

        [Scenario("System have to return proper status code during updating document when branch name not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringUpdatingDocumentWhenBranchNameNotSpecified()
        {
            try
            {
                await GivenSystemContainsDocumentInBranchInProject("docs/updated-index.md", "Latest", "project-a");
                await WhenUserUpdatesDocumentInBranchInProject("docs/updated-index.md", "", "project-a");
                ThenSystemReturnsStatusCodeNotFound();
            }
            finally
            {
                var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/project-a/branches/Latest/documents");
                var postResult = await httpClient.DeleteAsync("docs+updated-index.md");
            }
        }

        [Scenario("System have to return proper status code during updating document when document uri not specified")]
        public async Task SystemHaveToReturnProperStatusCodeDuringUpdatingDocumentWhenDocumentUriNotSpecified()
        {
            try
            {
                await GivenSystemContainsDocumentInBranchInProject("docs/updated-index.md", "Latest", "project-a");
                await WhenUserUpdatesDocumentInBranchInProject("", "Latest", "project-a");
                ThenSystemReturnsStatusCodeBadRequest();
            }
            finally
            {
                var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/project-a/branches/Latest/documents");
                var postResult = await httpClient.DeleteAsync("docs+updated-index.md");
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

        [When("User updates document in branch in project")]
        private async Task WhenUserUpdatesDocumentInBranchInProject(string documentName, string branchName, string projectId)
        {
            var encodedDocumentName = documentName.Replace("/", "+");

            var documentDto = new DocumentDto
            {
                Name = Path.GetFileName(documentName),
                Uri = encodedDocumentName,
                Content = GetBytes("test")
            };

            var httpClient = new RestClient<DocumentDto>($"http://localhost/api/projects/{projectId}/branches/{branchName}/documents");
            _response = await httpClient.PutAsync(encodedDocumentName, documentDto);
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
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