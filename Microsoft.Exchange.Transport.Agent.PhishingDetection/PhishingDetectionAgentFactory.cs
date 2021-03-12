using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.Transport.Agent.PhishingDetection
{
	// Token: 0x02000005 RID: 5
	public sealed class PhishingDetectionAgentFactory : RoutingAgentFactory
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002750 File Offset: 0x00000950
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new PhishingDetectionAgent(server);
		}
	}
}
