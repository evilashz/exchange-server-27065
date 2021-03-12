using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000017 RID: 23
	internal enum ContactsUploaderPerformanceTrackerSchema
	{
		// Token: 0x04000058 RID: 88
		[DisplayName("CU", "Read")]
		ContactsRead,
		// Token: 0x04000059 RID: 89
		[DisplayName("CU", "Exported")]
		ContactsExported,
		// Token: 0x0400005A RID: 90
		[DisplayName("CU", "Received")]
		ContactsReceived,
		// Token: 0x0400005B RID: 91
		[DisplayName("CU", "RpcC")]
		RpcCount,
		// Token: 0x0400005C RID: 92
		[DisplayName("CU", "RpcT")]
		RpcLatency,
		// Token: 0x0400005D RID: 93
		[DisplayName("CU", "Time")]
		CpuTime,
		// Token: 0x0400005E RID: 94
		[DisplayName("CU", "Size")]
		DataSize,
		// Token: 0x0400005F RID: 95
		[DisplayName("CU", "Result")]
		Result
	}
}
