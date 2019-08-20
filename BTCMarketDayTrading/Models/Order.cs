using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTCMarketDayTrading.Models
{
    public class Order
    {
        public long id { get; set; }
        public string currency { get; set; }
        public string instrument { get; set; }
        public string orderSide { get; set; }
        public string orderType { get; set; }
        public long creationTime { get; set; }
        public string status { get; set; }
        public string errorMessage { get; set; }
        public long price { get; set; }
        public long volume { get; set; }
        public long openVolume { get; set; }
        public string clientRequestId { get; set; }
        public List<Trade> trades { get; set; }
    }
}


