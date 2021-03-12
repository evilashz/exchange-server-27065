namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000195 RID: 405
	internal partial class BackgroundWorkerProgressDialog : global::Microsoft.Exchange.Management.SystemManager.WinForms.ProgressDialog
	{
		// Token: 0x06001041 RID: 4161 RVA: 0x0003FDFF File Offset: 0x0003DFFF
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.backgroundWorker.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000656 RID: 1622
		private global::System.ComponentModel.BackgroundWorker backgroundWorker;
	}
}
