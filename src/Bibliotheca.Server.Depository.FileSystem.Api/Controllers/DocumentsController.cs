using System;
using System.Threading.Tasks;
using Bibliotheca.Server.Depository.Abstractions;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/projects/{projectId}/branches/{branchName}/documents")]
    public class DocumentsController : Controller, IDocumentsController
    {
        [HttpGet("{fileUri}")]
        public Task<DocumentDto> Get(string projectId, string branchName, string fileUri)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<IActionResult> Post(string projectId, string branchName, [FromBody] DocumentDto document)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{fileUri}")]
        public Task<IActionResult> Put(string projectId, string branchName, string fileUri, [FromBody] DocumentDto document)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{fileUri}")]
        public Task<IActionResult> Delete(string projectId, string branchName, string fileUri)
        {
            throw new NotImplementedException();
        }
    }
}