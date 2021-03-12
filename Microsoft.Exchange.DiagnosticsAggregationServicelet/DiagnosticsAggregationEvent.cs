using System;

namespace Microsoft.Exchange.Servicelets.DiagnosticsAggregation
{
	// Token: 0x02000002 RID: 2
	internal enum DiagnosticsAggregationEvent
	{
		// Token: 0x04000002 RID: 2
		LogStarted,
		// Token: 0x04000003 RID: 3
		Information,
		// Token: 0x04000004 RID: 4
		LocalViewRequestSent,
		// Token: 0x04000005 RID: 5
		LocalViewRequestSentFailed,
		// Token: 0x04000006 RID: 6
		LocalViewRequestReceived,
		// Token: 0x04000007 RID: 7
		LocalViewRequestReceivedFailed,
		// Token: 0x04000008 RID: 8
		LocalViewResponseSent,
		// Token: 0x04000009 RID: 9
		LocalViewResponseReceived,
		// Token: 0x0400000A RID: 10
		AggregatedViewRequestReceived,
		// Token: 0x0400000B RID: 11
		AggregatedViewRequestReceivedFailed,
		// Token: 0x0400000C RID: 12
		AggregatedViewResponseSent,
		// Token: 0x0400000D RID: 13
		QueueSnapshotFileReadSucceeded,
		// Token: 0x0400000E RID: 14
		QueueSnapshotFileReadFailed,
		// Token: 0x0400000F RID: 15
		OutOfResources,
		// Token: 0x04000010 RID: 16
		ServiceletError
	}
}
