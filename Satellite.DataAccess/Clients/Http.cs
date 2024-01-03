using Satellite.Interfaces;

namespace Satellite.DataAccess.Services
{
    public class Http : IHttpClient
    {
        HttpClient _httpClient;
        public Http(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return _httpClient.GetAsync(url);
        }
    }
}
