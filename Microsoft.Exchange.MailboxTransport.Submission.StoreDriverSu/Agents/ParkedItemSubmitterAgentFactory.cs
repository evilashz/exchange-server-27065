using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission.Agents
{
	// Token: 0x02000009 RID: 9
	internal class ParkedItemSubmitterAgentFactory : StoreDriverAgentFactory
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00003811 File Offset: 0x00001A11
		public override StoreDriverAgent CreateAgent(SmtpServer server)
		{
			return new ParkedItemSubmitterAgent();
		}
	}
}
