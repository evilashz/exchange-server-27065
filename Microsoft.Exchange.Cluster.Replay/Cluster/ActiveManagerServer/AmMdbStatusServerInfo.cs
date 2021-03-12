using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000055 RID: 85
	internal class AmMdbStatusServerInfo
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x000144A1 File Offset: 0x000126A1
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x000144A9 File Offset: 0x000126A9
		internal AmServerName ServerName { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x000144B2 File Offset: 0x000126B2
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x000144BA File Offset: 0x000126BA
		internal bool IsNodeUp { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x000144C3 File Offset: 0x000126C3
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x000144CB File Offset: 0x000126CB
		internal TimeSpan TimeOut { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000144D4 File Offset: 0x000126D4
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x000144DC File Offset: 0x000126DC
		internal bool IsStoreRunning { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x000144E5 File Offset: 0x000126E5
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x000144ED File Offset: 0x000126ED
		internal bool IsReplayRunning { get; set; }

		// Token: 0x060003BA RID: 954 RVA: 0x000144F6 File Offset: 0x000126F6
		internal AmMdbStatusServerInfo(AmServerName serverName, bool isNodeUp, TimeSpan timeout)
		{
			this.ServerName = serverName;
			this.IsNodeUp = isNodeUp;
			this.TimeOut = timeout;
			this.IsReplayRunning = false;
			this.IsStoreRunning = false;
		}
	}
}
