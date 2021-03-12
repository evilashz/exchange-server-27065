using System;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200000B RID: 11
	internal interface IQueueFactory
	{
		// Token: 0x0600002A RID: 42
		ISchedulerQueue CreateNewQueueInstance();
	}
}
