using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000229 RID: 553
	public struct StoreDriverSubmissionTags
	{
		// Token: 0x04000C57 RID: 3159
		public const int StoreDriverSubmission = 0;

		// Token: 0x04000C58 RID: 3160
		public const int MapiStoreDriverSubmission = 1;

		// Token: 0x04000C59 RID: 3161
		public const int MailboxTransportSubmissionService = 3;

		// Token: 0x04000C5A RID: 3162
		public const int MeetingForwardNotification = 7;

		// Token: 0x04000C5B RID: 3163
		public const int ModeratedTransport = 10;

		// Token: 0x04000C5C RID: 3164
		public const int FaultInjection = 18;

		// Token: 0x04000C5D RID: 3165
		public const int SubmissionConnection = 19;

		// Token: 0x04000C5E RID: 3166
		public const int SubmissionConnectionPool = 20;

		// Token: 0x04000C5F RID: 3167
		public const int ParkedItemSubmitterAgent = 21;

		// Token: 0x04000C60 RID: 3168
		public static Guid guid = new Guid("2b76aa96-0fe5-4c87-8101-1d236c9fa3ab");
	}
}
