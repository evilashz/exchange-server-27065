using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.EventLogs
{
	// Token: 0x0200000A RID: 10
	internal static class ServicesEventLogConstants
	{
		// Token: 0x04000045 RID: 69
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StartedSuccessfully = new ExEventLog.EventTuple(2U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000046 RID: 70
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InternalServerError = new ExEventLog.EventTuple(3221225475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000047 RID: 71
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorMappingNotFound = new ExEventLog.EventTuple(3221225476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000048 RID: 72
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PushNotificationFailedFirstTime = new ExEventLog.EventTuple(2147483653U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000049 RID: 73
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PushNotificationFailedRetry = new ExEventLog.EventTuple(2147483654U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400004A RID: 74
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PushSubscriptionFailedFinal = new ExEventLog.EventTuple(3221225479U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400004B RID: 75
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToOpenCachedMailbox = new ExEventLog.EventTuple(3221225480U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004C RID: 76
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InitializePerformanceCountersFailed = new ExEventLog.EventTuple(3221225481U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004D RID: 77
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceDiscoveryFailure = new ExEventLog.EventTuple(3221225482U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400004E RID: 78
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoRespondingDestinationCAS = new ExEventLog.EventTuple(3221225483U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004F RID: 79
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyRightDenied = new ExEventLog.EventTuple(3221225484U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000050 RID: 80
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_KerberosRequired = new ExEventLog.EventTuple(3221225485U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000051 RID: 81
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SSLRequired = new ExEventLog.EventTuple(3221225486U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000052 RID: 82
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_KerberosConfigurationProblem = new ExEventLog.EventTuple(3221225487U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000053 RID: 83
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoApplicableDestinationCAS = new ExEventLog.EventTuple(3221225488U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000054 RID: 84
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoTrustedCertificateOnDestinationCAS = new ExEventLog.EventTuple(3221225489U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000055 RID: 85
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CASToCASVersionMismatch = new ExEventLog.EventTuple(3221225490U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000056 RID: 86
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExceededGroupSidLimit = new ExEventLog.EventTuple(3221225491U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000057 RID: 87
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoApplicableDestinationCASInAnySite = new ExEventLog.EventTuple(3221225492U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000058 RID: 88
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceDiscoveryForServerFailure = new ExEventLog.EventTuple(3221225493U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000059 RID: 89
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PushNotificationReadEventsFailed = new ExEventLog.EventTuple(2147483670U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005A RID: 90
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ImpersonationSecurityContext = new ExEventLog.EventTuple(23U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005B RID: 91
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateExpired = new ExEventLog.EventTuple(3221225496U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005C RID: 92
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateExpiresVerySoon = new ExEventLog.EventTuple(2147483673U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005D RID: 93
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateExpiresSoon = new ExEventLog.EventTuple(3221225498U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005E RID: 94
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToCreateADDevice = new ExEventLog.EventTuple(3221487643U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005F RID: 95
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryAccessDenied = new ExEventLog.EventTuple(3221487644U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000060 RID: 96
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StoppedSuccessfully = new ExEventLog.EventTuple(29U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000061 RID: 97
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_X509CerticateValidatorException = new ExEventLog.EventTuple(2147483678U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000062 RID: 98
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BITSDownloadManagerInitialized = new ExEventLog.EventTuple(100U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000063 RID: 99
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BITSDownloadManagerInitializationFailed = new ExEventLog.EventTuple(3221225573U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000064 RID: 100
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABDownloadStarted = new ExEventLog.EventTuple(102U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000065 RID: 101
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABDownloadFinished = new ExEventLog.EventTuple(103U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000066 RID: 102
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABDownloadFailedResubmitting = new ExEventLog.EventTuple(2147483752U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000067 RID: 103
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABDownloadJobIsPoison = new ExEventLog.EventTuple(3221225577U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000068 RID: 104
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABDownloadGuidParsingError = new ExEventLog.EventTuple(3221225578U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000069 RID: 105
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABDownloadOtherError = new ExEventLog.EventTuple(3221225579U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200000B RID: 11
		private enum Category : short
		{
			// Token: 0x0400006B RID: 107
			Core = 1
		}

		// Token: 0x0200000C RID: 12
		internal enum Message : uint
		{
			// Token: 0x0400006D RID: 109
			StartedSuccessfully = 2U,
			// Token: 0x0400006E RID: 110
			InternalServerError = 3221225475U,
			// Token: 0x0400006F RID: 111
			ErrorMappingNotFound,
			// Token: 0x04000070 RID: 112
			PushNotificationFailedFirstTime = 2147483653U,
			// Token: 0x04000071 RID: 113
			PushNotificationFailedRetry,
			// Token: 0x04000072 RID: 114
			PushSubscriptionFailedFinal = 3221225479U,
			// Token: 0x04000073 RID: 115
			UnableToOpenCachedMailbox,
			// Token: 0x04000074 RID: 116
			InitializePerformanceCountersFailed,
			// Token: 0x04000075 RID: 117
			ServiceDiscoveryFailure,
			// Token: 0x04000076 RID: 118
			NoRespondingDestinationCAS,
			// Token: 0x04000077 RID: 119
			ProxyRightDenied,
			// Token: 0x04000078 RID: 120
			KerberosRequired,
			// Token: 0x04000079 RID: 121
			SSLRequired,
			// Token: 0x0400007A RID: 122
			KerberosConfigurationProblem,
			// Token: 0x0400007B RID: 123
			NoApplicableDestinationCAS,
			// Token: 0x0400007C RID: 124
			NoTrustedCertificateOnDestinationCAS,
			// Token: 0x0400007D RID: 125
			CASToCASVersionMismatch,
			// Token: 0x0400007E RID: 126
			ExceededGroupSidLimit,
			// Token: 0x0400007F RID: 127
			NoApplicableDestinationCASInAnySite,
			// Token: 0x04000080 RID: 128
			ServiceDiscoveryForServerFailure,
			// Token: 0x04000081 RID: 129
			PushNotificationReadEventsFailed = 2147483670U,
			// Token: 0x04000082 RID: 130
			ImpersonationSecurityContext = 23U,
			// Token: 0x04000083 RID: 131
			CertificateExpired = 3221225496U,
			// Token: 0x04000084 RID: 132
			CertificateExpiresVerySoon = 2147483673U,
			// Token: 0x04000085 RID: 133
			CertificateExpiresSoon = 3221225498U,
			// Token: 0x04000086 RID: 134
			UnableToCreateADDevice = 3221487643U,
			// Token: 0x04000087 RID: 135
			DirectoryAccessDenied,
			// Token: 0x04000088 RID: 136
			StoppedSuccessfully = 29U,
			// Token: 0x04000089 RID: 137
			X509CerticateValidatorException = 2147483678U,
			// Token: 0x0400008A RID: 138
			BITSDownloadManagerInitialized = 100U,
			// Token: 0x0400008B RID: 139
			BITSDownloadManagerInitializationFailed = 3221225573U,
			// Token: 0x0400008C RID: 140
			OABDownloadStarted = 102U,
			// Token: 0x0400008D RID: 141
			OABDownloadFinished,
			// Token: 0x0400008E RID: 142
			OABDownloadFailedResubmitting = 2147483752U,
			// Token: 0x0400008F RID: 143
			OABDownloadJobIsPoison = 3221225577U,
			// Token: 0x04000090 RID: 144
			OABDownloadGuidParsingError,
			// Token: 0x04000091 RID: 145
			OABDownloadOtherError
		}
	}
}
