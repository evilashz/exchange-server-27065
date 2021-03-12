using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission.Agents
{
	// Token: 0x02000003 RID: 3
	internal class ApprovalSubmitterAgentFactory : StoreDriverAgentFactory
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002236 File Offset: 0x00000436
		public override StoreDriverAgent CreateAgent(SmtpServer server)
		{
			return new ApprovalSubmitterAgent();
		}
	}
}
