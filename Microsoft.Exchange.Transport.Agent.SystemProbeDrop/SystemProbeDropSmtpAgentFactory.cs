using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Agent.SystemProbeDrop
{
	// Token: 0x02000006 RID: 6
	public sealed class SystemProbeDropSmtpAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000227F File Offset: 0x0000047F
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			return new SystemProbeDropSmtpAgent();
		}
	}
}
