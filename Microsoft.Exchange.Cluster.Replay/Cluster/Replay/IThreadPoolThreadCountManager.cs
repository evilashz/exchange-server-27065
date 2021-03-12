using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000160 RID: 352
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IThreadPoolThreadCountManager
	{
		// Token: 0x06000E22 RID: 3618
		bool SetMinThreads(int workerThreads, int? completionPortThreads, bool force);

		// Token: 0x06000E23 RID: 3619
		bool IncrementMinThreadsBy(int incWorkerThreadsBy, int? incCompletionPortThreadsBy);

		// Token: 0x06000E24 RID: 3620
		bool DecrementMinThreadsBy(int decWorkerThreadsBy, int? decCompletionPortThreadsBy);

		// Token: 0x06000E25 RID: 3621
		bool Reset(bool force);
	}
}
