using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200003B RID: 59
	internal enum FindTrendingConversationMetadata
	{
		// Token: 0x040002B0 RID: 688
		[DisplayName("FTC", "TRC")]
		TotalRowCount,
		// Token: 0x040002B1 RID: 689
		[DisplayName("FTC", "QT")]
		AggregatedConversationQueryTime,
		// Token: 0x040002B2 RID: 690
		[DisplayName("FTC", "QRC")]
		AggregatedConversationQueryRpcCount,
		// Token: 0x040002B3 RID: 691
		[DisplayName("FTC", "QRL")]
		AggregatedConversationQueryRpcLatency,
		// Token: 0x040002B4 RID: 692
		[DisplayName("FTC", "QRLS")]
		AggregatedConversationQueryRpcLatencyOnStore,
		// Token: 0x040002B5 RID: 693
		[DisplayName("FTC", "QCpu")]
		AggregatedConversationQueryCPUTime,
		// Token: 0x040002B6 RID: 694
		[DisplayName("FTC", "QSTS")]
		AggregatedConversationQueryStartTimestamp,
		// Token: 0x040002B7 RID: 695
		[DisplayName("FTC", "QETS")]
		AggregatedConversationQueryEndTimestamp,
		// Token: 0x040002B8 RID: 696
		[DisplayName("FTC", "RT")]
		AggregatedConversationRankingTime,
		// Token: 0x040002B9 RID: 697
		[DisplayName("FTC", "RTC")]
		ReturnedTrendingConversations
	}
}
