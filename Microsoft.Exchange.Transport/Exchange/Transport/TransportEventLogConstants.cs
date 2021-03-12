using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000535 RID: 1333
	internal static class TransportEventLogConstants
	{
		// Token: 0x04001FB1 RID: 8113
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BindingIPv6ButDisabled = new ExEventLog.EventTuple(3221488616U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FB2 RID: 8114
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AddressInUse = new ExEventLog.EventTuple(3221488617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FB3 RID: 8115
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfiguredConnectors = new ExEventLog.EventTuple(1074004970U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FB4 RID: 8116
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveMaxConnectionReached = new ExEventLog.EventTuple(2147746801U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FB5 RID: 8117
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveMessageRejected = new ExEventLog.EventTuple(1074004978U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FB6 RID: 8118
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveAuthenticationFailedTooManyErrors = new ExEventLog.EventTuple(2147746804U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FB7 RID: 8119
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveRejectDueToStorageError = new ExEventLog.EventTuple(3221488629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FB8 RID: 8120
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveAuthorizationSubmitRejected = new ExEventLog.EventTuple(2147746810U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FB9 RID: 8121
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveAuthorizationRejected = new ExEventLog.EventTuple(2147746812U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FBA RID: 8122
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveMaxConnectionPerSourceReached = new ExEventLog.EventTuple(2147746813U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FBB RID: 8123
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_InternalSMTPServerListEmpty = new ExEventLog.EventTuple(2147746814U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04001FBC RID: 8124
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSubmitDenied = new ExEventLog.EventTuple(1074004991U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FBD RID: 8125
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSendAsDeniedTempAuthFailure = new ExEventLog.EventTuple(1074004992U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FBE RID: 8126
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSendAsDeniedSenderAddressDataInvalid = new ExEventLog.EventTuple(3221488641U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FBF RID: 8127
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSendAsDenied = new ExEventLog.EventTuple(1074004994U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FC0 RID: 8128
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSendOnBehalfOfDeniedTempAuthFailure = new ExEventLog.EventTuple(1074004995U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FC1 RID: 8129
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSendOnBehalfOfDeniedFromAddressDataInvalid = new ExEventLog.EventTuple(3221488644U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FC2 RID: 8130
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveSendOnBehalfOfDenied = new ExEventLog.EventTuple(1074004997U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FC3 RID: 8131
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveCouldNotDetermineUserNameOrSid = new ExEventLog.EventTuple(3221488646U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FC4 RID: 8132
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveMessageRateLimitExceeded = new ExEventLog.EventTuple(1074004999U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FC5 RID: 8133
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveTLSRequiredFailed = new ExEventLog.EventTuple(3221488648U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FC6 RID: 8134
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveCatchAll = new ExEventLog.EventTuple(3221488649U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FC7 RID: 8135
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveAuthenticationInitializationFailed = new ExEventLog.EventTuple(3221488650U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FC8 RID: 8136
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveAuthenticationFailed = new ExEventLog.EventTuple(2147746827U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FC9 RID: 8137
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveDirectTrustFailed = new ExEventLog.EventTuple(3221488652U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FCA RID: 8138
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveActiveManagerFailure = new ExEventLog.EventTuple(3221488653U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FCB RID: 8139
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveAvailabilityCounterFailure = new ExEventLog.EventTuple(3221488654U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FCC RID: 8140
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProhibitSendQuotaDeniedTempAuthFailure = new ExEventLog.EventTuple(1074005007U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FCD RID: 8141
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveConnectorAvailabilityLow = new ExEventLog.EventTuple(3221488656U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FCE RID: 8142
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveConnectorAvailabilityNormal = new ExEventLog.EventTuple(1074005009U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FCF RID: 8143
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProxyMserveLookupFailed = new ExEventLog.EventTuple(3221488658U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FD0 RID: 8144
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProxyInvalidPartnerId = new ExEventLog.EventTuple(3221488659U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FD1 RID: 8145
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProxyDnsLookupFailed = new ExEventLog.EventTuple(3221488660U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FD2 RID: 8146
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProxyCatchAll = new ExEventLog.EventTuple(3221488661U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FD3 RID: 8147
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProxyCounterFailure = new ExEventLog.EventTuple(3221488663U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FD4 RID: 8148
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveTooManyProxySessionFailures = new ExEventLog.EventTuple(3221488664U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FD5 RID: 8149
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveNoDestinationToProxyTo = new ExEventLog.EventTuple(3221488665U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FD6 RID: 8150
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpReceiveProcessingBlobFailed = new ExEventLog.EventTuple(3221488666U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FD7 RID: 8151
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendDnsConnectionFailure = new ExEventLog.EventTuple(2147747792U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FD8 RID: 8152
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendConnectionError = new ExEventLog.EventTuple(2147747794U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FD9 RID: 8153
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendAuthenticationFailed = new ExEventLog.EventTuple(2147747795U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FDA RID: 8154
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendAckMessage = new ExEventLog.EventTuple(1074005972U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FDB RID: 8155
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendAckConnection = new ExEventLog.EventTuple(1074005973U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FDC RID: 8156
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendRemoteDisconnected = new ExEventLog.EventTuple(2147747798U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FDD RID: 8157
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendNewSession = new ExEventLog.EventTuple(1074005975U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FDE RID: 8158
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExchangeAuthHashNotSupported = new ExEventLog.EventTuple(3221489627U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FDF RID: 8159
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SendConnectorInvalidSourceIPAddress = new ExEventLog.EventTuple(3221489630U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FE0 RID: 8160
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendTLSRequiredFailed = new ExEventLog.EventTuple(3221489631U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FE1 RID: 8161
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendAuthenticationInitializationFailed = new ExEventLog.EventTuple(3221489632U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FE2 RID: 8162
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendOutboundAuthenticationFailed = new ExEventLog.EventTuple(3221489633U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FE3 RID: 8163
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendDirectTrustFailed = new ExEventLog.EventTuple(3221489634U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FE4 RID: 8164
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendUnableToTransmitOrar = new ExEventLog.EventTuple(3221489635U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FE5 RID: 8165
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendUnableToTransmitLongOrar = new ExEventLog.EventTuple(3221489636U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FE6 RID: 8166
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendUnableToTransmitRDst = new ExEventLog.EventTuple(3221489637U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FE7 RID: 8167
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendOutboundAtTLSAuthLevelFailed = new ExEventLog.EventTuple(3221489638U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FE8 RID: 8168
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendAuthenticationFailureIgnored = new ExEventLog.EventTuple(2147747815U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FE9 RID: 8169
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendNewProxySession = new ExEventLog.EventTuple(1074005992U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FEA RID: 8170
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendProxyEhloOptionsDoNotMatch = new ExEventLog.EventTuple(3221489641U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FEB RID: 8171
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendInboundProxyEhloOptionsDoNotMatch = new ExEventLog.EventTuple(3221489642U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FEC RID: 8172
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendInboundProxyRecipientLimitsDoNotMatch = new ExEventLog.EventTuple(3221489643U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FED RID: 8173
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendPoisonForRemoteThresholdExceeded = new ExEventLog.EventTuple(2147747820U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FEE RID: 8174
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendInboundProxyNonCriticalEhloOptionsDoNotMatch = new ExEventLog.EventTuple(2147747821U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FEF RID: 8175
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SmtpSendProxyEhloOptionsDoNotMatchButStillContinueProxying = new ExEventLog.EventTuple(3221489646U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FF0 RID: 8176
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InboundProxyDestinationsTrackerDiagnosticInfo = new ExEventLog.EventTuple(1074005999U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FF1 RID: 8177
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InboundProxyDestinationsTrackerReject = new ExEventLog.EventTuple(3221489648U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FF2 RID: 8178
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InboundProxyDestinationsTrackerNearThreshold = new ExEventLog.EventTuple(2147747825U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FF3 RID: 8179
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InboundProxyAccountForestsTrackerReject = new ExEventLog.EventTuple(3221489650U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FF4 RID: 8180
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InboundProxyAccountForestsTrackerNearThreshold = new ExEventLog.EventTuple(2147747827U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FF5 RID: 8181
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DsnUnableToReadQuarantineConfig = new ExEventLog.EventTuple(3221490620U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FF6 RID: 8182
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DsnUnableToReadSystemMessageConfig = new ExEventLog.EventTuple(3221490621U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FF7 RID: 8183
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DsnDiskFull = new ExEventLog.EventTuple(3221490622U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FF8 RID: 8184
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_XProxyToCommandInvalidEncodedCertificateSubject = new ExEventLog.EventTuple(3221490623U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FF9 RID: 8185
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingPerfCountersLoadFailure = new ExEventLog.EventTuple(3221492617U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FFA RID: 8186
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingAdUnavailable = new ExEventLog.EventTuple(3221492618U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FFB RID: 8187
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingWillRetryLoad = new ExEventLog.EventTuple(3221492619U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FFC RID: 8188
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoServerFqdn = new ExEventLog.EventTuple(3221492620U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FFD RID: 8189
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoServerAdSite = new ExEventLog.EventTuple(3221492621U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001FFE RID: 8190
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoOwningServerForMdb = new ExEventLog.EventTuple(2147750798U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001FFF RID: 8191
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoRouteToAdSite = new ExEventLog.EventTuple(3221492623U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002000 RID: 8192
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoRouteToOwningServer = new ExEventLog.EventTuple(3221492624U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002001 RID: 8193
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoPfTreeMdbRoute = new ExEventLog.EventTuple(2147750801U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002002 RID: 8194
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoPfTreeRoute = new ExEventLog.EventTuple(3221492626U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002003 RID: 8195
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoSourceRgForRgConnector = new ExEventLog.EventTuple(3221492627U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002004 RID: 8196
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoTargetRgForRgConnector = new ExEventLog.EventTuple(3221492628U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002005 RID: 8197
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoServerRg = new ExEventLog.EventTuple(3221492629U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002006 RID: 8198
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoSourceBhServers = new ExEventLog.EventTuple(3221492630U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002007 RID: 8199
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoSourceBhRoute = new ExEventLog.EventTuple(3221492631U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002008 RID: 8200
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoRouteToConnector = new ExEventLog.EventTuple(3221492632U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002009 RID: 8201
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoTargetBhServer = new ExEventLog.EventTuple(3221492633U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400200A RID: 8202
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoTargetBhServers = new ExEventLog.EventTuple(3221492634U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400200B RID: 8203
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingInvalidSmarthosts = new ExEventLog.EventTuple(3221492635U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400200C RID: 8204
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingTransientConfigError = new ExEventLog.EventTuple(3221492639U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400200D RID: 8205
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingMaxConfigLoadRetriesReached = new ExEventLog.EventTuple(3221492640U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400200E RID: 8206
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoSourceRgForNonRgConnector = new ExEventLog.EventTuple(3221492643U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400200F RID: 8207
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingLocalConnectorWithConnectedDomains = new ExEventLog.EventTuple(3221492644U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002010 RID: 8208
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoConnectedRg = new ExEventLog.EventTuple(3221492645U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002011 RID: 8209
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingTableLogCreationFailure = new ExEventLog.EventTuple(3221492646U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002012 RID: 8210
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingTableLogDeletionFailure = new ExEventLog.EventTuple(3221492647U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002013 RID: 8211
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoDeliveryGroupForDatabase = new ExEventLog.EventTuple(2147750827U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002014 RID: 8212
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoRoutingGroupForDatabase = new ExEventLog.EventTuple(2147750828U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002015 RID: 8213
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoDagForDatabase = new ExEventLog.EventTuple(2147750829U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002016 RID: 8214
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoDestinationForDatabase = new ExEventLog.EventTuple(3221492654U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002017 RID: 8215
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoHubServersSelectedForDatabases = new ExEventLog.EventTuple(3221492655U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002018 RID: 8216
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingNoHubServersSelectedForTenant = new ExEventLog.EventTuple(3221492656U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002019 RID: 8217
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InactiveDagsExcludedFromDagSelector = new ExEventLog.EventTuple(1074009009U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400201A RID: 8218
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DagSelectorDiagnosticInfo = new ExEventLog.EventTuple(1074009010U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400201B RID: 8219
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TenantDagQuotaDiagnosticInfo = new ExEventLog.EventTuple(1074009011U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400201C RID: 8220
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingTableDatabaseFullReload = new ExEventLog.EventTuple(1074009012U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400201D RID: 8221
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RoutingDictionaryInsertFailure = new ExEventLog.EventTuple(3221492661U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400201E RID: 8222
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PipelineTracingActive = new ExEventLog.EventTuple(2147751292U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400201F RID: 8223
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PerfCountersLoadFailure = new ExEventLog.EventTuple(3221493117U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002020 RID: 8224
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExternalServersLatencyTimeNotSync = new ExEventLog.EventTuple(2147751294U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002021 RID: 8225
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MultiplePreProcessLatencies = new ExEventLog.EventTuple(3221493119U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002022 RID: 8226
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NullLatencyTreeLeaf = new ExEventLog.EventTuple(3221493120U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002023 RID: 8227
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MultipleCompletions = new ExEventLog.EventTuple(3221493121U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002024 RID: 8228
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StopService = new ExEventLog.EventTuple(1074010969U, 6, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002025 RID: 8229
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseInUse = new ExEventLog.EventTuple(3221494618U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002026 RID: 8230
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActivationTiming = new ExEventLog.EventTuple(2147752796U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002027 RID: 8231
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NewDatabaseCreated = new ExEventLog.EventTuple(1074010973U, 6, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002028 RID: 8232
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseSchemaNotSupported = new ExEventLog.EventTuple(3221494624U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002029 RID: 8233
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RetrieveServiceState = new ExEventLog.EventTuple(1074010977U, 6, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400202A RID: 8234
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActivationSlow = new ExEventLog.EventTuple(2147752802U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400202B RID: 8235
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HubTransportServiceStateChanged = new ExEventLog.EventTuple(2147752803U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400202C RID: 8236
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FrontendTransportServiceStateChanged = new ExEventLog.EventTuple(2147752804U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400202D RID: 8237
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FrontendTransportRestartOnServiceStateChange = new ExEventLog.EventTuple(2147752805U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400202E RID: 8238
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EdgeTransportServiceStateChanged = new ExEventLog.EventTuple(2147752806U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400202F RID: 8239
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FrontendTransportServiceInitializationFailure = new ExEventLog.EventTuple(3221494631U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002030 RID: 8240
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EdgeTransportInitializationFailure = new ExEventLog.EventTuple(3221494632U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002031 RID: 8241
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MSExchangeTransportInitializationFailure = new ExEventLog.EventTuple(3221494633U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002032 RID: 8242
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResubmitDueToConfigUpdate = new ExEventLog.EventTuple(1074011970U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002033 RID: 8243
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResubmitDueToInactivityTimeout = new ExEventLog.EventTuple(1074011971U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002034 RID: 8244
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NonSmtpGWBadDropDirectory = new ExEventLog.EventTuple(3221495620U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002035 RID: 8245
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NonSmtpGWQuotaExceeded = new ExEventLog.EventTuple(3221495621U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002036 RID: 8246
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_QueuingStatusAtShutdown = new ExEventLog.EventTuple(1074011974U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002037 RID: 8247
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NonSmtpGWPathTooLongException = new ExEventLog.EventTuple(3221495623U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002038 RID: 8248
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NonSmtpGWNoDropDirectory = new ExEventLog.EventTuple(3221495624U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002039 RID: 8249
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NonSmtpGWUnauthorizedAccess = new ExEventLog.EventTuple(3221495625U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400203A RID: 8250
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RetryDeliveryIfRejected = new ExEventLog.EventTuple(3221495626U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400203B RID: 8251
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnOpenConnectionAgentException = new ExEventLog.EventTuple(3221495627U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400203C RID: 8252
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnDeliverMailItemAgentException = new ExEventLog.EventTuple(3221495628U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400203D RID: 8253
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnCloseConnectionAgentException = new ExEventLog.EventTuple(3221495629U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400203E RID: 8254
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResubmitDueToUnavailabilityOfSameVersionHubs = new ExEventLog.EventTuple(1074011982U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400203F RID: 8255
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RedirectMessageStarted = new ExEventLog.EventTuple(1074011983U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002040 RID: 8256
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_QueueViewerExceptionDuringAsyncRetryQueue = new ExEventLog.EventTuple(3221495632U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002041 RID: 8257
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResubmitDueToOutboundConnectorChange = new ExEventLog.EventTuple(1074011985U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002042 RID: 8258
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RetryQueueOutboundConnectorLookupFailed = new ExEventLog.EventTuple(3221495634U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002043 RID: 8259
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryDoesNotExist = new ExEventLog.EventTuple(1074012969U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002044 RID: 8260
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoDirectoryPermission = new ExEventLog.EventTuple(3221496618U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002045 RID: 8261
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ReadOnlyFileFound = new ExEventLog.EventTuple(2147754795U, 8, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002046 RID: 8262
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotDeleteFile = new ExEventLog.EventTuple(3221496620U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002047 RID: 8263
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PickupFailedDueToStorageErrors = new ExEventLog.EventTuple(3221496621U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002048 RID: 8264
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCreatePickupDirectory = new ExEventLog.EventTuple(3221496622U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002049 RID: 8265
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoPermissionToRenamePickupFile = new ExEventLog.EventTuple(3221496623U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400204A RID: 8266
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AccessErrorModifyingPickupRegkey = new ExEventLog.EventTuple(3221496625U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400204B RID: 8267
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PickupIsBadmailingFile = new ExEventLog.EventTuple(3221496626U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400204C RID: 8268
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PickupFileEncrypted = new ExEventLog.EventTuple(3221496628U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400204D RID: 8269
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnSubmittedMessageAgentException = new ExEventLog.EventTuple(3221496817U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400204E RID: 8270
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnRoutedMessageAgentException = new ExEventLog.EventTuple(3221496818U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400204F RID: 8271
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransportConfigContainerNotFound = new ExEventLog.EventTuple(3221496824U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002050 RID: 8272
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResolverPerfCountersLoadFailure = new ExEventLog.EventTuple(3221496828U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002051 RID: 8273
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RetryCategorizationIfFailed = new ExEventLog.EventTuple(3221496829U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002052 RID: 8274
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmbiguousSender = new ExEventLog.EventTuple(2147755006U, 9, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002053 RID: 8275
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnCategorizedMessageAgentException = new ExEventLog.EventTuple(3221496831U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002054 RID: 8276
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnResolvedMessageAgentException = new ExEventLog.EventTuple(3221496832U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002055 RID: 8277
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AmbiguousRecipient = new ExEventLog.EventTuple(3221496833U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002056 RID: 8278
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NDRForUnrestrictedLargeDL = new ExEventLog.EventTuple(3221496834U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002057 RID: 8279
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CategorizerErrorRetrievingTenantOverride = new ExEventLog.EventTuple(3221496835U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002058 RID: 8280
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MessageCountEnqueuedToPoisonQueue = new ExEventLog.EventTuple(2147755793U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002059 RID: 8281
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonCountUpdated = new ExEventLog.EventTuple(1074013970U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400205A RID: 8282
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageCrash = new ExEventLog.EventTuple(3221497619U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400205B RID: 8283
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeletedPoisonPickupFile = new ExEventLog.EventTuple(2147755796U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400205C RID: 8284
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageLoadFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221497621U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400205D RID: 8285
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageSaveFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221497622U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400205E RID: 8286
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessageMarkFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221497623U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400205F RID: 8287
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonMessagePruneFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221497624U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002060 RID: 8288
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageSecurityTLSCertificateValidationFailure = new ExEventLog.EventTuple(3221498621U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002061 RID: 8289
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TlsDomainCertificateValidationFailure = new ExEventLog.EventTuple(3221498627U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002062 RID: 8290
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageNotAuthenticatedTlsNotStarted = new ExEventLog.EventTuple(3221498628U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002063 RID: 8291
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageToSecureDomainFailedDueToTlsNegotiationFailure = new ExEventLog.EventTuple(3221498629U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002064 RID: 8292
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageToSecureDomainFailedBecauseTlsNotOffered = new ExEventLog.EventTuple(3221498630U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002065 RID: 8293
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TlsDomainClientCertificateSubjectMismatch = new ExEventLog.EventTuple(3221498631U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002066 RID: 8294
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TlsDomainServerCertificateSubjectMismatch = new ExEventLog.EventTuple(3221498632U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002067 RID: 8295
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageNotAuthenticatedNoClientCertificate = new ExEventLog.EventTuple(3221498633U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002068 RID: 8296
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageNotAuthenticatedTlsNotAdvertised = new ExEventLog.EventTuple(3221498634U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002069 RID: 8297
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageToSecureDomainFailedBecauseTlsNegotiationFailed = new ExEventLog.EventTuple(3221498635U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400206A RID: 8298
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TlsDomainServerCertificateValidationFailure = new ExEventLog.EventTuple(3221498636U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400206B RID: 8299
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TlsDomainSecureDisabled = new ExEventLog.EventTuple(3221498637U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400206C RID: 8300
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TlsDomainCapabilitiesCertificateValidationFailure = new ExEventLog.EventTuple(3221498638U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400206D RID: 8301
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SessionFailedBecauseXOorgNotOffered = new ExEventLog.EventTuple(3221498639U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400206E RID: 8302
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SubjectAlternativeNameLimitExceeded = new ExEventLog.EventTuple(2147756816U, 11, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400206F RID: 8303
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateRevocationListCheckTrasientFailureTreatedAsSuccess = new ExEventLog.EventTuple(3221498642U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002070 RID: 8304
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToFlushTicketCacheOnInitialize = new ExEventLog.EventTuple(3221499617U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002071 RID: 8305
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigUpdateOccurred = new ExEventLog.EventTuple(1074015970U, 12, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002072 RID: 8306
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidServerRole = new ExEventLog.EventTuple(3221499619U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002073 RID: 8307
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReadConfigReceiveConnectorFailed = new ExEventLog.EventTuple(3221499624U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002074 RID: 8308
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReadConfigReceiveConnectorUnavail = new ExEventLog.EventTuple(3221499625U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002075 RID: 8309
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADRecipientCachePerfCountersLoadFailure = new ExEventLog.EventTuple(3221499626U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002076 RID: 8310
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SpnRegisterFailure = new ExEventLog.EventTuple(3221499627U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002077 RID: 8311
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InternalTransportCertificateMissingInAD = new ExEventLog.EventTuple(3221499628U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002078 RID: 8312
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotLoadInternalTransportCertificateFromStore = new ExEventLog.EventTuple(3221499629U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002079 RID: 8313
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotLoadSTARTTLSCertificateFromStore = new ExEventLog.EventTuple(3221499630U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400207A RID: 8314
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InternalTransportCertificateExpired = new ExEventLog.EventTuple(3221499631U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400207B RID: 8315
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_STARTTLSCertificateExpired = new ExEventLog.EventTuple(3221499632U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400207C RID: 8316
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InternalTransportCertificateExpiresSoon = new ExEventLog.EventTuple(3221499633U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400207D RID: 8317
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_STARTTLSCertificateExpiresSoon = new ExEventLog.EventTuple(3221499634U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400207E RID: 8318
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RemoteInternalTransportCertificateExpired = new ExEventLog.EventTuple(3221499635U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400207F RID: 8319
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RemoteSTARTTLSCertificateExpired = new ExEventLog.EventTuple(3221499636U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002080 RID: 8320
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReadConfigReceiveConnectorIgnored = new ExEventLog.EventTuple(2147757813U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002081 RID: 8321
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InternalTransportCertificateCorruptedInAD = new ExEventLog.EventTuple(3221499638U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002082 RID: 8322
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotLoadInternalTransportCertificateFallbackServerFQDN = new ExEventLog.EventTuple(2147757815U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002083 RID: 8323
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotLoadIntTransportCertificateFallbackEphemeralCertificate = new ExEventLog.EventTuple(2147757816U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002084 RID: 8324
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DisconnectingPerformanceCounters = new ExEventLog.EventTuple(2147757817U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002085 RID: 8325
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToDisconnectPerformanceCounters = new ExEventLog.EventTuple(2147757818U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002086 RID: 8326
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessHoldingPerformanceCounter = new ExEventLog.EventTuple(2147757820U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002087 RID: 8327
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_ServerLivingForConsiderableTime = new ExEventLog.EventTuple(1074015997U, 12, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04002088 RID: 8328
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_KillOrphanedWorker = new ExEventLog.EventTuple(2147757822U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002089 RID: 8329
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AnotherServiceRunning = new ExEventLog.EventTuple(2147757823U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400208A RID: 8330
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_KillOrphanedWorkerFailed = new ExEventLog.EventTuple(3221499648U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400208B RID: 8331
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToRegisterForDeletedObjectsNotification = new ExEventLog.EventTuple(3221499649U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400208C RID: 8332
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SystemLowOnMemory = new ExEventLog.EventTuple(2147757826U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400208D RID: 8333
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Exch50OrgNotFound = new ExEventLog.EventTuple(3221500618U, 13, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400208E RID: 8334
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessNotResponding = new ExEventLog.EventTuple(3221501617U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400208F RID: 8335
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AppConfigLoadFailed = new ExEventLog.EventTuple(3221501620U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002090 RID: 8336
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResourceUtilizationUp = new ExEventLog.EventTuple(2147760796U, 15, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002091 RID: 8337
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResourceUtilizationDown = new ExEventLog.EventTuple(1074018973U, 15, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002092 RID: 8338
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DiskSpaceLow = new ExEventLog.EventTuple(3221502622U, 15, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002093 RID: 8339
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PrivateBytesHigh = new ExEventLog.EventTuple(3221502623U, 15, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002094 RID: 8340
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ComponentFailedTransportServerUpdate = new ExEventLog.EventTuple(3221503620U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04002095 RID: 8341
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MessageTrackingLogPathIsNull = new ExEventLog.EventTuple(2147761803U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002096 RID: 8342
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReceiveProtocolLogPathIsNull = new ExEventLog.EventTuple(2147761804U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002097 RID: 8343
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SendProtocolLogPathIsNull = new ExEventLog.EventTuple(2147761805U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002098 RID: 8344
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReceiveProtocolLogPathIsNullUsingOld = new ExEventLog.EventTuple(2147761806U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04002099 RID: 8345
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SendProtocolLogPathIsNullUsingOld = new ExEventLog.EventTuple(2147761807U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400209A RID: 8346
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DefaultAuthoritativeDomainInvalid = new ExEventLog.EventTuple(3221503632U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400209B RID: 8347
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActivationFailed = new ExEventLog.EventTuple(3221503633U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400209C RID: 8348
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationLoaderExternalError = new ExEventLog.EventTuple(2147761810U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400209D RID: 8349
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationLoaderException = new ExEventLog.EventTuple(2147761811U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400209E RID: 8350
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidAcceptedDomain = new ExEventLog.EventTuple(3221503637U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400209F RID: 8351
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationLoaderSuccessfulUpdate = new ExEventLog.EventTuple(1074019990U, 16, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020A0 RID: 8352
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CannotStartAgents = new ExEventLog.EventTuple(3221503639U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020A1 RID: 8353
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RejectedAcceptedDomain = new ExEventLog.EventTuple(3221503640U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020A2 RID: 8354
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidAdapterGuid = new ExEventLog.EventTuple(3221503641U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020A3 RID: 8355
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NetworkAdapterIPQueryFailed = new ExEventLog.EventTuple(3221503642U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020A4 RID: 8356
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationLoaderNoADNotifications = new ExEventLog.EventTuple(2147761819U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020A5 RID: 8357
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationLoaderSuccessfulForcedUpdate = new ExEventLog.EventTuple(1074019996U, 16, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020A6 RID: 8358
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HeartbeatDestinationConfigChanged = new ExEventLog.EventTuple(2147761821U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020A7 RID: 8359
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AgentErrorHandlingOverrideConfigError = new ExEventLog.EventTuple(3221503646U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020A8 RID: 8360
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SchemaTypeMismatch = new ExEventLog.EventTuple(3221504617U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020A9 RID: 8361
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SchemaRequiredColumnNotFound = new ExEventLog.EventTuple(3221504618U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020AA RID: 8362
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetCorruptionError = new ExEventLog.EventTuple(3221504619U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020AB RID: 8363
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetOutOfSpaceError = new ExEventLog.EventTuple(3221504620U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020AC RID: 8364
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetLogFileError = new ExEventLog.EventTuple(3221504621U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020AD RID: 8365
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetPathError = new ExEventLog.EventTuple(3221504622U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020AE RID: 8366
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetMismatchError = new ExEventLog.EventTuple(3221504623U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020AF RID: 8367
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StartScanForMessages = new ExEventLog.EventTuple(1074020976U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020B0 RID: 8368
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StopScanForMessages = new ExEventLog.EventTuple(1074020977U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020B1 RID: 8369
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EndScanForMessages = new ExEventLog.EventTuple(1074020978U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020B2 RID: 8370
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetCheckpointFileError = new ExEventLog.EventTuple(3221504627U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020B3 RID: 8371
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetInitInstanceOutOfMemory = new ExEventLog.EventTuple(3221504628U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020B4 RID: 8372
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetInstanceNameInUse = new ExEventLog.EventTuple(3221504629U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020B5 RID: 8373
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetDatabaseNotFound = new ExEventLog.EventTuple(3221504630U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020B6 RID: 8374
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetDatabaseLogSetMismatch = new ExEventLog.EventTuple(3221504631U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020B7 RID: 8375
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetFragmentationError = new ExEventLog.EventTuple(3221504632U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020B8 RID: 8376
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetQuotaExceededError = new ExEventLog.EventTuple(3221504633U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020B9 RID: 8377
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetInsufficientResourcesError = new ExEventLog.EventTuple(3221504634U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020BA RID: 8378
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetIOError = new ExEventLog.EventTuple(3221504635U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020BB RID: 8379
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetOperationError = new ExEventLog.EventTuple(3221504636U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020BC RID: 8380
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetTableNotFound = new ExEventLog.EventTuple(3221504637U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020BD RID: 8381
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetFileNotFound = new ExEventLog.EventTuple(3221504638U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020BE RID: 8382
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetFileReadOnly = new ExEventLog.EventTuple(3221504639U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020BF RID: 8383
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_JetVersionStoreOutOfMemoryError = new ExEventLog.EventTuple(3221504640U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020C0 RID: 8384
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LastMessagesLoadedByBootScanner = new ExEventLog.EventTuple(1074020993U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020C1 RID: 8385
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseDriveIsNotAccessible = new ExEventLog.EventTuple(3221504642U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020C2 RID: 8386
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ColumnTooBigException = new ExEventLog.EventTuple(3221504643U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020C3 RID: 8387
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AgentDidNotCloseMimeStream = new ExEventLog.EventTuple(3221505617U, 18, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020C4 RID: 8388
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientValidationCacheLoaded = new ExEventLog.EventTuple(1074022969U, 19, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020C5 RID: 8389
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientGroupCacheLoaded = new ExEventLog.EventTuple(1074022970U, 19, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020C6 RID: 8390
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryUnavailableLoadingGroup = new ExEventLog.EventTuple(2147764798U, 19, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020C7 RID: 8391
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryUnavailableLoadingValidationCache = new ExEventLog.EventTuple(2147764799U, 19, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020C8 RID: 8392
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientHasDataValidationException = new ExEventLog.EventTuple(3221506624U, 19, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020C9 RID: 8393
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ORARMessageSubmitted = new ExEventLog.EventTuple(1074023969U, 20, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020CA RID: 8394
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailParseOrarBlob = new ExEventLog.EventTuple(2147765794U, 20, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020CB RID: 8395
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailGetRoutingAddress = new ExEventLog.EventTuple(2147765795U, 20, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020CC RID: 8396
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseRecoveryActionNone = new ExEventLog.EventTuple(3221504717U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020CD RID: 8397
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseRecoveryActionMove = new ExEventLog.EventTuple(2147762894U, 17, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020CE RID: 8398
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseRecoveryActionDelete = new ExEventLog.EventTuple(1074021071U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020CF RID: 8399
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseRecoveryActionFailed = new ExEventLog.EventTuple(3221504720U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020D0 RID: 8400
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseRecoveryActionFailedRegistryAccessDenied = new ExEventLog.EventTuple(3221504721U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020D1 RID: 8401
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DataBaseCorruptionDetected = new ExEventLog.EventTuple(1074021074U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020D2 RID: 8402
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DatabaseErrorDetected = new ExEventLog.EventTuple(1074021075U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020D3 RID: 8403
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TableLockedException = new ExEventLog.EventTuple(3221504724U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020D4 RID: 8404
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowRedundancyMessagesResubmitted = new ExEventLog.EventTuple(1074025969U, 22, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020D5 RID: 8405
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowRedundancyMessageResubmitSuppressed = new ExEventLog.EventTuple(1074025970U, 22, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020D6 RID: 8406
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowRedundancyPrimaryServerDatabaseStateChanged = new ExEventLog.EventTuple(3221509619U, 22, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020D7 RID: 8407
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowRedundancyPrimaryServerHeartbeatFailed = new ExEventLog.EventTuple(2147767796U, 22, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020D8 RID: 8408
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowRedundancyMessageDeferredDueToShadowFailure = new ExEventLog.EventTuple(3221509622U, 22, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020D9 RID: 8409
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ShadowRedundancyForcedHeartbeatReset = new ExEventLog.EventTuple(2147767799U, 22, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020DA RID: 8410
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ModeratedTransportNoArbitrationMailbox = new ExEventLog.EventTuple(3221510617U, 23, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020DB RID: 8411
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientStampedWithDeletedArbitrationMailbox = new ExEventLog.EventTuple(3221510618U, 23, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020DC RID: 8412
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RemovedMessageRepositoryRequest = new ExEventLog.EventTuple(1074027972U, 24, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020DD RID: 8413
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ModifiedMessageRepositoryRequest = new ExEventLog.EventTuple(1074027973U, 24, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020DE RID: 8414
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RegisterRpcServerFailure = new ExEventLog.EventTuple(1074027975U, 24, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020DF RID: 8415
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MessageAttributionFailed = new ExEventLog.EventTuple(3221511626U, 25, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020E0 RID: 8416
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransportWlmLogPathIsNull = new ExEventLog.EventTuple(3221511627U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020E1 RID: 8417
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResubmitRequestExpired = new ExEventLog.EventTuple(1074027980U, 24, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020E2 RID: 8418
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DuplicateResubmitRequest = new ExEventLog.EventTuple(2147769805U, 24, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020E3 RID: 8419
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MaxRunningResubmitRequest = new ExEventLog.EventTuple(2147769806U, 24, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020E4 RID: 8420
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MaxRecentResubmitRequest = new ExEventLog.EventTuple(2147769807U, 24, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020E5 RID: 8421
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InterceptorAgentConfigurationLoadingError = new ExEventLog.EventTuple(3221512617U, 18, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020E6 RID: 8422
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InterceptorAgentConfigurationReplaced = new ExEventLog.EventTuple(287146U, 18, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020E7 RID: 8423
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InterceptorAgentAccessDenied = new ExEventLog.EventTuple(3221512619U, 18, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020E8 RID: 8424
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InterceptorRuleNearingExpiration = new ExEventLog.EventTuple(2147770796U, 18, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040020E9 RID: 8425
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_QueueQuotaComponentLogPathIsNull = new ExEventLog.EventTuple(3221512622U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020EA RID: 8426
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NotEnoughMemoryToStartService = new ExEventLog.EventTuple(3221512623U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020EB RID: 8427
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FlowControlLogPathIsNull = new ExEventLog.EventTuple(3221512624U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020EC RID: 8428
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FilePathOnLockedVolume = new ExEventLog.EventTuple(3221512625U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040020ED RID: 8429
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BitlockerQueryFailed = new ExEventLog.EventTuple(3221512626U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000536 RID: 1334
		private enum Category : short
		{
			// Token: 0x040020EF RID: 8431
			SmtpReceive = 1,
			// Token: 0x040020F0 RID: 8432
			SmtpSend,
			// Token: 0x040020F1 RID: 8433
			Dsn,
			// Token: 0x040020F2 RID: 8434
			Routing,
			// Token: 0x040020F3 RID: 8435
			Logging,
			// Token: 0x040020F4 RID: 8436
			Components,
			// Token: 0x040020F5 RID: 8437
			RemoteDelivery,
			// Token: 0x040020F6 RID: 8438
			Pickup,
			// Token: 0x040020F7 RID: 8439
			Categorizer,
			// Token: 0x040020F8 RID: 8440
			PoisonMessage,
			// Token: 0x040020F9 RID: 8441
			MessageSecurity,
			// Token: 0x040020FA RID: 8442
			TransportService,
			// Token: 0x040020FB RID: 8443
			Exch50,
			// Token: 0x040020FC RID: 8444
			Process,
			// Token: 0x040020FD RID: 8445
			ResourceManager,
			// Token: 0x040020FE RID: 8446
			Configuration,
			// Token: 0x040020FF RID: 8447
			Storage,
			// Token: 0x04002100 RID: 8448
			Agents,
			// Token: 0x04002101 RID: 8449
			Transport_Address_Book,
			// Token: 0x04002102 RID: 8450
			Orar,
			// Token: 0x04002103 RID: 8451
			Unused,
			// Token: 0x04002104 RID: 8452
			ShadowRedundancy,
			// Token: 0x04002105 RID: 8453
			Approval,
			// Token: 0x04002106 RID: 8454
			TransportSafetyNet,
			// Token: 0x04002107 RID: 8455
			TransportTenantAttribution
		}

		// Token: 0x02000537 RID: 1335
		internal enum Message : uint
		{
			// Token: 0x04002109 RID: 8457
			BindingIPv6ButDisabled = 3221488616U,
			// Token: 0x0400210A RID: 8458
			AddressInUse,
			// Token: 0x0400210B RID: 8459
			ConfiguredConnectors = 1074004970U,
			// Token: 0x0400210C RID: 8460
			SmtpReceiveMaxConnectionReached = 2147746801U,
			// Token: 0x0400210D RID: 8461
			SmtpReceiveMessageRejected = 1074004978U,
			// Token: 0x0400210E RID: 8462
			SmtpReceiveAuthenticationFailedTooManyErrors = 2147746804U,
			// Token: 0x0400210F RID: 8463
			SmtpReceiveRejectDueToStorageError = 3221488629U,
			// Token: 0x04002110 RID: 8464
			SmtpReceiveAuthorizationSubmitRejected = 2147746810U,
			// Token: 0x04002111 RID: 8465
			SmtpReceiveAuthorizationRejected = 2147746812U,
			// Token: 0x04002112 RID: 8466
			SmtpReceiveMaxConnectionPerSourceReached,
			// Token: 0x04002113 RID: 8467
			InternalSMTPServerListEmpty,
			// Token: 0x04002114 RID: 8468
			SmtpReceiveSubmitDenied = 1074004991U,
			// Token: 0x04002115 RID: 8469
			SmtpReceiveSendAsDeniedTempAuthFailure,
			// Token: 0x04002116 RID: 8470
			SmtpReceiveSendAsDeniedSenderAddressDataInvalid = 3221488641U,
			// Token: 0x04002117 RID: 8471
			SmtpReceiveSendAsDenied = 1074004994U,
			// Token: 0x04002118 RID: 8472
			SmtpReceiveSendOnBehalfOfDeniedTempAuthFailure,
			// Token: 0x04002119 RID: 8473
			SmtpReceiveSendOnBehalfOfDeniedFromAddressDataInvalid = 3221488644U,
			// Token: 0x0400211A RID: 8474
			SmtpReceiveSendOnBehalfOfDenied = 1074004997U,
			// Token: 0x0400211B RID: 8475
			SmtpReceiveCouldNotDetermineUserNameOrSid = 3221488646U,
			// Token: 0x0400211C RID: 8476
			SmtpReceiveMessageRateLimitExceeded = 1074004999U,
			// Token: 0x0400211D RID: 8477
			SmtpReceiveTLSRequiredFailed = 3221488648U,
			// Token: 0x0400211E RID: 8478
			SmtpReceiveCatchAll,
			// Token: 0x0400211F RID: 8479
			SmtpReceiveAuthenticationInitializationFailed,
			// Token: 0x04002120 RID: 8480
			SmtpReceiveAuthenticationFailed = 2147746827U,
			// Token: 0x04002121 RID: 8481
			SmtpReceiveDirectTrustFailed = 3221488652U,
			// Token: 0x04002122 RID: 8482
			SmtpReceiveActiveManagerFailure,
			// Token: 0x04002123 RID: 8483
			SmtpReceiveAvailabilityCounterFailure,
			// Token: 0x04002124 RID: 8484
			SmtpReceiveProhibitSendQuotaDeniedTempAuthFailure = 1074005007U,
			// Token: 0x04002125 RID: 8485
			SmtpReceiveConnectorAvailabilityLow = 3221488656U,
			// Token: 0x04002126 RID: 8486
			SmtpReceiveConnectorAvailabilityNormal = 1074005009U,
			// Token: 0x04002127 RID: 8487
			SmtpReceiveProxyMserveLookupFailed = 3221488658U,
			// Token: 0x04002128 RID: 8488
			SmtpReceiveProxyInvalidPartnerId,
			// Token: 0x04002129 RID: 8489
			SmtpReceiveProxyDnsLookupFailed,
			// Token: 0x0400212A RID: 8490
			SmtpReceiveProxyCatchAll,
			// Token: 0x0400212B RID: 8491
			SmtpReceiveProxyCounterFailure = 3221488663U,
			// Token: 0x0400212C RID: 8492
			SmtpReceiveTooManyProxySessionFailures,
			// Token: 0x0400212D RID: 8493
			SmtpReceiveNoDestinationToProxyTo,
			// Token: 0x0400212E RID: 8494
			SmtpReceiveProcessingBlobFailed,
			// Token: 0x0400212F RID: 8495
			SmtpSendDnsConnectionFailure = 2147747792U,
			// Token: 0x04002130 RID: 8496
			SmtpSendConnectionError = 2147747794U,
			// Token: 0x04002131 RID: 8497
			SmtpSendAuthenticationFailed,
			// Token: 0x04002132 RID: 8498
			SmtpSendAckMessage = 1074005972U,
			// Token: 0x04002133 RID: 8499
			SmtpSendAckConnection,
			// Token: 0x04002134 RID: 8500
			SmtpSendRemoteDisconnected = 2147747798U,
			// Token: 0x04002135 RID: 8501
			SmtpSendNewSession = 1074005975U,
			// Token: 0x04002136 RID: 8502
			ExchangeAuthHashNotSupported = 3221489627U,
			// Token: 0x04002137 RID: 8503
			SendConnectorInvalidSourceIPAddress = 3221489630U,
			// Token: 0x04002138 RID: 8504
			SmtpSendTLSRequiredFailed,
			// Token: 0x04002139 RID: 8505
			SmtpSendAuthenticationInitializationFailed,
			// Token: 0x0400213A RID: 8506
			SmtpSendOutboundAuthenticationFailed,
			// Token: 0x0400213B RID: 8507
			SmtpSendDirectTrustFailed,
			// Token: 0x0400213C RID: 8508
			SmtpSendUnableToTransmitOrar,
			// Token: 0x0400213D RID: 8509
			SmtpSendUnableToTransmitLongOrar,
			// Token: 0x0400213E RID: 8510
			SmtpSendUnableToTransmitRDst,
			// Token: 0x0400213F RID: 8511
			SmtpSendOutboundAtTLSAuthLevelFailed,
			// Token: 0x04002140 RID: 8512
			SmtpSendAuthenticationFailureIgnored = 2147747815U,
			// Token: 0x04002141 RID: 8513
			SmtpSendNewProxySession = 1074005992U,
			// Token: 0x04002142 RID: 8514
			SmtpSendProxyEhloOptionsDoNotMatch = 3221489641U,
			// Token: 0x04002143 RID: 8515
			SmtpSendInboundProxyEhloOptionsDoNotMatch,
			// Token: 0x04002144 RID: 8516
			SmtpSendInboundProxyRecipientLimitsDoNotMatch,
			// Token: 0x04002145 RID: 8517
			SmtpSendPoisonForRemoteThresholdExceeded = 2147747820U,
			// Token: 0x04002146 RID: 8518
			SmtpSendInboundProxyNonCriticalEhloOptionsDoNotMatch,
			// Token: 0x04002147 RID: 8519
			SmtpSendProxyEhloOptionsDoNotMatchButStillContinueProxying = 3221489646U,
			// Token: 0x04002148 RID: 8520
			InboundProxyDestinationsTrackerDiagnosticInfo = 1074005999U,
			// Token: 0x04002149 RID: 8521
			InboundProxyDestinationsTrackerReject = 3221489648U,
			// Token: 0x0400214A RID: 8522
			InboundProxyDestinationsTrackerNearThreshold = 2147747825U,
			// Token: 0x0400214B RID: 8523
			InboundProxyAccountForestsTrackerReject = 3221489650U,
			// Token: 0x0400214C RID: 8524
			InboundProxyAccountForestsTrackerNearThreshold = 2147747827U,
			// Token: 0x0400214D RID: 8525
			DsnUnableToReadQuarantineConfig = 3221490620U,
			// Token: 0x0400214E RID: 8526
			DsnUnableToReadSystemMessageConfig,
			// Token: 0x0400214F RID: 8527
			DsnDiskFull,
			// Token: 0x04002150 RID: 8528
			XProxyToCommandInvalidEncodedCertificateSubject,
			// Token: 0x04002151 RID: 8529
			RoutingPerfCountersLoadFailure = 3221492617U,
			// Token: 0x04002152 RID: 8530
			RoutingAdUnavailable,
			// Token: 0x04002153 RID: 8531
			RoutingWillRetryLoad,
			// Token: 0x04002154 RID: 8532
			RoutingNoServerFqdn,
			// Token: 0x04002155 RID: 8533
			RoutingNoServerAdSite,
			// Token: 0x04002156 RID: 8534
			RoutingNoOwningServerForMdb = 2147750798U,
			// Token: 0x04002157 RID: 8535
			RoutingNoRouteToAdSite = 3221492623U,
			// Token: 0x04002158 RID: 8536
			RoutingNoRouteToOwningServer,
			// Token: 0x04002159 RID: 8537
			RoutingNoPfTreeMdbRoute = 2147750801U,
			// Token: 0x0400215A RID: 8538
			RoutingNoPfTreeRoute = 3221492626U,
			// Token: 0x0400215B RID: 8539
			RoutingNoSourceRgForRgConnector,
			// Token: 0x0400215C RID: 8540
			RoutingNoTargetRgForRgConnector,
			// Token: 0x0400215D RID: 8541
			RoutingNoServerRg,
			// Token: 0x0400215E RID: 8542
			RoutingNoSourceBhServers,
			// Token: 0x0400215F RID: 8543
			RoutingNoSourceBhRoute,
			// Token: 0x04002160 RID: 8544
			RoutingNoRouteToConnector,
			// Token: 0x04002161 RID: 8545
			RoutingNoTargetBhServer,
			// Token: 0x04002162 RID: 8546
			RoutingNoTargetBhServers,
			// Token: 0x04002163 RID: 8547
			RoutingInvalidSmarthosts,
			// Token: 0x04002164 RID: 8548
			RoutingTransientConfigError = 3221492639U,
			// Token: 0x04002165 RID: 8549
			RoutingMaxConfigLoadRetriesReached,
			// Token: 0x04002166 RID: 8550
			RoutingNoSourceRgForNonRgConnector = 3221492643U,
			// Token: 0x04002167 RID: 8551
			RoutingLocalConnectorWithConnectedDomains,
			// Token: 0x04002168 RID: 8552
			RoutingNoConnectedRg,
			// Token: 0x04002169 RID: 8553
			RoutingTableLogCreationFailure,
			// Token: 0x0400216A RID: 8554
			RoutingTableLogDeletionFailure,
			// Token: 0x0400216B RID: 8555
			RoutingNoDeliveryGroupForDatabase = 2147750827U,
			// Token: 0x0400216C RID: 8556
			RoutingNoRoutingGroupForDatabase,
			// Token: 0x0400216D RID: 8557
			RoutingNoDagForDatabase,
			// Token: 0x0400216E RID: 8558
			RoutingNoDestinationForDatabase = 3221492654U,
			// Token: 0x0400216F RID: 8559
			RoutingNoHubServersSelectedForDatabases,
			// Token: 0x04002170 RID: 8560
			RoutingNoHubServersSelectedForTenant,
			// Token: 0x04002171 RID: 8561
			InactiveDagsExcludedFromDagSelector = 1074009009U,
			// Token: 0x04002172 RID: 8562
			DagSelectorDiagnosticInfo,
			// Token: 0x04002173 RID: 8563
			TenantDagQuotaDiagnosticInfo,
			// Token: 0x04002174 RID: 8564
			RoutingTableDatabaseFullReload,
			// Token: 0x04002175 RID: 8565
			RoutingDictionaryInsertFailure = 3221492661U,
			// Token: 0x04002176 RID: 8566
			PipelineTracingActive = 2147751292U,
			// Token: 0x04002177 RID: 8567
			PerfCountersLoadFailure = 3221493117U,
			// Token: 0x04002178 RID: 8568
			ExternalServersLatencyTimeNotSync = 2147751294U,
			// Token: 0x04002179 RID: 8569
			MultiplePreProcessLatencies = 3221493119U,
			// Token: 0x0400217A RID: 8570
			NullLatencyTreeLeaf,
			// Token: 0x0400217B RID: 8571
			MultipleCompletions,
			// Token: 0x0400217C RID: 8572
			StopService = 1074010969U,
			// Token: 0x0400217D RID: 8573
			DatabaseInUse = 3221494618U,
			// Token: 0x0400217E RID: 8574
			ActivationTiming = 2147752796U,
			// Token: 0x0400217F RID: 8575
			NewDatabaseCreated = 1074010973U,
			// Token: 0x04002180 RID: 8576
			DatabaseSchemaNotSupported = 3221494624U,
			// Token: 0x04002181 RID: 8577
			RetrieveServiceState = 1074010977U,
			// Token: 0x04002182 RID: 8578
			ActivationSlow = 2147752802U,
			// Token: 0x04002183 RID: 8579
			HubTransportServiceStateChanged,
			// Token: 0x04002184 RID: 8580
			FrontendTransportServiceStateChanged,
			// Token: 0x04002185 RID: 8581
			FrontendTransportRestartOnServiceStateChange,
			// Token: 0x04002186 RID: 8582
			EdgeTransportServiceStateChanged,
			// Token: 0x04002187 RID: 8583
			FrontendTransportServiceInitializationFailure = 3221494631U,
			// Token: 0x04002188 RID: 8584
			EdgeTransportInitializationFailure,
			// Token: 0x04002189 RID: 8585
			MSExchangeTransportInitializationFailure,
			// Token: 0x0400218A RID: 8586
			ResubmitDueToConfigUpdate = 1074011970U,
			// Token: 0x0400218B RID: 8587
			ResubmitDueToInactivityTimeout,
			// Token: 0x0400218C RID: 8588
			NonSmtpGWBadDropDirectory = 3221495620U,
			// Token: 0x0400218D RID: 8589
			NonSmtpGWQuotaExceeded,
			// Token: 0x0400218E RID: 8590
			QueuingStatusAtShutdown = 1074011974U,
			// Token: 0x0400218F RID: 8591
			NonSmtpGWPathTooLongException = 3221495623U,
			// Token: 0x04002190 RID: 8592
			NonSmtpGWNoDropDirectory,
			// Token: 0x04002191 RID: 8593
			NonSmtpGWUnauthorizedAccess,
			// Token: 0x04002192 RID: 8594
			RetryDeliveryIfRejected,
			// Token: 0x04002193 RID: 8595
			OnOpenConnectionAgentException,
			// Token: 0x04002194 RID: 8596
			OnDeliverMailItemAgentException,
			// Token: 0x04002195 RID: 8597
			OnCloseConnectionAgentException,
			// Token: 0x04002196 RID: 8598
			ResubmitDueToUnavailabilityOfSameVersionHubs = 1074011982U,
			// Token: 0x04002197 RID: 8599
			RedirectMessageStarted,
			// Token: 0x04002198 RID: 8600
			QueueViewerExceptionDuringAsyncRetryQueue = 3221495632U,
			// Token: 0x04002199 RID: 8601
			ResubmitDueToOutboundConnectorChange = 1074011985U,
			// Token: 0x0400219A RID: 8602
			RetryQueueOutboundConnectorLookupFailed = 3221495634U,
			// Token: 0x0400219B RID: 8603
			DirectoryDoesNotExist = 1074012969U,
			// Token: 0x0400219C RID: 8604
			NoDirectoryPermission = 3221496618U,
			// Token: 0x0400219D RID: 8605
			ReadOnlyFileFound = 2147754795U,
			// Token: 0x0400219E RID: 8606
			CannotDeleteFile = 3221496620U,
			// Token: 0x0400219F RID: 8607
			PickupFailedDueToStorageErrors,
			// Token: 0x040021A0 RID: 8608
			FailedToCreatePickupDirectory,
			// Token: 0x040021A1 RID: 8609
			NoPermissionToRenamePickupFile,
			// Token: 0x040021A2 RID: 8610
			AccessErrorModifyingPickupRegkey = 3221496625U,
			// Token: 0x040021A3 RID: 8611
			PickupIsBadmailingFile,
			// Token: 0x040021A4 RID: 8612
			PickupFileEncrypted = 3221496628U,
			// Token: 0x040021A5 RID: 8613
			OnSubmittedMessageAgentException = 3221496817U,
			// Token: 0x040021A6 RID: 8614
			OnRoutedMessageAgentException,
			// Token: 0x040021A7 RID: 8615
			TransportConfigContainerNotFound = 3221496824U,
			// Token: 0x040021A8 RID: 8616
			ResolverPerfCountersLoadFailure = 3221496828U,
			// Token: 0x040021A9 RID: 8617
			RetryCategorizationIfFailed,
			// Token: 0x040021AA RID: 8618
			AmbiguousSender = 2147755006U,
			// Token: 0x040021AB RID: 8619
			OnCategorizedMessageAgentException = 3221496831U,
			// Token: 0x040021AC RID: 8620
			OnResolvedMessageAgentException,
			// Token: 0x040021AD RID: 8621
			AmbiguousRecipient,
			// Token: 0x040021AE RID: 8622
			NDRForUnrestrictedLargeDL,
			// Token: 0x040021AF RID: 8623
			CategorizerErrorRetrievingTenantOverride,
			// Token: 0x040021B0 RID: 8624
			MessageCountEnqueuedToPoisonQueue = 2147755793U,
			// Token: 0x040021B1 RID: 8625
			PoisonCountUpdated = 1074013970U,
			// Token: 0x040021B2 RID: 8626
			PoisonMessageCrash = 3221497619U,
			// Token: 0x040021B3 RID: 8627
			DeletedPoisonPickupFile = 2147755796U,
			// Token: 0x040021B4 RID: 8628
			PoisonMessageLoadFailedRegistryAccessDenied = 3221497621U,
			// Token: 0x040021B5 RID: 8629
			PoisonMessageSaveFailedRegistryAccessDenied,
			// Token: 0x040021B6 RID: 8630
			PoisonMessageMarkFailedRegistryAccessDenied,
			// Token: 0x040021B7 RID: 8631
			PoisonMessagePruneFailedRegistryAccessDenied,
			// Token: 0x040021B8 RID: 8632
			MessageSecurityTLSCertificateValidationFailure = 3221498621U,
			// Token: 0x040021B9 RID: 8633
			TlsDomainCertificateValidationFailure = 3221498627U,
			// Token: 0x040021BA RID: 8634
			MessageNotAuthenticatedTlsNotStarted,
			// Token: 0x040021BB RID: 8635
			MessageToSecureDomainFailedDueToTlsNegotiationFailure,
			// Token: 0x040021BC RID: 8636
			MessageToSecureDomainFailedBecauseTlsNotOffered,
			// Token: 0x040021BD RID: 8637
			TlsDomainClientCertificateSubjectMismatch,
			// Token: 0x040021BE RID: 8638
			TlsDomainServerCertificateSubjectMismatch,
			// Token: 0x040021BF RID: 8639
			MessageNotAuthenticatedNoClientCertificate,
			// Token: 0x040021C0 RID: 8640
			MessageNotAuthenticatedTlsNotAdvertised,
			// Token: 0x040021C1 RID: 8641
			MessageToSecureDomainFailedBecauseTlsNegotiationFailed,
			// Token: 0x040021C2 RID: 8642
			TlsDomainServerCertificateValidationFailure,
			// Token: 0x040021C3 RID: 8643
			TlsDomainSecureDisabled,
			// Token: 0x040021C4 RID: 8644
			TlsDomainCapabilitiesCertificateValidationFailure,
			// Token: 0x040021C5 RID: 8645
			SessionFailedBecauseXOorgNotOffered,
			// Token: 0x040021C6 RID: 8646
			SubjectAlternativeNameLimitExceeded = 2147756816U,
			// Token: 0x040021C7 RID: 8647
			CertificateRevocationListCheckTrasientFailureTreatedAsSuccess = 3221498642U,
			// Token: 0x040021C8 RID: 8648
			FailedToFlushTicketCacheOnInitialize = 3221499617U,
			// Token: 0x040021C9 RID: 8649
			ConfigUpdateOccurred = 1074015970U,
			// Token: 0x040021CA RID: 8650
			InvalidServerRole = 3221499619U,
			// Token: 0x040021CB RID: 8651
			ReadConfigReceiveConnectorFailed = 3221499624U,
			// Token: 0x040021CC RID: 8652
			ReadConfigReceiveConnectorUnavail,
			// Token: 0x040021CD RID: 8653
			ADRecipientCachePerfCountersLoadFailure,
			// Token: 0x040021CE RID: 8654
			SpnRegisterFailure,
			// Token: 0x040021CF RID: 8655
			InternalTransportCertificateMissingInAD,
			// Token: 0x040021D0 RID: 8656
			CannotLoadInternalTransportCertificateFromStore,
			// Token: 0x040021D1 RID: 8657
			CannotLoadSTARTTLSCertificateFromStore,
			// Token: 0x040021D2 RID: 8658
			InternalTransportCertificateExpired,
			// Token: 0x040021D3 RID: 8659
			STARTTLSCertificateExpired,
			// Token: 0x040021D4 RID: 8660
			InternalTransportCertificateExpiresSoon,
			// Token: 0x040021D5 RID: 8661
			STARTTLSCertificateExpiresSoon,
			// Token: 0x040021D6 RID: 8662
			RemoteInternalTransportCertificateExpired,
			// Token: 0x040021D7 RID: 8663
			RemoteSTARTTLSCertificateExpired,
			// Token: 0x040021D8 RID: 8664
			ReadConfigReceiveConnectorIgnored = 2147757813U,
			// Token: 0x040021D9 RID: 8665
			InternalTransportCertificateCorruptedInAD = 3221499638U,
			// Token: 0x040021DA RID: 8666
			CannotLoadInternalTransportCertificateFallbackServerFQDN = 2147757815U,
			// Token: 0x040021DB RID: 8667
			CannotLoadIntTransportCertificateFallbackEphemeralCertificate,
			// Token: 0x040021DC RID: 8668
			DisconnectingPerformanceCounters,
			// Token: 0x040021DD RID: 8669
			FailedToDisconnectPerformanceCounters,
			// Token: 0x040021DE RID: 8670
			ProcessHoldingPerformanceCounter = 2147757820U,
			// Token: 0x040021DF RID: 8671
			ServerLivingForConsiderableTime = 1074015997U,
			// Token: 0x040021E0 RID: 8672
			KillOrphanedWorker = 2147757822U,
			// Token: 0x040021E1 RID: 8673
			AnotherServiceRunning,
			// Token: 0x040021E2 RID: 8674
			KillOrphanedWorkerFailed = 3221499648U,
			// Token: 0x040021E3 RID: 8675
			FailedToRegisterForDeletedObjectsNotification,
			// Token: 0x040021E4 RID: 8676
			SystemLowOnMemory = 2147757826U,
			// Token: 0x040021E5 RID: 8677
			Exch50OrgNotFound = 3221500618U,
			// Token: 0x040021E6 RID: 8678
			ProcessNotResponding = 3221501617U,
			// Token: 0x040021E7 RID: 8679
			AppConfigLoadFailed = 3221501620U,
			// Token: 0x040021E8 RID: 8680
			ResourceUtilizationUp = 2147760796U,
			// Token: 0x040021E9 RID: 8681
			ResourceUtilizationDown = 1074018973U,
			// Token: 0x040021EA RID: 8682
			DiskSpaceLow = 3221502622U,
			// Token: 0x040021EB RID: 8683
			PrivateBytesHigh,
			// Token: 0x040021EC RID: 8684
			ComponentFailedTransportServerUpdate = 3221503620U,
			// Token: 0x040021ED RID: 8685
			MessageTrackingLogPathIsNull = 2147761803U,
			// Token: 0x040021EE RID: 8686
			ReceiveProtocolLogPathIsNull,
			// Token: 0x040021EF RID: 8687
			SendProtocolLogPathIsNull,
			// Token: 0x040021F0 RID: 8688
			ReceiveProtocolLogPathIsNullUsingOld,
			// Token: 0x040021F1 RID: 8689
			SendProtocolLogPathIsNullUsingOld,
			// Token: 0x040021F2 RID: 8690
			DefaultAuthoritativeDomainInvalid = 3221503632U,
			// Token: 0x040021F3 RID: 8691
			ActivationFailed,
			// Token: 0x040021F4 RID: 8692
			ConfigurationLoaderExternalError = 2147761810U,
			// Token: 0x040021F5 RID: 8693
			ConfigurationLoaderException,
			// Token: 0x040021F6 RID: 8694
			InvalidAcceptedDomain = 3221503637U,
			// Token: 0x040021F7 RID: 8695
			ConfigurationLoaderSuccessfulUpdate = 1074019990U,
			// Token: 0x040021F8 RID: 8696
			CannotStartAgents = 3221503639U,
			// Token: 0x040021F9 RID: 8697
			RejectedAcceptedDomain,
			// Token: 0x040021FA RID: 8698
			InvalidAdapterGuid,
			// Token: 0x040021FB RID: 8699
			NetworkAdapterIPQueryFailed,
			// Token: 0x040021FC RID: 8700
			ConfigurationLoaderNoADNotifications = 2147761819U,
			// Token: 0x040021FD RID: 8701
			ConfigurationLoaderSuccessfulForcedUpdate = 1074019996U,
			// Token: 0x040021FE RID: 8702
			HeartbeatDestinationConfigChanged = 2147761821U,
			// Token: 0x040021FF RID: 8703
			AgentErrorHandlingOverrideConfigError = 3221503646U,
			// Token: 0x04002200 RID: 8704
			SchemaTypeMismatch = 3221504617U,
			// Token: 0x04002201 RID: 8705
			SchemaRequiredColumnNotFound,
			// Token: 0x04002202 RID: 8706
			JetCorruptionError,
			// Token: 0x04002203 RID: 8707
			JetOutOfSpaceError,
			// Token: 0x04002204 RID: 8708
			JetLogFileError,
			// Token: 0x04002205 RID: 8709
			JetPathError,
			// Token: 0x04002206 RID: 8710
			JetMismatchError,
			// Token: 0x04002207 RID: 8711
			StartScanForMessages = 1074020976U,
			// Token: 0x04002208 RID: 8712
			StopScanForMessages,
			// Token: 0x04002209 RID: 8713
			EndScanForMessages,
			// Token: 0x0400220A RID: 8714
			JetCheckpointFileError = 3221504627U,
			// Token: 0x0400220B RID: 8715
			JetInitInstanceOutOfMemory,
			// Token: 0x0400220C RID: 8716
			JetInstanceNameInUse,
			// Token: 0x0400220D RID: 8717
			JetDatabaseNotFound,
			// Token: 0x0400220E RID: 8718
			JetDatabaseLogSetMismatch,
			// Token: 0x0400220F RID: 8719
			JetFragmentationError,
			// Token: 0x04002210 RID: 8720
			JetQuotaExceededError,
			// Token: 0x04002211 RID: 8721
			JetInsufficientResourcesError,
			// Token: 0x04002212 RID: 8722
			JetIOError,
			// Token: 0x04002213 RID: 8723
			JetOperationError,
			// Token: 0x04002214 RID: 8724
			JetTableNotFound,
			// Token: 0x04002215 RID: 8725
			JetFileNotFound,
			// Token: 0x04002216 RID: 8726
			JetFileReadOnly,
			// Token: 0x04002217 RID: 8727
			JetVersionStoreOutOfMemoryError,
			// Token: 0x04002218 RID: 8728
			LastMessagesLoadedByBootScanner = 1074020993U,
			// Token: 0x04002219 RID: 8729
			DatabaseDriveIsNotAccessible = 3221504642U,
			// Token: 0x0400221A RID: 8730
			ColumnTooBigException,
			// Token: 0x0400221B RID: 8731
			AgentDidNotCloseMimeStream = 3221505617U,
			// Token: 0x0400221C RID: 8732
			RecipientValidationCacheLoaded = 1074022969U,
			// Token: 0x0400221D RID: 8733
			RecipientGroupCacheLoaded,
			// Token: 0x0400221E RID: 8734
			DirectoryUnavailableLoadingGroup = 2147764798U,
			// Token: 0x0400221F RID: 8735
			DirectoryUnavailableLoadingValidationCache,
			// Token: 0x04002220 RID: 8736
			RecipientHasDataValidationException = 3221506624U,
			// Token: 0x04002221 RID: 8737
			ORARMessageSubmitted = 1074023969U,
			// Token: 0x04002222 RID: 8738
			FailParseOrarBlob = 2147765794U,
			// Token: 0x04002223 RID: 8739
			FailGetRoutingAddress,
			// Token: 0x04002224 RID: 8740
			DatabaseRecoveryActionNone = 3221504717U,
			// Token: 0x04002225 RID: 8741
			DatabaseRecoveryActionMove = 2147762894U,
			// Token: 0x04002226 RID: 8742
			DatabaseRecoveryActionDelete = 1074021071U,
			// Token: 0x04002227 RID: 8743
			DatabaseRecoveryActionFailed = 3221504720U,
			// Token: 0x04002228 RID: 8744
			DatabaseRecoveryActionFailedRegistryAccessDenied,
			// Token: 0x04002229 RID: 8745
			DataBaseCorruptionDetected = 1074021074U,
			// Token: 0x0400222A RID: 8746
			DatabaseErrorDetected,
			// Token: 0x0400222B RID: 8747
			TableLockedException = 3221504724U,
			// Token: 0x0400222C RID: 8748
			ShadowRedundancyMessagesResubmitted = 1074025969U,
			// Token: 0x0400222D RID: 8749
			ShadowRedundancyMessageResubmitSuppressed,
			// Token: 0x0400222E RID: 8750
			ShadowRedundancyPrimaryServerDatabaseStateChanged = 3221509619U,
			// Token: 0x0400222F RID: 8751
			ShadowRedundancyPrimaryServerHeartbeatFailed = 2147767796U,
			// Token: 0x04002230 RID: 8752
			ShadowRedundancyMessageDeferredDueToShadowFailure = 3221509622U,
			// Token: 0x04002231 RID: 8753
			ShadowRedundancyForcedHeartbeatReset = 2147767799U,
			// Token: 0x04002232 RID: 8754
			ModeratedTransportNoArbitrationMailbox = 3221510617U,
			// Token: 0x04002233 RID: 8755
			RecipientStampedWithDeletedArbitrationMailbox,
			// Token: 0x04002234 RID: 8756
			RemovedMessageRepositoryRequest = 1074027972U,
			// Token: 0x04002235 RID: 8757
			ModifiedMessageRepositoryRequest,
			// Token: 0x04002236 RID: 8758
			RegisterRpcServerFailure = 1074027975U,
			// Token: 0x04002237 RID: 8759
			MessageAttributionFailed = 3221511626U,
			// Token: 0x04002238 RID: 8760
			TransportWlmLogPathIsNull,
			// Token: 0x04002239 RID: 8761
			ResubmitRequestExpired = 1074027980U,
			// Token: 0x0400223A RID: 8762
			DuplicateResubmitRequest = 2147769805U,
			// Token: 0x0400223B RID: 8763
			MaxRunningResubmitRequest,
			// Token: 0x0400223C RID: 8764
			MaxRecentResubmitRequest,
			// Token: 0x0400223D RID: 8765
			InterceptorAgentConfigurationLoadingError = 3221512617U,
			// Token: 0x0400223E RID: 8766
			InterceptorAgentConfigurationReplaced = 287146U,
			// Token: 0x0400223F RID: 8767
			InterceptorAgentAccessDenied = 3221512619U,
			// Token: 0x04002240 RID: 8768
			InterceptorRuleNearingExpiration = 2147770796U,
			// Token: 0x04002241 RID: 8769
			QueueQuotaComponentLogPathIsNull = 3221512622U,
			// Token: 0x04002242 RID: 8770
			NotEnoughMemoryToStartService,
			// Token: 0x04002243 RID: 8771
			FlowControlLogPathIsNull,
			// Token: 0x04002244 RID: 8772
			FilePathOnLockedVolume,
			// Token: 0x04002245 RID: 8773
			BitlockerQueryFailed
		}
	}
}
