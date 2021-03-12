using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.SharedMailboxSentItems
{
	// Token: 0x020000B2 RID: 178
	internal interface IPerformanceCounters
	{
		// Token: 0x060005BA RID: 1466
		void IncrementSentItemsMessages();

		// Token: 0x060005BB RID: 1467
		void IncrementErrors();

		// Token: 0x060005BC RID: 1468
		void UpdateAverageMessageCopyTime(TimeSpan elapsedTime);
	}
}
