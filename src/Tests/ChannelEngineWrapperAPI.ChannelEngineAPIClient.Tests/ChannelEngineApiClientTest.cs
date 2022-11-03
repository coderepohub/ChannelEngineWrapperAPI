using ChannelEngineAPIClient;
using ChannelEngineAPIClient.Helpers;
using ChannelEngineApiModels;
using ChannelEngineApiModels.APIRequests;
using ChannelEngineApiModels.APIResponses;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System.Net;

namespace ChannelEngineWrapperAPI.ChannelEngineAPIClient.Tests
{
    [TestClass]
    public class ChannelEngineApiClientTest
    {
        private readonly Mock<IHttpClientProviders> _mockHttpClientProviders;
        private readonly IOptions<UrlOptions> _mockOptions;
        private readonly ChannelEngineApiClient channelEngineApiClient;
        public ChannelEngineApiClientTest()
        {
            _mockHttpClientProviders = new Mock<IHttpClientProviders>();
            _mockOptions = Options.Create<UrlOptions>(new UrlOptions()
            {
                Url = "https://mockuri/api/v2/"
            });
            channelEngineApiClient = new ChannelEngineApiClient(_mockOptions, _mockHttpClientProviders.Object);
        }

        /// <summary>
        /// Test to check in progress orders api.
        /// </summary>
        [TestMethod]
        public async Task ChannelEngineApiClient_GetInProgressOrdersAsync()
        {
            var orderapiresponse = MockedOrdersApiResponse();
            var httpResponseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(orderapiresponse))
            };

            _mockHttpClientProviders.Setup(c => c.GetJsonAsync(It.IsAny<HttpClient>(), It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            var response = await channelEngineApiClient.GetInProgressOrdersAsync("/mock/uri");

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsNotNull(response.Content);
            Assert.IsNotNull(response.Content.First());
            Assert.AreEqual(1, response.Content.First().Id);

        }

        /// <summary>
        /// Test to check update product stock is true.
        /// </summary>
        [TestMethod]
        public async Task ChannelEngineApiClient_UpdateProductStock_True()
        {
            var updateStockResponse = MockedUpdateProductStockResponse_Success();
            var httpResponseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(updateStockResponse))
            };

            var updateStockRequests = new List<UpdateStockRequest>()
            {
                new UpdateStockRequest()
                {
                    MerchantProductNo = "productnumber_1",
                    StockLocations = new List<StockLocation>()
                    {
                        new StockLocation()
                        {
                            Stock = "25",
                            StockLocationId = 2
                        }
                    }
                }
            };

            _mockHttpClientProviders.Setup(c => c.PutAsync(It.IsAny<HttpClient>(), It.IsAny<string>(),It.IsAny<StringContent>())).ReturnsAsync(httpResponseMessage);

            var response = await channelEngineApiClient.UpdateProductStock(updateStockRequests, "/mock/uri");

            Assert.IsNotNull(response);
            Assert.IsTrue(response);
        }

        /// <summary>
        /// Test to check update product stock is true.
        /// </summary>
        [TestMethod]
        public async Task ChannelEngineApiClient_UpdateProductStock_False()
        {
            var updateStockResponse = MockedUpdateProductStockResponse_Fail();
            var httpResponseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(updateStockResponse))
            };

            var updateStockRequests = new List<UpdateStockRequest>()
            {
                new UpdateStockRequest()
                {
                    MerchantProductNo = "productnumber_1",
                    StockLocations = new List<StockLocation>()
                    {
                        new StockLocation()
                        {
                            Stock = "25",
                            StockLocationId = 2
                        }
                    }
                }
            };

            _mockHttpClientProviders.Setup(c => c.PutAsync(It.IsAny<HttpClient>(), It.IsAny<string>(), It.IsAny<StringContent>())).ReturnsAsync(httpResponseMessage);

            var response = await channelEngineApiClient.UpdateProductStock(updateStockRequests, "/mock/uri");

            Assert.IsNotNull(response);
            Assert.IsFalse(response);
        }

        #region Helper Mocked Responses

        private UpdateProductStockResponse MockedUpdateProductStockResponse_Success()
        {
            return new UpdateProductStockResponse()
            {
                Message = "Success",
                Success = true
            };
        }

        private UpdateProductStockResponse MockedUpdateProductStockResponse_Fail()
        {
            return new UpdateProductStockResponse()
            {
                Message = "Fail",
                Success = false
            };
        }

        private OrdersApiResponse MockedOrdersApiResponse()
        {

            return new OrdersApiResponse()
            {
                Content = new List<OrdersContent>()
                {
                    new OrdersContent()
                    {
                        Status = "IN_PROGRESS",
                        Id = 1,
                        ChannelId = 1,
                        GlobalChannelId = 55,
                        ChannelOrderSupport = "SPLIT_ORDER_LINES",
                        ChannelOrderNo = "CE-TEST-32480",
                        MerchantOrderNo = "GetInProgressData5",
                        SubTotalInclVat = 7,
                        SubTotalVat = 1,
                        ShippingCostsVat = 0,
                        TotalInclVat =7,
                        TotalVat = 1,
                        Lines = new List<MerchantOrderLineResponse>()
                        {
                            new MerchantOrderLineResponse()
                            {
                                Status = "IN_PROGRESS",
                                IsFulfillmentByMarketplace = false,
                                Gtin = "8719351029609",
                                Description = "T-shirt met lange mouw BASIC petrol: S",
                                MerchantProductNo = "001201-S",
                                Quantity = 1,
                            }

                        }
                    },
                    new OrdersContent()
                    {
                        Status = "IN_PROGRESS",
                        Id = 2,
                        ChannelId = 1,
                        GlobalChannelId = 55,
                        ChannelOrderSupport = "SPLIT_ORDER_LINES",
                        ChannelOrderNo = "CE-TEST-32480",
                        MerchantOrderNo = "GetInProgressData5",
                        SubTotalInclVat = 7,
                        SubTotalVat = 1,
                        ShippingCostsVat = 0,
                        TotalInclVat =7,
                        TotalVat = 1,
                        Lines = new List<MerchantOrderLineResponse>()
                        {
                            new MerchantOrderLineResponse()
                            {
                                Status = "IN_PROGRESS",
                                IsFulfillmentByMarketplace = false,
                                Gtin = "8719351029609",
                                Description = "T-shirt met lange mouw BASIC petrol: S",
                                Quantity = 3,
                                MerchantProductNo = "001201-S",
                            },
                             new MerchantOrderLineResponse()
                            {
                                Status = "IN_PROGRESS",
                                IsFulfillmentByMarketplace = false,
                                Gtin = "8719351029609",
                                Description = "T-shirt met lange mouw BASIC petrol: M",
                                Quantity = 2,
                                MerchantProductNo = "001201-M",
                            }


                        }
                    }
                },
                StatusCode = 200

            };
        }
        #endregion
    }
}