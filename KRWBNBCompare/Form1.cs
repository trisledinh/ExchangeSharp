using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExchangeSharp;

namespace KRWBNBCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

		private async void FetchTickers()
		{
			this.UseWaitCursor = true;
			try
			{
				var binanceAPI = new ExchangeBinanceAPI();

				//var api = ExchangeAPI.GetExchangeAPI(cmbExchange.SelectedItem as string);
				//var tickers = await api.GetTickersAsync();

				var tickers = await binanceAPI.GetTickersAsync();

				drgResult.DataSource = tickers;
				drgResult.Show();


			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				Invoke(new Action(() => this.UseWaitCursor = false));
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			FetchTickers();
		}
	}
}
