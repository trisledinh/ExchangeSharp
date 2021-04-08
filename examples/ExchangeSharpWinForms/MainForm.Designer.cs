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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRateUSDT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRateKRW = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Ticker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetTickers
            // 
            this.btnGetTickers.Location = new System.Drawing.Point(487, 28);
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
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rate USDT to VND:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRateKRW);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnGetTickers);
            this.groupBox1.Controls.Add(this.txtRateUSDT);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // txtRateUSDT
            // 
            this.txtRateUSDT.Location = new System.Drawing.Point(156, 28);
            this.txtRateUSDT.Name = "txtRateUSDT";
            this.txtRateUSDT.Size = new System.Drawing.Size(276, 22);
            this.txtRateUSDT.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Rate KRW to VND:";
            // 
            // txtRateKRW
            // 
            this.txtRateKRW.Location = new System.Drawing.Point(156, 64);
            this.txtRateKRW.Name = "txtRateKRW";
            this.txtRateKRW.Size = new System.Drawing.Size(276, 22);
            this.txtRateKRW.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ticker,
            this.Price1,
            this.Price2,
            this.Percent});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 100);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(784, 356);
            this.dataGridView1.TabIndex = 5;
            // 
            // Ticker
            // 
            this.Ticker.DataPropertyName = "Ticker";
            this.Ticker.HeaderText = "Coin";
            this.Ticker.MinimumWidth = 6;
            this.Ticker.Name = "Ticker";
            this.Ticker.ReadOnly = true;
            // 
            // Price1
            // 
            this.Price1.DataPropertyName = "Price1";
            this.Price1.HeaderText = "USDT Price";
            this.Price1.MinimumWidth = 6;
            this.Price1.Name = "Price1";
            this.Price1.ReadOnly = true;
            // 
            // Price2
            // 
            this.Price2.DataPropertyName = "Price2";
            this.Price2.HeaderText = "KRW Price";
            this.Price2.MinimumWidth = 6;
            this.Price2.Name = "Price2";
            this.Price2.ReadOnly = true;
            // 
            // Percent
            // 
            this.Percent.DataPropertyName = "Percent";
            this.Percent.HeaderText = "Percent";
            this.Percent.MinimumWidth = 6;
            this.Percent.Name = "Percent";
            this.Percent.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(784, 456);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "ExchangeSharp Test";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGetTickers;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtRateKRW;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtRateUSDT;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Ticker;
		private System.Windows.Forms.DataGridViewTextBoxColumn Price1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Price2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Percent;
	}
}

