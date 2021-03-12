using System;
using Microsoft.Exchange.WorkingSet.SignalApi;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.WorkingSetActionProcessors
{
	// Token: 0x020000CE RID: 206
	internal interface IActionProcessor
	{
		// Token: 0x06000654 RID: 1620
		void Process(StoreDriverDeliveryEventArgsImpl argsImpl, Action action, int traceId);
	}
}
