namespace ExchangeSharpWinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetTickers = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRateUSDT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRateKRW = new System.Windows.Forms.TextBox();
            this.drgBnbKRW = new System.Windows.Forms.DataGridView();
            this.Ticker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabBinance = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtRateKRW2 = new System.Windows.Forms.TextBox();
            this.drgHuobiKRW = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRateUSDT2 = new System.Windows.Forms.TextBox();
            this.btnGetTicker2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.drgBnbKRW)).BeginInit();
            this.tabBinance.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drgHuobiKRW)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetTickers
            // 
            this.btnGetTickers.Location = new System.Drawing.Point(485, 29);
            this.btnGetTickers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGetTickers.Name = "btnGetTickers";
            this.btnGetTickers.Size = new System.Drawing.Size(162, 58);
            this.btnGetTickers.TabIndex = 0;
            this.btnGetTickers.Text = "Get Tickers";
            this.btnGetTickers.UseVisualStyleBackColor = true;
            this.btnGetTickers.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rate USDT to VND:";
            // 
            // txtRateUSDT
            // 
            this.txtRateUSDT.Location = new System.Drawing.Point(154, 29);
            this.txtRateUSDT.Name = "txtRateUSDT";
            this.txtRateUSDT.Size = new System.Drawing.Size(276, 22);
            this.txtRateUSDT.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Rate KRW to VND:";
            // 
            // txtRateKRW
            // 
            this.txtRateKRW.Location = new System.Drawing.Point(154, 65);
            this.txtRateKRW.Name = "txtRateKRW";
            this.txtRateKRW.Size = new System.Drawing.Size(276, 22);
            this.txtRateKRW.TabIndex = 6;
            // 
            // drgBnbKRW
            // 
            this.drgBnbKRW.AllowUserToAddRows = false;
            this.drgBnbKRW.AllowUserToDeleteRows = false;
            this.drgBnbKRW.AllowUserToOrderColumns = true;
            this.drgBnbKRW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drgBnbKRW.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.drgBnbKRW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drgBnbKRW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ticker,
            this.Price1,
            this.Price2,
            this.Percent});
            this.drgBnbKRW.Location = new System.Drawing.Point(3, 121);
            this.drgBnbKRW.Name = "drgBnbKRW";
            this.drgBnbKRW.RowHeadersWidth = 51;
            this.drgBnbKRW.RowTemplate.Height = 24;
            this.drgBnbKRW.Size = new System.Drawing.Size(770, 303);
            this.drgBnbKRW.TabIndex = 5;
            // 
            // Ticker
            // 
            this.Ticker.DataPropertyName = "Ticker";
            this.Ticker.HeaderText = "Coin";
            this.Ticker.MinimumWidth = 6;
            this.Ticker.Name = "Ticker";
            // 
            // Price1
            // 
            this.Price1.DataPropertyName = "Price1";
            this.Price1.HeaderText = "USDT Price";
            this.Price1.MinimumWidth = 6;
            this.Price1.Name = "Price1";
            // 
            // Price2
            // 
            this.Price2.DataPropertyName = "Price2";
            this.Price2.HeaderText = "KRW Price";
            this.Price2.MinimumWidth = 6;
            this.Price2.Name = "Price2";
            // 
            // Percent
            // 
            this.Percent.DataPropertyName = "Percent";
            this.Percent.HeaderText = "Percent";
            this.Percent.MinimumWidth = 6;
            this.Percent.Name = "Percent";
            // 
            // tabBinance
            // 
            this.tabBinance.Controls.Add(this.tabPage1);
            this.tabBinance.Controls.Add(this.tabPage2);
            this.tabBinance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabBinance.Location = new System.Drawing.Point(0, 0);
            this.tabBinance.Name = "tabBinance";
            this.tabBinance.SelectedIndex = 0;
            this.tabBinance.Size = new System.Drawing.Size(784, 456);
            this.tabBinance.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtRateKRW);
            this.tabPage1.Controls.Add(this.drgBnbKRW);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtRateUSDT);
            this.tabPage1.Controls.Add(this.btnGetTickers);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 427);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Binance - Bithumb";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtRateKRW2);
            this.tabPage2.Controls.Add(this.drgHuobiKRW);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtRateUSDT2);
            this.tabPage2.Controls.Add(this.btnGetTicker2);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 427);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Huobi - Bithumb";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtRateKRW2
            // 
            this.txtRateKRW2.Location = new System.Drawing.Point(154, 52);
            this.txtRateKRW2.Name = "txtRateKRW2";
            this.txtRateKRW2.Size = new System.Drawing.Size(276, 22);
            this.txtRateKRW2.TabIndex = 12;
            // 
            // drgHuobiKRW
            // 
            this.drgHuobiKRW.AllowUserToAddRows = false;
            this.drgHuobiKRW.AllowUserToDeleteRows = false;
            this.drgHuobiKRW.AllowUserToOrderColumns = true;
            this.drgHuobiKRW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drgHuobiKRW.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.drgHuobiKRW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drgHuobiKRW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.drgHuobiKRW.Location = new System.Drawing.Point(3, 106);
            this.drgHuobiKRW.Name = "drgHuobiKRW";
            this.drgHuobiKRW.RowHeadersWidth = 51;
            this.drgHuobiKRW.RowTemplate.Height = 24;
            this.drgHuobiKRW.Size = new System.Drawing.Size(770, 318);
            this.drgHuobiKRW.TabIndex = 10;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Ticker";
            this.dataGridViewTextBoxColumn1.HeaderText = "Coin";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Price1";
            this.dataGridViewTextBoxColumn2.HeaderText = "USDT Price";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Price2";
            this.dataGridViewTextBoxColumn3.HeaderText = "KRW Price";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Percent";
            this.dataGridViewTextBoxColumn4.HeaderText = "Percent";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Rate KRW to VND:";
            // 
            // txtRateUSDT2
            // 
            this.txtRateUSDT2.Location = new System.Drawing.Point(154, 16);
            this.txtRateUSDT2.Name = "txtRateUSDT2";
            this.txtRateUSDT2.Size = new System.Drawing.Size(276, 22);
            this.txtRateUSDT2.TabIndex = 9;
            // 
            // btnGetTicker2
            // 
            this.btnGetTicker2.Location = new System.Drawing.Point(485, 16);
            this.btnGetTicker2.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetTicker2.Name = "btnGetTicker2";
            this.btnGetTicker2.Size = new System.Drawing.Size(162, 58);
            this.btnGetTicker2.TabIndex = 7;
            this.btnGetTicker2.Text = "Get Tickers";
            this.btnGetTicker2.UseVisualStyleBackColor = true;
            this.btnGetTicker2.Click += new System.EventHandler(this.btnGetTicker2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Rate USDT to VND:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(784, 456);
            this.Controls.Add(this.tabBinance);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "Exchange Checking";
            ((System.ComponentModel.ISupportInitialize)(this.drgBnbKRW)).EndInit();
            this.tabBinance.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drgHuobiKRW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGetTickers;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtRateKRW;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtRateUSDT;
		private System.Windows.Forms.DataGridView drgBnbKRW;
		private System.Windows.Forms.DataGridViewTextBoxColumn Ticker;
		private System.Windows.Forms.DataGridViewTextBoxColumn Price1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Price2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Percent;
		private System.Windows.Forms.TabControl tabBinance;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox txtRateKRW2;
		private System.Windows.Forms.DataGridView drgHuobiKRW;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtRateUSDT2;
		private System.Windows.Forms.Button btnGetTicker2;
		private System.Windows.Forms.Label label4;
	}
}

