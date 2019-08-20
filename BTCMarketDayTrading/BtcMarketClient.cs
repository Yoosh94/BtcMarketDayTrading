using BTCMarketDayTrading.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace BTCMarketDayTrading
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

        public async Task<Transactions> GetTransaction()
        {
            var endpoint = $"/v2/transaction/history";
            string signature = CreateSignature(endpoint);
            client.DefaultRequestHeaders.Add("signature", signature);
            var response = await client.GetAsync(endpoint);
            var content = await response.Content.ReadAsAsync<Transactions>();
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

        public async Task<OrderResponse> GetOrderHistory(OrderRequest order)
        {
            var endpoint = $"/order/history";
            var jsonString = JsonConvert.SerializeObject(order);
            string signature = CreateSignaturePost(endpoint, jsonString);
            client.DefaultRequestHeaders.Add("signature", signature);
            var response = await client.PostAsync<OrderRequest>(endpoint, order,new JsonMediaTypeFormatter());
            var content = await response.Content.ReadAsAsync<OrderResponse>();
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

        private string CreateSignaturePost(string endpoint,string content)
        {
            var data = $"{endpoint}\n{client.DefaultRequestHeaders.GetValues("timestamp").First()}\n{content}";
            Console.WriteLine(data);
            var encoding = Encoding.UTF8;
            using (var hasher = new HMACSHA512(Convert.FromBase64String(_options.Value.PrivateApiKey)))
            {
                return Convert.ToBase64String(hasher.ComputeHash(encoding.GetBytes(data)));
            }
        }
    }
}
