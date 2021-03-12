using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x02000027 RID: 39
	internal sealed class UnwrapJournalGlobals
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00007A72 File Offset: 0x00005C72
		public static TimeSpan RetryIntervalOnError
		{
			get
			{
				return UnwrapJournalGlobals.retryInterval;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00007A79 File Offset: 0x00005C79
		public static ExEventLog Logger
		{
			get
			{
				return UnwrapJournalGlobals.logger;
			}
		}

		// Token: 0x0400010A RID: 266
		public const string RegistryKeyPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Exchange_Test\\v15\\BCM";

		// Token: 0x0400010B RID: 267
		public const string DisableUnJournalingInDCKeyName = "DisableUnJournalAgent";

		// Token: 0x0400010C RID: 268
		public const string EhaMigrationMailboxName = "ehamigrationmailbox";

		// Token: 0x0400010D RID: 269
		public const string OfficialAgentName = "Unwrap Journal Agent";

		// Token: 0x0400010E RID: 270
		public const string EhaDeliveryPriorityReason = "eha legacy archive journaling";

		// Token: 0x0400010F RID: 271
		public const string LiveArchiveJournalDeliveryPriorityReason = "Live archive journaling";

		// Token: 0x04000110 RID: 272
		public const string ProcessedOnSubmitted = "Microsoft.Exchange.MessagingPolicies.UnJournalAgent.ProcessedOnSubmitted";

		// Token: 0x04000111 RID: 273
		public const string ProcessedInternalJournalReport = "Microsoft.Exchange.Journaling.ProcessedOnRoutedInternalJournalReport";

		// Token: 0x04000112 RID: 274
		public const string ProcessedOnSubmittedForJournalNdr = "Microsoft.Exchange.MessagingPolicies.UnJournalAgent.ProcessedOnSubmittedForJournalNdr";

		// Token: 0x04000113 RID: 275
		private static TimeSpan retryInterval = new TimeSpan(0, 20, 0);

		// Token: 0x04000114 RID: 276
		private static ExEventLog logger = new ExEventLog(new Guid("7D2A0005-2C75-42ac-B495-8FE62F3B4FCF"), "MSExchange Messaging Policies");
	}
}
