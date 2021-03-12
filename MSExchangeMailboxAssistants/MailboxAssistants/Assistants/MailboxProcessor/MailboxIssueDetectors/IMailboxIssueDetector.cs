using System;
using Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor.MailboxProcessorDefinitions;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor.MailboxIssueDetectors
{
	// Token: 0x02000235 RID: 565
	internal interface IMailboxIssueDetector : IMailboxProcessor
	{
		// Token: 0x0600154F RID: 5455
		bool IsMailboxProblemDetected(MailboxProcessorMailboxData mailboxData);

		// Token: 0x06001550 RID: 5456
		MailboxProcessorNotificationEntry GetMailboxInformation(MailboxProcessorMailboxData mailboxData);

		// Token: 0x06001551 RID: 5457
		void SubmitToRepair(MailboxProcessorNotificationEntry detectedMailbox);
	}
}
