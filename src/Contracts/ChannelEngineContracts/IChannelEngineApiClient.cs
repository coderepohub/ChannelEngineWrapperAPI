using ChannelEngineApiModels.APIRequests;
using ChannelEngineApiModels.APIResponses;

namespace ChannelEngineContracts
{
    public interface IChannelEngineApiClient
    {
        /// <summary>
        /// Get the In Progress Orders
        /// </summary>
        /// <param name="uri">Uri for the In Proigress API.</param>
        /// <returns>Orders API Response.</returns>
        Task<OrdersApiResponse> GetInProgressOrdersAsync(string uri);

        /// <summary>
        /// Update stock of the Product in a location
        /// </summary>
        /// <param name="updateStockRequest">Update Stock Request.</param>
        /// <param name="uri">Uri of the update product stock.</param>
        /// <returns>Is updated or not. true/false.</returns>
        Task<bool> UpdateProductStock(IEnumerable<UpdateStockRequest> updateStockRequest, string uri);
    }
}