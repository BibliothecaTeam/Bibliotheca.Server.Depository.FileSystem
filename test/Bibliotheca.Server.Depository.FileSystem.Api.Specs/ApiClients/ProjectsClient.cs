using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Bibliotheca.Server.Depository.Abstractions.DataTransferObjects;
using Newtonsoft.Json;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients
{
    public class ProjectsClient
    {
        public async Task<HttpResponse<IList<ProjectDto>>> GetAsync()
        {
            var httpResponse = new HttpResponse<IList<ProjectDto>>();
            var url = $"http://localhost/api/projects";
            var client = ApiTestServer.Instance.CreateClient();
            client.AddSecurityToken();

            var response = await client.GetAsync(url);
            httpResponse.StatusCode = response.StatusCode;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var deserializedObject = JsonConvert.DeserializeObject<IList<ProjectDto>>(content);
                httpResponse.Content = deserializedObject;
            }

            return httpResponse;
        }

        public async Task<HttpResponse<ProjectDto>> GetByIdAsync(string projectId)
        {
            var httpResponse = new HttpResponse<ProjectDto>();
            var url = $"http://localhost/api/projects/{projectId}";
            var client = ApiTestServer.Instance.CreateClient();
            client.AddSecurityToken();

            var response = await client.GetAsync(url);
            httpResponse.StatusCode = response.StatusCode;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var deserializedObject = JsonConvert.DeserializeObject<ProjectDto>(content);
                httpResponse.Content = deserializedObject;
            }

            return httpResponse;
        }
    }
}