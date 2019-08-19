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
    public class ValuesController : ControllerBase
    {
        private readonly IBtcMarketClient _btcClient;
        public ValuesController(IBtcMarketClient btcClient)
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
        public async Task<List<Order>> GetOrderHistory([FromBody] OrderRequest order)
        {

            var value = await _btcClient.GetOrderHistory(order);
            //var earliest = value.transactions.Where(x => x.action == "Buy Order");
            return value.orders;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
