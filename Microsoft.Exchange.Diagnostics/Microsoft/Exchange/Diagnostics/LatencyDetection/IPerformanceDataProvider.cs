using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000169 RID: 361
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPerformanceDataProvider
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000A4A RID: 2634
		string Name { get; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000A4B RID: 2635
		bool ThreadLocal { get; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000A4C RID: 2636
		string Operations { get; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000A4D RID: 2637
		bool IsSnapshotInProgress { get; }

		// Token: 0x06000A4E RID: 2638
		PerformanceData TakeSnapshot(bool begin);

		// Token: 0x06000A4F RID: 2639
		void ResetOperations();
	}
}
