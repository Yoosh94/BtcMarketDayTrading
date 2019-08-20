using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BTCMarketDayTrading.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTCMarketDayTrading.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IBtcMarketClient _btcClient;
        public AccountsController(IBtcMarketClient btcClient)
        {
            _btcClient = btcClient;
        }

        [HttpGet]
        [Route("fee")]
        public async Task<ActionResult<double>> GetTradingFee()
        {
            var value = await _btcClient.GetTradingFee();
            return value.tradingFeeRate;
        }

        [HttpGet]
        [Route("transactions")]
        public async Task<IEnumerable<Transaction>> GetTransactions()
        {

            var value = await _btcClient.GetTransaction();
            return value.transactions;
        }

        [HttpGet]
        [Route("transaction/{currency}")]
        public async Task<IEnumerable<Transaction>> GetTransactions(string currency)
        {

            var value = await _btcClient.GetTransaction(currency);
            //var earliest = value.transactions.Where(x => x.action == "Buy Order");
            return value.transactions;
        }

        [HttpPost]
        [Route("order")]
        public async Task<IEnumerable<Order>> GetOrderHistory([FromBody] OrderRequest order)
        {

            var value = await _btcClient.GetOrderHistory(order);
            // This should hopefully give us all the bid orders that went through.
            var bidOnly = value.orders.Where(x => x.orderSide == "Bid" && x.trades.Count > 0);
            return bidOnly;
        }
    }
}
