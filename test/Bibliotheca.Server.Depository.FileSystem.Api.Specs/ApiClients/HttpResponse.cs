using System.Net;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }
    }

    public class HttpResponse<T> : HttpResponse
    {
        public T Content { get; set; }
    }
}