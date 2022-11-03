namespace ChannelEngineApiModels.APIResponses
{
    public class OrdersApiResponse
    {
        public IEnumerable<OrdersContent> Content { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int StatusCode { get; set; }
        public string RequestId { get; set; }
        public string LogId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic ValidationErrors { get; set; }
    }
}
