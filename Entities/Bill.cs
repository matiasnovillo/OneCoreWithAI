namespace OneCore.Entities
{
    public class Bill
    {
        public int BillId { get; set; }
        public string FileName { get; set; }
        public string FileURL { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ProviderName { get; set; }
        public string ProviderAddress { get; set; }
        public string BillingNumber { get; set; }
        public string BillingDate { get; set; }
        public string BillingTotal { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
