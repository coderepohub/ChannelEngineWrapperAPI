namespace ChannelEngineAPIClient.Helpers
{
    /// <summary>
    /// Http Client Providers
    /// </summary>
    public interface IHttpClientProviders
    {
        /// <summary>
        /// Http Get Method call.
        /// </summary>
        /// <param name="httpClient">Http client.</param>
        /// <param name="uri">uri path of the api.</param>
        /// <returns>Http Response Message.</returns>
        Task<HttpResponseMessage> GetJsonAsync(HttpClient httpClient,string uri);

        /// <summary>
        /// Http Put Method Call.
        /// </summary>
        /// <param name="httpClient">Http Client</param>
        /// <param name="uri">uri path of the api.</param>
        /// <param name="body">Body to be passed in stringcontent.</param>
        /// <returns>HttpResponse Message.</returns>
        Task<HttpResponseMessage> PutAsync(HttpClient httpClient, string uri, StringContent body);
    }
}
