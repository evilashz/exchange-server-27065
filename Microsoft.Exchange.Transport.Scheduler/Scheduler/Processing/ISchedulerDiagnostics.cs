using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000013 RID: 19
	internal interface ISchedulerDiagnostics
	{
		// Token: 0x0600004A RID: 74
		void Received();

		// Token: 0x0600004B RID: 75
		void Dispatched();

		// Token: 0x0600004C RID: 76
		void Throttled();

		// Token: 0x0600004D RID: 77
		void Processed();

		// Token: 0x0600004E RID: 78
		void ScopedQueueCreated(int count);

		// Token: 0x0600004F RID: 79
		void ScopedQueueDestroyed(int count);

		// Token: 0x06000050 RID: 80
		void VisitCurrentScopedQueues(IDictionary<IMessageScope, ScopedQueue> currentQueues);

		// Token: 0x06000051 RID: 81
		void RegisterQueueLogging(IQueueLogProvider logProvider);

		// Token: 0x06000052 RID: 82
		SchedulerDiagnosticsInfo GetDiagnosticsInfo();
	}
}
