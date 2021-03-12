namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200014F RID: 335
	public partial class PropertyPageDialog : global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangeDialog
	{
		// Token: 0x06000D9C RID: 3484 RVA: 0x000339A9 File Offset: 0x00031BA9
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00033A08 File Offset: 0x00031C08
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			base.SuspendLayout();
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			base.CancelVisible = true;
			base.ClientSize = new global::System.Drawing.Size(421, 435);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "PropertyPageDialog";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000571 RID: 1393
		private global::System.ComponentModel.IContainer components;
	}
}
