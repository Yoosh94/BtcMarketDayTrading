using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTCMarketDayTrading.Models
{
    public class Transactions
    {
        public bool success { get; set; }
        public IEnumerable<Transaction> transactions { get; set; }
    }
}
