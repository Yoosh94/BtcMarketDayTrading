namespace BTCMarketDayTrading.Service.Models
{
    public class Transaction
    {
        public int id { get; set; }
        public long balance { get; set; }
        public long amount { get; set; }
        public string action { get; set; }
        public string recordType { get; set; }
        public string currency { get; set; }
    }
}
