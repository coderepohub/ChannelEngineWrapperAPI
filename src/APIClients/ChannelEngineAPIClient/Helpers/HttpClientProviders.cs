namespace ChannelEngineAPIClient.Helpers
{
    public class HttpClientProviders : IHttpClientProviders
    {
        ///<inheritdoc/>
        public async Task<HttpResponseMessage> GetJsonAsync(HttpClient httpClient, string uri)
        {
            return await httpClient.GetAsync(uri);
        }

        ///<inheritdoc/>
        public async Task<HttpResponseMessage> PutAsync(HttpClient httpClient, string uri,StringContent body)
        {
            return await httpClient.PutAsync(uri,body);
        }
    }
}
