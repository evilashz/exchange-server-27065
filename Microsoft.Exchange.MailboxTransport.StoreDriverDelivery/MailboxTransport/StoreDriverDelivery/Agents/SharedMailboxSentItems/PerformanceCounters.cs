using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.SharedMailboxSentItems
{
	// Token: 0x020000B6 RID: 182
	internal sealed class PerformanceCounters : IPerformanceCounters
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x0001F57B File Offset: 0x0001D77B
		public PerformanceCounters(SharedMailboxSentItemsDeliveryAgentInstance perfCounterInstance)
		{
			ArgumentValidator.ThrowIfNull("perfCounterInstance", perfCounterInstance);
			this.perfCounterInstance = perfCounterInstance;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001F595 File Offset: 0x0001D795
		public void IncrementSentItemsMessages()
		{
			this.perfCounterInstance.SentItemsMessages.Increment();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001F5A8 File Offset: 0x0001D7A8
		public void IncrementErrors()
		{
			this.perfCounterInstance.Errors.Increment();
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001F5BB File Offset: 0x0001D7BB
		public void UpdateAverageMessageCopyTime(TimeSpan elapsedTime)
		{
			this.perfCounterInstance.AverageMessageCopyTime.IncrementBy(elapsedTime.Ticks);
			this.perfCounterInstance.AverageMessageCopyTimeBase.Increment();
		}

		// Token: 0x0400034A RID: 842
		private readonly SharedMailboxSentItemsDeliveryAgentInstance perfCounterInstance;
	}
}
