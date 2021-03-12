using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;

namespace Microsoft.Exchange.Transport.Agent.SharedMailboxSentItemsRoutingAgent
{
	// Token: 0x0200000B RID: 11
	internal sealed class SharedMailboxSentItemsRoutingFactory : RoutingAgentFactory
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002964 File Offset: 0x00000B64
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			ITracer sharedMailboxSentItemsAgentTracer = ExTraceGlobals.SharedMailboxSentItemsAgentTracer;
			return new SharedMailboxSentItemsRoutingAgent(new SharedMailboxConfigurationFactory(), new SentItemWrapperCreator(sharedMailboxSentItemsAgentTracer), sharedMailboxSentItemsAgentTracer);
		}
	}
}
