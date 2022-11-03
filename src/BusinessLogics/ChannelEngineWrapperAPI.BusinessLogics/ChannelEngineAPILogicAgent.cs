using ChannelEngineApiModels;
using ChannelEngineApiModels.APIRequests;
using ChannelEngineContracts;
using Microsoft.Extensions.Options;
using System.Net;

namespace ChannelEngineWrapperAPI.BusinessLogics
{
    public class ChannelEngineAPILogicAgent : IChannelEngineAPILogicAgent
    {
        private IChannelEngineApiClient _channelEngineApiClient;
        private readonly OrdersUriOptions _options;
        public ChannelEngineAPILogicAgent(IChannelEngineApiClient channelEngineApiClient, IOptions<OrdersUriOptions> options)
        {
            _channelEngineApiClient = channelEngineApiClient;
            _options = options.Value;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<OrdersContent>> GetInProgressOrders()
        {
            string uri = _options.GetInProgressOrderUri;
            var inProgressOrders = await _channelEngineApiClient.GetInProgressOrdersAsync(uri);
            if (inProgressOrders == null)
                return null;
            if (inProgressOrders.StatusCode != (int)HttpStatusCode.OK)
            {
                return new List<OrdersContent>();
            }
            return inProgressOrders.Content;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<OrderedProductDetails>> TopFiveInProgressOrders(IEnumerable<OrdersContent> ordersContents)
        {

            var groupedProducts = ordersContents.SelectMany(x => x.Lines).ToList().GroupBy(c=>c.MerchantProductNo);

            if (groupedProducts == null || !groupedProducts.Any())
                return null;
            List<OrderedProductDetails> orderedProducts = new List<OrderedProductDetails>();
            foreach (var products in groupedProducts)
            {
                int quantity = 0;
                foreach (var item in products)
                {
                    quantity = quantity + item.Quantity;
                }
                orderedProducts.Add(new OrderedProductDetails()
                {
                    ProductName = products.First().Description,
                    GTIN = products.First().Gtin,
                    TotalQuantity = quantity,
                    MerchantProductNo = products.First().MerchantProductNo,
                    stockLocationId = products.First().StockLocation?.Id
                }) ;
            }

            var topfiveInProgressOrders = orderedProducts.OrderByDescending(c => c.TotalQuantity).Take(5).ToList();
            return topfiveInProgressOrders;
        }

        ///<inheritdoc/>
        public async Task<bool> UpdateProductStock(string productNo, int stockLocationId)
        {
            string uri = _options.UpdateStockUri;
            var updateStockRequest = new List<UpdateStockRequest>()
            {
                new UpdateStockRequest
                {
                    MerchantProductNo = productNo,
                    StockLocations = new List<StockLocation>
                    {
                        new StockLocation()
                        {
                            StockLocationId = stockLocationId,
                            Stock = "25"
                        }
                    }
                }
            };
            var response = await _channelEngineApiClient.UpdateProductStock(updateStockRequest, uri);
            return response;
        }
    }
}