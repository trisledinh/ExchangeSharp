using System;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using ExchangeSharp;
using System.Collections.Generic;
using ExchangeSharpWinForms.Entities;
using ExchangeSharp.TelegramBot;

namespace ExchangeSharpWinForms
{
	public partial class MainForm : Form
	{
		private decimal rateUSDTVND = 24450;
		private decimal rateKRWVND = 20.5M;

		//private decimal rateUSDTVND2 = 24450;
		//private decimal rateKRWVND2 = 20.5M;

		//private decimal rateUSDTVND3 = 24450;
		//private decimal rateKRWVND3 = 20.5M;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			TelegramBot telegramBot = new TelegramBot();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());

		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			System.Threading.Timer timer = new System.Threading.Timer(new System.Threading.TimerCallback(MyIntervalFunction));
			timer.Change(0, 1000*60*15);
		}

		private void MyIntervalFunction(object obj)
		{
			this.Invoke((MethodInvoker)delegate
				{
					FetchTickersBNB("XRP");
				});
					
		}

		public async void FetchTickersBNB(string symbol)
		{
			try
			{
				var binanceAPI = new ExchangeBinanceAPI();

				var bnbTickers = await binanceAPI.GetTickersAsync();
				bnbTickers = bnbTickers.Where(p => p.Key.ToUpper().Contains(symbol));

				var bithumpAPI = new ExchangeBithumbAPI();
				var bithumpTickers = await bithumpAPI.GetTickersAsync();

				if (!string.IsNullOrEmpty(txtCoinNameBNB.Text))
					bithumpTickers = bithumpTickers.Where(p => p.Key.ToUpper().Contains(symbol));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var bnbTicker in bnbTickers)
				{
					foreach (var bithumpTicker in bithumpTickers)
					{
						if (bnbTicker.Key.Replace("USDT", "") == bithumpTicker.Key)
						{
							Quote item = new Quote();
							item.Ticker = bnbTicker.Key.ToUpper();
							item.Price1 = bnbTicker.Value.Last;
							item.Price2 = bithumpTicker.Value.Last;

							decimal total1 = item.Price1 * rateUSDTVND;
							decimal percent = (item.Price2 * rateKRWVND * 0.9975M - total1) / total1 * 100;
							item.Percent = percent;

							if (percent > TelegramBot.minRatePercent)
							{
								TelegramBot.SendMessageToChannel($"Different between BNB and Bithumb for {symbol} with USDT rate: {rateUSDTVND} and KRW Rate : {rateKRWVND}:\r\n {percent}", null);
							}	
						}
					}
				}

				//textTickersResult.Text = b.ToString();
			}
			catch(Exception ex)
			{

			}
		}
		private async void FetchTickersBNB()
		{
			//if (!Created || string.IsNullOrWhiteSpace(cmbExchange.SelectedItem as string))
			//{
			//    return;
			//}

			if (string.IsNullOrEmpty(txtRateUSDT.Text))
			{
				MessageBox.Show("Please type Rate USDT to VND (for example: 24450)!");
				return;
			}

			if (string.IsNullOrEmpty(txtRateKRW.Text))
			{
				MessageBox.Show("Please type Rate KRW to VND (for example: 20.5)!");
				return;
			}

			rateUSDTVND = decimal.Parse(txtRateUSDT.Text);
			rateKRWVND = decimal.Parse(txtRateKRW.Text);

			this.UseWaitCursor = true;
			try
			{
				var binanceAPI = new ExchangeBinanceAPI();

				var bnbTickers = await binanceAPI.GetTickersAsync();
				bnbTickers = bnbTickers.Where(p => p.Key.ToUpper().Contains("USDT"));

				if (!string.IsNullOrEmpty(txtCoinNameBNB.Text))
					bnbTickers = bnbTickers.Where(p => p.Key.ToUpper().Contains(txtCoinNameBNB.Text.ToUpper()));
				//bnbTickers = bnbTickers.Where(p => p.Key.Contains("USDT") && !p.Key.Contains("UP") && !p.Key.Contains("DOWN") && !p.Key.Contains("BEAR") && !p.Key.Contains("BULL")
				//&& !p.Key.Contains("BTCST") && !p.Key.Contains("DREP") && !p.Key.Contains("YFII") && !p.Key.Contains("BCH") && !p.Key.Contains("BZRX"));

				//BULL BTCST DREP YFII BCHSV BCHABC BZRZ

				var bithumpAPI = new ExchangeBithumbAPI();
				var bithumpTickers = await bithumpAPI.GetTickersAsync();

				if (!string.IsNullOrEmpty(txtCoinNameBNB.Text))
					bithumpTickers = bithumpTickers.Where(p => p.Key.ToUpper().Contains(txtCoinNameBNB.Text.ToUpper()));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var bnbTicker in bnbTickers)
				{
					foreach (var bithumpTicker in bithumpTickers)
					{
						if (bnbTicker.Key.Replace("USDT", "") == bithumpTicker.Key)
						{
							Quote item = new Quote();
							item.Ticker = bnbTicker.Key.ToUpper();
							item.Price1 = bnbTicker.Value.Last;
							item.Price2 = bithumpTicker.Value.Last;

							decimal total1 = item.Price1 * rateUSDTVND;
							decimal percent = (item.Price2 * rateKRWVND * 0.9975M - total1) / total1 * 100;
							item.Percent = percent;

							listQoutes.Add(item);
						}
					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				drgBnbKRW.DataSource = listQoutes;
				drgBnbKRW.Show();

				this.UseWaitCursor = false;
				//textTickersResult.Text = b.ToString();
			}
			catch (Exception ex)
			{
				//textTickersResult.Text = ex.ToString();
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.UseWaitCursor = false;
				//Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		private async void FetchTickersHuobi()
		{
			if (string.IsNullOrEmpty(txtRateUSDT2.Text))
			{
				MessageBox.Show("Please type Rate USDT to VND (for example: 24450)!");
				return;
			}

			if (string.IsNullOrEmpty(txtRateKRW2.Text))
			{
				MessageBox.Show("Please type Rate KRW to VND (for example: 20.5)!");
				return;
			}

			rateUSDTVND = decimal.Parse(txtRateUSDT2.Text);
			rateKRWVND = decimal.Parse(txtRateKRW2.Text);

			this.UseWaitCursor = true;
			try
			{
				var houbiAPI = new ExchangeHuobiAPI();

				var huobiTickers = await houbiAPI.GetTickersAsync();
				huobiTickers = huobiTickers.Where(p => p.Key.ToUpper().Contains("USDT"));

				var bithumpAPI = new ExchangeBithumbAPI();
				var bithumpTickers = await bithumpAPI.GetTickersAsync();

				if (!string.IsNullOrEmpty(txtCoinNameHuobi.Text))
					huobiTickers = huobiTickers.Where(p => p.Key.ToUpper().Contains(txtCoinNameHuobi.Text.ToUpper()));

				if (!string.IsNullOrEmpty(txtCoinNameHuobi.Text))
					bithumpTickers = bithumpTickers.Where(p => p.Key.ToUpper().Contains(txtCoinNameHuobi.Text.ToUpper()));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var huobiTicker in huobiTickers)
				{
					foreach (var bithumpTicker in bithumpTickers)
					{
						if (huobiTicker.Key.ToUpper().Replace("USDT", "") == bithumpTicker.Key)
						{
							Quote item = new Quote();
							item.Ticker = huobiTicker.Key.ToUpper();
							item.Price1 = huobiTicker.Value.Last;
							item.Price2 = bithumpTicker.Value.Last;

							decimal total1 = item.Price1 * rateUSDTVND;
							decimal percent = (item.Price2 * rateKRWVND * 0.9975M - total1) / total1 * 100;
							item.Percent = percent;

							listQoutes.Add(item);
						}
					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				drgHuobiKRW.DataSource = listQoutes;
				drgHuobiKRW.Show();

				this.UseWaitCursor = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.UseWaitCursor = false;
				//Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		private async void FetchTickersCoinbase()
		{
			if (string.IsNullOrEmpty(txtRateUSDT3.Text))
			{
				MessageBox.Show("Please type Rate USDT to VND (for example: 24450)!");
				return;
			}

			if (string.IsNullOrEmpty(txtRateKRW3.Text))
			{
				MessageBox.Show("Please type Rate KRW to VND (for example: 20.5)!");
				return;
			}

			rateUSDTVND = decimal.Parse(txtRateUSDT3.Text);
			rateKRWVND = decimal.Parse(txtRateKRW3.Text);

			this.UseWaitCursor = true;
			try
			{
				var coinbaseAPI = new ExchangeCoinbaseAPI();

				var coinbaseTickers = await coinbaseAPI.GetTickersAsync();
				coinbaseTickers = coinbaseTickers.Where(p => p.Key.ToUpper().Contains("USD"));

				var bithumpAPI = new ExchangeBithumbAPI();
				var bithumpTickers = await bithumpAPI.GetTickersAsync();

				if (!string.IsNullOrEmpty(txtCoinName3.Text))
					coinbaseTickers = coinbaseTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName3.Text.ToUpper()));

				if (!string.IsNullOrEmpty(txtCoinName3.Text))
					bithumpTickers = bithumpTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName3.Text.ToUpper()));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var coinbaseTicker in coinbaseTickers)
				{
					foreach (var bithumpTicker in bithumpTickers)
					{
						if (coinbaseTicker.Key.ToUpper().Replace("-USD", "") == bithumpTicker.Key)
						{
							Quote item = new Quote();
							item.Ticker = coinbaseTicker.Key.ToUpper();
							item.Price1 = coinbaseTicker.Value.Last;
							item.Price2 = bithumpTicker.Value.Last;

							decimal total1 = item.Price1 * rateUSDTVND;
							decimal percent = (item.Price2 * rateKRWVND * 0.9975M - total1) / total1 * 100;
							item.Percent = percent;

							listQoutes.Add(item);
						}
					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				drgCoinbase.DataSource = listQoutes;
				drgCoinbase.Show();

				this.UseWaitCursor = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.UseWaitCursor = false;
				//Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		private async void FetchTickersBittrex()
		{
			if (string.IsNullOrEmpty(txtRateUSDT4.Text))
			{
				MessageBox.Show("Please type Rate USDT to VND (for example: 24450)!");
				return;
			}

			if (string.IsNullOrEmpty(txtRateKRW4.Text))
			{
				MessageBox.Show("Please type Rate KRW to VND (for example: 20.5)!");
				return;
			}

			rateUSDTVND = decimal.Parse(txtRateUSDT4.Text);
			rateKRWVND = decimal.Parse(txtRateKRW4.Text);

			this.UseWaitCursor = true;
			try
			{
				var bittrexAPI = new ExchangeBittrexAPI();

				var bittrexTickers = await bittrexAPI.GetTickersAsync();
				bittrexTickers = bittrexTickers.Where(p => p.Key.ToUpper().Contains("USDT"));

				var bithumpAPI = new ExchangeBithumbAPI();
				var bithumpTickers = await bithumpAPI.GetTickersAsync();

				if (!string.IsNullOrEmpty(txtCoinName4.Text))
					bittrexTickers = bittrexTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName4.Text.ToUpper()));

				if (!string.IsNullOrEmpty(txtCoinName4.Text))
					bithumpTickers = bithumpTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName4.Text.ToUpper()));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var bittrexTicker in bittrexTickers)
				{
					foreach (var bithumpTicker in bithumpTickers)
					{
						if (bittrexTicker.Key.ToUpper().Replace(@"USDT-", "") == bithumpTicker.Key)
						{
							Quote item = new Quote();
							item.Ticker = bittrexTicker.Key.ToUpper();
							item.Price1 = bittrexTicker.Value.Last;
							item.Price2 = bithumpTicker.Value.Last;

							decimal total1 = item.Price1 * rateUSDTVND;
							decimal percent = (item.Price2 * rateKRWVND * 0.9975M - total1) / total1 * 100;
							item.Percent = percent;

							listQoutes.Add(item);
						}
					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				drgBittrex.DataSource = listQoutes;
				drgBittrex.Show();

				this.UseWaitCursor = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.UseWaitCursor = false;
				//Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		private async void FetchTickersPoloniexBithumb()
		{
			if (string.IsNullOrEmpty(txtRateUSDT5.Text))
			{
				MessageBox.Show("Please type Rate USDT to VND (for example: 24450)!");
				return;
			}

			if (string.IsNullOrEmpty(txtRateKRW5.Text))
			{
				MessageBox.Show("Please type Rate KRW to VND (for example: 20.5)!");
				return;
			}

			rateUSDTVND = decimal.Parse(txtRateUSDT5.Text);
			rateKRWVND = decimal.Parse(txtRateKRW5.Text);

			this.UseWaitCursor = true;
			try
			{
				var poloniexAPI = new ExchangePoloniexAPI();

				var poloTickers = await poloniexAPI.GetTickersAsync();
				poloTickers = poloTickers.Where(p => p.Key.ToUpper().Contains("USDT"));

				if (!string.IsNullOrEmpty(txtCoinName5.Text))
					poloTickers = poloTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName5.Text.ToUpper()));

				var bithumpAPI = new ExchangeBithumbAPI();
				var bithumbTickers = await bithumpAPI.GetTickersAsync();

				if (!string.IsNullOrEmpty(txtCoinName5.Text))
					bithumbTickers = bithumbTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName5.Text.ToUpper()));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var bnbTicker in poloTickers)
				{
					foreach (var bithumbTicker in bithumbTickers)
					{
						if (bnbTicker.Key.Replace("USDT_", "") == bithumbTicker.Key)
						{
							Quote item = new Quote();
							item.Ticker = bnbTicker.Key.ToUpper();
							item.Price1 = bnbTicker.Value.Last;
							item.Price2 = bithumbTicker.Value.Last;

							decimal total1 = item.Price1 * rateUSDTVND;
							decimal percent = (item.Price2 * rateKRWVND * 0.9975M - total1) / total1 * 100;
							item.Percent = percent;

							listQoutes.Add(item);
						}
					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				drgPoloniexBithumb.DataSource = listQoutes;
				drgPoloniexBithumb.Show();

				this.UseWaitCursor = false;
				//textTickersResult.Text = b.ToString();
			}
			catch (Exception ex)
			{
				//textTickersResult.Text = ex.ToString();
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.UseWaitCursor = false;
				//Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		private async void FetchTickersOkexxBithumb()
		{
			if (string.IsNullOrEmpty(txtRateUSDT6.Text))
			{
				MessageBox.Show("Please type Rate USDT to VND (for example: 24450)!");
				return;
			}

			if (string.IsNullOrEmpty(txtRateKRW6.Text))
			{
				MessageBox.Show("Please type Rate KRW to VND (for example: 20.5)!");
				return;
			}

			rateUSDTVND = decimal.Parse(txtRateUSDT6.Text);
			rateKRWVND = decimal.Parse(txtRateKRW6.Text);

			this.UseWaitCursor = true;
			try
			{
				var okexAPI = new ExchangeOKExAPI();

				var okexTickers = await okexAPI.GetTickersAsync();
				okexTickers = okexTickers.Where(p => p.Key.ToUpper().Contains("USDT"));

				if (!string.IsNullOrEmpty(txtCoinName6.Text))
					okexTickers = okexTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName6.Text.ToUpper()));

				var bithumpAPI = new ExchangeBithumbAPI();
				var bithumbTickers = await bithumpAPI.GetTickersAsync();

				if (!string.IsNullOrEmpty(txtCoinName6.Text))
					bithumbTickers = bithumbTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName6.Text.ToUpper()));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var okexTicker in okexTickers)
				{
					foreach (var bithumbTicker in bithumbTickers)
					{
						if (okexTicker.Key.Replace("-USDT", "") == bithumbTicker.Key)
						{
							Quote item = new Quote();
							item.Ticker = okexTicker.Key.ToUpper();
							item.Price1 = okexTicker.Value.Last;
							item.Price2 = bithumbTicker.Value.Last;

							decimal total1 = item.Price1 * rateUSDTVND;
							decimal percent = (item.Price2 * rateKRWVND * 0.9975M - total1) / total1 * 100;
							item.Percent = percent;

							listQoutes.Add(item);
						}
					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				drgOkexBithumb.DataSource = listQoutes;
				drgOkexBithumb.Show();

				this.UseWaitCursor = false;
				//textTickersResult.Text = b.ToString();
			}
			catch (Exception ex)
			{
				//textTickersResult.Text = ex.ToString();
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.UseWaitCursor = false;
				//Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		private async void FetchTickersKucoinBithumb()
		{
			if (string.IsNullOrEmpty(txtRateUSDT7.Text))
			{
				MessageBox.Show("Please type Rate USDT to VND (for example: 24450)!");
				return;
			}

			if (string.IsNullOrEmpty(txtRateKRW7.Text))
			{
				MessageBox.Show("Please type Rate KRW to VND (for example: 20.5)!");
				return;
			}

			rateUSDTVND = decimal.Parse(txtRateUSDT7.Text);
			rateKRWVND = decimal.Parse(txtRateKRW7.Text);

			this.UseWaitCursor = true;
			try
			{
				var kucoinAPI = new ExchangeKuCoinAPI();

				var kucoinTickers = await kucoinAPI.GetTickersAsync();
				kucoinTickers = kucoinTickers.Where(p => p.Key.ToUpper().Contains("USDT"));

				if (!string.IsNullOrEmpty(txtCoinName7.Text))
					kucoinTickers = kucoinTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName7.Text.ToUpper()));

				var bithumpAPI = new ExchangeBithumbAPI();
				var bithumbTickers = await bithumpAPI.GetTickersAsync();

				if (!string.IsNullOrEmpty(txtCoinName7.Text))
					bithumbTickers = bithumbTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName7.Text.ToUpper()));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var kucoinTicker in kucoinTickers)
				{
					foreach (var bithumbTicker in bithumbTickers)
					{
						if (kucoinTicker.Key.Replace("-USDT", "") == bithumbTicker.Key)
						{
							Quote item = new Quote();
							item.Ticker = kucoinTicker.Key.ToUpper();
							item.Price1 = kucoinTicker.Value.Last;
							item.Price2 = bithumbTicker.Value.Last;

							decimal total1 = item.Price1 * rateUSDTVND;
							decimal percent = (item.Price2 * rateKRWVND * 0.9975M - total1) / total1 * 100;
							item.Percent = percent;

							listQoutes.Add(item);
						}
					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				drgKucoinBithumb.DataSource = listQoutes;
				drgKucoinBithumb.Show();

				this.UseWaitCursor = false;
			}
			catch (Exception ex)
			{
				//textTickersResult.Text = ex.ToString();
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.UseWaitCursor = false;
				//Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		private async void FetchTickersHitBithumb()
		{
			if (string.IsNullOrEmpty(txtRateUSDT8.Text))
			{
				MessageBox.Show("Please type Rate USDT to VND (for example: 24450)!");
				return;
			}

			if (string.IsNullOrEmpty(txtRateKRW8.Text))
			{
				MessageBox.Show("Please type Rate KRW to VND (for example: 20.5)!");
				return;
			}

			rateUSDTVND = decimal.Parse(txtRateUSDT8.Text);
			rateKRWVND = decimal.Parse(txtRateKRW8.Text);

			this.UseWaitCursor = true;
			try
			{
				var hitBTCAPI = new ExchangeHitBTCAPI();

				var hitBTCTickers = await hitBTCAPI.GetTickersAsync();
				hitBTCTickers = hitBTCTickers.Where(p => p.Key.ToUpper().Contains("USD"));

				if (!string.IsNullOrEmpty(txtCoinName8.Text))
					hitBTCTickers = hitBTCTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName8.Text.ToUpper()));

				var bithumpAPI = new ExchangeBithumbAPI();
				var bithumbTickers = await bithumpAPI.GetTickersAsync();

				if (!string.IsNullOrEmpty(txtCoinName8.Text))
					bithumbTickers = bithumbTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName8.Text.ToUpper()));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var hitBTCTicker in hitBTCTickers)
				{
					foreach (var bithumbTicker in bithumbTickers)
					{
						if (hitBTCTicker.Key.Replace("USD", "") == bithumbTicker.Key)
						{
							if (hitBTCTicker.Value.Last > 0 && bithumbTicker.Value.Last > 0)
							{
								Quote item = new Quote();
								item.Ticker = hitBTCTicker.Key.ToUpper();
								item.Price1 = hitBTCTicker.Value.Last;
								item.Price2 = bithumbTicker.Value.Last;

								decimal total1 = item.Price1 * rateUSDTVND;
								decimal percent = (item.Price2 * rateKRWVND * 0.9975M - total1) / total1 * 100;
								item.Percent = percent;

								listQoutes.Add(item);
							}
						}
					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				drgHitBithumb.DataSource = listQoutes;
				drgHitBithumb.Show();

				this.UseWaitCursor = false;
			}
			catch (Exception ex)
			{
				//textTickersResult.Text = ex.ToString();
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.UseWaitCursor = false;
				//Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		private async void FetchTickersBNBBittrex()
		{
			//rateUSDTVND = decimal.Parse(txtRateUSDT4.Text);
			//rateKRWVND = decimal.Parse(txtRateKRW4.Text);

			this.UseWaitCursor = true;
			try
			{
				var bittrexAPI = new ExchangeBittrexAPI();

				var bittrexTickers = await bittrexAPI.GetTickersAsync();
				bittrexTickers = bittrexTickers.Where(p => p.Key.ToUpper().Contains("USDT"));

				var bnbAPI = new ExchangeBinanceAPI();
				var bnbTickers = await bnbAPI.GetTickersAsync();

				if (!string.IsNullOrEmpty(txtCoinName9.Text))
					bittrexTickers = bittrexTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName9.Text.ToUpper()));

				if (!string.IsNullOrEmpty(txtCoinName9.Text))
					bnbTickers = bnbTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName9.Text.ToUpper()));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var bittrexTicker in bittrexTickers)
				{
					foreach (var bnbTicker in bnbTickers)
					{
						if (bittrexTicker.Key.ToUpper().Replace(@"USDT-", "") == bnbTicker.Key.ToUpper().Replace("USDT", ""))
						{
							Quote item = new Quote();
							item.Ticker = bittrexTicker.Key.ToUpper();
							item.Price1 = bnbTicker.Value.Last; 
							item.Price2 = bittrexTicker.Value.Last; 

							//decimal total1 = item.Price1 ;
							decimal percent = (item.Price2 - item.Price1) / item.Price1 * 100;
							item.Percent = percent;

							listQoutes.Add(item);
						}
					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				drgBNBBittrex.DataSource = listQoutes;
				drgBNBBittrex.Show();

				this.UseWaitCursor = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.UseWaitCursor = false;
				//Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		private async void FetchTickersBnbKucoin()
		{

			this.UseWaitCursor = true;
			try
			{
				var kucoinAPI = new ExchangeKuCoinAPI();

				var kucoinTickers = await kucoinAPI.GetTickersAsync();
				kucoinTickers = kucoinTickers.Where(p => p.Key.ToUpper().Contains("USDT"));

				if (!string.IsNullOrEmpty(txtCoinName10.Text))
					kucoinTickers = kucoinTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName10.Text.ToUpper()));

				var binanceAPI = new ExchangeBinanceAPI();
				var bnbTickers = await binanceAPI.GetTickersAsync();

				if (!string.IsNullOrEmpty(txtCoinName10.Text))
					bnbTickers = bnbTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName10.Text.ToUpper()));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var kucoinTicker in kucoinTickers)
				{
					foreach (var bnbTicker in bnbTickers)
					{
						if (kucoinTicker.Key.Replace("-USDT", "") == bnbTicker.Key.Replace("USDT", ""))
						{
							Quote item = new Quote();
							item.Ticker = kucoinTicker.Key.ToUpper();
							item.Price1 = bnbTicker.Value.Last; 
							item.Price2 = kucoinTicker.Value.Last;

							decimal percent = (item.Price2 - item.Price1) / item.Price1 * 100;
							item.Percent = percent;
							item.Percent = percent;

							listQoutes.Add(item);
						}
					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				drgBNBKucoin.DataSource = listQoutes;
				drgBNBKucoin.Show();

				this.UseWaitCursor = false;
			}
			catch (Exception ex)
			{
				//textTickersResult.Text = ex.ToString();
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.UseWaitCursor = false;
				//Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		private async void FetchTickersMXC1INCH()
		{

			this.UseWaitCursor = true;
			try
			{
				var inchAPI = new Exchange1InchAPI();
				var tokens = await inchAPI.GetTokensMetadataAsync();

				var mxcAPI = new ExchangeMXCAPI();

				var mxcTickers = await mxcAPI.GetTickersAsync();
				mxcTickers = mxcTickers.Where(p => p.Key.ToUpper().Contains("_USDT"));

				if (!string.IsNullOrEmpty(txtCoinName11.Text))
					mxcTickers = mxcTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName11.Text.ToUpper()));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var mxcTicker in mxcTickers)
				{
					string symbol = mxcTicker.Key.Replace("_USDT", "");

					var token = tokens.Where(p=>p.symbol == symbol);

					if(token!= null && token.Count() > 0)
					{
						decimal estimateAmount = 1000 / mxcTicker.Value.Last;

						var inchTicker = await inchAPI.GetQuoteAsync(symbol, estimateAmount);

						if(inchTicker!= null)
						{
							Quote item = new Quote();
							item.Ticker = mxcTicker.Key.ToUpper();
							item.Price1 = mxcTicker.Value.Last;
							item.Price2 = inchTicker.Last;

							decimal percent = (item.Price2 - item.Price1) / item.Price1 * 100;
							item.Percent = percent;
							item.Percent = percent;

							listQoutes.Add(item);

							listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();
							drgMXC1INCH.DataSource = null;
							drgMXC1INCH.DataSource = listQoutes;
							drgMXC1INCH.Show();
						}

					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				drgMXC1INCH.DataSource = listQoutes;
				drgMXC1INCH.Show();

				this.UseWaitCursor = false;
			}
			catch (Exception ex)
			{
				//textTickersResult.Text = ex.ToString();
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.UseWaitCursor = false;
				//Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		private async void FetchTickersBNB1INCH()
		{

			this.UseWaitCursor = true;
			try
			{
				var inchAPI = new Exchange1InchAPI();
				var tokens = await inchAPI.GetTokensMetadataAsync();

				var bnbAPI = new ExchangeBinanceAPI();

				var bnbTickers = await bnbAPI.GetTickersAsync();
				bnbTickers = bnbTickers.Where(p => p.Key.ToUpper().Contains("USDT"));

				if (!string.IsNullOrEmpty(txtCoinName12.Text))
					bnbTickers = bnbTickers.Where(p => p.Key.ToUpper().Contains(txtCoinName12.Text.ToUpper()));

				List<Quote> listQoutes = new List<Quote>();
				foreach (var mxcTicker in bnbTickers)
				{
					string symbol = mxcTicker.Key.Replace("USDT", "");

					var token = tokens.Where(p => p.symbol == symbol);

					if (token != null && token.Count() > 0)
					{
						decimal estimateAmount = 1000 / mxcTicker.Value.Last;

						var inchTicker = await inchAPI.GetQuoteAsync(symbol, estimateAmount);

						if (inchTicker != null)
						{
							Quote item = new Quote();
							item.Ticker = mxcTicker.Key.ToUpper();
							item.Price1 = mxcTicker.Value.Last;
							item.Price2 = inchTicker.Last;

							decimal percent = (item.Price2 - item.Price1) / item.Price1 * 100;
							item.Percent = percent;
							item.Percent = percent;

							listQoutes.Add(item);

							listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();
							drgBNBInch.DataSource = null;
							drgBNBInch.DataSource = listQoutes;
							drgBNBInch.Show();
						}

					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				drgBNBInch.DataSource = listQoutes;
				drgBNBInch.Show();

				this.UseWaitCursor = false;
			}
			catch (Exception ex)
			{
				//textTickersResult.Text = ex.ToString();
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.UseWaitCursor = false;
				//Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		public MainForm()
		{
			InitializeComponent();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			txtRateUSDT.Text = rateUSDTVND.ToString();
			txtRateKRW.Text = rateKRWVND.ToString();
			txtRateUSDT2.Text = rateUSDTVND.ToString();
			txtRateKRW2.Text = rateKRWVND.ToString();
			txtRateUSDT3.Text = rateUSDTVND.ToString();
			txtRateKRW3.Text = rateKRWVND.ToString();
			txtRateUSDT4.Text = rateUSDTVND.ToString();
			txtRateKRW4.Text = rateKRWVND.ToString();
			txtRateUSDT5.Text = rateUSDTVND.ToString();
			txtRateKRW5.Text = rateKRWVND.ToString();
			txtRateUSDT6.Text = rateUSDTVND.ToString();
			txtRateKRW6.Text = rateKRWVND.ToString();
			txtRateUSDT7.Text = rateUSDTVND.ToString();
			txtRateKRW7.Text = rateKRWVND.ToString();
			txtRateUSDT8.Text = rateUSDTVND.ToString();
			txtRateKRW8.Text = rateKRWVND.ToString();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			FetchTickersBNB();
		}

		//private void cmbExchange_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	FetchTickersHuobi();
		//}

		private void btnGetTicker2_Click(object sender, EventArgs e)
		{
			FetchTickersHuobi();
			this.UseWaitCursor = false;
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			FetchTickersCoinbase();
			this.UseWaitCursor = false;
		}

		private void btnTickerBitrex_Click(object sender, EventArgs e)
		{
			FetchTickersBittrex();
			this.UseWaitCursor = false;
		}

		private void btnTickerBnBYobit_Click(object sender, EventArgs e)
		{
			FetchTickersPoloniexBithumb();
			this.UseWaitCursor = false;
		}

		private void btnTickerOkex_Click(object sender, EventArgs e)
		{
			FetchTickersOkexxBithumb();
			this.UseWaitCursor = false;
		}

		private void btnTickerKucoin_Click(object sender, EventArgs e)
		{
			FetchTickersKucoinBithumb();
			this.UseWaitCursor = false;
		}

		private void btnTickerHitBtc_Click(object sender, EventArgs e)
		{
			FetchTickersHitBithumb();
			this.UseWaitCursor = false;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			FetchTickersBNBBittrex();
		}

		private void btnBNBKucoin_Click(object sender, EventArgs e)
		{
			FetchTickersBnbKucoin();
		}

		private void btnMXC1INCH_Click(object sender, EventArgs e)
		{
			FetchTickersMXC1INCH();
		}

		private void btnBNBInch_Click(object sender, EventArgs e)
		{
			FetchTickersBNB1INCH();
		}
	}
}
