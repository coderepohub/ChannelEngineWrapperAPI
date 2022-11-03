using ChannelEngineApiModels;
using System.ComponentModel;

namespace ChannelEngine.Clients.WebApp.Models
{
    public class ChannelAPIAgentTestCaseModel
    {
        [DisplayName("Is In Progress Orders Fetched Sucessfully")]
        public bool IsInProgressOrderFetched { get; set; }

        [DisplayName("Is Top Orders Fetched Sucessfully")]
        public bool IsTopFiveOrdersFetched { get; set; }

        [DisplayName("Is Stock Updated Sucessfully")]
        public bool IsOrderUpdated { get; set; }
        public IEnumerable<OrderedProductDetails> FetchedTopOrders { get; set; }
    }

}
