using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace KanBanAuthpassthru.TypedClients
{
    public class ApiClient
    {
        public readonly HttpClient HttpClient;
        public ApiClient(HttpClient client)
        {
            HttpClient = client;
            HttpClient.BaseAddress = new Uri("https://localhost:7007");
        }

        public async Task<HttpResponseMessage> MakeGetCallAsync(HttpRequest request, CancellationToken token)
        {
            HttpRequestMessage hrm = new HttpRequestMessage(HttpMethod.Get, $"{request.Path}{request.QueryString.Value!}");
            return await HttpClient.SendAsync(hrm, token);
        }

        public async Task<HttpResponseMessage> MakePostCallAsync(HttpRequest request, CancellationToken token)
        {            
            var content = new StreamContent(request.Body);

            content.Headers.ContentType = new MediaTypeHeaderValue(request.Headers.ContentType);

            HttpRequestMessage hrm = new HttpRequestMessage(HttpMethod.Post, request.Path);

            hrm.Content = content;
            hrm.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(request.Headers.Accept));

            return await HttpClient.SendAsync(hrm, token);
        }
    }
}
