namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200022D RID: 557
	public sealed partial class ObjectPickerForm : global::Microsoft.Exchange.Management.SystemManager.WinForms.SearchDialog, global::Microsoft.Exchange.Management.SystemManager.WinForms.ISelectedObjectsProvider
	{
		// Token: 0x060019C0 RID: 6592 RVA: 0x0006FEA9 File Offset: 0x0006E0A9
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ObjectPicker = null;
			}
			base.Dispose(disposing);
		}
	}
}
