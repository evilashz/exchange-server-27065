using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000393 RID: 915
	internal static class MRSEventLogConstants
	{
		// Token: 0x0400113A RID: 4410
		public const string EventSource = "MSExchange Mailbox Replication";

		// Token: 0x0400113B RID: 4411
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarted = new ExEventLog.EventTuple(263144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400113C RID: 4412
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopped = new ExEventLog.EventTuple(263145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400113D RID: 4413
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceIsDisabled = new ExEventLog.EventTuple(2147746794U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400113E RID: 4414
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceFailedToRegisterEndpoint = new ExEventLog.EventTuple(2147746795U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400113F RID: 4415
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceConfigCorrupt = new ExEventLog.EventTuple(3221488620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001140 RID: 4416
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToFindMbxServer = new ExEventLog.EventTuple(3221488621U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001141 RID: 4417
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToProcessJobsInDatabase = new ExEventLog.EventTuple(2147746798U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001142 RID: 4418
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToDetermineHostedMdbsOnServer = new ExEventLog.EventTuple(2147746799U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001143 RID: 4419
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RemovedOrphanedMoveRequest = new ExEventLog.EventTuple(2147746800U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001144 RID: 4420
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedInvalidRequest = new ExEventLog.EventTuple(2147746801U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001145 RID: 4421
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceFailedToStart = new ExEventLog.EventTuple(3221488626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001146 RID: 4422
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RemovedCompletedRequest = new ExEventLog.EventTuple(263155U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001147 RID: 4423
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CrashEvent = new ExEventLog.EventTuple(3221488628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001148 RID: 4424
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ScanADInconsistencyRequestFailEvent = new ExEventLog.EventTuple(3221488629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001149 RID: 4425
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RequestFatalFailure = new ExEventLog.EventTuple(3221488716U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400114A RID: 4426
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RequestTransientFailure = new ExEventLog.EventTuple(2147746893U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400114B RID: 4427
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MoveStarted = new ExEventLog.EventTuple(263246U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400114C RID: 4428
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RequestContinued = new ExEventLog.EventTuple(263247U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400114D RID: 4429
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MoveSeedingStarted = new ExEventLog.EventTuple(263248U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400114E RID: 4430
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MoveSeedingCompleted = new ExEventLog.EventTuple(263249U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400114F RID: 4431
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MoveFinalizationStarted = new ExEventLog.EventTuple(263250U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001150 RID: 4432
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RequestCompleted = new ExEventLog.EventTuple(263251U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001151 RID: 4433
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RequestCanceled = new ExEventLog.EventTuple(263252U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001152 RID: 4434
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MoveIncrementalSyncCompleted = new ExEventLog.EventTuple(263253U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001153 RID: 4435
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MoveUnableToApplySearchCriteria = new ExEventLog.EventTuple(2147746902U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001154 RID: 4436
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MoveUnableToUpdateSourceMailbox = new ExEventLog.EventTuple(3221488727U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001155 RID: 4437
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCleanupCanceledRequest = new ExEventLog.EventTuple(2147746904U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001156 RID: 4438
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToUpdateCompletedRequest = new ExEventLog.EventTuple(2147746905U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001157 RID: 4439
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RequestSaveFailed = new ExEventLog.EventTuple(2147746906U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001158 RID: 4440
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DestinationMailboxCleanupFailed = new ExEventLog.EventTuple(2147746907U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001159 RID: 4441
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SourceMailboxResetFailed = new ExEventLog.EventTuple(2147746908U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400115A RID: 4442
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SourceMailboxCleanupFailed = new ExEventLog.EventTuple(2147746909U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400115B RID: 4443
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LocalDestinationMailboxResetFailed = new ExEventLog.EventTuple(2147746910U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400115C RID: 4444
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReportFlushFailed = new ExEventLog.EventTuple(2147746911U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400115D RID: 4445
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncStateSaveFailed = new ExEventLog.EventTuple(2147746912U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400115E RID: 4446
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToProcessRequest = new ExEventLog.EventTuple(3221488737U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400115F RID: 4447
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToPreserveMailboxSignature = new ExEventLog.EventTuple(2147746914U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001160 RID: 4448
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MoveRestartedDueToSignatureChange = new ExEventLog.EventTuple(2147746915U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001161 RID: 4449
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DestinationMailboxSeedMBICacheFailed = new ExEventLog.EventTuple(2147746916U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001162 RID: 4450
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DestinationMailboxSyncStateDeletionFailed = new ExEventLog.EventTuple(2147746917U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001163 RID: 4451
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DestinationMailboxMoveHistoryEntryFailed = new ExEventLog.EventTuple(2147746918U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001164 RID: 4452
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RequestCompletedWithWarnings = new ExEventLog.EventTuple(2147746919U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001165 RID: 4453
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToReadGlobalDatabaseState = new ExEventLog.EventTuple(2147746920U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001166 RID: 4454
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToWriteGlobalDatabaseState = new ExEventLog.EventTuple(2147746921U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001167 RID: 4455
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PeriodicTaskStoppingExecution = new ExEventLog.EventTuple(3221488746U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001168 RID: 4456
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplicationConstraintCheckNotSatisfied = new ExEventLog.EventTuple(2147746923U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001169 RID: 4457
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReplicationConstraintCheckSatisfied = new ExEventLog.EventTuple(2147746924U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400116A RID: 4458
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DestinationMailboxResetNotGuaranteed = new ExEventLog.EventTuple(2147746926U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400116B RID: 4459
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADWriteFailed = new ExEventLog.EventTuple(2147746927U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400116C RID: 4460
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RemoteDestinationMailboxResetFailed = new ExEventLog.EventTuple(2147746928U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400116D RID: 4461
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RequestIsPoisoned = new ExEventLog.EventTuple(3221488753U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400116E RID: 4462
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IncrementalMoveRestartDueToGlobalCounterRangeDepletion = new ExEventLog.EventTuple(2147746930U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400116F RID: 4463
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SourceMailboxMoveHistoryEntryFailed = new ExEventLog.EventTuple(2147746931U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001170 RID: 4464
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MoveRestartedDueToMailboxCorruption = new ExEventLog.EventTuple(2147746932U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000394 RID: 916
		private enum Category : short
		{
			// Token: 0x04001172 RID: 4466
			Service = 1,
			// Token: 0x04001173 RID: 4467
			Request
		}

		// Token: 0x02000395 RID: 917
		internal enum Message : uint
		{
			// Token: 0x04001175 RID: 4469
			ServiceStarted = 263144U,
			// Token: 0x04001176 RID: 4470
			ServiceStopped,
			// Token: 0x04001177 RID: 4471
			ServiceIsDisabled = 2147746794U,
			// Token: 0x04001178 RID: 4472
			ServiceFailedToRegisterEndpoint,
			// Token: 0x04001179 RID: 4473
			ServiceConfigCorrupt = 3221488620U,
			// Token: 0x0400117A RID: 4474
			UnableToFindMbxServer,
			// Token: 0x0400117B RID: 4475
			UnableToProcessJobsInDatabase = 2147746798U,
			// Token: 0x0400117C RID: 4476
			UnableToDetermineHostedMdbsOnServer,
			// Token: 0x0400117D RID: 4477
			RemovedOrphanedMoveRequest,
			// Token: 0x0400117E RID: 4478
			FailedInvalidRequest,
			// Token: 0x0400117F RID: 4479
			ServiceFailedToStart = 3221488626U,
			// Token: 0x04001180 RID: 4480
			RemovedCompletedRequest = 263155U,
			// Token: 0x04001181 RID: 4481
			CrashEvent = 3221488628U,
			// Token: 0x04001182 RID: 4482
			ScanADInconsistencyRequestFailEvent,
			// Token: 0x04001183 RID: 4483
			RequestFatalFailure = 3221488716U,
			// Token: 0x04001184 RID: 4484
			RequestTransientFailure = 2147746893U,
			// Token: 0x04001185 RID: 4485
			MoveStarted = 263246U,
			// Token: 0x04001186 RID: 4486
			RequestContinued,
			// Token: 0x04001187 RID: 4487
			MoveSeedingStarted,
			// Token: 0x04001188 RID: 4488
			MoveSeedingCompleted,
			// Token: 0x04001189 RID: 4489
			MoveFinalizationStarted,
			// Token: 0x0400118A RID: 4490
			RequestCompleted,
			// Token: 0x0400118B RID: 4491
			RequestCanceled,
			// Token: 0x0400118C RID: 4492
			MoveIncrementalSyncCompleted,
			// Token: 0x0400118D RID: 4493
			MoveUnableToApplySearchCriteria = 2147746902U,
			// Token: 0x0400118E RID: 4494
			MoveUnableToUpdateSourceMailbox = 3221488727U,
			// Token: 0x0400118F RID: 4495
			FailedToCleanupCanceledRequest = 2147746904U,
			// Token: 0x04001190 RID: 4496
			FailedToUpdateCompletedRequest,
			// Token: 0x04001191 RID: 4497
			RequestSaveFailed,
			// Token: 0x04001192 RID: 4498
			DestinationMailboxCleanupFailed,
			// Token: 0x04001193 RID: 4499
			SourceMailboxResetFailed,
			// Token: 0x04001194 RID: 4500
			SourceMailboxCleanupFailed,
			// Token: 0x04001195 RID: 4501
			LocalDestinationMailboxResetFailed,
			// Token: 0x04001196 RID: 4502
			ReportFlushFailed,
			// Token: 0x04001197 RID: 4503
			SyncStateSaveFailed,
			// Token: 0x04001198 RID: 4504
			UnableToProcessRequest = 3221488737U,
			// Token: 0x04001199 RID: 4505
			UnableToPreserveMailboxSignature = 2147746914U,
			// Token: 0x0400119A RID: 4506
			MoveRestartedDueToSignatureChange,
			// Token: 0x0400119B RID: 4507
			DestinationMailboxSeedMBICacheFailed,
			// Token: 0x0400119C RID: 4508
			DestinationMailboxSyncStateDeletionFailed,
			// Token: 0x0400119D RID: 4509
			DestinationMailboxMoveHistoryEntryFailed,
			// Token: 0x0400119E RID: 4510
			RequestCompletedWithWarnings,
			// Token: 0x0400119F RID: 4511
			UnableToReadGlobalDatabaseState,
			// Token: 0x040011A0 RID: 4512
			UnableToWriteGlobalDatabaseState,
			// Token: 0x040011A1 RID: 4513
			PeriodicTaskStoppingExecution = 3221488746U,
			// Token: 0x040011A2 RID: 4514
			ReplicationConstraintCheckNotSatisfied = 2147746923U,
			// Token: 0x040011A3 RID: 4515
			ReplicationConstraintCheckSatisfied,
			// Token: 0x040011A4 RID: 4516
			DestinationMailboxResetNotGuaranteed = 2147746926U,
			// Token: 0x040011A5 RID: 4517
			ADWriteFailed,
			// Token: 0x040011A6 RID: 4518
			RemoteDestinationMailboxResetFailed,
			// Token: 0x040011A7 RID: 4519
			RequestIsPoisoned = 3221488753U,
			// Token: 0x040011A8 RID: 4520
			IncrementalMoveRestartDueToGlobalCounterRangeDepletion = 2147746930U,
			// Token: 0x040011A9 RID: 4521
			SourceMailboxMoveHistoryEntryFailed,
			// Token: 0x040011AA RID: 4522
			MoveRestartedDueToMailboxCorruption
		}
	}
}
