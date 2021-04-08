using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeSharpWinForms.Entities
{
	class Quote
	{
		public string Ticker { get; set; }

		public decimal Price1 { get; set; }

		public decimal Price2 { get; set; }

		public decimal Percent { get; set; }
	}
}
