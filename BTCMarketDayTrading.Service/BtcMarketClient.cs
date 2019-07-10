using BTCMarketDayTrading.Service.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
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
        public async Task<TradingFee> GetTradingFee()
        {
            var endpoint = "/account/BTC/AUD/tradingfee";
            string signature = CreateSignature(endpoint);
            client.DefaultRequestHeaders.Add("signature", signature);
            var response =  await client.GetAsync(endpoint);
            var content = await response.Content.ReadAsAsync<TradingFee>();
            return content;
        }

        public async Task<Transactions> GetTransaction(string currency)
        {
            var endpoint = $"/v2/transaction/history/{currency}";
            string signature = CreateSignature(endpoint);
            client.DefaultRequestHeaders.Add("signature", signature);
            var response = await client.GetAsync(endpoint);
            var content = await response.Content.ReadAsAsync<Transactions>();
            return content;
        }

        private string CreateSignature(string endpoint)
        {
            var data = $"{endpoint}\n{client.DefaultRequestHeaders.GetValues("timestamp").First()}\n";
            var encoding = Encoding.UTF8;
            using (var hasher = new HMACSHA512(Convert.FromBase64String(_options.Value.PrivateApiKey)))
            {
                return Convert.ToBase64String(hasher.ComputeHash(encoding.GetBytes(data)));
            }
        }
    }
}
