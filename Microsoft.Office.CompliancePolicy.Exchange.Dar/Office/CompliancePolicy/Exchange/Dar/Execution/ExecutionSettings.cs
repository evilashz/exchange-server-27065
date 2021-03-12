using System;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Execution
{
	// Token: 0x02000009 RID: 9
	internal class ExecutionSettings
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002A30 File Offset: 0x00000C30
		public ExecutionSettings()
		{
			this.CacheSize = 100;
			this.CacheExpiryPeriod = new TimeSpan(0, 30, 0);
			this.PageSize = 0;
			this.MaxTaskExecutionTime = new TimeSpan(1, 0, 0);
			this.MaxTasksQueued = 100;
			this.MaxThreadCount = 100;
			this.MaxTasksDelayCached = 100;
			this.MaxDelayCacheTime = TimeSpan.FromHours(1.0);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A9B File Offset: 0x00000C9B
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002AA3 File Offset: 0x00000CA3
		public int CacheSize { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002AAC File Offset: 0x00000CAC
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public TimeSpan CacheExpiryPeriod { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002ABD File Offset: 0x00000CBD
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002AC5 File Offset: 0x00000CC5
		public int PageSize { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002ACE File Offset: 0x00000CCE
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002AD6 File Offset: 0x00000CD6
		public TimeSpan MaxTaskExecutionTime { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002ADF File Offset: 0x00000CDF
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002AE7 File Offset: 0x00000CE7
		public int MaxThreadCount { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002AF0 File Offset: 0x00000CF0
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public int MaxTasksQueued { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002B01 File Offset: 0x00000D01
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002B09 File Offset: 0x00000D09
		public int MaxTasksDelayCached { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002B12 File Offset: 0x00000D12
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002B1A File Offset: 0x00000D1A
		public TimeSpan MaxDelayCacheTime { get; private set; }
	}
}
