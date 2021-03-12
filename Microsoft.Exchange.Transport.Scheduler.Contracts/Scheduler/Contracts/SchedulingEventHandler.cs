using System;

namespace Microsoft.Exchange.Transport.Scheduler.Contracts
{
	// Token: 0x02000004 RID: 4
	// (Invoke) Token: 0x06000005 RID: 5
	internal delegate void SchedulingEventHandler(SchedulingState state, ISchedulableMessage message);
}
