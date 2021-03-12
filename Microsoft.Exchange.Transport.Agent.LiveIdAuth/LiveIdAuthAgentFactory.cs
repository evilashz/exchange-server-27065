using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Agent.LiveIdAuth
{
	// Token: 0x02000003 RID: 3
	public sealed class LiveIdAuthAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000023C1 File Offset: 0x000005C1
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			return new LiveIdAuthAgent();
		}
	}
}
