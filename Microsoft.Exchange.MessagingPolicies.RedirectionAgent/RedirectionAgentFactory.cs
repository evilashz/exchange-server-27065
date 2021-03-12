using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.MessagingPolicies.Redirection
{
	// Token: 0x02000002 RID: 2
	public class RedirectionAgentFactory : RoutingAgentFactory
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new RedirectionAgent();
		}
	}
}
