using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bibliotheca.Server.Depository.Abstractions;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/projects/{projectId}/branches")]
    public class BranchesController : Controller, IBranchesController
    {
        [HttpGet]
        public Task<IList<BranchDto>> Get(string projectId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{branchName}")]
        public Task<BranchDto> Get(string projectId, string branchName)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<IActionResult> Post(string projectId, [FromBody] BranchDto branch)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{branchName}")]
        public Task<IActionResult> Put(string projectId, string branchName, [FromBody] BranchDto branch)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{branchName}")]
        public Task<IActionResult> Delete(string projectId, string branchName)
        {
            throw new NotImplementedException();
        }
    }
}