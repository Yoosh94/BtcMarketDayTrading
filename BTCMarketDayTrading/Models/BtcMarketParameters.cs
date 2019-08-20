using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTCMarketDayTrading.Models
{
    public class BtcMarketParameters
    {
        public string BtcMarketBaseUrl { get; set; }
        public string PrivateApiKey { get; set; }
        public string PublicApiKey { get; set; }
    }
}
