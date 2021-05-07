using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography;

namespace ExchangeSharp
{
	public partial class ExchangeMXCAPI : ExchangeAPI
	{
		public override string BaseUrl { get; set; } = "https://www.mxc.com/open/api/v2";

		public ExchangeMXCAPI()
		{
			//RequestContentType = "application/x-www-form-urlencoded";
			NonceStyle = NonceStyle.UnixMilliseconds;
			MarketSymbolSeparator = "_";
			MarketSymbolIsUppercase = true;
			//WebSocketOrderBookType = WebSocketOrderBookType.FullBookAlways;
		}

		public override string PeriodSecondsToString(int seconds)
		{
			return CryptoUtility.SecondsToPeriodStringLong(seconds);
		}


		#region Public APIs

		protected override async Task<IEnumerable<string>> OnGetMarketSymbolsAsync()
		{
			var m = await GetMarketSymbolsMetadataAsync();
			return m.Select(x => x.MarketSymbol);
		}

		protected internal override async Task<IEnumerable<ExchangeMarket>> OnGetMarketSymbolsMetadataAsync()
		{
			List<ExchangeMarket> markets = new List<ExchangeMarket>();
			JToken allMarketSymbols = await MakeJsonRequestAsync<JToken>("/market/symbols", BaseUrl, null);
			foreach (var marketSymbol in allMarketSymbols)
			{
				string symbol = marketSymbol["symbol"].ToStringLowerInvariant();
				int idx = symbol.LastIndexOf("_");
				
				var baseCurrency = symbol.Substring(0, idx); 
				var quoteCurrency = symbol.Substring(idx-1, symbol.Length - idx);

				var pricePrecision = marketSymbol["price_scale"].ConvertInvariant<double>();
				var priceStepSize = Math.Pow(10, -pricePrecision).ConvertInvariant<decimal>();
				var amountPrecision = marketSymbol["quantity_scale"].ConvertInvariant<double>();
				var quantityStepSize = Math.Pow(10, -amountPrecision).ConvertInvariant<decimal>();
				var market = new ExchangeMarket
				{
					BaseCurrency = baseCurrency,
					QuoteCurrency = quoteCurrency,
					MarketSymbol = symbol,
					IsActive = true,
					PriceStepSize = priceStepSize,
					QuantityStepSize = quantityStepSize,
					MinPrice = priceStepSize,
					MinTradeSize = quantityStepSize,
				};
				markets.Add(market);
			}
			return markets;
		}

		protected override async Task<ExchangeTicker> OnGetTickerAsync(string marketSymbol)
		{
			JToken ticker = await MakeJsonRequestAsync<JToken>("market/ticker?symbol=" + marketSymbol);
			ExchangeTicker exchangeTicker = new ExchangeTicker();
			exchangeTicker.MarketSymbol = marketSymbol;
			exchangeTicker.Ask = decimal.Parse(ticker.SelectToken("data[0].ask").ToString());
			exchangeTicker.Bid = decimal.Parse(ticker.SelectToken("data[0].bid").ToString());
			exchangeTicker.Last = decimal.Parse(ticker.SelectToken("data[0].last").ToString());
			//exchangeTicker.Volume = decimal.Parse(ticker.SelectToken("data[0].volume").ToString());

			return exchangeTicker;
			//return await this.ParseTickerAsync(ticker["data"], marketSymbol, "ask", "bid", "last", "volume", "volume", "time", TimestampType.UnixMillisecondsDouble);
		}

		protected async override Task<IEnumerable<KeyValuePair<string, ExchangeTicker>>> OnGetTickersAsync()
		{
			List<KeyValuePair<string, ExchangeTicker>> tickers = new List<KeyValuePair<string, ExchangeTicker>>();
			string symbol;
			JToken obj = await MakeJsonRequestAsync<JToken>("/market/ticker", BaseUrl, null);
			Dictionary<string, ExchangeMarket> markets = await this.GetExchangeMarketDictionaryFromCacheAsync();
			foreach (JToken child in obj)
			{
				symbol = child["symbol"].ToStringInvariant();
				if (markets.ContainsKey(symbol))
				{
					tickers.Add(new KeyValuePair<string, ExchangeTicker>(symbol, await this.ParseTickerAsync(child, symbol, "ask", "bid", "last", "volume", "volume")));
				}
			}

			return tickers;
		}

		#endregion
	}
}
