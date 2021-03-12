using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.JobQueues
{
	// Token: 0x0200073A RID: 1850
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Configuration
	{
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x0004A2B3 File Offset: 0x000484B3
		// (set) Token: 0x060023E1 RID: 9185 RVA: 0x0004A2BB File Offset: 0x000484BB
		public int MaxAllowedQueueLength { get; private set; }

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x0004A2C4 File Offset: 0x000484C4
		// (set) Token: 0x060023E3 RID: 9187 RVA: 0x0004A2CC File Offset: 0x000484CC
		public int MaxAllowedPendingJobCount { get; private set; }

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x0004A2D5 File Offset: 0x000484D5
		// (set) Token: 0x060023E5 RID: 9189 RVA: 0x0004A2DD File Offset: 0x000484DD
		public TimeSpan DispatcherWakeUpInterval { get; private set; }

		// Token: 0x060023E6 RID: 9190 RVA: 0x0004A2E6 File Offset: 0x000484E6
		public Configuration(int maxAllowedQueueLength, int maxAllowedPendingJobCount, TimeSpan dispatcherWakeUpInterval)
		{
			this.MaxAllowedPendingJobCount = maxAllowedPendingJobCount;
			this.MaxAllowedQueueLength = maxAllowedQueueLength;
			this.DispatcherWakeUpInterval = dispatcherWakeUpInterval;
		}
	}
}
