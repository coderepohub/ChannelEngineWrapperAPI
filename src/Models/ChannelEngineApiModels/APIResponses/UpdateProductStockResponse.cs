namespace ChannelEngineApiModels.APIResponses
{
    public class UpdateProductStockResponse
    {
        public dynamic Content { get; set; }
        public int StatusCode { get; set; }
        public string RequestId { get; set; }
        public string LogId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic ValidationErrors { get; set; }
    }
}
