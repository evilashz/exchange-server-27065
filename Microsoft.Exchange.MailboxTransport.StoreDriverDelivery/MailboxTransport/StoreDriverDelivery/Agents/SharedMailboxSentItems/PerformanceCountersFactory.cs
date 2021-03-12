using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.SharedMailboxSentItems
{
	// Token: 0x020000B7 RID: 183
	internal class PerformanceCountersFactory : IPerformanceCountersFactory
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x0001F5E6 File Offset: 0x0001D7E6
		public IPerformanceCounters GetCounterInstance(string mdbGuid)
		{
			return new PerformanceCounters(SharedMailboxSentItemsDeliveryAgent.GetInstance(mdbGuid));
		}
	}
}
