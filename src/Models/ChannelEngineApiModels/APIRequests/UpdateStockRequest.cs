namespace ChannelEngineApiModels.APIRequests
{
    public class UpdateStockRequest
    {
        public string MerchantProductNo { get; set; }
        public IEnumerable<StockLocation> StockLocations { get; set; }
    }
}
