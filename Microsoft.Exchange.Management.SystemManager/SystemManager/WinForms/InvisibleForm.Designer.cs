namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200013A RID: 314
	internal partial class InvisibleForm : global::Microsoft.Exchange.Management.SystemManager.WinForms.ExchangeForm
	{
		// Token: 0x06000C65 RID: 3173 RVA: 0x0002C933 File Offset: 0x0002AB33
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.backgroundWorker.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000508 RID: 1288
		private global::System.ComponentModel.BackgroundWorker backgroundWorker;
	}
}
