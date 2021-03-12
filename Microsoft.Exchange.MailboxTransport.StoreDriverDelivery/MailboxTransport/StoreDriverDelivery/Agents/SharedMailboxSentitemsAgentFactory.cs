using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.SharedMailboxSentItems;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000B9 RID: 185
	internal sealed class SharedMailboxSentitemsAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x0001FC81 File Offset: 0x0001DE81
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new SharedMailboxSentItemsAgent(new PerformanceCountersFactory(), new Logger(SharedMailboxSentitemsAgentFactory.TracerInstance));
		}

		// Token: 0x04000351 RID: 849
		private static readonly Trace TracerInstance = ExTraceGlobals.SharedMailboxSentItemsAgentTracer;
	}
}
