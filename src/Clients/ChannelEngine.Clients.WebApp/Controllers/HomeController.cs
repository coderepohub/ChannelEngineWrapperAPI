using ChannelEngine.Clients.WebApp.Models;
using ChannelEngineApiModels;
using ChannelEngineContracts;
using ChannelEngineWrapperAPI.BusinessLogics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChannelEngine.Clients.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChannelEngineAPILogicAgent _channelEngineAPILogicAgent;

        public HomeController(ILogger<HomeController> logger, IChannelEngineAPILogicAgent channelEngineAPILogicAgent)
        {
            _logger = logger;
            _channelEngineAPILogicAgent = channelEngineAPILogicAgent;
        }

        public async Task<IActionResult> Index()
        {
            //First fetch all in progress orders
            var inProgressOrders = await _channelEngineAPILogicAgent.GetInProgressOrders();

            //Getting top 5 in progress task 
            var topFiveOrders = await _channelEngineAPILogicAgent.TopFiveInProgressOrders(inProgressOrders);

            //Update stock of any product to 25
            string productNumber = topFiveOrders.First().MerchantProductNo;
            int locationId = topFiveOrders.First().stockLocationId ?? 0;
            var isUpdated = await _channelEngineAPILogicAgent.UpdateProductStock(topFiveOrders.First().MerchantProductNo, locationId);

            var testcaseModel = new ChannelAPIAgentTestCaseModel
            {
                IsInProgressOrderFetched = inProgressOrders != null && inProgressOrders.Any(),
                IsTopFiveOrdersFetched = topFiveOrders != null && topFiveOrders.Any(),
                IsOrderUpdated = isUpdated ,
                FetchedTopOrders = topFiveOrders 

            };

            return View(testcaseModel);
        }
    }
}