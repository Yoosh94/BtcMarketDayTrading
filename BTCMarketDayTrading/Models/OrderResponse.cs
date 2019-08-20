using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTCMarketDayTrading.Models
{
    public class OrderResponse
    {
        public bool success { get; set; }
        public int? errorCode { get; set; }
        public string errorMessage { get; set; }
        public List<Order> orders { get; set; }
    }
}

