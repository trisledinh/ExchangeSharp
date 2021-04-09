using System;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using ExchangeSharp;
using System.Collections.Generic;
using ExchangeSharpWinForms.Entities;

namespace ExchangeSharpWinForms
{
	public partial class MainForm : Form
	{
		private decimal rateUSDTVND = 24450;
		private decimal rateKRWVND = 20.5M;

		private decimal rateUSDTVND2 = 24450;
		private decimal rateKRWVND2 = 20.5M;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

		private async void FetchTickersBNB()
		{
			//if (!Created || string.IsNullOrWhiteSpace(cmbExchange.SelectedItem as string))
			//{
			//    return;
			//}

			if(string.IsNullOrEmpty(txtRateUSDT.Text))
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

				//bnbTickers = bnbTickers.Where(p => p.Key.Contains("USDT") && !p.Key.Contains("UP") && !p.Key.Contains("DOWN") && !p.Key.Contains("BEAR") && !p.Key.Contains("BULL")
				//&& !p.Key.Contains("BTCST") && !p.Key.Contains("DREP") && !p.Key.Contains("YFII") && !p.Key.Contains("BCH") && !p.Key.Contains("BZRX"));

				//BULL BTCST DREP YFII BCHSV BCHABC BZRZ

				var bithumpAPI = new ExchangeBithumbAPI();
				var bithumpTickers = await bithumpAPI.GetTickersAsync();

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
							decimal percent = (item.Price2 * rateKRWVND * 0.9975M - total1) / total1*100;
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
				Invoke(new Action(() => this.UseWaitCursor = false));
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

			rateUSDTVND2 = decimal.Parse(txtRateUSDT2.Text);
			rateKRWVND2 = decimal.Parse(txtRateKRW2.Text);

			this.UseWaitCursor = true;
			try
			{
				var houbiAPI = new ExchangeHuobiAPI();

				var huobiTickers = await houbiAPI.GetTickersAsync();
				huobiTickers = huobiTickers.Where(p => p.Key.ToUpper().Contains("USDT"));

				var bithumpAPI = new ExchangeBithumbAPI();
				var bithumpTickers = await bithumpAPI.GetTickersAsync();

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

							decimal total1 = item.Price1 * rateUSDTVND2;
							decimal percent = (item.Price2 * rateKRWVND2 * 0.9975M - total1) / total1 * 100;
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
				Invoke(new Action(() => this.UseWaitCursor = false));
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
			txtRateUSDT2.Text = rateUSDTVND2.ToString();
			txtRateKRW2.Text = rateKRWVND2.ToString();
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
		}
	}
}
