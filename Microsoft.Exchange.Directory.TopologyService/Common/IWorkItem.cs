using System;

namespace Microsoft.Exchange.Directory.TopologyService.Common
{
	// Token: 0x02000002 RID: 2
	internal interface IWorkItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		string Id { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		bool IsCompleted { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		bool IsPending { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4
		ResultType ResultType { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5
		DateTime WhenStarted { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000006 RID: 6
		DateTime WhenCompleted { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000007 RID: 7
		TimeSpan TimeoutInterval { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000008 RID: 8
		bool IsOverdue { get; }

		// Token: 0x06000009 RID: 9
		void StartExecuting(Action<IWorkItemResult> continuation);

		// Token: 0x0600000A RID: 10
		void StartCancel(int waitAmount, Action<IWorkItemResult> continuation);
	}
}
