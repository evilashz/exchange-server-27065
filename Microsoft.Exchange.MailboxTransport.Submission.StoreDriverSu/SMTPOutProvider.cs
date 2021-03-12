using System;
using Microsoft.Exchange.MailboxTransport.Shared.Smtp;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000021 RID: 33
	internal class SMTPOutProvider : IOutProvider
	{
		// Token: 0x0600013F RID: 319 RVA: 0x000095FE File Offset: 0x000077FE
		private SMTPOutProvider()
		{
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00009606 File Offset: 0x00007806
		public static SMTPOutProvider Instance
		{
			get
			{
				return SMTPOutProvider.instance;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00009610 File Offset: 0x00007810
		public SmtpMailItemResult SendMessage(SubmissionReadOnlyMailItem submissionReadOnlyMailItem)
		{
			if (submissionReadOnlyMailItem.Recipients.Count <= 0)
			{
				throw new ArgumentException("SMTPOutProvider-SendMessage: submissionReadOnlyMailItem.Recipients.Count should be greater than 0.");
			}
			return SmtpMailItemSender.Instance.Send(submissionReadOnlyMailItem, SubmissionConfiguration.Instance.App.UseLocalHubOnly, SubmissionConfiguration.Instance.App.SmtpOutWaitTimeOut);
		}

		// Token: 0x04000095 RID: 149
		private const string NextHopDomain = "MailboxTransportSubmissionInternalProxy";

		// Token: 0x04000096 RID: 150
		private static readonly SMTPOutProvider instance = new SMTPOutProvider();
	}
}
