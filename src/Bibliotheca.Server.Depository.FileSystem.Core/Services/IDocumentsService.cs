using System.Threading.Tasks;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Services
{
    public interface IDocumentsService
    {
        Task<DocumentDto> GetDocumentAsync(string projectId, string branchName, string fileUri);

        Task<ActionConfirmation> CreateDocumentAsync(string projectId, string branchName, DocumentDto document);

        Task<ActionConfirmation> UpdateDocumentAsync(string projectId, string branchName, string fileUri, DocumentDto document);

        Task<ActionConfirmation> DeleteDocumentAsync(string projectId, string branchName, string fileUri);
    }
}