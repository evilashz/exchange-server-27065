using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission.Agents
{
	// Token: 0x02000007 RID: 7
	internal class MfnSubmitterAgentFactory : StoreDriverAgentFactory
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00003715 File Offset: 0x00001915
		public override StoreDriverAgent CreateAgent(SmtpServer server)
		{
			return new MfnSubmitterAgent();
		}
	}
}
