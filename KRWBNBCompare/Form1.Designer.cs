
namespace KRWBNBCompare
{
    partial class Form1
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
            this.txtRateUSDT = new System.Windows.Forms.TextBox();
            this.lblRateUSDTVND = new System.Windows.Forms.Label();
            this.lblRateKRW = new System.Windows.Forms.Label();
            this.txtRateKWR = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.drgResult = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.drgResult)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRateUSDT
            // 
            this.txtRateUSDT.Location = new System.Drawing.Point(212, 49);
            this.txtRateUSDT.Name = "txtRateUSDT";
            this.txtRateUSDT.Size = new System.Drawing.Size(174, 22);
            this.txtRateUSDT.TabIndex = 0;
            // 
            // lblRateUSDTVND
            // 
            this.lblRateUSDTVND.AutoSize = true;
            this.lblRateUSDTVND.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRateUSDTVND.Location = new System.Drawing.Point(29, 51);
            this.lblRateUSDTVND.Name = "lblRateUSDTVND";
            this.lblRateUSDTVND.Size = new System.Drawing.Size(177, 20);
            this.lblRateUSDTVND.TabIndex = 1;
            this.lblRateUSDTVND.Text = "Rate USDT to VND:";
            // 
            // lblRateKRW
            // 
            this.lblRateKRW.AutoSize = true;
            this.lblRateKRW.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRateKRW.Location = new System.Drawing.Point(412, 51);
            this.lblRateKRW.Name = "lblRateKRW";
            this.lblRateKRW.Size = new System.Drawing.Size(169, 20);
            this.lblRateKRW.TabIndex = 2;
            this.lblRateKRW.Text = "Rate KRW to VND:";
            // 
            // txtRateKWR
            // 
            this.txtRateKWR.Location = new System.Drawing.Point(578, 49);
            this.txtRateKWR.Name = "txtRateKWR";
            this.txtRateKWR.Size = new System.Drawing.Size(178, 22);
            this.txtRateKWR.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(312, 107);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(179, 37);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Results:";
            // 
            // drgResult
            // 
            this.drgResult.AllowUserToAddRows = false;
            this.drgResult.AllowUserToDeleteRows = false;
            this.drgResult.AllowUserToOrderColumns = true;
            this.drgResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drgResult.Location = new System.Drawing.Point(33, 204);
            this.drgResult.Name = "drgResult";
            this.drgResult.ReadOnly = true;
            this.drgResult.RowHeadersWidth = 51;
            this.drgResult.RowTemplate.Height = 24;
            this.drgResult.Size = new System.Drawing.Size(723, 150);
            this.drgResult.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.drgResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtRateKWR);
            this.Controls.Add(this.lblRateKRW);
            this.Controls.Add(this.lblRateUSDTVND);
            this.Controls.Add(this.txtRateUSDT);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.drgResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

		#endregion

		private System.Windows.Forms.TextBox txtRateUSDT;
		private System.Windows.Forms.Label lblRateUSDTVND;
		private System.Windows.Forms.Label lblRateKRW;
		private System.Windows.Forms.TextBox txtRateKWR;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView drgResult;
	}
}

