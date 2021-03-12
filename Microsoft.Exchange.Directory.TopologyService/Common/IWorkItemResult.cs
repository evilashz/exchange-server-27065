using System;

namespace Microsoft.Exchange.Directory.TopologyService.Common
{
	// Token: 0x02000027 RID: 39
	internal interface IWorkItemResult
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000143 RID: 323
		Exception Exception { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000144 RID: 324
		string WorkItemId { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000145 RID: 325
		ResultType ResultType { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000146 RID: 326
		int Latency { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000147 RID: 327
		DateTime WhenStarted { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000148 RID: 328
		DateTime WhenCompleted { get; }
	}
}
