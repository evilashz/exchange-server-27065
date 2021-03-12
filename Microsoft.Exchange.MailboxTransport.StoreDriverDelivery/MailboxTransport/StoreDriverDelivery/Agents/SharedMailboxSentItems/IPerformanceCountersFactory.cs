using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.SharedMailboxSentItems
{
	// Token: 0x020000B3 RID: 179
	internal interface IPerformanceCountersFactory
	{
		// Token: 0x060005BD RID: 1469
		IPerformanceCounters GetCounterInstance(string mdbGuid);
	}
}
