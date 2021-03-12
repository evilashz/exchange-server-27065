using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000011 RID: 17
	internal interface IQueueLogProvider
	{
		// Token: 0x06000048 RID: 72
		IEnumerable<QueueLogInfo> FlushLogs(DateTime checkpoint, ISchedulerMetering metering);
	}
}
