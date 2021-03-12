using System;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000109 RID: 265
	public abstract class ContentResultPane : ResultPane
	{
		// Token: 0x0600097D RID: 2429 RVA: 0x00021346 File Offset: 0x0001F546
		public ContentResultPane()
		{
			base.Name = "ContentResultPane";
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00021359 File Offset: 0x0001F559
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys)131139)
			{
				this.CopyContentToClipBoard();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00021374 File Offset: 0x0001F574
		private void CopyContentToClipBoard()
		{
			using (new ControlWaitCursor(this))
			{
				WinformsHelper.SetDataObjectToClipboard(this.GetContent(), true);
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000213B4 File Offset: 0x0001F5B4
		protected virtual string GetContent()
		{
			return string.Empty;
		}
	}
}
