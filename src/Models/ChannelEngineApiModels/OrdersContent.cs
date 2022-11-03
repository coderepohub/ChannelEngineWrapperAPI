namespace ChannelEngineApiModels
{
    public class OrdersContent
    {
        public int Id { get; set; }
        public string ChannelName { get; set; }
        public int ChannelId { get; set; }
        public string GlobalChannelName { get; set; }
        public int GlobalChannelId { get; set; }
        public string ChannelOrderSupport { get; set; }
        public string ChannelOrderNo { get; set; }
        public string MerchantOrderNo { get; set; }
        public string Status { get; set; }
        public bool IsBusinessOrder { get; set; }
        public string AcknowledgedDate { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string MerchantComment { get; set; }
        public MerchantAddressResponse BillingAddress { get; set; }
        public MerchantAddressResponse ShippingAddress { get; set; }
        public decimal SubTotalInclVat { get; set; }
        public decimal SubTotalVat { get; set; }
        public decimal ShippingCostsVat { get; set; }
        public decimal TotalInclVat { get; set; }
        public decimal TotalVat { get; set; }
        public decimal OriginalSubTotalInclVat { get; set; }
        public decimal OriginalSubTotalVat { get; set; }
        public decimal OriginalShippingCostsInclVat { get; set; }
        public decimal OriginalShippingCostsVat { get; set; }
        public decimal OriginalTotalInclVat { get; set; }
        public decimal OriginalTotalVat { get; set; }
        public IEnumerable<MerchantOrderLineResponse> Lines { get; set; }
        public decimal ShippingCostsInclVat { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CompanyRegistrationNo { get; set; }
        public string VatNo { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentReferenceNo { get; set; }
        public string CurrencyCode { get; set; }
        public string OrderDate { get; set; }
        public dynamic ExtraData { get; set; }
    }
}
