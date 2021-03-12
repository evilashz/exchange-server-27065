using System;

namespace Microsoft.Exchange.Transport.Scheduler.Contracts
{
	// Token: 0x02000006 RID: 6
	internal interface IProcessingScheduler
	{
		// Token: 0x06000008 RID: 8
		void Submit(ISchedulableMessage message);

		// Token: 0x06000009 RID: 9
		bool TryGetNext(out ISchedulableMessage message);

		// Token: 0x0600000A RID: 10
		void SubscribeToEvent(SchedulingState state, SchedulingEventHandler handler);

		// Token: 0x0600000B RID: 11
		void UnsubscribeFromEvent(SchedulingState state, SchedulingEventHandler handler);
	}
}
