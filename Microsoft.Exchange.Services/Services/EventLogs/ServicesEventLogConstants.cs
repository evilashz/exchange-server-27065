using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.EventLogs
{
	// Token: 0x02000F78 RID: 3960
	internal static class ServicesEventLogConstants
	{
		// Token: 0x04003884 RID: 14468
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StartedSuccessfully = new ExEventLog.EventTuple(2U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04003885 RID: 14469
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InternalServerError = new ExEventLog.EventTuple(3221225475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04003886 RID: 14470
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorMappingNotFound = new ExEventLog.EventTuple(3221225476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04003887 RID: 14471
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PushNotificationFailedFirstTime = new ExEventLog.EventTuple(2147483653U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04003888 RID: 14472
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PushNotificationFailedRetry = new ExEventLog.EventTuple(2147483654U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04003889 RID: 14473
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PushSubscriptionFailedFinal = new ExEventLog.EventTuple(3221225479U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400388A RID: 14474
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToOpenCachedMailbox = new ExEventLog.EventTuple(3221225480U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400388B RID: 14475
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InitializePerformanceCountersFailed = new ExEventLog.EventTuple(3221225481U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400388C RID: 14476
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceDiscoveryFailure = new ExEventLog.EventTuple(3221225482U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400388D RID: 14477
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoRespondingDestinationCAS = new ExEventLog.EventTuple(3221225483U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400388E RID: 14478
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyRightDenied = new ExEventLog.EventTuple(3221225484U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400388F RID: 14479
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_KerberosRequired = new ExEventLog.EventTuple(3221225485U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04003890 RID: 14480
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SSLRequired = new ExEventLog.EventTuple(3221225486U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04003891 RID: 14481
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_KerberosConfigurationProblem = new ExEventLog.EventTuple(3221225487U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04003892 RID: 14482
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoApplicableDestinationCAS = new ExEventLog.EventTuple(3221225488U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04003893 RID: 14483
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoTrustedCertificateOnDestinationCAS = new ExEventLog.EventTuple(3221225489U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04003894 RID: 14484
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CASToCASVersionMismatch = new ExEventLog.EventTuple(3221225490U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04003895 RID: 14485
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExceededGroupSidLimit = new ExEventLog.EventTuple(3221225491U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04003896 RID: 14486
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoApplicableDestinationCASInAnySite = new ExEventLog.EventTuple(3221225492U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04003897 RID: 14487
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceDiscoveryForServerFailure = new ExEventLog.EventTuple(3221225493U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04003898 RID: 14488
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PushNotificationReadEventsFailed = new ExEventLog.EventTuple(2147483670U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04003899 RID: 14489
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ImpersonationSecurityContext = new ExEventLog.EventTuple(23U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400389A RID: 14490
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateExpired = new ExEventLog.EventTuple(3221225496U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400389B RID: 14491
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateExpiresVerySoon = new ExEventLog.EventTuple(2147483673U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400389C RID: 14492
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateExpiresSoon = new ExEventLog.EventTuple(3221225498U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400389D RID: 14493
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToCreateADDevice = new ExEventLog.EventTuple(3221487643U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400389E RID: 14494
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryAccessDenied = new ExEventLog.EventTuple(3221487644U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400389F RID: 14495
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoppedSuccessfully = new ExEventLog.EventTuple(29U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040038A0 RID: 14496
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_X509CerticateValidatorException = new ExEventLog.EventTuple(2147483678U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040038A1 RID: 14497
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BITSDownloadManagerInitialized = new ExEventLog.EventTuple(100U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040038A2 RID: 14498
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BITSDownloadManagerInitializationFailed = new ExEventLog.EventTuple(3221225573U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040038A3 RID: 14499
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABDownloadStarted = new ExEventLog.EventTuple(102U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040038A4 RID: 14500
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABDownloadFinished = new ExEventLog.EventTuple(103U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040038A5 RID: 14501
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABDownloadFailedResubmitting = new ExEventLog.EventTuple(2147483752U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040038A6 RID: 14502
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABDownloadJobIsPoison = new ExEventLog.EventTuple(3221225577U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040038A7 RID: 14503
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABDownloadGuidParsingError = new ExEventLog.EventTuple(3221225578U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040038A8 RID: 14504
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABDownloadOtherError = new ExEventLog.EventTuple(3221225579U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000F79 RID: 3961
		private enum Category : short
		{
			// Token: 0x040038AA RID: 14506
			Core = 1
		}

		// Token: 0x02000F7A RID: 3962
		internal enum Message : uint
		{
			// Token: 0x040038AC RID: 14508
			StartedSuccessfully = 2U,
			// Token: 0x040038AD RID: 14509
			InternalServerError = 3221225475U,
			// Token: 0x040038AE RID: 14510
			ErrorMappingNotFound,
			// Token: 0x040038AF RID: 14511
			PushNotificationFailedFirstTime = 2147483653U,
			// Token: 0x040038B0 RID: 14512
			PushNotificationFailedRetry,
			// Token: 0x040038B1 RID: 14513
			PushSubscriptionFailedFinal = 3221225479U,
			// Token: 0x040038B2 RID: 14514
			UnableToOpenCachedMailbox,
			// Token: 0x040038B3 RID: 14515
			InitializePerformanceCountersFailed,
			// Token: 0x040038B4 RID: 14516
			ServiceDiscoveryFailure,
			// Token: 0x040038B5 RID: 14517
			NoRespondingDestinationCAS,
			// Token: 0x040038B6 RID: 14518
			ProxyRightDenied,
			// Token: 0x040038B7 RID: 14519
			KerberosRequired,
			// Token: 0x040038B8 RID: 14520
			SSLRequired,
			// Token: 0x040038B9 RID: 14521
			KerberosConfigurationProblem,
			// Token: 0x040038BA RID: 14522
			NoApplicableDestinationCAS,
			// Token: 0x040038BB RID: 14523
			NoTrustedCertificateOnDestinationCAS,
			// Token: 0x040038BC RID: 14524
			CASToCASVersionMismatch,
			// Token: 0x040038BD RID: 14525
			ExceededGroupSidLimit,
			// Token: 0x040038BE RID: 14526
			NoApplicableDestinationCASInAnySite,
			// Token: 0x040038BF RID: 14527
			ServiceDiscoveryForServerFailure,
			// Token: 0x040038C0 RID: 14528
			PushNotificationReadEventsFailed = 2147483670U,
			// Token: 0x040038C1 RID: 14529
			ImpersonationSecurityContext = 23U,
			// Token: 0x040038C2 RID: 14530
			CertificateExpired = 3221225496U,
			// Token: 0x040038C3 RID: 14531
			CertificateExpiresVerySoon = 2147483673U,
			// Token: 0x040038C4 RID: 14532
			CertificateExpiresSoon = 3221225498U,
			// Token: 0x040038C5 RID: 14533
			UnableToCreateADDevice = 3221487643U,
			// Token: 0x040038C6 RID: 14534
			DirectoryAccessDenied,
			// Token: 0x040038C7 RID: 14535
			StoppedSuccessfully = 29U,
			// Token: 0x040038C8 RID: 14536
			X509CerticateValidatorException = 2147483678U,
			// Token: 0x040038C9 RID: 14537
			BITSDownloadManagerInitialized = 100U,
			// Token: 0x040038CA RID: 14538
			BITSDownloadManagerInitializationFailed = 3221225573U,
			// Token: 0x040038CB RID: 14539
			OABDownloadStarted = 102U,
			// Token: 0x040038CC RID: 14540
			OABDownloadFinished,
			// Token: 0x040038CD RID: 14541
			OABDownloadFailedResubmitting = 2147483752U,
			// Token: 0x040038CE RID: 14542
			OABDownloadJobIsPoison = 3221225577U,
			// Token: 0x040038CF RID: 14543
			OABDownloadGuidParsingError,
			// Token: 0x040038D0 RID: 14544
			OABDownloadOtherError
		}
	}
}
