using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000015 RID: 21
	internal static class MailboxTransportEventLogConstants
	{
		// Token: 0x0400006B RID: 107
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverPerfCountersLoadFailure = new ExEventLog.EventTuple(3221488620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400006C RID: 108
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverPoisonMessage = new ExEventLog.EventTuple(3221488625U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006D RID: 109
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverFailFastFailure = new ExEventLog.EventTuple(3221488626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006E RID: 110
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverPoisonMessageInMapiSubmit = new ExEventLog.EventTuple(3221488627U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006F RID: 111
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToGenerateNDRInMapiSubmit = new ExEventLog.EventTuple(3221488628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000070 RID: 112
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidSender = new ExEventLog.EventTuple(2147746808U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000071 RID: 113
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TooManySubmissionThreads = new ExEventLog.EventTuple(2147746809U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000072 RID: 114
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessingMeetingMessageFailure = new ExEventLog.EventTuple(2147746810U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000073 RID: 115
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeliveryFailedNoLegacyDN = new ExEventLog.EventTuple(3221488635U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000074 RID: 116
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverGetLocalIPFailure = new ExEventLog.EventTuple(3221488637U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000075 RID: 117
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverRegisterRpcServerFailure = new ExEventLog.EventTuple(3221488638U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000076 RID: 118
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowStoreDriverPoisonMessage = new ExEventLog.EventTuple(3221488639U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000077 RID: 119
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowStoreDriverPoisonMessageInSubmit = new ExEventLog.EventTuple(3221488640U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000078 RID: 120
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowFailedToGenerateNdrInSubmit = new ExEventLog.EventTuple(3221488641U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000079 RID: 121
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowInvalidSender = new ExEventLog.EventTuple(2147746819U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007A RID: 122
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OofHistoryCorruption = new ExEventLog.EventTuple(3221488644U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007B RID: 123
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OofHistoryFolderMissing = new ExEventLog.EventTuple(3221488645U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007C RID: 124
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ApprovalCannotStampExpiry = new ExEventLog.EventTuple(3221488646U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007D RID: 125
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UMPartnerMessageArrivedTooLate = new ExEventLog.EventTuple(2147746823U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007E RID: 126
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DeliveryHang = new ExEventLog.EventTuple(3221488648U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007F RID: 127
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ApprovalArbitrationMailboxQuota = new ExEventLog.EventTuple(2147746825U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000016 RID: 22
		private enum Category : short
		{
			// Token: 0x04000081 RID: 129
			MSExchangeStoreDriver = 1,
			// Token: 0x04000082 RID: 130
			MeetingMessageProcessing,
			// Token: 0x04000083 RID: 131
			OofHistory,
			// Token: 0x04000084 RID: 132
			Approval,
			// Token: 0x04000085 RID: 133
			UnifiedMessaging
		}

		// Token: 0x02000017 RID: 23
		internal enum Message : uint
		{
			// Token: 0x04000087 RID: 135
			StoreDriverPerfCountersLoadFailure = 3221488620U,
			// Token: 0x04000088 RID: 136
			StoreDriverPoisonMessage = 3221488625U,
			// Token: 0x04000089 RID: 137
			StoreDriverFailFastFailure,
			// Token: 0x0400008A RID: 138
			StoreDriverPoisonMessageInMapiSubmit,
			// Token: 0x0400008B RID: 139
			FailedToGenerateNDRInMapiSubmit,
			// Token: 0x0400008C RID: 140
			InvalidSender = 2147746808U,
			// Token: 0x0400008D RID: 141
			TooManySubmissionThreads,
			// Token: 0x0400008E RID: 142
			ProcessingMeetingMessageFailure,
			// Token: 0x0400008F RID: 143
			DeliveryFailedNoLegacyDN = 3221488635U,
			// Token: 0x04000090 RID: 144
			StoreDriverGetLocalIPFailure = 3221488637U,
			// Token: 0x04000091 RID: 145
			StoreDriverRegisterRpcServerFailure,
			// Token: 0x04000092 RID: 146
			ShadowStoreDriverPoisonMessage,
			// Token: 0x04000093 RID: 147
			ShadowStoreDriverPoisonMessageInSubmit,
			// Token: 0x04000094 RID: 148
			ShadowFailedToGenerateNdrInSubmit,
			// Token: 0x04000095 RID: 149
			ShadowInvalidSender = 2147746819U,
			// Token: 0x04000096 RID: 150
			OofHistoryCorruption = 3221488644U,
			// Token: 0x04000097 RID: 151
			OofHistoryFolderMissing,
			// Token: 0x04000098 RID: 152
			ApprovalCannotStampExpiry,
			// Token: 0x04000099 RID: 153
			UMPartnerMessageArrivedTooLate = 2147746823U,
			// Token: 0x0400009A RID: 154
			DeliveryHang = 3221488648U,
			// Token: 0x0400009B RID: 155
			ApprovalArbitrationMailboxQuota = 2147746825U
		}
	}
}
