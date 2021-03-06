namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001E8 RID: 488
	public partial class EumProxyAddressDialog : global::Microsoft.Exchange.Management.SystemManager.WinForms.ProxyAddressDialog
	{
		// Token: 0x060015E2 RID: 5602 RVA: 0x0005A2C8 File Offset: 0x000584C8
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.addressLabel = new global::System.Windows.Forms.Label();
			this.addressTextBox = new global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangeTextBox();
			this.dialplanLabel = new global::System.Windows.Forms.Label();
			this.dialplanPickerLauncherTextBox = new global::Microsoft.Exchange.Management.SystemManager.WinForms.PickerLauncherTextBox();
			this.proxyErrorProvider = new global::System.Windows.Forms.ErrorProvider(this.components);
			this.tableLayoutPanel1 = new global::Microsoft.Exchange.Management.SystemManager.WinForms.AutoTableLayoutPanel();
			((global::System.ComponentModel.ISupportInitialize)this.proxyErrorProvider).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			base.ContentPage.SuspendLayout();
			base.SuspendLayout();
			this.addressLabel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.addressLabel.AutoSize = true;
			this.addressLabel.Location = new global::System.Drawing.Point(13, 12);
			this.addressLabel.Margin = new global::System.Windows.Forms.Padding(0);
			this.addressLabel.Name = "addressLabel";
			this.addressLabel.Size = new global::System.Drawing.Size(400, 13);
			this.addressLabel.TabIndex = 0;
			this.addressLabel.Text = "addressLabel";
			this.addressTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.addressTextBox.Location = new global::System.Drawing.Point(16, 28);
			this.addressTextBox.Margin = new global::System.Windows.Forms.Padding(3, 3, 0, 0);
			this.addressTextBox.Name = "addressTextBox";
			this.addressTextBox.Size = new global::System.Drawing.Size(397, 20);
			this.addressTextBox.TabIndex = 1;
			this.addressTextBox.MaxLength = global::Microsoft.Exchange.Data.ProxyAddressBase.MaxLength - 2;
			this.dialplanLabel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.dialplanLabel.AutoSize = true;
			this.dialplanLabel.Location = new global::System.Drawing.Point(13, 60);
			this.dialplanLabel.Margin = new global::System.Windows.Forms.Padding(0, 12, 0, 0);
			this.dialplanLabel.Name = "dialplanLabel";
			this.dialplanLabel.Size = new global::System.Drawing.Size(400, 13);
			this.dialplanLabel.TabIndex = 2;
			this.dialplanLabel.Text = "dialplanLabel";
			this.dialplanPickerLauncherTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.dialplanPickerLauncherTextBox.AutoSize = true;
			this.dialplanPickerLauncherTextBox.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.dialplanPickerLauncherTextBox.Location = new global::System.Drawing.Point(16, 76);
			this.dialplanPickerLauncherTextBox.Margin = new global::System.Windows.Forms.Padding(3, 3, 0, 0);
			this.dialplanPickerLauncherTextBox.Name = "dialplanPickerLauncherTextBox";
			this.dialplanPickerLauncherTextBox.Size = new global::System.Drawing.Size(397, 23);
			this.dialplanPickerLauncherTextBox.TabIndex = 3;
			this.proxyErrorProvider.BlinkStyle = global::System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.proxyErrorProvider.ContainerControl = this;
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.addressLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.dialplanPickerLauncherTextBox, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.dialplanLabel, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.addressTextBox, 0, 1);
			this.tableLayoutPanel1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.AutoLayout = true;
			this.tableLayoutPanel1.Location = new global::System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new global::System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new global::System.Windows.Forms.Padding(13, 12, 16, 12);
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new global::System.Drawing.Size(429, 108);
			this.tableLayoutPanel1.TabIndex = 0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelVisible = true;
			base.ClientSize = new global::System.Drawing.Size(429, 225);
			base.Margin = new global::System.Windows.Forms.Padding(5, 3, 5, 3);
			base.Name = "CustomAddressDialog";
			base.OkEnabled = true;
			base.ContentPage.Controls.Add(this.tableLayoutPanel1);
			base.Controls.SetChildIndex(base.ContentPage, 0);
			((global::System.ComponentModel.ISupportInitialize)this.proxyErrorProvider).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			base.ContentPage.ResumeLayout(false);
			base.ContentPage.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040007E9 RID: 2025
		private global::System.Windows.Forms.Label addressLabel;

		// Token: 0x040007EA RID: 2026
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangeTextBox addressTextBox;

		// Token: 0x040007EB RID: 2027
		private global::System.Windows.Forms.Label dialplanLabel;

		// Token: 0x040007EC RID: 2028
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.PickerLauncherTextBox dialplanPickerLauncherTextBox;

		// Token: 0x040007ED RID: 2029
		private global::System.Windows.Forms.ErrorProvider proxyErrorProvider;

		// Token: 0x040007EE RID: 2030
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040007EF RID: 2031
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.AutoTableLayoutPanel tableLayoutPanel1;
	}
}
