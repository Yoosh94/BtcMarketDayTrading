using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTCMarketDayTrading.Service.Models
{
    public class OrderRequest
    {
        public string currency { get; set; }
        public string instrument { get; set; }
        public long limit { get; set; }
        public long? since { get; set; }
    }
}