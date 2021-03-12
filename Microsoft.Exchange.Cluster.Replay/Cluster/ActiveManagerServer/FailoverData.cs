using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200008F RID: 143
	internal class FailoverData : IActionData
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001B102 File Offset: 0x00019302
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0001B10A File Offset: 0x0001930A
		public ExDateTime Time { get; private set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001B113 File Offset: 0x00019313
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x0001B11B File Offset: 0x0001931B
		public string DataStr { get; private set; }

		// Token: 0x06000557 RID: 1367 RVA: 0x0001B124 File Offset: 0x00019324
		public FailoverData()
		{
			this.Initialize(ExDateTime.Now, string.Empty);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001B13C File Offset: 0x0001933C
		public void Initialize(ExDateTime actionTime, string dataStr)
		{
			this.Time = actionTime;
			this.DataStr = dataStr;
		}
	}
}
