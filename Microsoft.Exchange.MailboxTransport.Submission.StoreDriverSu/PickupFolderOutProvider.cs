using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.MailboxTransport.Shared.Smtp;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000022 RID: 34
	internal class PickupFolderOutProvider : IOutProvider
	{
		// Token: 0x06000143 RID: 323 RVA: 0x0000966B File Offset: 0x0000786B
		private PickupFolderOutProvider()
		{
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00009673 File Offset: 0x00007873
		public static PickupFolderOutProvider Instance
		{
			get
			{
				return PickupFolderOutProvider.instance;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000967C File Offset: 0x0000787C
		public SmtpMailItemResult SendMessage(SubmissionReadOnlyMailItem submissionReadOnlyMailItem)
		{
			PickupFolder pickupFolder = new PickupFolder();
			SmtpResponse smtpResponse;
			string text;
			pickupFolder.WriteMessage(submissionReadOnlyMailItem.MailItem, submissionReadOnlyMailItem.Recipients, out smtpResponse, out text);
			return null;
		}

		// Token: 0x04000097 RID: 151
		private static readonly PickupFolderOutProvider instance = new PickupFolderOutProvider();
	}
}
