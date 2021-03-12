using System;
using Microsoft.Exchange.MailboxTransport.Shared.Smtp;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000020 RID: 32
	internal static class OutProvider
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000095B4 File Offset: 0x000077B4
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000095E9 File Offset: 0x000077E9
		public static IOutProvider OutProviderInstance
		{
			get
			{
				if (OutProvider.outProvider == null)
				{
					if (SubmissionConfiguration.Instance.App.IsWriteToPickupFolderEnabled)
					{
						OutProvider.outProvider = PickupFolderOutProvider.Instance;
					}
					else
					{
						OutProvider.outProvider = SMTPOutProvider.Instance;
					}
				}
				return OutProvider.outProvider;
			}
			set
			{
				OutProvider.outProvider = value;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000095F1 File Offset: 0x000077F1
		public static SmtpMailItemResult SendMessage(SubmissionReadOnlyMailItem submissionReadOnlyMailItem)
		{
			return OutProvider.OutProviderInstance.SendMessage(submissionReadOnlyMailItem);
		}

		// Token: 0x04000094 RID: 148
		private static IOutProvider outProvider;
	}
}
