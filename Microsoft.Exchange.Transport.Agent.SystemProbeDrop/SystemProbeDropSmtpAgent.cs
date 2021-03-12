using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Agent.SystemProbeDrop
{
	// Token: 0x02000005 RID: 5
	internal sealed class SystemProbeDropSmtpAgent : SmtpReceiveAgent
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002237 File Offset: 0x00000437
		public SystemProbeDropSmtpAgent()
		{
			base.OnEndOfHeaders += this.OnEndOfHeadersHandler;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002251 File Offset: 0x00000451
		public void OnEndOfHeadersHandler(ReceiveMessageEventSource source, EndOfHeadersEventArgs args)
		{
			if (SystemProbeDropHelper.IsAgentEnabled() && args.Headers != null && SystemProbeDropHelper.ShouldDropMessage(args.Headers, "OnEndOfHeaders"))
			{
				SystemProbeDropHelper.DiscardMessage(args.MailItem);
			}
		}
	}
}
