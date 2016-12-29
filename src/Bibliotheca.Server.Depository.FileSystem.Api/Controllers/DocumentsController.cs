using System.Threading.Tasks;
using Bibliotheca.Server.Depository.Abstractions;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using Bibliotheca.Server.Depository.FileSystem.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/projects/{projectId}/branches/{branchName}/documents")]
    public class DocumentsController : Controller, IDocumentsController
    {
        private readonly IDocumentsService _documentsService;

        public DocumentsController(IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        [HttpGet("{fileUri}")]
        public async Task<DocumentDto> Get(string projectId, string branchName, string fileUri)
        {
            var document = await _documentsService.GetDocumentAsync(projectId, branchName, fileUri);
            return document;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string projectId, string branchName, [FromBody] DocumentDto document)
        {
            await _documentsService.CreateDocumentAsync(projectId, branchName, document);

            document.Content = null;
            return Created($"/projects/{projectId}/branches/{branchName}/documents/{document.Uri}", document);
        }

        [HttpPut("{fileUri}")]
        public async Task<IActionResult> Put(string projectId, string branchName, string fileUri, [FromBody] DocumentDto document)
        {
            await _documentsService.UpdateDocumentAsync(projectId, branchName, fileUri, document);
            return Ok();
        }

        [HttpDelete("{fileUri}")]
        public async Task<IActionResult> Delete(string projectId, string branchName, string fileUri)
        {
            await _documentsService.DeleteDocumentAsync(projectId, branchName, fileUri);
            return Ok();
        }
    }
}