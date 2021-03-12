using System;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001F3 RID: 499
	internal struct AggregatedOperationStatistics
	{
		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003CB6C File Offset: 0x0003AD6C
		public static AggregatedOperationStatistics operator -(AggregatedOperationStatistics s1, AggregatedOperationStatistics s2)
		{
			return new AggregatedOperationStatistics
			{
				Count = s1.Count - s2.Count,
				TotalMilliseconds = s1.TotalMilliseconds - s2.TotalMilliseconds
			};
		}

		// Token: 0x04000A8D RID: 2701
		public AggregatedOperationType Type;

		// Token: 0x04000A8E RID: 2702
		public long Count;

		// Token: 0x04000A8F RID: 2703
		public double TotalMilliseconds;
	}
}
