using ChannelEngineAPIClient;
using ChannelEngineAPIClient.Helpers;
using ChannelEngineApiModels;
using ChannelEngineContracts;
using ChannelEngineWrapperAPI.BusinessLogics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChannelEngine.Clinets.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false)
       .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            IChannelEngineAPILogicAgent channelEngineApiLogicAgent = serviceProvider.GetRequiredService<IChannelEngineAPILogicAgent>();
            UseCaseMessageDisplayStyle();
            Console.WriteLine("Executing Use Case 1 - Fetching All Inprogress Orders");
            var getAllInProgressOrders = await channelEngineApiLogicAgent.GetInProgressOrders();
            if (getAllInProgressOrders != null && getAllInProgressOrders.Any())
            {
                Console.WriteLine($"Fetched all InProgress Orders. ");
            }
            else
            {
                Console.WriteLine("No Inprogress Orders - case failed");
                return;
            }
            UseCaseMessageDisplayStyle();
            Console.WriteLine("Executing Use Case 2 - Fetching Top 5 Inprogress Orders");
            var getTopOrders = await channelEngineApiLogicAgent.TopFiveInProgressOrders(getAllInProgressOrders);
            if (getTopOrders != null && getTopOrders.Any())
            {
                Console.WriteLine("Displaying Top 5 Inprogress Orders.");
                Console.WriteLine("=========================================");
                Console.WriteLine($"Product Name\t\t\t\t\t GTIN \t Total Quantity");
                Console.WriteLine("=========================================");
                foreach (var topOrder in getTopOrders)
                {
                    Console.WriteLine($"{topOrder.ProductName}\t{topOrder.GTIN}\t{topOrder.TotalQuantity}");
                    Console.WriteLine("---------------------------------------------------------------");
                }
            }
            UseCaseMessageDisplayStyle();
            string productNumber = getTopOrders.First().MerchantProductNo;
            int locationId = getTopOrders.First().stockLocationId ?? 0;
            Console.WriteLine($"Executing Use Case 3 - Updating stock of product - {getTopOrders.First().ProductName} having product number {productNumber} to 25.");
            var isUpdated = await channelEngineApiLogicAgent.UpdateProductStock(getTopOrders.First().MerchantProductNo, locationId);
            if (isUpdated)
            {
                Console.WriteLine("Updated the stock.");
            }
            else
            {
                Console.WriteLine("Updation failed.");
            }

            Console.ReadLine();
        }

        private static void UseCaseMessageDisplayStyle()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=============================================");
            Console.WriteLine("=============================================");
            Console.ResetColor();
        }

        private static void ConfigureServices(ServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions<UrlOptions>().Bind(configuration.GetSection(UrlOptions.Name));
            serviceCollection.AddOptions<OrdersUriOptions>().Bind(configuration.GetSection(OrdersUriOptions.Name));

            serviceCollection.AddSingleton<IHttpClientProviders, HttpClientProviders>();
            serviceCollection.AddSingleton<IChannelEngineApiClient, ChannelEngineApiClient>();
            serviceCollection.AddSingleton<IChannelEngineAPILogicAgent, ChannelEngineAPILogicAgent>();
        }

    }
}