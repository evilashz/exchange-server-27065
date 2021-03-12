using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000225 RID: 549
	public struct StoreDriverTags
	{
		// Token: 0x04000C29 RID: 3113
		public const int StoreDriver = 0;

		// Token: 0x04000C2A RID: 3114
		public const int MapiSubmit = 1;

		// Token: 0x04000C2B RID: 3115
		public const int MapiDeliver = 2;

		// Token: 0x04000C2C RID: 3116
		public const int MailSubmissionService = 3;

		// Token: 0x04000C2D RID: 3117
		public const int BridgeheadPicker = 4;

		// Token: 0x04000C2E RID: 3118
		public const int CalendarProcessing = 5;

		// Token: 0x04000C2F RID: 3119
		public const int ExceptionHandling = 6;

		// Token: 0x04000C30 RID: 3120
		public const int MeetingForwardNotification = 7;

		// Token: 0x04000C31 RID: 3121
		public const int ApprovalAgent = 8;

		// Token: 0x04000C32 RID: 3122
		public const int MailboxRule = 9;

		// Token: 0x04000C33 RID: 3123
		public const int ModeratedTransport = 10;

		// Token: 0x04000C34 RID: 3124
		public const int Conversations = 12;

		// Token: 0x04000C35 RID: 3125
		public const int MailSubmissionRedundancyManager = 14;

		// Token: 0x04000C36 RID: 3126
		public const int UMPlayonPhoneAgent = 15;

		// Token: 0x04000C37 RID: 3127
		public const int SmsDeliveryAgent = 16;

		// Token: 0x04000C38 RID: 3128
		public const int UMPartnerMessageAgent = 17;

		// Token: 0x04000C39 RID: 3129
		public const int FaultInjection = 18;

		// Token: 0x04000C3A RID: 3130
		public const int SubmissionConnection = 19;

		// Token: 0x04000C3B RID: 3131
		public const int SubmissionConnectionPool = 20;

		// Token: 0x04000C3C RID: 3132
		public const int UnJournalDeliveryAgent = 21;

		// Token: 0x04000C3D RID: 3133
		public static Guid guid = new Guid("a77be922-83fd-4eb1-9e88-6caadbc7340e");
	}
}
