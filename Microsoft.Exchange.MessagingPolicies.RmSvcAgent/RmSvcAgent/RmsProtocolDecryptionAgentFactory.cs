using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000027 RID: 39
	public sealed class RmsProtocolDecryptionAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x0000A38C File Offset: 0x0000858C
		public RmsProtocolDecryptionAgentFactory()
		{
			RmsDecryptionAgentPerfCounters.MessageDecrypted.RawValue = 0L;
			RmsDecryptionAgentPerfCounters.MessageFailedToDecrypt.RawValue = 0L;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000A3AC File Offset: 0x000085AC
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			return new RmsProtocolDecryptionAgent();
		}
	}
}
