using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Transport.Agent.TrustedMail
{
	// Token: 0x02000006 RID: 6
	public sealed class InboundTrustAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002C6E File Offset: 0x00000E6E
		internal bool IsFrontEndTransport
		{
			get
			{
				return this.isFrontEndTransport;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002C76 File Offset: 0x00000E76
		internal string ComputerName
		{
			get
			{
				return this.computerName;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002C7E File Offset: 0x00000E7E
		public InboundTrustAgentFactory()
		{
			this.computerName = ComputerInformation.DnsPhysicalFullyQualifiedDomainName;
			this.isFrontEndTransport = (Components.Configuration.ProcessTransportRole == ProcessTransportRole.FrontEnd);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			return new InboundTrustAgent(server, this, TrustedMailUtils.TrustedMailAgentsEnabled);
		}

		// Token: 0x04000012 RID: 18
		private readonly bool isFrontEndTransport;

		// Token: 0x04000013 RID: 19
		private readonly string computerName;
	}
}
