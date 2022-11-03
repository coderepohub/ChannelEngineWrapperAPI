using ChannelEngineAPIClient.Helpers;
using ChannelEngineApiModels;
using ChannelEngineApiModels.APIRequests;
using ChannelEngineApiModels.APIResponses;
using ChannelEngineApiModels.Enums;
using ChannelEngineContracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ChannelEngineAPIClient
{
    public class ChannelEngineApiClient : IChannelEngineApiClient
    {
        private readonly Uri ChannelEngineBaseUri;
        private readonly UrlOptions _options;
        private readonly IHttpClientProviders _httpClientProviders;

        public ChannelEngineApiClient(IOptions<UrlOptions> options, IHttpClientProviders httpClientProviders)
        {
            _options = options.Value;
            ChannelEngineBaseUri = new Uri(_options.Url);
            _httpClientProviders = httpClientProviders;
        }

        ///<inheritdoc/>
        public async Task<OrdersApiResponse> GetInProgressOrdersAsync(string uri)
        {
            var httpResponseMessage = await SendRequest<OrdersApiResponse>(Method.GET, uri);
            var httpResponseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var inProgressOrders = JsonConvert.DeserializeObject<OrdersApiResponse>(httpResponseContent);
            return inProgressOrders;
        }

        ///<inheritdoc/>
        public async Task<bool> UpdateProductStock(IEnumerable<UpdateStockRequest> updateStockRequest, string uri)
        {
            var httpResponseMessage = await SendRequest<IEnumerable<UpdateStockRequest>>(Method.PUT, uri, updateStockRequest);
            var httpResponseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var updateStockResponse = JsonConvert.DeserializeObject<UpdateProductStockResponse>(httpResponseContent);
            if (updateStockResponse != null && updateStockResponse.Success)
                return true;
            return false;
        }

        private async Task<HttpResponseMessage> SendRequest<T>(Method method, string uri, T body = null) where T : class
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentException($"uri can not be null or empty");
            }

            HttpResponseMessage response = new HttpResponseMessage();

            using (var httpClient = new HttpClient(CreateHandler()) { BaseAddress = ChannelEngineBaseUri })
            {
                try
                {
                    switch (method)
                    {
                        case Method.GET:
                            response = await _httpClientProviders.GetJsonAsync(httpClient, uri);
                            break;
                        case Method.PUT:
                            var bodySerialized = JsonConvert.SerializeObject(body);
                            var formattedBody = new StringContent(bodySerialized, Encoding.UTF8, "application/json");
                            response = await _httpClientProviders.PutAsync(httpClient, uri, formattedBody);
                            break;
                        default:
                            response = new HttpResponseMessage()
                            {
                                StatusCode = HttpStatusCode.MethodNotAllowed,
                            };
                            break;
                    }
                }
                catch (Exception ex)
                {

                    response = new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Content = new StringContent(ex.Message)
                    };
                }
                return response;
            }

        }

        private HttpClientHandler CreateHandler()
        {
            var clientHandler = new HttpClientHandler();

            clientHandler.CookieContainer = new CookieContainer();

            return clientHandler;
        }
    }
}