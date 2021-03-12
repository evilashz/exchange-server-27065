using System;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace Microsoft.Exchange.Management.SystemManager.WinForms.Design
{
	// Token: 0x0200021C RID: 540
	public class WorkCenterDesigner : ScrollableControlDesigner
	{
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x000683DD File Offset: 0x000665DD
		public WorkCenter WorkCenter
		{
			get
			{
				return this.Control as WorkCenter;
			}
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x000683EA File Offset: 0x000665EA
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			base.EnableDesignMode(this.WorkCenter.TopPanel, "TopPanel");
		}
	}
}
