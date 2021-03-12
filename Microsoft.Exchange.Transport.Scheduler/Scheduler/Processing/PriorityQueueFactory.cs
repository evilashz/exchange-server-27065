using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000026 RID: 38
	internal class PriorityQueueFactory : IQueueFactory
	{
		// Token: 0x060000BB RID: 187 RVA: 0x0000450C File Offset: 0x0000270C
		public PriorityQueueFactory(IDictionary<DeliveryPriority, int> distributions)
		{
			ArgumentValidator.ThrowIfNull("distributions", distributions);
			this.distributions = distributions;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004526 File Offset: 0x00002726
		public ISchedulerQueue CreateNewQueueInstance()
		{
			return new PriorityDistributedQueue(this.distributions);
		}

		// Token: 0x0400006D RID: 109
		private readonly IDictionary<DeliveryPriority, int> distributions;
	}
}
