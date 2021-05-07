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
	public partial class Exchange1InchAPI: ExchangeAPI
	{
		public override string BaseUrl { get; set; } = "https://api.1inch.exchange/v3.0/56";

		public string BaseBNBURL { get; set; } = "https://api.1inch.exchange/v3.0/56";

		public string BaseETHURL { get; set; } = "https://api.1inch.exchange/v3.0/1";

		public string QuoteBNBURL { get; set; } = "/quotes?deepLevel=2&mainRouteParts=20&parts=80&virtualParts=80&fromTokenAddress={0}&toTokenAddress={1}&amount={2}&gasPrice=5000000000&protocolWhiteList=WBNB,BURGERSWAP,PANCAKESWAP,VENUS,JULSWAP,BAKERYSWAP,BSC_ONE_INCH_LP,ACRYPTOS,BSC_DODO,APESWAP,SPARTAN,BELTSWAP,VSWAP,VPEGSWAP,HYPERSWAP,BSC_DODO_V2,SWAPSWIPE,ELLIPSIS_FINANCE,NERVE,BSC_SMOOTHY_FINANCE,CHEESESWAP,BSC_PMM1,PANCAKESWAP_V2,URANIUM,MDEX&protocols=WBNB,BURGERSWAP,PANCAKESWAP,VENUS,JULSWAP,BAKERYSWAP,BSC_ONE_INCH_LP,ACRYPTOS,BSC_DODO,APESWAP,SPARTAN,BELTSWAP,VSWAP,VPEGSWAP,HYPERSWAP,BSC_DODO_V2,SWAPSWIPE,ELLIPSIS_FINANCE,NERVE,BSC_SMOOTHY_FINANCE,CHEESESWAP,BSC_PMM1,PANCAKESWAP_V2,URANIUM,MDEX&deepLevels=1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1&mainRoutePartsList=1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1&partsList=1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1&virtualPartsList=1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";

		private List<InchTokenObject> tokens = new List<InchTokenObject>();

		public Exchange1InchAPI()
		{
			NonceStyle = NonceStyle.UnixMilliseconds;
			MarketSymbolSeparator = string.Empty;
			MarketSymbolIsUppercase = true;
		}

		public override string PeriodSecondsToString(int seconds)
		{
			return CryptoUtility.SecondsToPeriodStringLong(seconds);
		}


		#region Public APIs

		public async Task<IEnumerable<InchTokenObject>> GetTokensMetadataAsync()
		{
			List<InchTokenObject> markets = new List<InchTokenObject>();

			JToken allMarketSymbols = await MakeJsonRequestAsync<JToken>("/tokens", BaseUrl, null);

			foreach (var marketSymbol in allMarketSymbols.First.First)
			{
				InchTokenObject inchTokenObject = marketSymbol.First.ToObject<InchTokenObject>();

				markets.Add(inchTokenObject);
			}

			tokens = markets;

			return markets;
		}


		public async Task<ExchangeTicker> GetQuoteAsync(string marketSymbol, decimal estimateAmount)
		{
			var token = tokens.Where(p => p.symbol == marketSymbol);
			var usdtToken = tokens.Where(p => p.symbol == "USDT");
			if (token!= null && token.Count() > 0)
			{
				string fromAddress = token.FirstOrDefault().address;  
				string toAddress = usdtToken.FirstOrDefault().address;

				decimal amount = decimal.Floor(estimateAmount*(decimal)Math.Pow(10,token.FirstOrDefault().decimals));

				JToken ticker = await MakeJsonRequestAsync<JToken>("/quote?fromTokenAddress=" + fromAddress + "&toTokenAddress=" + toAddress + "&amount=" + amount);
				//JToken ticker = await MakeJsonRequestAsync<JToken>(string.Format(QuoteBNBURL, fromAddress, toAddress, amount));

				//decimal toTokenAmount = decimal.Parse(ticker["toTokenAmount"]);
				ExchangeTicker exchangeTicker = new ExchangeTicker();
				exchangeTicker.MarketSymbol = marketSymbol;

				decimal toAmount = decimal.Parse(ticker.SelectToken("toTokenAmount").ToString());
				//decimal fromAmount = 1000000000000000000000m;

				exchangeTicker.Last = toAmount/amount;

				return exchangeTicker;

				//return await this.ParseTickerAsync(ticker["data"], marketSymbol, "ask", "bid", "last", "volume", "volume", "time", TimestampType.UnixMillisecondsDouble);

			}
			//string fromAddress = tokens.Where(p => p.name == marketSymbol).FirstOrDefault().address;

			return null;
		}

		#endregion
	}
}
