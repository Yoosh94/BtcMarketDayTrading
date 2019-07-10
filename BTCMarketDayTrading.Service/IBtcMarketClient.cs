using BTCMarketDayTrading.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTCMarketDayTrading.Service
{
    public interface IBtcMarketClient
    {
        Task<TradingFee> GetTradingFee();
        Task<Transactions> GetTransaction(string currency);
    }
}
