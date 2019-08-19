using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTCMarketDayTrading.Service.Models
{
    public class Trade
    {
        public long id { get; set; }
        public long creationTime { get; set; }
        public string description { get; set; }
        public long price { get; set; }
        public long volume { get; set; }
        public long fee { get; set; }
    }
}

