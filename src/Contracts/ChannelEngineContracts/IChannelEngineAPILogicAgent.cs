using ChannelEngineApiModels;

namespace ChannelEngineContracts
{
    public interface IChannelEngineAPILogicAgent
    {
        /// <summary>
        /// Get Inprogress Orders
        /// </summary>
        /// <returns>return list of Inprogress Orders</returns>
        Task<IEnumerable<OrdersContent>> GetInProgressOrders();

        /// <summary>
        /// Get the top 5 In progress Orders from the existing In progress Orders
        /// </summary>
        /// <param name="ordersContents"></param>
        /// <returns>returns List of top 5 orders</returns>
        Task<IEnumerable<OrderedProductDetails>> TopFiveInProgressOrders(IEnumerable<OrdersContent> ordersContents);

        /// <summary>
        /// Update the stock of a product to 25. 
        /// </summary>
        /// <param name="productNo">Product No. to udpate the product.</param>
        /// <param name="stockLocationId">Stock Location Id.</param>
        /// <returns></returns>
        Task<bool> UpdateProductStock(string productNo, int stockLocationId);
    }
}
