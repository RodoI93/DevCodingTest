using System;
using System.Net.Http.Headers;

namespace DevCodingTest.Services
{
    public class HttpClientService
    {
        HttpClient _httpClient;
        public HttpClientService() 
        {
            
        }

        public async Task<HttpResponseMessage> GetData(string uri)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.BaseAddress = new Uri(uri);

            HttpResponseMessage response = await _httpClient.GetAsync("");

            return response;
        }
    }
}
