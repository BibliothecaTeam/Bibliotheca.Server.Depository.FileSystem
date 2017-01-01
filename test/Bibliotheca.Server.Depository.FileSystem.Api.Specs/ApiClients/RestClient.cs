using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Bibliotheca.Server.Depository.FileSystem.Api.Specs.Infrastructure;
using Newtonsoft.Json;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.ApiClients
{
    public class RestClient<T>
    {
        private readonly string _address;
        
        public RestClient(string address)
        {
            _address = address;
        }

        public async Task<HttpResponse<IList<T>>> GetAsync()
        {
            var httpResponse = new HttpResponse<IList<T>>();
            var client = ApiTestServer.Instance.CreateClient();
            client.AddSecurityToken();

            var response = await client.GetAsync(_address);
            httpResponse.StatusCode = response.StatusCode;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var deserializedObject = JsonConvert.DeserializeObject<IList<T>>(content);
                httpResponse.Content = deserializedObject;
            }

            return httpResponse;
        }

        public async Task<HttpResponse<T>> GetByIdAsync(string id)
        {
            var httpResponse = new HttpResponse<T>();
            var url = $"{_address}/{id}";
            var client = ApiTestServer.Instance.CreateClient();
            client.AddSecurityToken();

            var response = await client.GetAsync(url);
            httpResponse.StatusCode = response.StatusCode;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var deserializedObject = JsonConvert.DeserializeObject<T>(content);
                httpResponse.Content = deserializedObject;
            }

            return httpResponse;
        }

        public async Task<HttpResponse<T>> PostAsync(T newObject)
        {
            var httpResponse = new HttpResponse<T>();
            var client = ApiTestServer.Instance.CreateClient();
            client.AddSecurityToken();

            var projectContent = JsonConvert.SerializeObject(newObject);
            var stringContent = new StringContent(projectContent);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(_address, stringContent);
            httpResponse.StatusCode = response.StatusCode;

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var content = await response.Content.ReadAsStringAsync();
                var deserializedObject = JsonConvert.DeserializeObject<T>(content);
                httpResponse.Content = deserializedObject;
            }

            return httpResponse;
        }

        public async Task<HttpResponse<T>> PutAsync(string id, T updatedObject)
        {
            var httpResponse = new HttpResponse<T>();
            var url = $"{_address}/{id}";
            var client = ApiTestServer.Instance.CreateClient();
            client.AddSecurityToken();

            var projectContent = JsonConvert.SerializeObject(updatedObject);
            var stringContent = new StringContent(projectContent);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(url, stringContent);
            httpResponse.StatusCode = response.StatusCode;
            return httpResponse;
        }

        public async Task<HttpResponse<T>> DeleteAsync(string id)
        {
            var httpResponse = new HttpResponse<T>();
            var url = $"{_address}/{id}";
            var client = ApiTestServer.Instance.CreateClient();
            client.AddSecurityToken();

            var response = await client.DeleteAsync(url);
            httpResponse.StatusCode = response.StatusCode;
            return httpResponse;
        }
    }
}