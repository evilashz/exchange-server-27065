using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000047 RID: 71
	internal static class MailboxTransportEventLogConstants
	{
		// Token: 0x0400014B RID: 331
		public const string EventSource = "MSExchange Store Driver Delivery";

		// Token: 0x0400014C RID: 332
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverPerfCountersLoadFailure = new ExEventLog.EventTuple(3221488620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400014D RID: 333
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverFailFastFailure = new ExEventLog.EventTuple(3221488626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400014E RID: 334
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessingMeetingMessageFailure = new ExEventLog.EventTuple(2147746810U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400014F RID: 335
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeliveryFailedNoLegacyDN = new ExEventLog.EventTuple(3221488635U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000150 RID: 336
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverGetLocalIPFailure = new ExEventLog.EventTuple(3221488637U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000151 RID: 337
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MdbHeadersFailure = new ExEventLog.EventTuple(2147746816U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000152 RID: 338
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OofHistoryCorruption = new ExEventLog.EventTuple(3221488644U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000153 RID: 339
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OofHistoryFolderMissing = new ExEventLog.EventTuple(3221488645U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000154 RID: 340
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ApprovalCannotStampExpiry = new ExEventLog.EventTuple(3221488646U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000155 RID: 341
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UMPartnerMessageArrivedTooLate = new ExEventLog.EventTuple(2147746823U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000156 RID: 342
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DeliveryHang = new ExEventLog.EventTuple(3221488648U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000157 RID: 343
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ApprovalArbitrationMailboxQuota = new ExEventLog.EventTuple(2147746825U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000158 RID: 344
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageLoadFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221488650U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000159 RID: 345
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageSaveFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221488651U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400015A RID: 346
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageMarkFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221488652U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400015B RID: 347
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PeopleIKnowAgentException = new ExEventLog.EventTuple(3221488653U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400015C RID: 348
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkingSetAgentException = new ExEventLog.EventTuple(3221488654U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400015D RID: 349
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OfficeGraphAgentException = new ExEventLog.EventTuple(3221488655U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400015E RID: 350
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SharedMailboxSentItemsAgentException = new ExEventLog.EventTuple(3221488656U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000048 RID: 72
		private enum Category : short
		{
			// Token: 0x04000160 RID: 352
			MSExchangeStoreDriverDelivery = 1,
			// Token: 0x04000161 RID: 353
			MeetingMessageProcessing,
			// Token: 0x04000162 RID: 354
			OofHistory,
			// Token: 0x04000163 RID: 355
			Approval,
			// Token: 0x04000164 RID: 356
			UnifiedMessaging
		}

		// Token: 0x02000049 RID: 73
		internal enum Message : uint
		{
			// Token: 0x04000166 RID: 358
			StoreDriverPerfCountersLoadFailure = 3221488620U,
			// Token: 0x04000167 RID: 359
			StoreDriverFailFastFailure = 3221488626U,
			// Token: 0x04000168 RID: 360
			ProcessingMeetingMessageFailure = 2147746810U,
			// Token: 0x04000169 RID: 361
			DeliveryFailedNoLegacyDN = 3221488635U,
			// Token: 0x0400016A RID: 362
			StoreDriverGetLocalIPFailure = 3221488637U,
			// Token: 0x0400016B RID: 363
			MdbHeadersFailure = 2147746816U,
			// Token: 0x0400016C RID: 364
			OofHistoryCorruption = 3221488644U,
			// Token: 0x0400016D RID: 365
			OofHistoryFolderMissing,
			// Token: 0x0400016E RID: 366
			ApprovalCannotStampExpiry,
			// Token: 0x0400016F RID: 367
			UMPartnerMessageArrivedTooLate = 2147746823U,
			// Token: 0x04000170 RID: 368
			DeliveryHang = 3221488648U,
			// Token: 0x04000171 RID: 369
			ApprovalArbitrationMailboxQuota = 2147746825U,
			// Token: 0x04000172 RID: 370
			PoisonMessageLoadFailedRegistryAccessDenied = 3221488650U,
			// Token: 0x04000173 RID: 371
			PoisonMessageSaveFailedRegistryAccessDenied,
			// Token: 0x04000174 RID: 372
			PoisonMessageMarkFailedRegistryAccessDenied,
			// Token: 0x04000175 RID: 373
			PeopleIKnowAgentException,
			// Token: 0x04000176 RID: 374
			WorkingSetAgentException,
			// Token: 0x04000177 RID: 375
			OfficeGraphAgentException,
			// Token: 0x04000178 RID: 376
			SharedMailboxSentItemsAgentException
		}
	}
}
