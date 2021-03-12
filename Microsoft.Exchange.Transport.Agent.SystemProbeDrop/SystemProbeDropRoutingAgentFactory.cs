using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.Transport.Agent.SystemProbeDrop
{
	// Token: 0x02000004 RID: 4
	public sealed class SystemProbeDropRoutingAgentFactory : RoutingAgentFactory
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002228 File Offset: 0x00000428
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new SystemProbeDropRoutingAgent();
		}
	}
}
