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

		private async void FetchTickers()
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
				bnbTickers = bnbTickers.Where(p => p.Key.Contains("USDT") && !p.Key.Contains("UP") && !p.Key.Contains("DOWN") && !p.Key.Contains("BEAR"));

				var bithumpAPI = new ExchangeBithumbAPI();
				var bithumpTickers = await bithumpAPI.GetTickersAsync();

				List<Quote> listQoutes = new List<Quote>();
				foreach (var bnbTicker in bnbTickers)
				{
					foreach (var bithumpTicker in bithumpTickers)
					{
						if (bnbTicker.Key.Contains(bithumpTicker.Key))
						{
							Quote item = new Quote();
							item.Ticker = bnbTicker.Key;
							item.Price1 = bnbTicker.Value.Last;
							item.Price2 = bithumpTicker.Value.Last;

							decimal total1 = item.Price1 * rateUSDTVND;
							decimal percent = (item.Price2 * rateKRWVND * 0.975M - total1) / total1*100;
							item.Percent = percent;

							listQoutes.Add(item);
						}
					}
				}

				listQoutes = listQoutes.OrderByDescending(p => p.Percent).ToList();

				dataGridView1.DataSource = listQoutes;
				dataGridView1.Show();

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

		public MainForm()
		{
			InitializeComponent();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			txtRateUSDT.Text = rateUSDTVND.ToString();
			txtRateKRW.Text = rateKRWVND.ToString();
			//foreach (var exchange in ExchangeAPI.GetExchangeAPIs())
			//{
			//    cmbExchange.Items.Add(exchange.Name);
			//}
			//cmbExchange.SelectedIndex = 0;
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			FetchTickers();
		}

		private void cmbExchange_SelectedIndexChanged(object sender, EventArgs e)
		{
			FetchTickers();
		}
	}
}
