using BTCMarketDayTrading.Service.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BTCMarketDayTrading.Service
{
    public class BtcMarketClient : IBtcMarketClient
    {
        private readonly HttpClient client;
        private readonly IOptions<BtcMarketParameters> _options;

        public BtcMarketClient(HttpClient client, IOptions<BtcMarketParameters> options)
        {
            this.client = client;
            _options = options;
        }
        public async TradingFee GetTradingFee()
        {
            var endpoint = "/account/{instrument}/{currency}/tradingfee";
            var response =  await client.GetAsync(endpoint);
            var content = response.Content.ReadAsAsync<TradingFee>();
            return content;
        }
    }
}
