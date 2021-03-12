using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000227 RID: 551
	public struct StoreDriverDeliveryTags
	{
		// Token: 0x04000C41 RID: 3137
		public const int StoreDriverDelivery = 0;

		// Token: 0x04000C42 RID: 3138
		public const int MapiDeliver = 2;

		// Token: 0x04000C43 RID: 3139
		public const int BridgeheadPicker = 4;

		// Token: 0x04000C44 RID: 3140
		public const int CalendarProcessing = 5;

		// Token: 0x04000C45 RID: 3141
		public const int ExceptionHandling = 6;

		// Token: 0x04000C46 RID: 3142
		public const int MeetingForwardNotification = 7;

		// Token: 0x04000C47 RID: 3143
		public const int ApprovalAgent = 8;

		// Token: 0x04000C48 RID: 3144
		public const int MailboxRule = 9;

		// Token: 0x04000C49 RID: 3145
		public const int ModeratedTransport = 10;

		// Token: 0x04000C4A RID: 3146
		public const int Conversations = 12;

		// Token: 0x04000C4B RID: 3147
		public const int UMPlayonPhoneAgent = 15;

		// Token: 0x04000C4C RID: 3148
		public const int SmsDeliveryAgent = 16;

		// Token: 0x04000C4D RID: 3149
		public const int UMPartnerMessageAgent = 17;

		// Token: 0x04000C4E RID: 3150
		public const int FaultInjection = 18;

		// Token: 0x04000C4F RID: 3151
		public const int GroupEscalationAgent = 22;

		// Token: 0x04000C50 RID: 3152
		public const int MeetingMessageProcessingAgent = 23;

		// Token: 0x04000C51 RID: 3153
		public const int MeetingSeriesMessageOrderingAgent = 24;

		// Token: 0x04000C52 RID: 3154
		public const int SharedMailboxSentItemsAgent = 26;

		// Token: 0x04000C53 RID: 3155
		public static Guid guid = new Guid("D81003EF-1A7B-4AF0-BA18-236DB5A83114");
	}
}
