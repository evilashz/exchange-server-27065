using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Transport.Agent.TrustedMail
{
	// Token: 0x02000008 RID: 8
	public sealed class OutboundTrustAgentFactory : RoutingAgentFactory
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000333E File Offset: 0x0000153E
		internal string ComputerName
		{
			get
			{
				return this.computerName;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003346 File Offset: 0x00001546
		public OutboundTrustAgentFactory()
		{
			this.computerName = ComputerInformation.DnsPhysicalFullyQualifiedDomainName;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003359 File Offset: 0x00001559
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new OutboundTrustAgent(this, server, TrustedMailUtils.TrustedMailAgentsEnabled);
		}

		// Token: 0x0400001A RID: 26
		private readonly string computerName;
	}
}
