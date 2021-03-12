using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000061 RID: 97
	public abstract class PostRefreshActionBase
	{
		// Token: 0x060003B3 RID: 947
		public abstract void DoPostRefreshAction(DataTableLoader loader, RefreshRequestEventArgs refreshRequest);

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000D1A5 File Offset: 0x0000B3A5
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000D1AD File Offset: 0x0000B3AD
		public DataView FilteredDataView { get; set; }
	}
}
