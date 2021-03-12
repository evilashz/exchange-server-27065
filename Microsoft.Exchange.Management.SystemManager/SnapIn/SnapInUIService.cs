using System;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementConsole;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200029D RID: 669
	internal class SnapInUIService : UIService
	{
		// Token: 0x06001C5C RID: 7260 RVA: 0x0007AF39 File Offset: 0x00079139
		public SnapInUIService(NamespaceSnapInBase snapIn) : base(null)
		{
			this.snapIn = snapIn;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0007AF49 File Offset: 0x00079149
		public SnapInUIService(NamespaceSnapInBase snapIn, Control control) : base(control)
		{
			this.snapIn = snapIn;
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0007AF59 File Offset: 0x00079159
		public override void SetUIDirty()
		{
			this.snapIn.IsModified = true;
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0007AF68 File Offset: 0x00079168
		protected override DialogResult OnShowDialog(Form form)
		{
			DialogResult result;
			try
			{
				result = this.snapIn.Console.ShowDialog(form);
			}
			catch (InvalidOperationException)
			{
				result = form.ShowDialog(base.GetDialogOwnerWindow());
			}
			return result;
		}

		// Token: 0x04000A94 RID: 2708
		private NamespaceSnapInBase snapIn;
	}
}
