using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Threading.Tasks;
using BTCMarketDayTrading.Service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace BTCMarketDayTrading.Service
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private DateTime unixTime { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            unixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        

        var btcMarketOptions = Configuration.GetValue<BtcMarketParameters>("BtcMarketParameters");

            services.AddHttpClient<IBtcMarketClient, BtcMarketClient>(x =>
            {


                x.BaseAddress = new Uri(btcMarketOptions.BtcMarketBaseUrl);
                x.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                x.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));
                x.DefaultRequestHeaders.Add("apikey", btcMarketOptions.PublicApiKey);
                x.DefaultRequestHeaders.Add("timestamp", GetNetworkTime().ToString());

            });

            services.Configure<BtcMarketParameters>(Configuration.GetSection("BtcMarketParameters"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static double GetNetworkTime()
        {
            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            long unixTime = (long)span.TotalMilliseconds;
            return unixTime;
        }
    }
}
