using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000003 RID: 3
	internal static class TransportEventLogConstants
	{
		// Token: 0x0400001B RID: 27
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BindingIPv6ButDisabled = new ExEventLog.EventTuple(3221488616U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001C RID: 28
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AddressInUse = new ExEventLog.EventTuple(3221488617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001D RID: 29
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfiguredConnectors = new ExEventLog.EventTuple(1074004970U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001E RID: 30
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveMaxConnectionReached = new ExEventLog.EventTuple(2147746801U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001F RID: 31
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveMessageRejected = new ExEventLog.EventTuple(1074004978U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000020 RID: 32
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveAuthenticationFailedTooManyErrors = new ExEventLog.EventTuple(2147746804U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000021 RID: 33
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveRejectDueToStorageError = new ExEventLog.EventTuple(3221488629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000022 RID: 34
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveAuthorizationSubmitRejected = new ExEventLog.EventTuple(2147746810U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000023 RID: 35
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveAuthorizationRejected = new ExEventLog.EventTuple(2147746812U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000024 RID: 36
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveMaxConnectionPerSourceReached = new ExEventLog.EventTuple(2147746813U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000025 RID: 37
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_InternalSMTPServerListEmpty = new ExEventLog.EventTuple(2147746814U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000026 RID: 38
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSubmitDenied = new ExEventLog.EventTuple(1074004991U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000027 RID: 39
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSendAsDeniedTempAuthFailure = new ExEventLog.EventTuple(1074004992U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000028 RID: 40
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSendAsDeniedSenderAddressDataInvalid = new ExEventLog.EventTuple(3221488641U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000029 RID: 41
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSendAsDenied = new ExEventLog.EventTuple(1074004994U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002A RID: 42
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSendOnBehalfOfDeniedTempAuthFailure = new ExEventLog.EventTuple(1074004995U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002B RID: 43
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSendOnBehalfOfDeniedFromAddressDataInvalid = new ExEventLog.EventTuple(3221488644U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002C RID: 44
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSendOnBehalfOfDenied = new ExEventLog.EventTuple(1074004997U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002D RID: 45
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveCouldNotDetermineUserNameOrSid = new ExEventLog.EventTuple(3221488646U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002E RID: 46
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveMessageRateLimitExceeded = new ExEventLog.EventTuple(1074004999U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002F RID: 47
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveTLSRequiredFailed = new ExEventLog.EventTuple(3221488648U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000030 RID: 48
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveCatchAll = new ExEventLog.EventTuple(3221488649U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000031 RID: 49
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveAuthenticationInitializationFailed = new ExEventLog.EventTuple(3221488650U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000032 RID: 50
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveAuthenticationFailed = new ExEventLog.EventTuple(2147746827U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000033 RID: 51
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveDirectTrustFailed = new ExEventLog.EventTuple(3221488652U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000034 RID: 52
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveActiveManagerFailure = new ExEventLog.EventTuple(3221488653U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000035 RID: 53
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveAvailabilityCounterFailure = new ExEventLog.EventTuple(3221488654U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000036 RID: 54
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProhibitSendQuotaDeniedTempAuthFailure = new ExEventLog.EventTuple(1074005007U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000037 RID: 55
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveConnectorAvailabilityLow = new ExEventLog.EventTuple(3221488656U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000038 RID: 56
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveConnectorAvailabilityNormal = new ExEventLog.EventTuple(1074005009U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000039 RID: 57
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProxyMserveLookupFailed = new ExEventLog.EventTuple(3221488658U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003A RID: 58
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProxyInvalidPartnerId = new ExEventLog.EventTuple(3221488659U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003B RID: 59
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProxyDnsLookupFailed = new ExEventLog.EventTuple(3221488660U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003C RID: 60
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProxyCatchAll = new ExEventLog.EventTuple(3221488661U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003D RID: 61
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProxyCounterFailure = new ExEventLog.EventTuple(3221488663U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003E RID: 62
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveTooManyProxySessionFailures = new ExEventLog.EventTuple(3221488664U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003F RID: 63
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveNoDestinationToProxyTo = new ExEventLog.EventTuple(3221488665U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000040 RID: 64
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProcessingBlobFailed = new ExEventLog.EventTuple(3221488666U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000041 RID: 65
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendDnsConnectionFailure = new ExEventLog.EventTuple(2147747792U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000042 RID: 66
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendConnectionError = new ExEventLog.EventTuple(2147747794U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000043 RID: 67
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendAuthenticationFailed = new ExEventLog.EventTuple(2147747795U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000044 RID: 68
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendAckMessage = new ExEventLog.EventTuple(1074005972U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000045 RID: 69
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendAckConnection = new ExEventLog.EventTuple(1074005973U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000046 RID: 70
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendRemoteDisconnected = new ExEventLog.EventTuple(2147747798U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000047 RID: 71
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendNewSession = new ExEventLog.EventTuple(1074005975U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000048 RID: 72
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExchangeAuthHashNotSupported = new ExEventLog.EventTuple(3221489627U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000049 RID: 73
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SendConnectorInvalidSourceIPAddress = new ExEventLog.EventTuple(3221489630U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004A RID: 74
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendTLSRequiredFailed = new ExEventLog.EventTuple(3221489631U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400004B RID: 75
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendAuthenticationInitializationFailed = new ExEventLog.EventTuple(3221489632U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004C RID: 76
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendOutboundAuthenticationFailed = new ExEventLog.EventTuple(3221489633U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004D RID: 77
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendDirectTrustFailed = new ExEventLog.EventTuple(3221489634U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400004E RID: 78
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendUnableToTransmitOrar = new ExEventLog.EventTuple(3221489635U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004F RID: 79
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendUnableToTransmitLongOrar = new ExEventLog.EventTuple(3221489636U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000050 RID: 80
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendUnableToTransmitRDst = new ExEventLog.EventTuple(3221489637U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000051 RID: 81
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendOutboundAtTLSAuthLevelFailed = new ExEventLog.EventTuple(3221489638U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000052 RID: 82
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendAuthenticationFailureIgnored = new ExEventLog.EventTuple(2147747815U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000053 RID: 83
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendNewProxySession = new ExEventLog.EventTuple(1074005992U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000054 RID: 84
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendProxyEhloOptionsDoNotMatch = new ExEventLog.EventTuple(3221489641U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000055 RID: 85
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendInboundProxyEhloOptionsDoNotMatch = new ExEventLog.EventTuple(3221489642U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000056 RID: 86
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendInboundProxyRecipientLimitsDoNotMatch = new ExEventLog.EventTuple(3221489643U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000057 RID: 87
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendPoisonForRemoteThresholdExceeded = new ExEventLog.EventTuple(2147747820U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000058 RID: 88
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendInboundProxyNonCriticalEhloOptionsDoNotMatch = new ExEventLog.EventTuple(2147747821U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000059 RID: 89
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendProxyEhloOptionsDoNotMatchButStillContinueProxying = new ExEventLog.EventTuple(3221489646U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005A RID: 90
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InboundProxyDestinationsTrackerDiagnosticInfo = new ExEventLog.EventTuple(1074005999U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005B RID: 91
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InboundProxyDestinationsTrackerReject = new ExEventLog.EventTuple(3221489648U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005C RID: 92
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InboundProxyDestinationsTrackerNearThreshold = new ExEventLog.EventTuple(2147747825U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005D RID: 93
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InboundProxyAccountForestsTrackerReject = new ExEventLog.EventTuple(3221489650U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005E RID: 94
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InboundProxyAccountForestsTrackerNearThreshold = new ExEventLog.EventTuple(2147747827U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005F RID: 95
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DsnUnableToReadQuarantineConfig = new ExEventLog.EventTuple(3221490620U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000060 RID: 96
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DsnUnableToReadSystemMessageConfig = new ExEventLog.EventTuple(3221490621U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000061 RID: 97
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DsnDiskFull = new ExEventLog.EventTuple(3221490622U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000062 RID: 98
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_XProxyToCommandInvalidEncodedCertificateSubject = new ExEventLog.EventTuple(3221490623U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000063 RID: 99
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingPerfCountersLoadFailure = new ExEventLog.EventTuple(3221492617U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000064 RID: 100
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingAdUnavailable = new ExEventLog.EventTuple(3221492618U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000065 RID: 101
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingWillRetryLoad = new ExEventLog.EventTuple(3221492619U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000066 RID: 102
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoServerFqdn = new ExEventLog.EventTuple(3221492620U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000067 RID: 103
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoServerAdSite = new ExEventLog.EventTuple(3221492621U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000068 RID: 104
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoOwningServerForMdb = new ExEventLog.EventTuple(2147750798U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000069 RID: 105
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoRouteToAdSite = new ExEventLog.EventTuple(3221492623U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006A RID: 106
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoRouteToOwningServer = new ExEventLog.EventTuple(3221492624U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400006B RID: 107
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoPfTreeMdbRoute = new ExEventLog.EventTuple(2147750801U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006C RID: 108
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoPfTreeRoute = new ExEventLog.EventTuple(3221492626U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006D RID: 109
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoSourceRgForRgConnector = new ExEventLog.EventTuple(3221492627U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006E RID: 110
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoTargetRgForRgConnector = new ExEventLog.EventTuple(3221492628U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006F RID: 111
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoServerRg = new ExEventLog.EventTuple(3221492629U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000070 RID: 112
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoSourceBhServers = new ExEventLog.EventTuple(3221492630U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000071 RID: 113
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoSourceBhRoute = new ExEventLog.EventTuple(3221492631U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000072 RID: 114
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoRouteToConnector = new ExEventLog.EventTuple(3221492632U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000073 RID: 115
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoTargetBhServer = new ExEventLog.EventTuple(3221492633U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000074 RID: 116
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoTargetBhServers = new ExEventLog.EventTuple(3221492634U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000075 RID: 117
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingInvalidSmarthosts = new ExEventLog.EventTuple(3221492635U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000076 RID: 118
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingTransientConfigError = new ExEventLog.EventTuple(3221492639U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000077 RID: 119
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingMaxConfigLoadRetriesReached = new ExEventLog.EventTuple(3221492640U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000078 RID: 120
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoSourceRgForNonRgConnector = new ExEventLog.EventTuple(3221492643U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000079 RID: 121
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingLocalConnectorWithConnectedDomains = new ExEventLog.EventTuple(3221492644U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007A RID: 122
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoConnectedRg = new ExEventLog.EventTuple(3221492645U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007B RID: 123
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingTableLogCreationFailure = new ExEventLog.EventTuple(3221492646U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007C RID: 124
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingTableLogDeletionFailure = new ExEventLog.EventTuple(3221492647U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007D RID: 125
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoDeliveryGroupForDatabase = new ExEventLog.EventTuple(2147750827U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007E RID: 126
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoRoutingGroupForDatabase = new ExEventLog.EventTuple(2147750828U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007F RID: 127
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoDagForDatabase = new ExEventLog.EventTuple(2147750829U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000080 RID: 128
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoDestinationForDatabase = new ExEventLog.EventTuple(3221492654U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000081 RID: 129
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoHubServersSelectedForDatabases = new ExEventLog.EventTuple(3221492655U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000082 RID: 130
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoHubServersSelectedForTenant = new ExEventLog.EventTuple(3221492656U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000083 RID: 131
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InactiveDagsExcludedFromDagSelector = new ExEventLog.EventTuple(1074009009U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000084 RID: 132
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DagSelectorDiagnosticInfo = new ExEventLog.EventTuple(1074009010U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000085 RID: 133
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TenantDagQuotaDiagnosticInfo = new ExEventLog.EventTuple(1074009011U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000086 RID: 134
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingTableDatabaseFullReload = new ExEventLog.EventTuple(1074009012U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000087 RID: 135
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingDictionaryInsertFailure = new ExEventLog.EventTuple(3221492661U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000088 RID: 136
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PipelineTracingActive = new ExEventLog.EventTuple(2147751292U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000089 RID: 137
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerfCountersLoadFailure = new ExEventLog.EventTuple(3221493117U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400008A RID: 138
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExternalServersLatencyTimeNotSync = new ExEventLog.EventTuple(2147751294U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400008B RID: 139
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MultiplePreProcessLatencies = new ExEventLog.EventTuple(3221493119U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400008C RID: 140
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NullLatencyTreeLeaf = new ExEventLog.EventTuple(3221493120U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400008D RID: 141
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MultipleCompletions = new ExEventLog.EventTuple(3221493121U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400008E RID: 142
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StopService = new ExEventLog.EventTuple(1074010969U, 6, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400008F RID: 143
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseInUse = new ExEventLog.EventTuple(3221494618U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000090 RID: 144
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActivationTiming = new ExEventLog.EventTuple(2147752796U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000091 RID: 145
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NewDatabaseCreated = new ExEventLog.EventTuple(1074010973U, 6, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000092 RID: 146
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseSchemaNotSupported = new ExEventLog.EventTuple(3221494624U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000093 RID: 147
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RetrieveServiceState = new ExEventLog.EventTuple(1074010977U, 6, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000094 RID: 148
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActivationSlow = new ExEventLog.EventTuple(2147752802U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000095 RID: 149
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HubTransportServiceStateChanged = new ExEventLog.EventTuple(2147752803U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000096 RID: 150
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FrontendTransportServiceStateChanged = new ExEventLog.EventTuple(2147752804U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000097 RID: 151
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FrontendTransportRestartOnServiceStateChange = new ExEventLog.EventTuple(2147752805U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000098 RID: 152
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EdgeTransportServiceStateChanged = new ExEventLog.EventTuple(2147752806U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000099 RID: 153
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FrontendTransportServiceInitializationFailure = new ExEventLog.EventTuple(3221494631U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009A RID: 154
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EdgeTransportInitializationFailure = new ExEventLog.EventTuple(3221494632U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009B RID: 155
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MSExchangeTransportInitializationFailure = new ExEventLog.EventTuple(3221494633U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009C RID: 156
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResubmitDueToConfigUpdate = new ExEventLog.EventTuple(1074011970U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009D RID: 157
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResubmitDueToInactivityTimeout = new ExEventLog.EventTuple(1074011971U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400009E RID: 158
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NonSmtpGWBadDropDirectory = new ExEventLog.EventTuple(3221495620U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400009F RID: 159
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NonSmtpGWQuotaExceeded = new ExEventLog.EventTuple(3221495621U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A0 RID: 160
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_QueuingStatusAtShutdown = new ExEventLog.EventTuple(1074011974U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000A1 RID: 161
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NonSmtpGWPathTooLongException = new ExEventLog.EventTuple(3221495623U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A2 RID: 162
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NonSmtpGWNoDropDirectory = new ExEventLog.EventTuple(3221495624U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A3 RID: 163
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NonSmtpGWUnauthorizedAccess = new ExEventLog.EventTuple(3221495625U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A4 RID: 164
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RetryDeliveryIfRejected = new ExEventLog.EventTuple(3221495626U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A5 RID: 165
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnOpenConnectionAgentException = new ExEventLog.EventTuple(3221495627U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000A6 RID: 166
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnDeliverMailItemAgentException = new ExEventLog.EventTuple(3221495628U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000A7 RID: 167
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnCloseConnectionAgentException = new ExEventLog.EventTuple(3221495629U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000A8 RID: 168
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResubmitDueToUnavailabilityOfSameVersionHubs = new ExEventLog.EventTuple(1074011982U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000A9 RID: 169
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RedirectMessageStarted = new ExEventLog.EventTuple(1074011983U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000AA RID: 170
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_QueueViewerExceptionDuringAsyncRetryQueue = new ExEventLog.EventTuple(3221495632U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000AB RID: 171
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResubmitDueToOutboundConnectorChange = new ExEventLog.EventTuple(1074011985U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000AC RID: 172
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RetryQueueOutboundConnectorLookupFailed = new ExEventLog.EventTuple(3221495634U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000AD RID: 173
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryDoesNotExist = new ExEventLog.EventTuple(1074012969U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000AE RID: 174
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoDirectoryPermission = new ExEventLog.EventTuple(3221496618U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000AF RID: 175
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ReadOnlyFileFound = new ExEventLog.EventTuple(2147754795U, 8, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B0 RID: 176
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotDeleteFile = new ExEventLog.EventTuple(3221496620U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B1 RID: 177
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PickupFailedDueToStorageErrors = new ExEventLog.EventTuple(3221496621U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B2 RID: 178
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCreatePickupDirectory = new ExEventLog.EventTuple(3221496622U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B3 RID: 179
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoPermissionToRenamePickupFile = new ExEventLog.EventTuple(3221496623U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B4 RID: 180
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AccessErrorModifyingPickupRegkey = new ExEventLog.EventTuple(3221496625U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B5 RID: 181
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PickupIsBadmailingFile = new ExEventLog.EventTuple(3221496626U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B6 RID: 182
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PickupFileEncrypted = new ExEventLog.EventTuple(3221496628U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B7 RID: 183
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnSubmittedMessageAgentException = new ExEventLog.EventTuple(3221496817U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B8 RID: 184
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnRoutedMessageAgentException = new ExEventLog.EventTuple(3221496818U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000B9 RID: 185
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransportConfigContainerNotFound = new ExEventLog.EventTuple(3221496824U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000BA RID: 186
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResolverPerfCountersLoadFailure = new ExEventLog.EventTuple(3221496828U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000BB RID: 187
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RetryCategorizationIfFailed = new ExEventLog.EventTuple(3221496829U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000BC RID: 188
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmbiguousSender = new ExEventLog.EventTuple(2147755006U, 9, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000BD RID: 189
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnCategorizedMessageAgentException = new ExEventLog.EventTuple(3221496831U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000BE RID: 190
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnResolvedMessageAgentException = new ExEventLog.EventTuple(3221496832U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000BF RID: 191
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmbiguousRecipient = new ExEventLog.EventTuple(3221496833U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000C0 RID: 192
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NDRForUnrestrictedLargeDL = new ExEventLog.EventTuple(3221496834U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000C1 RID: 193
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CategorizerErrorRetrievingTenantOverride = new ExEventLog.EventTuple(3221496835U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000C2 RID: 194
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MessageCountEnqueuedToPoisonQueue = new ExEventLog.EventTuple(2147755793U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C3 RID: 195
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonCountUpdated = new ExEventLog.EventTuple(1074013970U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C4 RID: 196
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageCrash = new ExEventLog.EventTuple(3221497619U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C5 RID: 197
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeletedPoisonPickupFile = new ExEventLog.EventTuple(2147755796U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C6 RID: 198
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageLoadFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221497621U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C7 RID: 199
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageSaveFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221497622U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C8 RID: 200
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageMarkFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221497623U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000C9 RID: 201
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessagePruneFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221497624U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000CA RID: 202
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageSecurityTLSCertificateValidationFailure = new ExEventLog.EventTuple(3221498621U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000CB RID: 203
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TlsDomainCertificateValidationFailure = new ExEventLog.EventTuple(3221498627U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000CC RID: 204
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageNotAuthenticatedTlsNotStarted = new ExEventLog.EventTuple(3221498628U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000CD RID: 205
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageToSecureDomainFailedDueToTlsNegotiationFailure = new ExEventLog.EventTuple(3221498629U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000CE RID: 206
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageToSecureDomainFailedBecauseTlsNotOffered = new ExEventLog.EventTuple(3221498630U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000CF RID: 207
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TlsDomainClientCertificateSubjectMismatch = new ExEventLog.EventTuple(3221498631U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D0 RID: 208
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TlsDomainServerCertificateSubjectMismatch = new ExEventLog.EventTuple(3221498632U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D1 RID: 209
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageNotAuthenticatedNoClientCertificate = new ExEventLog.EventTuple(3221498633U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D2 RID: 210
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageNotAuthenticatedTlsNotAdvertised = new ExEventLog.EventTuple(3221498634U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D3 RID: 211
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageToSecureDomainFailedBecauseTlsNegotiationFailed = new ExEventLog.EventTuple(3221498635U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D4 RID: 212
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TlsDomainServerCertificateValidationFailure = new ExEventLog.EventTuple(3221498636U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D5 RID: 213
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TlsDomainSecureDisabled = new ExEventLog.EventTuple(3221498637U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D6 RID: 214
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TlsDomainCapabilitiesCertificateValidationFailure = new ExEventLog.EventTuple(3221498638U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D7 RID: 215
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SessionFailedBecauseXOorgNotOffered = new ExEventLog.EventTuple(3221498639U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D8 RID: 216
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SubjectAlternativeNameLimitExceeded = new ExEventLog.EventTuple(2147756816U, 11, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D9 RID: 217
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateRevocationListCheckTrasientFailureTreatedAsSuccess = new ExEventLog.EventTuple(3221498642U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000DA RID: 218
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToFlushTicketCacheOnInitialize = new ExEventLog.EventTuple(3221499617U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DB RID: 219
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigUpdateOccurred = new ExEventLog.EventTuple(1074015970U, 12, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DC RID: 220
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidServerRole = new ExEventLog.EventTuple(3221499619U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DD RID: 221
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReadConfigReceiveConnectorFailed = new ExEventLog.EventTuple(3221499624U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DE RID: 222
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReadConfigReceiveConnectorUnavail = new ExEventLog.EventTuple(3221499625U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000DF RID: 223
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADRecipientCachePerfCountersLoadFailure = new ExEventLog.EventTuple(3221499626U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E0 RID: 224
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SpnRegisterFailure = new ExEventLog.EventTuple(3221499627U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E1 RID: 225
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InternalTransportCertificateMissingInAD = new ExEventLog.EventTuple(3221499628U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E2 RID: 226
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotLoadInternalTransportCertificateFromStore = new ExEventLog.EventTuple(3221499629U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E3 RID: 227
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotLoadSTARTTLSCertificateFromStore = new ExEventLog.EventTuple(3221499630U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E4 RID: 228
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InternalTransportCertificateExpired = new ExEventLog.EventTuple(3221499631U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E5 RID: 229
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_STARTTLSCertificateExpired = new ExEventLog.EventTuple(3221499632U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E6 RID: 230
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InternalTransportCertificateExpiresSoon = new ExEventLog.EventTuple(3221499633U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E7 RID: 231
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_STARTTLSCertificateExpiresSoon = new ExEventLog.EventTuple(3221499634U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E8 RID: 232
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RemoteInternalTransportCertificateExpired = new ExEventLog.EventTuple(3221499635U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E9 RID: 233
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RemoteSTARTTLSCertificateExpired = new ExEventLog.EventTuple(3221499636U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000EA RID: 234
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReadConfigReceiveConnectorIgnored = new ExEventLog.EventTuple(2147757813U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000EB RID: 235
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InternalTransportCertificateCorruptedInAD = new ExEventLog.EventTuple(3221499638U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000EC RID: 236
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotLoadInternalTransportCertificateFallbackServerFQDN = new ExEventLog.EventTuple(2147757815U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000ED RID: 237
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotLoadIntTransportCertificateFallbackEphemeralCertificate = new ExEventLog.EventTuple(2147757816U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000EE RID: 238
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DisconnectingPerformanceCounters = new ExEventLog.EventTuple(2147757817U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000EF RID: 239
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToDisconnectPerformanceCounters = new ExEventLog.EventTuple(2147757818U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F0 RID: 240
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessHoldingPerformanceCounter = new ExEventLog.EventTuple(2147757820U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F1 RID: 241
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_ServerLivingForConsiderableTime = new ExEventLog.EventTuple(1074015997U, 12, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000F2 RID: 242
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_KillOrphanedWorker = new ExEventLog.EventTuple(2147757822U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F3 RID: 243
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AnotherServiceRunning = new ExEventLog.EventTuple(2147757823U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F4 RID: 244
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_KillOrphanedWorkerFailed = new ExEventLog.EventTuple(3221499648U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F5 RID: 245
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToRegisterForDeletedObjectsNotification = new ExEventLog.EventTuple(3221499649U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F6 RID: 246
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SystemLowOnMemory = new ExEventLog.EventTuple(2147757826U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F7 RID: 247
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Exch50OrgNotFound = new ExEventLog.EventTuple(3221500618U, 13, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F8 RID: 248
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessNotResponding = new ExEventLog.EventTuple(3221501617U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F9 RID: 249
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AppConfigLoadFailed = new ExEventLog.EventTuple(3221501620U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000FA RID: 250
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResourceUtilizationUp = new ExEventLog.EventTuple(2147760796U, 15, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000FB RID: 251
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResourceUtilizationDown = new ExEventLog.EventTuple(1074018973U, 15, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000FC RID: 252
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DiskSpaceLow = new ExEventLog.EventTuple(3221502622U, 15, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000FD RID: 253
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PrivateBytesHigh = new ExEventLog.EventTuple(3221502623U, 15, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000FE RID: 254
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ComponentFailedTransportServerUpdate = new ExEventLog.EventTuple(3221503620U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000FF RID: 255
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MessageTrackingLogPathIsNull = new ExEventLog.EventTuple(2147761803U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000100 RID: 256
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReceiveProtocolLogPathIsNull = new ExEventLog.EventTuple(2147761804U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000101 RID: 257
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SendProtocolLogPathIsNull = new ExEventLog.EventTuple(2147761805U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000102 RID: 258
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReceiveProtocolLogPathIsNullUsingOld = new ExEventLog.EventTuple(2147761806U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000103 RID: 259
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SendProtocolLogPathIsNullUsingOld = new ExEventLog.EventTuple(2147761807U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000104 RID: 260
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DefaultAuthoritativeDomainInvalid = new ExEventLog.EventTuple(3221503632U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000105 RID: 261
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActivationFailed = new ExEventLog.EventTuple(3221503633U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000106 RID: 262
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationLoaderExternalError = new ExEventLog.EventTuple(2147761810U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000107 RID: 263
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationLoaderException = new ExEventLog.EventTuple(2147761811U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000108 RID: 264
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidAcceptedDomain = new ExEventLog.EventTuple(3221503637U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000109 RID: 265
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationLoaderSuccessfulUpdate = new ExEventLog.EventTuple(1074019990U, 16, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400010A RID: 266
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CannotStartAgents = new ExEventLog.EventTuple(3221503639U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400010B RID: 267
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RejectedAcceptedDomain = new ExEventLog.EventTuple(3221503640U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400010C RID: 268
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidAdapterGuid = new ExEventLog.EventTuple(3221503641U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400010D RID: 269
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NetworkAdapterIPQueryFailed = new ExEventLog.EventTuple(3221503642U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400010E RID: 270
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationLoaderNoADNotifications = new ExEventLog.EventTuple(2147761819U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400010F RID: 271
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationLoaderSuccessfulForcedUpdate = new ExEventLog.EventTuple(1074019996U, 16, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000110 RID: 272
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HeartbeatDestinationConfigChanged = new ExEventLog.EventTuple(2147761821U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000111 RID: 273
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AgentErrorHandlingOverrideConfigError = new ExEventLog.EventTuple(3221503646U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000112 RID: 274
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SchemaTypeMismatch = new ExEventLog.EventTuple(3221504617U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000113 RID: 275
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SchemaRequiredColumnNotFound = new ExEventLog.EventTuple(3221504618U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000114 RID: 276
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetCorruptionError = new ExEventLog.EventTuple(3221504619U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000115 RID: 277
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetOutOfSpaceError = new ExEventLog.EventTuple(3221504620U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000116 RID: 278
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetLogFileError = new ExEventLog.EventTuple(3221504621U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000117 RID: 279
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetPathError = new ExEventLog.EventTuple(3221504622U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000118 RID: 280
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetMismatchError = new ExEventLog.EventTuple(3221504623U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000119 RID: 281
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StartScanForMessages = new ExEventLog.EventTuple(1074020976U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011A RID: 282
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StopScanForMessages = new ExEventLog.EventTuple(1074020977U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011B RID: 283
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EndScanForMessages = new ExEventLog.EventTuple(1074020978U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011C RID: 284
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetCheckpointFileError = new ExEventLog.EventTuple(3221504627U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011D RID: 285
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetInitInstanceOutOfMemory = new ExEventLog.EventTuple(3221504628U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011E RID: 286
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetInstanceNameInUse = new ExEventLog.EventTuple(3221504629U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011F RID: 287
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetDatabaseNotFound = new ExEventLog.EventTuple(3221504630U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000120 RID: 288
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetDatabaseLogSetMismatch = new ExEventLog.EventTuple(3221504631U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000121 RID: 289
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetFragmentationError = new ExEventLog.EventTuple(3221504632U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000122 RID: 290
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetQuotaExceededError = new ExEventLog.EventTuple(3221504633U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000123 RID: 291
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetInsufficientResourcesError = new ExEventLog.EventTuple(3221504634U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000124 RID: 292
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetIOError = new ExEventLog.EventTuple(3221504635U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000125 RID: 293
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetOperationError = new ExEventLog.EventTuple(3221504636U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000126 RID: 294
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetTableNotFound = new ExEventLog.EventTuple(3221504637U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000127 RID: 295
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetFileNotFound = new ExEventLog.EventTuple(3221504638U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000128 RID: 296
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetFileReadOnly = new ExEventLog.EventTuple(3221504639U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000129 RID: 297
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetVersionStoreOutOfMemoryError = new ExEventLog.EventTuple(3221504640U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012A RID: 298
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LastMessagesLoadedByBootScanner = new ExEventLog.EventTuple(1074020993U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012B RID: 299
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseDriveIsNotAccessible = new ExEventLog.EventTuple(3221504642U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012C RID: 300
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ColumnTooBigException = new ExEventLog.EventTuple(3221504643U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012D RID: 301
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AgentDidNotCloseMimeStream = new ExEventLog.EventTuple(3221505617U, 18, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012E RID: 302
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientValidationCacheLoaded = new ExEventLog.EventTuple(1074022969U, 19, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012F RID: 303
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientGroupCacheLoaded = new ExEventLog.EventTuple(1074022970U, 19, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000130 RID: 304
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryUnavailableLoadingGroup = new ExEventLog.EventTuple(2147764798U, 19, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000131 RID: 305
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryUnavailableLoadingValidationCache = new ExEventLog.EventTuple(2147764799U, 19, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000132 RID: 306
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientHasDataValidationException = new ExEventLog.EventTuple(3221506624U, 19, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000133 RID: 307
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ORARMessageSubmitted = new ExEventLog.EventTuple(1074023969U, 20, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000134 RID: 308
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailParseOrarBlob = new ExEventLog.EventTuple(2147765794U, 20, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000135 RID: 309
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailGetRoutingAddress = new ExEventLog.EventTuple(2147765795U, 20, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000136 RID: 310
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseRecoveryActionNone = new ExEventLog.EventTuple(3221504717U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000137 RID: 311
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseRecoveryActionMove = new ExEventLog.EventTuple(2147762894U, 17, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000138 RID: 312
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseRecoveryActionDelete = new ExEventLog.EventTuple(1074021071U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000139 RID: 313
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseRecoveryActionFailed = new ExEventLog.EventTuple(3221504720U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400013A RID: 314
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseRecoveryActionFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221504721U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400013B RID: 315
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DataBaseCorruptionDetected = new ExEventLog.EventTuple(1074021074U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400013C RID: 316
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseErrorDetected = new ExEventLog.EventTuple(1074021075U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400013D RID: 317
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TableLockedException = new ExEventLog.EventTuple(3221504724U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400013E RID: 318
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowRedundancyMessagesResubmitted = new ExEventLog.EventTuple(1074025969U, 22, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400013F RID: 319
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowRedundancyMessageResubmitSuppressed = new ExEventLog.EventTuple(1074025970U, 22, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000140 RID: 320
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowRedundancyPrimaryServerDatabaseStateChanged = new ExEventLog.EventTuple(3221509619U, 22, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000141 RID: 321
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowRedundancyPrimaryServerHeartbeatFailed = new ExEventLog.EventTuple(2147767796U, 22, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000142 RID: 322
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowRedundancyMessageDeferredDueToShadowFailure = new ExEventLog.EventTuple(3221509622U, 22, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000143 RID: 323
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowRedundancyForcedHeartbeatReset = new ExEventLog.EventTuple(2147767799U, 22, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000144 RID: 324
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ModeratedTransportNoArbitrationMailbox = new ExEventLog.EventTuple(3221510617U, 23, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000145 RID: 325
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientStampedWithDeletedArbitrationMailbox = new ExEventLog.EventTuple(3221510618U, 23, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000146 RID: 326
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RemovedMessageRepositoryRequest = new ExEventLog.EventTuple(1074027972U, 24, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000147 RID: 327
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ModifiedMessageRepositoryRequest = new ExEventLog.EventTuple(1074027973U, 24, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000148 RID: 328
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RegisterRpcServerFailure = new ExEventLog.EventTuple(1074027975U, 24, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000149 RID: 329
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageAttributionFailed = new ExEventLog.EventTuple(3221511626U, 25, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400014A RID: 330
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransportWlmLogPathIsNull = new ExEventLog.EventTuple(3221511627U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400014B RID: 331
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResubmitRequestExpired = new ExEventLog.EventTuple(1074027980U, 24, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400014C RID: 332
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DuplicateResubmitRequest = new ExEventLog.EventTuple(2147769805U, 24, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400014D RID: 333
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MaxRunningResubmitRequest = new ExEventLog.EventTuple(2147769806U, 24, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400014E RID: 334
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MaxRecentResubmitRequest = new ExEventLog.EventTuple(2147769807U, 24, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400014F RID: 335
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InterceptorAgentConfigurationLoadingError = new ExEventLog.EventTuple(3221512617U, 18, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000150 RID: 336
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InterceptorAgentConfigurationReplaced = new ExEventLog.EventTuple(287146U, 18, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000151 RID: 337
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InterceptorAgentAccessDenied = new ExEventLog.EventTuple(3221512619U, 18, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000152 RID: 338
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InterceptorRuleNearingExpiration = new ExEventLog.EventTuple(2147770796U, 18, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000153 RID: 339
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_QueueQuotaComponentLogPathIsNull = new ExEventLog.EventTuple(3221512622U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000154 RID: 340
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NotEnoughMemoryToStartService = new ExEventLog.EventTuple(3221512623U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000155 RID: 341
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FlowControlLogPathIsNull = new ExEventLog.EventTuple(3221512624U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000156 RID: 342
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FilePathOnLockedVolume = new ExEventLog.EventTuple(3221512625U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000157 RID: 343
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BitlockerQueryFailed = new ExEventLog.EventTuple(3221512626U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000004 RID: 4
		private enum Category : short
		{
			// Token: 0x04000159 RID: 345
			SmtpReceive = 1,
			// Token: 0x0400015A RID: 346
			SmtpSend,
			// Token: 0x0400015B RID: 347
			Dsn,
			// Token: 0x0400015C RID: 348
			Routing,
			// Token: 0x0400015D RID: 349
			Logging,
			// Token: 0x0400015E RID: 350
			Components,
			// Token: 0x0400015F RID: 351
			RemoteDelivery,
			// Token: 0x04000160 RID: 352
			Pickup,
			// Token: 0x04000161 RID: 353
			Categorizer,
			// Token: 0x04000162 RID: 354
			PoisonMessage,
			// Token: 0x04000163 RID: 355
			MessageSecurity,
			// Token: 0x04000164 RID: 356
			TransportService,
			// Token: 0x04000165 RID: 357
			Exch50,
			// Token: 0x04000166 RID: 358
			Process,
			// Token: 0x04000167 RID: 359
			ResourceManager,
			// Token: 0x04000168 RID: 360
			Configuration,
			// Token: 0x04000169 RID: 361
			Storage,
			// Token: 0x0400016A RID: 362
			Agents,
			// Token: 0x0400016B RID: 363
			Transport_Address_Book,
			// Token: 0x0400016C RID: 364
			Orar,
			// Token: 0x0400016D RID: 365
			Unused,
			// Token: 0x0400016E RID: 366
			ShadowRedundancy,
			// Token: 0x0400016F RID: 367
			Approval,
			// Token: 0x04000170 RID: 368
			TransportSafetyNet,
			// Token: 0x04000171 RID: 369
			TransportTenantAttribution
		}

		// Token: 0x02000005 RID: 5
		internal enum Message : uint
		{
			// Token: 0x04000173 RID: 371
			BindingIPv6ButDisabled = 3221488616U,
			// Token: 0x04000174 RID: 372
			AddressInUse,
			// Token: 0x04000175 RID: 373
			ConfiguredConnectors = 1074004970U,
			// Token: 0x04000176 RID: 374
			SmtpReceiveMaxConnectionReached = 2147746801U,
			// Token: 0x04000177 RID: 375
			SmtpReceiveMessageRejected = 1074004978U,
			// Token: 0x04000178 RID: 376
			SmtpReceiveAuthenticationFailedTooManyErrors = 2147746804U,
			// Token: 0x04000179 RID: 377
			SmtpReceiveRejectDueToStorageError = 3221488629U,
			// Token: 0x0400017A RID: 378
			SmtpReceiveAuthorizationSubmitRejected = 2147746810U,
			// Token: 0x0400017B RID: 379
			SmtpReceiveAuthorizationRejected = 2147746812U,
			// Token: 0x0400017C RID: 380
			SmtpReceiveMaxConnectionPerSourceReached,
			// Token: 0x0400017D RID: 381
			InternalSMTPServerListEmpty,
			// Token: 0x0400017E RID: 382
			SmtpReceiveSubmitDenied = 1074004991U,
			// Token: 0x0400017F RID: 383
			SmtpReceiveSendAsDeniedTempAuthFailure,
			// Token: 0x04000180 RID: 384
			SmtpReceiveSendAsDeniedSenderAddressDataInvalid = 3221488641U,
			// Token: 0x04000181 RID: 385
			SmtpReceiveSendAsDenied = 1074004994U,
			// Token: 0x04000182 RID: 386
			SmtpReceiveSendOnBehalfOfDeniedTempAuthFailure,
			// Token: 0x04000183 RID: 387
			SmtpReceiveSendOnBehalfOfDeniedFromAddressDataInvalid = 3221488644U,
			// Token: 0x04000184 RID: 388
			SmtpReceiveSendOnBehalfOfDenied = 1074004997U,
			// Token: 0x04000185 RID: 389
			SmtpReceiveCouldNotDetermineUserNameOrSid = 3221488646U,
			// Token: 0x04000186 RID: 390
			SmtpReceiveMessageRateLimitExceeded = 1074004999U,
			// Token: 0x04000187 RID: 391
			SmtpReceiveTLSRequiredFailed = 3221488648U,
			// Token: 0x04000188 RID: 392
			SmtpReceiveCatchAll,
			// Token: 0x04000189 RID: 393
			SmtpReceiveAuthenticationInitializationFailed,
			// Token: 0x0400018A RID: 394
			SmtpReceiveAuthenticationFailed = 2147746827U,
			// Token: 0x0400018B RID: 395
			SmtpReceiveDirectTrustFailed = 3221488652U,
			// Token: 0x0400018C RID: 396
			SmtpReceiveActiveManagerFailure,
			// Token: 0x0400018D RID: 397
			SmtpReceiveAvailabilityCounterFailure,
			// Token: 0x0400018E RID: 398
			SmtpReceiveProhibitSendQuotaDeniedTempAuthFailure = 1074005007U,
			// Token: 0x0400018F RID: 399
			SmtpReceiveConnectorAvailabilityLow = 3221488656U,
			// Token: 0x04000190 RID: 400
			SmtpReceiveConnectorAvailabilityNormal = 1074005009U,
			// Token: 0x04000191 RID: 401
			SmtpReceiveProxyMserveLookupFailed = 3221488658U,
			// Token: 0x04000192 RID: 402
			SmtpReceiveProxyInvalidPartnerId,
			// Token: 0x04000193 RID: 403
			SmtpReceiveProxyDnsLookupFailed,
			// Token: 0x04000194 RID: 404
			SmtpReceiveProxyCatchAll,
			// Token: 0x04000195 RID: 405
			SmtpReceiveProxyCounterFailure = 3221488663U,
			// Token: 0x04000196 RID: 406
			SmtpReceiveTooManyProxySessionFailures,
			// Token: 0x04000197 RID: 407
			SmtpReceiveNoDestinationToProxyTo,
			// Token: 0x04000198 RID: 408
			SmtpReceiveProcessingBlobFailed,
			// Token: 0x04000199 RID: 409
			SmtpSendDnsConnectionFailure = 2147747792U,
			// Token: 0x0400019A RID: 410
			SmtpSendConnectionError = 2147747794U,
			// Token: 0x0400019B RID: 411
			SmtpSendAuthenticationFailed,
			// Token: 0x0400019C RID: 412
			SmtpSendAckMessage = 1074005972U,
			// Token: 0x0400019D RID: 413
			SmtpSendAckConnection,
			// Token: 0x0400019E RID: 414
			SmtpSendRemoteDisconnected = 2147747798U,
			// Token: 0x0400019F RID: 415
			SmtpSendNewSession = 1074005975U,
			// Token: 0x040001A0 RID: 416
			ExchangeAuthHashNotSupported = 3221489627U,
			// Token: 0x040001A1 RID: 417
			SendConnectorInvalidSourceIPAddress = 3221489630U,
			// Token: 0x040001A2 RID: 418
			SmtpSendTLSRequiredFailed,
			// Token: 0x040001A3 RID: 419
			SmtpSendAuthenticationInitializationFailed,
			// Token: 0x040001A4 RID: 420
			SmtpSendOutboundAuthenticationFailed,
			// Token: 0x040001A5 RID: 421
			SmtpSendDirectTrustFailed,
			// Token: 0x040001A6 RID: 422
			SmtpSendUnableToTransmitOrar,
			// Token: 0x040001A7 RID: 423
			SmtpSendUnableToTransmitLongOrar,
			// Token: 0x040001A8 RID: 424
			SmtpSendUnableToTransmitRDst,
			// Token: 0x040001A9 RID: 425
			SmtpSendOutboundAtTLSAuthLevelFailed,
			// Token: 0x040001AA RID: 426
			SmtpSendAuthenticationFailureIgnored = 2147747815U,
			// Token: 0x040001AB RID: 427
			SmtpSendNewProxySession = 1074005992U,
			// Token: 0x040001AC RID: 428
			SmtpSendProxyEhloOptionsDoNotMatch = 3221489641U,
			// Token: 0x040001AD RID: 429
			SmtpSendInboundProxyEhloOptionsDoNotMatch,
			// Token: 0x040001AE RID: 430
			SmtpSendInboundProxyRecipientLimitsDoNotMatch,
			// Token: 0x040001AF RID: 431
			SmtpSendPoisonForRemoteThresholdExceeded = 2147747820U,
			// Token: 0x040001B0 RID: 432
			SmtpSendInboundProxyNonCriticalEhloOptionsDoNotMatch,
			// Token: 0x040001B1 RID: 433
			SmtpSendProxyEhloOptionsDoNotMatchButStillContinueProxying = 3221489646U,
			// Token: 0x040001B2 RID: 434
			InboundProxyDestinationsTrackerDiagnosticInfo = 1074005999U,
			// Token: 0x040001B3 RID: 435
			InboundProxyDestinationsTrackerReject = 3221489648U,
			// Token: 0x040001B4 RID: 436
			InboundProxyDestinationsTrackerNearThreshold = 2147747825U,
			// Token: 0x040001B5 RID: 437
			InboundProxyAccountForestsTrackerReject = 3221489650U,
			// Token: 0x040001B6 RID: 438
			InboundProxyAccountForestsTrackerNearThreshold = 2147747827U,
			// Token: 0x040001B7 RID: 439
			DsnUnableToReadQuarantineConfig = 3221490620U,
			// Token: 0x040001B8 RID: 440
			DsnUnableToReadSystemMessageConfig,
			// Token: 0x040001B9 RID: 441
			DsnDiskFull,
			// Token: 0x040001BA RID: 442
			XProxyToCommandInvalidEncodedCertificateSubject,
			// Token: 0x040001BB RID: 443
			RoutingPerfCountersLoadFailure = 3221492617U,
			// Token: 0x040001BC RID: 444
			RoutingAdUnavailable,
			// Token: 0x040001BD RID: 445
			RoutingWillRetryLoad,
			// Token: 0x040001BE RID: 446
			RoutingNoServerFqdn,
			// Token: 0x040001BF RID: 447
			RoutingNoServerAdSite,
			// Token: 0x040001C0 RID: 448
			RoutingNoOwningServerForMdb = 2147750798U,
			// Token: 0x040001C1 RID: 449
			RoutingNoRouteToAdSite = 3221492623U,
			// Token: 0x040001C2 RID: 450
			RoutingNoRouteToOwningServer,
			// Token: 0x040001C3 RID: 451
			RoutingNoPfTreeMdbRoute = 2147750801U,
			// Token: 0x040001C4 RID: 452
			RoutingNoPfTreeRoute = 3221492626U,
			// Token: 0x040001C5 RID: 453
			RoutingNoSourceRgForRgConnector,
			// Token: 0x040001C6 RID: 454
			RoutingNoTargetRgForRgConnector,
			// Token: 0x040001C7 RID: 455
			RoutingNoServerRg,
			// Token: 0x040001C8 RID: 456
			RoutingNoSourceBhServers,
			// Token: 0x040001C9 RID: 457
			RoutingNoSourceBhRoute,
			// Token: 0x040001CA RID: 458
			RoutingNoRouteToConnector,
			// Token: 0x040001CB RID: 459
			RoutingNoTargetBhServer,
			// Token: 0x040001CC RID: 460
			RoutingNoTargetBhServers,
			// Token: 0x040001CD RID: 461
			RoutingInvalidSmarthosts,
			// Token: 0x040001CE RID: 462
			RoutingTransientConfigError = 3221492639U,
			// Token: 0x040001CF RID: 463
			RoutingMaxConfigLoadRetriesReached,
			// Token: 0x040001D0 RID: 464
			RoutingNoSourceRgForNonRgConnector = 3221492643U,
			// Token: 0x040001D1 RID: 465
			RoutingLocalConnectorWithConnectedDomains,
			// Token: 0x040001D2 RID: 466
			RoutingNoConnectedRg,
			// Token: 0x040001D3 RID: 467
			RoutingTableLogCreationFailure,
			// Token: 0x040001D4 RID: 468
			RoutingTableLogDeletionFailure,
			// Token: 0x040001D5 RID: 469
			RoutingNoDeliveryGroupForDatabase = 2147750827U,
			// Token: 0x040001D6 RID: 470
			RoutingNoRoutingGroupForDatabase,
			// Token: 0x040001D7 RID: 471
			RoutingNoDagForDatabase,
			// Token: 0x040001D8 RID: 472
			RoutingNoDestinationForDatabase = 3221492654U,
			// Token: 0x040001D9 RID: 473
			RoutingNoHubServersSelectedForDatabases,
			// Token: 0x040001DA RID: 474
			RoutingNoHubServersSelectedForTenant,
			// Token: 0x040001DB RID: 475
			InactiveDagsExcludedFromDagSelector = 1074009009U,
			// Token: 0x040001DC RID: 476
			DagSelectorDiagnosticInfo,
			// Token: 0x040001DD RID: 477
			TenantDagQuotaDiagnosticInfo,
			// Token: 0x040001DE RID: 478
			RoutingTableDatabaseFullReload,
			// Token: 0x040001DF RID: 479
			RoutingDictionaryInsertFailure = 3221492661U,
			// Token: 0x040001E0 RID: 480
			PipelineTracingActive = 2147751292U,
			// Token: 0x040001E1 RID: 481
			PerfCountersLoadFailure = 3221493117U,
			// Token: 0x040001E2 RID: 482
			ExternalServersLatencyTimeNotSync = 2147751294U,
			// Token: 0x040001E3 RID: 483
			MultiplePreProcessLatencies = 3221493119U,
			// Token: 0x040001E4 RID: 484
			NullLatencyTreeLeaf,
			// Token: 0x040001E5 RID: 485
			MultipleCompletions,
			// Token: 0x040001E6 RID: 486
			StopService = 1074010969U,
			// Token: 0x040001E7 RID: 487
			DatabaseInUse = 3221494618U,
			// Token: 0x040001E8 RID: 488
			ActivationTiming = 2147752796U,
			// Token: 0x040001E9 RID: 489
			NewDatabaseCreated = 1074010973U,
			// Token: 0x040001EA RID: 490
			DatabaseSchemaNotSupported = 3221494624U,
			// Token: 0x040001EB RID: 491
			RetrieveServiceState = 1074010977U,
			// Token: 0x040001EC RID: 492
			ActivationSlow = 2147752802U,
			// Token: 0x040001ED RID: 493
			HubTransportServiceStateChanged,
			// Token: 0x040001EE RID: 494
			FrontendTransportServiceStateChanged,
			// Token: 0x040001EF RID: 495
			FrontendTransportRestartOnServiceStateChange,
			// Token: 0x040001F0 RID: 496
			EdgeTransportServiceStateChanged,
			// Token: 0x040001F1 RID: 497
			FrontendTransportServiceInitializationFailure = 3221494631U,
			// Token: 0x040001F2 RID: 498
			EdgeTransportInitializationFailure,
			// Token: 0x040001F3 RID: 499
			MSExchangeTransportInitializationFailure,
			// Token: 0x040001F4 RID: 500
			ResubmitDueToConfigUpdate = 1074011970U,
			// Token: 0x040001F5 RID: 501
			ResubmitDueToInactivityTimeout,
			// Token: 0x040001F6 RID: 502
			NonSmtpGWBadDropDirectory = 3221495620U,
			// Token: 0x040001F7 RID: 503
			NonSmtpGWQuotaExceeded,
			// Token: 0x040001F8 RID: 504
			QueuingStatusAtShutdown = 1074011974U,
			// Token: 0x040001F9 RID: 505
			NonSmtpGWPathTooLongException = 3221495623U,
			// Token: 0x040001FA RID: 506
			NonSmtpGWNoDropDirectory,
			// Token: 0x040001FB RID: 507
			NonSmtpGWUnauthorizedAccess,
			// Token: 0x040001FC RID: 508
			RetryDeliveryIfRejected,
			// Token: 0x040001FD RID: 509
			OnOpenConnectionAgentException,
			// Token: 0x040001FE RID: 510
			OnDeliverMailItemAgentException,
			// Token: 0x040001FF RID: 511
			OnCloseConnectionAgentException,
			// Token: 0x04000200 RID: 512
			ResubmitDueToUnavailabilityOfSameVersionHubs = 1074011982U,
			// Token: 0x04000201 RID: 513
			RedirectMessageStarted,
			// Token: 0x04000202 RID: 514
			QueueViewerExceptionDuringAsyncRetryQueue = 3221495632U,
			// Token: 0x04000203 RID: 515
			ResubmitDueToOutboundConnectorChange = 1074011985U,
			// Token: 0x04000204 RID: 516
			RetryQueueOutboundConnectorLookupFailed = 3221495634U,
			// Token: 0x04000205 RID: 517
			DirectoryDoesNotExist = 1074012969U,
			// Token: 0x04000206 RID: 518
			NoDirectoryPermission = 3221496618U,
			// Token: 0x04000207 RID: 519
			ReadOnlyFileFound = 2147754795U,
			// Token: 0x04000208 RID: 520
			CannotDeleteFile = 3221496620U,
			// Token: 0x04000209 RID: 521
			PickupFailedDueToStorageErrors,
			// Token: 0x0400020A RID: 522
			FailedToCreatePickupDirectory,
			// Token: 0x0400020B RID: 523
			NoPermissionToRenamePickupFile,
			// Token: 0x0400020C RID: 524
			AccessErrorModifyingPickupRegkey = 3221496625U,
			// Token: 0x0400020D RID: 525
			PickupIsBadmailingFile,
			// Token: 0x0400020E RID: 526
			PickupFileEncrypted = 3221496628U,
			// Token: 0x0400020F RID: 527
			OnSubmittedMessageAgentException = 3221496817U,
			// Token: 0x04000210 RID: 528
			OnRoutedMessageAgentException,
			// Token: 0x04000211 RID: 529
			TransportConfigContainerNotFound = 3221496824U,
			// Token: 0x04000212 RID: 530
			ResolverPerfCountersLoadFailure = 3221496828U,
			// Token: 0x04000213 RID: 531
			RetryCategorizationIfFailed,
			// Token: 0x04000214 RID: 532
			AmbiguousSender = 2147755006U,
			// Token: 0x04000215 RID: 533
			OnCategorizedMessageAgentException = 3221496831U,
			// Token: 0x04000216 RID: 534
			OnResolvedMessageAgentException,
			// Token: 0x04000217 RID: 535
			AmbiguousRecipient,
			// Token: 0x04000218 RID: 536
			NDRForUnrestrictedLargeDL,
			// Token: 0x04000219 RID: 537
			CategorizerErrorRetrievingTenantOverride,
			// Token: 0x0400021A RID: 538
			MessageCountEnqueuedToPoisonQueue = 2147755793U,
			// Token: 0x0400021B RID: 539
			PoisonCountUpdated = 1074013970U,
			// Token: 0x0400021C RID: 540
			PoisonMessageCrash = 3221497619U,
			// Token: 0x0400021D RID: 541
			DeletedPoisonPickupFile = 2147755796U,
			// Token: 0x0400021E RID: 542
			PoisonMessageLoadFailedRegistryAccessDenied = 3221497621U,
			// Token: 0x0400021F RID: 543
			PoisonMessageSaveFailedRegistryAccessDenied,
			// Token: 0x04000220 RID: 544
			PoisonMessageMarkFailedRegistryAccessDenied,
			// Token: 0x04000221 RID: 545
			PoisonMessagePruneFailedRegistryAccessDenied,
			// Token: 0x04000222 RID: 546
			MessageSecurityTLSCertificateValidationFailure = 3221498621U,
			// Token: 0x04000223 RID: 547
			TlsDomainCertificateValidationFailure = 3221498627U,
			// Token: 0x04000224 RID: 548
			MessageNotAuthenticatedTlsNotStarted,
			// Token: 0x04000225 RID: 549
			MessageToSecureDomainFailedDueToTlsNegotiationFailure,
			// Token: 0x04000226 RID: 550
			MessageToSecureDomainFailedBecauseTlsNotOffered,
			// Token: 0x04000227 RID: 551
			TlsDomainClientCertificateSubjectMismatch,
			// Token: 0x04000228 RID: 552
			TlsDomainServerCertificateSubjectMismatch,
			// Token: 0x04000229 RID: 553
			MessageNotAuthenticatedNoClientCertificate,
			// Token: 0x0400022A RID: 554
			MessageNotAuthenticatedTlsNotAdvertised,
			// Token: 0x0400022B RID: 555
			MessageToSecureDomainFailedBecauseTlsNegotiationFailed,
			// Token: 0x0400022C RID: 556
			TlsDomainServerCertificateValidationFailure,
			// Token: 0x0400022D RID: 557
			TlsDomainSecureDisabled,
			// Token: 0x0400022E RID: 558
			TlsDomainCapabilitiesCertificateValidationFailure,
			// Token: 0x0400022F RID: 559
			SessionFailedBecauseXOorgNotOffered,
			// Token: 0x04000230 RID: 560
			SubjectAlternativeNameLimitExceeded = 2147756816U,
			// Token: 0x04000231 RID: 561
			CertificateRevocationListCheckTrasientFailureTreatedAsSuccess = 3221498642U,
			// Token: 0x04000232 RID: 562
			FailedToFlushTicketCacheOnInitialize = 3221499617U,
			// Token: 0x04000233 RID: 563
			ConfigUpdateOccurred = 1074015970U,
			// Token: 0x04000234 RID: 564
			InvalidServerRole = 3221499619U,
			// Token: 0x04000235 RID: 565
			ReadConfigReceiveConnectorFailed = 3221499624U,
			// Token: 0x04000236 RID: 566
			ReadConfigReceiveConnectorUnavail,
			// Token: 0x04000237 RID: 567
			ADRecipientCachePerfCountersLoadFailure,
			// Token: 0x04000238 RID: 568
			SpnRegisterFailure,
			// Token: 0x04000239 RID: 569
			InternalTransportCertificateMissingInAD,
			// Token: 0x0400023A RID: 570
			CannotLoadInternalTransportCertificateFromStore,
			// Token: 0x0400023B RID: 571
			CannotLoadSTARTTLSCertificateFromStore,
			// Token: 0x0400023C RID: 572
			InternalTransportCertificateExpired,
			// Token: 0x0400023D RID: 573
			STARTTLSCertificateExpired,
			// Token: 0x0400023E RID: 574
			InternalTransportCertificateExpiresSoon,
			// Token: 0x0400023F RID: 575
			STARTTLSCertificateExpiresSoon,
			// Token: 0x04000240 RID: 576
			RemoteInternalTransportCertificateExpired,
			// Token: 0x04000241 RID: 577
			RemoteSTARTTLSCertificateExpired,
			// Token: 0x04000242 RID: 578
			ReadConfigReceiveConnectorIgnored = 2147757813U,
			// Token: 0x04000243 RID: 579
			InternalTransportCertificateCorruptedInAD = 3221499638U,
			// Token: 0x04000244 RID: 580
			CannotLoadInternalTransportCertificateFallbackServerFQDN = 2147757815U,
			// Token: 0x04000245 RID: 581
			CannotLoadIntTransportCertificateFallbackEphemeralCertificate,
			// Token: 0x04000246 RID: 582
			DisconnectingPerformanceCounters,
			// Token: 0x04000247 RID: 583
			FailedToDisconnectPerformanceCounters,
			// Token: 0x04000248 RID: 584
			ProcessHoldingPerformanceCounter = 2147757820U,
			// Token: 0x04000249 RID: 585
			ServerLivingForConsiderableTime = 1074015997U,
			// Token: 0x0400024A RID: 586
			KillOrphanedWorker = 2147757822U,
			// Token: 0x0400024B RID: 587
			AnotherServiceRunning,
			// Token: 0x0400024C RID: 588
			KillOrphanedWorkerFailed = 3221499648U,
			// Token: 0x0400024D RID: 589
			FailedToRegisterForDeletedObjectsNotification,
			// Token: 0x0400024E RID: 590
			SystemLowOnMemory = 2147757826U,
			// Token: 0x0400024F RID: 591
			Exch50OrgNotFound = 3221500618U,
			// Token: 0x04000250 RID: 592
			ProcessNotResponding = 3221501617U,
			// Token: 0x04000251 RID: 593
			AppConfigLoadFailed = 3221501620U,
			// Token: 0x04000252 RID: 594
			ResourceUtilizationUp = 2147760796U,
			// Token: 0x04000253 RID: 595
			ResourceUtilizationDown = 1074018973U,
			// Token: 0x04000254 RID: 596
			DiskSpaceLow = 3221502622U,
			// Token: 0x04000255 RID: 597
			PrivateBytesHigh,
			// Token: 0x04000256 RID: 598
			ComponentFailedTransportServerUpdate = 3221503620U,
			// Token: 0x04000257 RID: 599
			MessageTrackingLogPathIsNull = 2147761803U,
			// Token: 0x04000258 RID: 600
			ReceiveProtocolLogPathIsNull,
			// Token: 0x04000259 RID: 601
			SendProtocolLogPathIsNull,
			// Token: 0x0400025A RID: 602
			ReceiveProtocolLogPathIsNullUsingOld,
			// Token: 0x0400025B RID: 603
			SendProtocolLogPathIsNullUsingOld,
			// Token: 0x0400025C RID: 604
			DefaultAuthoritativeDomainInvalid = 3221503632U,
			// Token: 0x0400025D RID: 605
			ActivationFailed,
			// Token: 0x0400025E RID: 606
			ConfigurationLoaderExternalError = 2147761810U,
			// Token: 0x0400025F RID: 607
			ConfigurationLoaderException,
			// Token: 0x04000260 RID: 608
			InvalidAcceptedDomain = 3221503637U,
			// Token: 0x04000261 RID: 609
			ConfigurationLoaderSuccessfulUpdate = 1074019990U,
			// Token: 0x04000262 RID: 610
			CannotStartAgents = 3221503639U,
			// Token: 0x04000263 RID: 611
			RejectedAcceptedDomain,
			// Token: 0x04000264 RID: 612
			InvalidAdapterGuid,
			// Token: 0x04000265 RID: 613
			NetworkAdapterIPQueryFailed,
			// Token: 0x04000266 RID: 614
			ConfigurationLoaderNoADNotifications = 2147761819U,
			// Token: 0x04000267 RID: 615
			ConfigurationLoaderSuccessfulForcedUpdate = 1074019996U,
			// Token: 0x04000268 RID: 616
			HeartbeatDestinationConfigChanged = 2147761821U,
			// Token: 0x04000269 RID: 617
			AgentErrorHandlingOverrideConfigError = 3221503646U,
			// Token: 0x0400026A RID: 618
			SchemaTypeMismatch = 3221504617U,
			// Token: 0x0400026B RID: 619
			SchemaRequiredColumnNotFound,
			// Token: 0x0400026C RID: 620
			JetCorruptionError,
			// Token: 0x0400026D RID: 621
			JetOutOfSpaceError,
			// Token: 0x0400026E RID: 622
			JetLogFileError,
			// Token: 0x0400026F RID: 623
			JetPathError,
			// Token: 0x04000270 RID: 624
			JetMismatchError,
			// Token: 0x04000271 RID: 625
			StartScanForMessages = 1074020976U,
			// Token: 0x04000272 RID: 626
			StopScanForMessages,
			// Token: 0x04000273 RID: 627
			EndScanForMessages,
			// Token: 0x04000274 RID: 628
			JetCheckpointFileError = 3221504627U,
			// Token: 0x04000275 RID: 629
			JetInitInstanceOutOfMemory,
			// Token: 0x04000276 RID: 630
			JetInstanceNameInUse,
			// Token: 0x04000277 RID: 631
			JetDatabaseNotFound,
			// Token: 0x04000278 RID: 632
			JetDatabaseLogSetMismatch,
			// Token: 0x04000279 RID: 633
			JetFragmentationError,
			// Token: 0x0400027A RID: 634
			JetQuotaExceededError,
			// Token: 0x0400027B RID: 635
			JetInsufficientResourcesError,
			// Token: 0x0400027C RID: 636
			JetIOError,
			// Token: 0x0400027D RID: 637
			JetOperationError,
			// Token: 0x0400027E RID: 638
			JetTableNotFound,
			// Token: 0x0400027F RID: 639
			JetFileNotFound,
			// Token: 0x04000280 RID: 640
			JetFileReadOnly,
			// Token: 0x04000281 RID: 641
			JetVersionStoreOutOfMemoryError,
			// Token: 0x04000282 RID: 642
			LastMessagesLoadedByBootScanner = 1074020993U,
			// Token: 0x04000283 RID: 643
			DatabaseDriveIsNotAccessible = 3221504642U,
			// Token: 0x04000284 RID: 644
			ColumnTooBigException,
			// Token: 0x04000285 RID: 645
			AgentDidNotCloseMimeStream = 3221505617U,
			// Token: 0x04000286 RID: 646
			RecipientValidationCacheLoaded = 1074022969U,
			// Token: 0x04000287 RID: 647
			RecipientGroupCacheLoaded,
			// Token: 0x04000288 RID: 648
			DirectoryUnavailableLoadingGroup = 2147764798U,
			// Token: 0x04000289 RID: 649
			DirectoryUnavailableLoadingValidationCache,
			// Token: 0x0400028A RID: 650
			RecipientHasDataValidationException = 3221506624U,
			// Token: 0x0400028B RID: 651
			ORARMessageSubmitted = 1074023969U,
			// Token: 0x0400028C RID: 652
			FailParseOrarBlob = 2147765794U,
			// Token: 0x0400028D RID: 653
			FailGetRoutingAddress,
			// Token: 0x0400028E RID: 654
			DatabaseRecoveryActionNone = 3221504717U,
			// Token: 0x0400028F RID: 655
			DatabaseRecoveryActionMove = 2147762894U,
			// Token: 0x04000290 RID: 656
			DatabaseRecoveryActionDelete = 1074021071U,
			// Token: 0x04000291 RID: 657
			DatabaseRecoveryActionFailed = 3221504720U,
			// Token: 0x04000292 RID: 658
			DatabaseRecoveryActionFailedRegistryAccessDenied,
			// Token: 0x04000293 RID: 659
			DataBaseCorruptionDetected = 1074021074U,
			// Token: 0x04000294 RID: 660
			DatabaseErrorDetected,
			// Token: 0x04000295 RID: 661
			TableLockedException = 3221504724U,
			// Token: 0x04000296 RID: 662
			ShadowRedundancyMessagesResubmitted = 1074025969U,
			// Token: 0x04000297 RID: 663
			ShadowRedundancyMessageResubmitSuppressed,
			// Token: 0x04000298 RID: 664
			ShadowRedundancyPrimaryServerDatabaseStateChanged = 3221509619U,
			// Token: 0x04000299 RID: 665
			ShadowRedundancyPrimaryServerHeartbeatFailed = 2147767796U,
			// Token: 0x0400029A RID: 666
			ShadowRedundancyMessageDeferredDueToShadowFailure = 3221509622U,
			// Token: 0x0400029B RID: 667
			ShadowRedundancyForcedHeartbeatReset = 2147767799U,
			// Token: 0x0400029C RID: 668
			ModeratedTransportNoArbitrationMailbox = 3221510617U,
			// Token: 0x0400029D RID: 669
			RecipientStampedWithDeletedArbitrationMailbox,
			// Token: 0x0400029E RID: 670
			RemovedMessageRepositoryRequest = 1074027972U,
			// Token: 0x0400029F RID: 671
			ModifiedMessageRepositoryRequest,
			// Token: 0x040002A0 RID: 672
			RegisterRpcServerFailure = 1074027975U,
			// Token: 0x040002A1 RID: 673
			MessageAttributionFailed = 3221511626U,
			// Token: 0x040002A2 RID: 674
			TransportWlmLogPathIsNull,
			// Token: 0x040002A3 RID: 675
			ResubmitRequestExpired = 1074027980U,
			// Token: 0x040002A4 RID: 676
			DuplicateResubmitRequest = 2147769805U,
			// Token: 0x040002A5 RID: 677
			MaxRunningResubmitRequest,
			// Token: 0x040002A6 RID: 678
			MaxRecentResubmitRequest,
			// Token: 0x040002A7 RID: 679
			InterceptorAgentConfigurationLoadingError = 3221512617U,
			// Token: 0x040002A8 RID: 680
			InterceptorAgentConfigurationReplaced = 287146U,
			// Token: 0x040002A9 RID: 681
			InterceptorAgentAccessDenied = 3221512619U,
			// Token: 0x040002AA RID: 682
			InterceptorRuleNearingExpiration = 2147770796U,
			// Token: 0x040002AB RID: 683
			QueueQuotaComponentLogPathIsNull = 3221512622U,
			// Token: 0x040002AC RID: 684
			NotEnoughMemoryToStartService,
			// Token: 0x040002AD RID: 685
			FlowControlLogPathIsNull,
			// Token: 0x040002AE RID: 686
			FilePathOnLockedVolume,
			// Token: 0x040002AF RID: 687
			BitlockerQueryFailed
		}
	}
}
