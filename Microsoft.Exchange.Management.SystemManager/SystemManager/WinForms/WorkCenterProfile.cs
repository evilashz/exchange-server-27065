using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000D9 RID: 217
	public class WorkCenterProfile : ResultPaneProfile
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x000198E7 File Offset: 0x00017AE7
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x000198EF File Offset: 0x00017AEF
		public ResultPaneProfile TopResultPane { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x000198F8 File Offset: 0x00017AF8
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00019900 File Offset: 0x00017B00
		public ResultPaneProfile BottomResultPane { get; set; }

		// Token: 0x0600079E RID: 1950 RVA: 0x00019909 File Offset: 0x00017B09
		public override bool HasPermission()
		{
			return this.TopResultPane.HasPermission();
		}
	}
}
