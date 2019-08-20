using BTCMarketDayTrading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTCMarketDayTrading
{
    public interface IBtcMarketClient
    {
        Task<TradingFee> GetTradingFee();
        Task<Transactions> GetTransaction();
        Task<Transactions> GetTransaction(string currency);
        Task<OrderResponse> GetOrderHistory(OrderRequest order);
    }
}
