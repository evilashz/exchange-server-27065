using System;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000009 RID: 9
	internal interface ISchedulerQueue
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31
		bool IsEmpty { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32
		long Count { get; }

		// Token: 0x06000021 RID: 33
		void Enqueue(ISchedulableMessage message);

		// Token: 0x06000022 RID: 34
		bool TryDequeue(out ISchedulableMessage message);

		// Token: 0x06000023 RID: 35
		bool TryPeek(out ISchedulableMessage message);
	}
}
