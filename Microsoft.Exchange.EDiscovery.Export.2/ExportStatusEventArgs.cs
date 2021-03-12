using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000054 RID: 84
	public class ExportStatusEventArgs : EventArgs
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00017B6C File Offset: 0x00015D6C
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00017B74 File Offset: 0x00015D74
		public int ActualCount { get; internal set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00017B7D File Offset: 0x00015D7D
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x00017B85 File Offset: 0x00015D85
		public long ActualBytes { get; internal set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00017B8E File Offset: 0x00015D8E
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00017B96 File Offset: 0x00015D96
		public int ActualMailboxesProcessed { get; internal set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00017B9F File Offset: 0x00015D9F
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00017BA7 File Offset: 0x00015DA7
		public int ActualMailboxesTotal { get; internal set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00017BB0 File Offset: 0x00015DB0
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x00017BB8 File Offset: 0x00015DB8
		public TimeSpan TotalDuration { get; internal set; }
	}
}
