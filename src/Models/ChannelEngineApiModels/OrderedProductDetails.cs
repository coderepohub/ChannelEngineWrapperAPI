namespace ChannelEngineApiModels
{
    public class OrderedProductDetails
    {
        public string ProductName { get; set; }
        public string GTIN { get; set; }
        public int TotalQuantity { get; set; }

        public int? stockLocationId { get; set; }

        public string MerchantProductNo { get; set; }
    }
}
