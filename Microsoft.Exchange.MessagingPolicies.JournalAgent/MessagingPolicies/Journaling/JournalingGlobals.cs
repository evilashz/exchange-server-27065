using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000017 RID: 23
	internal sealed class JournalingGlobals
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000098A4 File Offset: 0x00007AA4
		public static ExEventLog Logger
		{
			get
			{
				return JournalingGlobals.logger;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000098AB File Offset: 0x00007AAB
		public static TimeSpan RetryIntervalOnError
		{
			get
			{
				return JournalingGlobals.retryInterval;
			}
		}

		// Token: 0x0400008A RID: 138
		public const string OfficialAgentName = "Journaling";

		// Token: 0x0400008B RID: 139
		public const string RecipientInfoProperty = "Microsoft.Exchange.Journaling.OriginalRecipientInfo";

		// Token: 0x0400008C RID: 140
		public const string ExternalFlagProperty = "Microsoft.Exchange.Journaling.External";

		// Token: 0x0400008D RID: 141
		public const string InternalFlagProperty = "Microsoft.Exchange.Journaling.Internal";

		// Token: 0x0400008E RID: 142
		public const string ProcessedOnSubmitted = "Microsoft.Exchange.Journaling.ProcessedOnSubmitted";

		// Token: 0x0400008F RID: 143
		public const string ProcessedOnRouted = "Microsoft.Exchange.Journaling.ProcessedOnRouted";

		// Token: 0x04000090 RID: 144
		public const string ProcessedInternalJournalReport = "Microsoft.Exchange.Journaling.ProcessedOnRoutedInternalJournalReport";

		// Token: 0x04000091 RID: 145
		public const string ProcessedOnSubmittedByUnJournalAgent = "Microsoft.Exchange.MessagingPolicies.UnJournalAgent.ProcessedOnSubmitted";

		// Token: 0x04000092 RID: 146
		public const string ProcessedMessage = "X-MS-Exchange-Organization-Processed-By-Journaling";

		// Token: 0x04000093 RID: 147
		public const string ProcessedMessageByGcc = "X-MS-Exchange-Organization-Processed-By-Gcc-Journaling";

		// Token: 0x04000094 RID: 148
		public const string AuthMechanism = "X-MS-Exchange-Organization-AuthMechanism";

		// Token: 0x04000095 RID: 149
		public const int MapiSubmitMechanism = 3;

		// Token: 0x04000096 RID: 150
		public const int SecureMapiSubmitMechanism = 4;

		// Token: 0x04000097 RID: 151
		public const int SecureInternalSubmit = 5;

		// Token: 0x04000098 RID: 152
		public const string FqdnForMessageId = "journal.report.generator";

		// Token: 0x04000099 RID: 153
		public const string ProcessedByUnjournalHeader = "X-MS-Exchange-Organization-Unjournal-Processed";

		// Token: 0x0400009A RID: 154
		public const string ProcessedByUnjournalForNdrHeader = "X-MS-Exchange-Organization-Unjournal-ProcessedNdr";

		// Token: 0x0400009B RID: 155
		public const string InternalJournalReportHeader = "X-MS-InternalJournal";

		// Token: 0x0400009C RID: 156
		public const string OriginalMessageId = "Microsoft.Exchange.Journaling.OriginalMessageId";

		// Token: 0x0400009D RID: 157
		public const string IsGccFlag = "Microsoft.Exchange.Journaling.IsGccFlag";

		// Token: 0x0400009E RID: 158
		public const string RuleIds = "Microsoft.Exchange.Journaling.RuleIds";

		// Token: 0x0400009F RID: 159
		public const string TransportSettingsContainer = "Transport Settings";

		// Token: 0x040000A0 RID: 160
		private static ExEventLog logger = new ExEventLog(new Guid("7D2A0005-2C75-42ac-B495-8FE62F3B4FCF"), "MSExchange Messaging Policies");

		// Token: 0x040000A1 RID: 161
		private static TimeSpan retryInterval = new TimeSpan(0, 20, 0);
	}
}
