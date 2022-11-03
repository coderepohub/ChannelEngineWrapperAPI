using ChannelEngineApiModels;
using ChannelEngineApiModels.APIRequests;
using ChannelEngineApiModels.APIResponses;
using ChannelEngineContracts;
using ChannelEngineWrapperAPI.BusinessLogics;
using Microsoft.Extensions.Options;
using Moq;

namespace ChannelEngineWrapperAPI.BusineesLogics.Tests
{
    [TestClass]
    public class ChannelEngineAPILogicAgentTests
    {
        private readonly Mock<IChannelEngineApiClient> _channelEngineApiClientMock;
        private readonly IOptions<OrdersUriOptions> _mockOptions;
        private readonly IChannelEngineAPILogicAgent channelEngineAPILogicAgent;

        public ChannelEngineAPILogicAgentTests()
        {
            _channelEngineApiClientMock = new Mock<IChannelEngineApiClient>();
            _mockOptions = Options.Create<OrdersUriOptions>(new OrdersUriOptions()
            {
                GetInProgressOrderUri = "/uri/inprogressapi",
                UpdateStockUri = "/uri/updatestockuri"
            });
            channelEngineAPILogicAgent = new ChannelEngineAPILogicAgent(_channelEngineApiClientMock.Object, _mockOptions);
        }

        /// <summary>
        /// Test to check if OrderApiu return null value
        /// </summary>
        [TestMethod]
        public async Task ChannelEngineAPILogicAgent_GetInProgressOrdersAsync_ReturnsNull()
        {
            OrdersApiResponse ordersApiResponse = null;
            _channelEngineApiClientMock.Setup(c => c.GetInProgressOrdersAsync("/uri/inprogressapi")).ReturnsAsync(ordersApiResponse);

            var getInProgressOrders = await channelEngineAPILogicAgent.GetInProgressOrders();

            Assert.IsNull(getInProgressOrders);
        }

        /// <summary>
        /// Test to check if OrderApi doesnot return sucess value
        /// </summary>
        [TestMethod]
        public async Task ChannelEngineAPILogicAgent_GetInProgressOrdersAsync_ReturnsUnSuccessStatus()
        {
            OrdersApiResponse ordersApiResponse = new OrdersApiResponse()
            {
                StatusCode = 500,
            };
            _channelEngineApiClientMock.Setup(c => c.GetInProgressOrdersAsync("/uri/inprogressapi")).ReturnsAsync(ordersApiResponse);

            var getInProgressOrders = await channelEngineAPILogicAgent.GetInProgressOrders();

            Assert.IsNotNull(getInProgressOrders);
            Assert.AreEqual(getInProgressOrders.Count(), 0);
        }


        /// <summary>
        /// Test to check if OrderApi doesnot return sucess value
        /// </summary>
        [TestMethod]
        public async Task ChannelEngineAPILogicAgent_GetInProgressOrdersAsync_ReturnsValidResult()
        {
            OrdersApiResponse ordersApiResponse = MockedOrdersApiResponse();
            _channelEngineApiClientMock.Setup(c => c.GetInProgressOrdersAsync("/uri/inprogressapi")).ReturnsAsync(ordersApiResponse);

            var getInProgressOrders = await channelEngineAPILogicAgent.GetInProgressOrders();

            Assert.IsNotNull(getInProgressOrders);
            Assert.AreEqual(getInProgressOrders.Count(), 1);
            Assert.AreEqual(getInProgressOrders.First().Id, 1);
            Assert.AreEqual(getInProgressOrders.First().Status, "IN_PROGRESS");
            Assert.AreEqual(getInProgressOrders.First().Lines.Count(), 1);
            Assert.AreEqual(getInProgressOrders.First().Lines.First().Quantity, 1);
        }


        /// <summary>
        /// Test to check if Top In progress ordered products should return value when no result
        /// </summary>
        [TestMethod]
        public async Task ChannelEngineAPILogicAgent_GetTopfiveOrdersAsync_ReturnsNull()
        {
            IEnumerable<OrdersContent> ordersContents = new List<OrdersContent>();


            var getTopProducts = await channelEngineAPILogicAgent.TopFiveInProgressOrders(ordersContents);

            Assert.IsNull(getTopProducts);
        }


        /// <summary>
        /// Test to filter top in progress orders
        /// </summary>
        [TestMethod]
        public async Task ChannelEngineAPILogicAgent_GetTopfiveOrdersAsync_ReturnsTopInProgressOrder()
        {
            var mockedAPI = MockedOrdersApiResponse();
            IEnumerable<OrdersContent> ordersContents = mockedAPI.Content;

            var getTopProducts = await channelEngineAPILogicAgent.TopFiveInProgressOrders(ordersContents);

            Assert.IsNotNull(getTopProducts);
            Assert.AreEqual(getTopProducts.Count(), 2);
            Assert.AreEqual(getTopProducts.First().TotalQuantity, 4);
            Assert.AreEqual(getTopProducts.Last().TotalQuantity, 2);
        }

        /// <summary>
        /// Test to check order stock update
        /// </summary>
        [TestMethod]
        public async Task ChannelEngineAPILogicAgent_UpdateProductStock_ReturnsTrue()
        {
            _channelEngineApiClientMock.Setup(c => c.UpdateProductStock(It.IsAny<IEnumerable<UpdateStockRequest>>(), "/uri/updatestockuri")).ReturnsAsync(true);


            var isProductStockUpdated = await channelEngineAPILogicAgent.UpdateProductStock("product_no", 1);

            Assert.IsTrue(isProductStockUpdated);
        }

        #region Helper Mocked Responses
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