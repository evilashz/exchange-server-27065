using System;
using Microsoft.Exchange.MailboxTransport.Shared.Smtp;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200001F RID: 31
	internal interface IOutProvider
	{
		// Token: 0x0600013B RID: 315
		SmtpMailItemResult SendMessage(SubmissionReadOnlyMailItem submissionReadOnlyMailItem);
	}
}
