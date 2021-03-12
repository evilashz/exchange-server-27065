namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001C1 RID: 449
	public abstract partial class ProxyAddressDialog : global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangeDialog
	{
		// Token: 0x060012EE RID: 4846 RVA: 0x0004C9A0 File Offset: 0x0004ABA0
		private void InitializeComponent()
		{
			this.exchangePage = new global::Microsoft.Exchange.Management.SystemManager.WinForms.ProxyAddressDialog.ProxyAddressContentPage(this);
			base.SuspendLayout();
			this.exchangePage.AutoSize = true;
			this.exchangePage.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.exchangePage.Dock = global::System.Windows.Forms.DockStyle.Fill;
			base.Controls.Add(this.exchangePage);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400070E RID: 1806
		private global::Microsoft.Exchange.Management.SystemManager.WinForms.ProxyAddressDialog.ProxyAddressContentPage exchangePage;
	}
}
