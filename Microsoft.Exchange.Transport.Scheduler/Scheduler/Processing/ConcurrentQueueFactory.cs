using System;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200000C RID: 12
	internal class ConcurrentQueueFactory : IQueueFactory
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000029F7 File Offset: 0x00000BF7
		public ISchedulerQueue CreateNewQueueInstance()
		{
			return new ConcurrentQueueWrapper();
		}
	}
}
