using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.EdgeSync.Common
{
	// Token: 0x02000054 RID: 84
	public static class EdgeSyncEventLogConstants
	{
		// Token: 0x0400017A RID: 378
		public const string EventSource = "MSExchange EdgeSync";

		// Token: 0x0400017B RID: 379
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EdgeSyncStarted = new ExEventLog.EventTuple(1074005027U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400017C RID: 380
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EdgeSyncStopping = new ExEventLog.EventTuple(1074005028U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400017D RID: 381
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EdgeSyncStopped = new ExEventLog.EventTuple(1074005029U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400017E RID: 382
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Failure = new ExEventLog.EventTuple(3221488686U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400017F RID: 383
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationFailureException = new ExEventLog.EventTuple(3221488687U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000180 RID: 384
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EdgeLeaseException = new ExEventLog.EventTuple(3221488640U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000181 RID: 385
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToReadEntriesFromAD = new ExEventLog.EventTuple(2147746805U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000182 RID: 386
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InitializationFailureException = new ExEventLog.EventTuple(2147746837U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000183 RID: 387
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProbeFailed = new ExEventLog.EventTuple(3221488620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000184 RID: 388
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedDirectTrustMatch = new ExEventLog.EventTuple(3221497720U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000185 RID: 389
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EdgeTopologyException = new ExEventLog.EventTuple(3221488641U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000186 RID: 390
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoCredentialsFound = new ExEventLog.EventTuple(2147746824U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000187 RID: 391
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CredentialDecryptionException = new ExEventLog.EventTuple(3221488649U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000188 RID: 392
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MservEntrySyncFailure = new ExEventLog.EventTuple(3221488650U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000189 RID: 393
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CAPICertificateRequired = new ExEventLog.EventTuple(3221488651U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400018A RID: 394
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EhfServiceContractViolation = new ExEventLog.EventTuple(3221488717U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400018B RID: 395
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EhfOperationTimedOut = new ExEventLog.EventTuple(3221488718U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400018C RID: 396
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EhfCommunicationFailure = new ExEventLog.EventTuple(3221488719U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400018D RID: 397
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EhfEntrySyncFailure = new ExEventLog.EventTuple(3221488720U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400018E RID: 398
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EhfFailedUpdateSyncErrors = new ExEventLog.EventTuple(3221488721U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400018F RID: 399
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EhfPerEntryFailuresInBatch = new ExEventLog.EventTuple(3221488722U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000190 RID: 400
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EhfTransientFailure = new ExEventLog.EventTuple(3221488723U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000191 RID: 401
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EhfInvalidCredentials = new ExEventLog.EventTuple(3221488724U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000192 RID: 402
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EhfPerfCountersLoadFailure = new ExEventLog.EventTuple(3221488725U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000193 RID: 403
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EhfAdminSyncFailedToConnectToConfigNamingContext = new ExEventLog.EventTuple(3221488726U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000194 RID: 404
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EhfWebServiceVersionIsNotSupported = new ExEventLog.EventTuple(3221488727U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000195 RID: 405
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EhfFailedToClearForceDomainSyncFlagFromDomainSync = new ExEventLog.EventTuple(3221488728U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000196 RID: 406
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EhfFailedToClearForceDomainSyncFlagFromCompanySync = new ExEventLog.EventTuple(3221488729U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000197 RID: 407
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EhfAdminSyncTransientFailure = new ExEventLog.EventTuple(3221488730U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000198 RID: 408
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EhfAdminSyncPermanentFailure = new ExEventLog.EventTuple(3221488731U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000199 RID: 409
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EhfAdminSyncTransientFailureRetryThresholdReached = new ExEventLog.EventTuple(3221488732U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000055 RID: 85
		private enum Category : short
		{
			// Token: 0x0400019B RID: 411
			Synchronization = 1,
			// Token: 0x0400019C RID: 412
			Topology,
			// Token: 0x0400019D RID: 413
			SyncNow,
			// Token: 0x0400019E RID: 414
			Initialization
		}

		// Token: 0x02000056 RID: 86
		internal enum Message : uint
		{
			// Token: 0x040001A0 RID: 416
			EdgeSyncStarted = 1074005027U,
			// Token: 0x040001A1 RID: 417
			EdgeSyncStopping,
			// Token: 0x040001A2 RID: 418
			EdgeSyncStopped,
			// Token: 0x040001A3 RID: 419
			Failure = 3221488686U,
			// Token: 0x040001A4 RID: 420
			ConfigurationFailureException,
			// Token: 0x040001A5 RID: 421
			EdgeLeaseException = 3221488640U,
			// Token: 0x040001A6 RID: 422
			FailedToReadEntriesFromAD = 2147746805U,
			// Token: 0x040001A7 RID: 423
			InitializationFailureException = 2147746837U,
			// Token: 0x040001A8 RID: 424
			ProbeFailed = 3221488620U,
			// Token: 0x040001A9 RID: 425
			FailedDirectTrustMatch = 3221497720U,
			// Token: 0x040001AA RID: 426
			EdgeTopologyException = 3221488641U,
			// Token: 0x040001AB RID: 427
			NoCredentialsFound = 2147746824U,
			// Token: 0x040001AC RID: 428
			CredentialDecryptionException = 3221488649U,
			// Token: 0x040001AD RID: 429
			MservEntrySyncFailure,
			// Token: 0x040001AE RID: 430
			CAPICertificateRequired,
			// Token: 0x040001AF RID: 431
			EhfServiceContractViolation = 3221488717U,
			// Token: 0x040001B0 RID: 432
			EhfOperationTimedOut,
			// Token: 0x040001B1 RID: 433
			EhfCommunicationFailure,
			// Token: 0x040001B2 RID: 434
			EhfEntrySyncFailure,
			// Token: 0x040001B3 RID: 435
			EhfFailedUpdateSyncErrors,
			// Token: 0x040001B4 RID: 436
			EhfPerEntryFailuresInBatch,
			// Token: 0x040001B5 RID: 437
			EhfTransientFailure,
			// Token: 0x040001B6 RID: 438
			EhfInvalidCredentials,
			// Token: 0x040001B7 RID: 439
			EhfPerfCountersLoadFailure,
			// Token: 0x040001B8 RID: 440
			EhfAdminSyncFailedToConnectToConfigNamingContext,
			// Token: 0x040001B9 RID: 441
			EhfWebServiceVersionIsNotSupported,
			// Token: 0x040001BA RID: 442
			EhfFailedToClearForceDomainSyncFlagFromDomainSync,
			// Token: 0x040001BB RID: 443
			EhfFailedToClearForceDomainSyncFlagFromCompanySync,
			// Token: 0x040001BC RID: 444
			EhfAdminSyncTransientFailure,
			// Token: 0x040001BD RID: 445
			EhfAdminSyncPermanentFailure,
			// Token: 0x040001BE RID: 446
			EhfAdminSyncTransientFailureRetryThresholdReached
		}
	}
}
