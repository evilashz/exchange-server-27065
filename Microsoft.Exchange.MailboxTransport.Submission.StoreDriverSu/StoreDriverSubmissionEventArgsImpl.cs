using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000011 RID: 17
	internal class StoreDriverSubmissionEventArgsImpl : StoreDriverSubmissionEventArgs
	{
		// Token: 0x0600006B RID: 107 RVA: 0x000051FE File Offset: 0x000033FE
		internal StoreDriverSubmissionEventArgsImpl(MailItem mailItem, SubmissionItem submissionItem, MailItemSubmitter mailItemSubmitter)
		{
			this.mailItem = mailItem;
			this.submissionItem = submissionItem;
			this.mailItemSubmitter = mailItemSubmitter;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000521B File Offset: 0x0000341B
		public override MailItem MailItem
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00005223 File Offset: 0x00003423
		internal SubmissionItem SubmissionItem
		{
			get
			{
				return this.submissionItem;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000522B File Offset: 0x0000342B
		internal MailItemSubmitter MailItemSubmitter
		{
			get
			{
				return this.mailItemSubmitter;
			}
		}

		// Token: 0x04000026 RID: 38
		private MailItem mailItem;

		// Token: 0x04000027 RID: 39
		private SubmissionItem submissionItem;

		// Token: 0x04000028 RID: 40
		private MailItemSubmitter mailItemSubmitter;
	}
}
