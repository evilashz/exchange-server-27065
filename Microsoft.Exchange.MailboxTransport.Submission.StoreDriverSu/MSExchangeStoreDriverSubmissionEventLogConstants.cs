using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000043 RID: 67
	internal static class MSExchangeStoreDriverSubmissionEventLogConstants
	{
		// Token: 0x0400016D RID: 365
		public const string EventSource = "MSExchange Store Driver Submission";

		// Token: 0x0400016E RID: 366
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverSubmissionPerfCountersLoadFailure = new ExEventLog.EventTuple(3221488620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400016F RID: 367
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverSubmissionStartFailure = new ExEventLog.EventTuple(3221488621U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000170 RID: 368
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverSubmissionPoisonMessage = new ExEventLog.EventTuple(3221488625U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000171 RID: 369
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverSubmissionFailFastFailure = new ExEventLog.EventTuple(3221488626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000172 RID: 370
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverSubmissionPoisonMessageInMapiSubmit = new ExEventLog.EventTuple(3221488627U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000173 RID: 371
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToGenerateNDRInMapiSubmit = new ExEventLog.EventTuple(3221488628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000174 RID: 372
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidSender = new ExEventLog.EventTuple(2147746808U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000175 RID: 373
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TooManySubmissionThreads = new ExEventLog.EventTuple(2147746809U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000176 RID: 374
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorWritingToLam = new ExEventLog.EventTuple(3221488634U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000177 RID: 375
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LamNotificationIdNotSetOnMessage = new ExEventLog.EventTuple(3221488635U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000178 RID: 376
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorWritingToLamLamNotificationIdNotSet = new ExEventLog.EventTuple(3221488636U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000179 RID: 377
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoreDriverSubmissionGetLocalIPFailure = new ExEventLog.EventTuple(3221488637U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400017A RID: 378
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorParsingLamNotificationId = new ExEventLog.EventTuple(3221488638U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400017B RID: 379
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageLoadFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221497621U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400017C RID: 380
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageSaveFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221497622U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400017D RID: 381
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageMarkFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221497623U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400017E RID: 382
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_QuarantineInfoLoadFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221497624U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400017F RID: 383
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CannotStartAgents = new ExEventLog.EventTuple(3221503639U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000044 RID: 68
		private enum Category : short
		{
			// Token: 0x04000181 RID: 385
			MSExchangeStoreDriverSubmission = 1
		}

		// Token: 0x02000045 RID: 69
		internal enum Message : uint
		{
			// Token: 0x04000183 RID: 387
			StoreDriverSubmissionPerfCountersLoadFailure = 3221488620U,
			// Token: 0x04000184 RID: 388
			StoreDriverSubmissionStartFailure,
			// Token: 0x04000185 RID: 389
			StoreDriverSubmissionPoisonMessage = 3221488625U,
			// Token: 0x04000186 RID: 390
			StoreDriverSubmissionFailFastFailure,
			// Token: 0x04000187 RID: 391
			StoreDriverSubmissionPoisonMessageInMapiSubmit,
			// Token: 0x04000188 RID: 392
			FailedToGenerateNDRInMapiSubmit,
			// Token: 0x04000189 RID: 393
			InvalidSender = 2147746808U,
			// Token: 0x0400018A RID: 394
			TooManySubmissionThreads,
			// Token: 0x0400018B RID: 395
			ErrorWritingToLam = 3221488634U,
			// Token: 0x0400018C RID: 396
			LamNotificationIdNotSetOnMessage,
			// Token: 0x0400018D RID: 397
			ErrorWritingToLamLamNotificationIdNotSet,
			// Token: 0x0400018E RID: 398
			StoreDriverSubmissionGetLocalIPFailure,
			// Token: 0x0400018F RID: 399
			ErrorParsingLamNotificationId,
			// Token: 0x04000190 RID: 400
			PoisonMessageLoadFailedRegistryAccessDenied = 3221497621U,
			// Token: 0x04000191 RID: 401
			PoisonMessageSaveFailedRegistryAccessDenied,
			// Token: 0x04000192 RID: 402
			PoisonMessageMarkFailedRegistryAccessDenied,
			// Token: 0x04000193 RID: 403
			QuarantineInfoLoadFailedRegistryAccessDenied,
			// Token: 0x04000194 RID: 404
			CannotStartAgents = 3221503639U
		}
	}
}
