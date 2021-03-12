using System;

namespace Microsoft.Exchange.Data.Mime.Internal
{
	// Token: 0x02000011 RID: 17
	internal static class MimeConstant
	{
		// Token: 0x0400003B RID: 59
		public const string XMSExchangeOrganizationMBTSubmissionProcessedMessage = "X-MS-Exchange-Organization-Processed-By-MBTSubmission";

		// Token: 0x0400003C RID: 60
		public const string XMSExchangeOrganizationJournalingProcessedMessage = "X-MS-Exchange-Organization-Processed-By-Journaling";

		// Token: 0x0400003D RID: 61
		public const string XMSExchangeOrganizationGccJournalingProcessedMessage = "X-MS-Exchange-Organization-Processed-By-Gcc-Journaling";

		// Token: 0x0400003E RID: 62
		public const string XMSExchangeOrganizationProcessedByUnjournal = "X-MS-Exchange-Organization-Unjournal-Processed";

		// Token: 0x0400003F RID: 63
		public const string XMSExchangeOrganizationSenderIsRecipient = "X-MS-Exchange-Organization-Unjournal-SenderIsRecipient";

		// Token: 0x04000040 RID: 64
		public const string XMSExchangeOrganizationSenderAddress = "X-MS-Exchange-Organization-Unjournal-SenderAddress";

		// Token: 0x04000041 RID: 65
		public const string XMSExchangeOrganizationProcessedByUnjournalForNdr = "X-MS-Exchange-Organization-Unjournal-ProcessedNdr";

		// Token: 0x04000042 RID: 66
		public const string XMSExchangeOrganizationMessageOriginalDate = "X-MS-Exchange-Organization-Unjournal-OriginalReceiveDate";

		// Token: 0x04000043 RID: 67
		public const string XMSExchangeOrganizationMessageExpiryDate = "X-MS-Exchange-Organization-Unjournal-OriginalExpiryDate";

		// Token: 0x04000044 RID: 68
		public const string XMSInternalJournalReport = "X-MS-InternalJournal";

		// Token: 0x04000045 RID: 69
		public const string XMSExchangeOrganizationGeneratedMessageSource = "X-MS-Exchange-Organization-Generated-Message-Source";

		// Token: 0x04000046 RID: 70
		public const string XMSExchangeGeneratedMessageSource = "X-MS-Exchange-Generated-Message-Source";

		// Token: 0x04000047 RID: 71
		public const string XMSExchangeParentMessageId = "X-MS-Exchange-Parent-Message-Id";

		// Token: 0x04000048 RID: 72
		public const string XMSExchangeMessageIsNdr = "X-MS-Exchange-Message-Is-Ndr";

		// Token: 0x04000049 RID: 73
		public const string XMSExchangeOrganizationFfoServiceTagHeader = "X-MS-Exchange-Organization-FFO-ServiceTag";

		// Token: 0x0400004A RID: 74
		public const string XMSExchangeOrganizationCrossPremiseEncrypted = "X-MS-Exchange-Organization-CrossPremiseEncrypted";

		// Token: 0x0400004B RID: 75
		public const string XMSExchangeOrganizationCrossPremiseDecrypted = "X-MS-Exchange-Organization-CrossPremiseDecrypted";

		// Token: 0x0400004C RID: 76
		public const string XMSExchangeOrganizationApaExecuted = "X-MS-Exchange-Organization-MessageProtectedByApa";

		// Token: 0x0400004D RID: 77
		public const string XMSExchangeOrganizationJournalNdrSkipTransportMailboxRules = "X-MS-Exchange-Organization-JournalNdr-Skip-TransportMailboxRules";

		// Token: 0x0400004E RID: 78
		public const string XMSExchangeOrganizationMalwareFilterPolicy = "X-MS-Exchange-Organization-MalwareFilterPolicy";

		// Token: 0x0400004F RID: 79
		public const string XHeaderDecryptCrossPremise = "X-Decrypt-CrossPremise";

		// Token: 0x04000050 RID: 80
		public const string TestMessageHeader = "X-MS-Exchange-Organization-Test-Message";

		// Token: 0x04000051 RID: 81
		public const string TestMessageLogForHeader = "X-MS-Exchange-Organization-Test-Message-Log-For";

		// Token: 0x04000052 RID: 82
		public const string TestMessageSupressValue = "Supress";

		// Token: 0x04000053 RID: 83
		public const string TestMessageDeliverValue = "Deliver";

		// Token: 0x04000054 RID: 84
		public const string TestMessageSendReportToHeader = "X-MS-Exchange-Organization-Test-Message-Send-Report-To";

		// Token: 0x04000055 RID: 85
		public const string TestMessageOptionsHeader = "X-MS-Exchange-Organization-Test-Message-Options";

		// Token: 0x04000056 RID: 86
		public const string XMSExchangeOrganizationDeliveryFolder = "X-MS-Exchange-Organization-DeliveryFolder";

		// Token: 0x04000057 RID: 87
		public const string XMSExchangeOrganizationStorageQuota = "X-MS-Exchange-Organization-StorageQuota";

		// Token: 0x04000058 RID: 88
		public const string EndOfInjectedXHeaders = "X-EndOfInjectedXHeaders";

		// Token: 0x04000059 RID: 89
		public const string XCreatedBy = "X-CreatedBy";

		// Token: 0x0400005A RID: 90
		public const string XSender = "X-Sender";

		// Token: 0x0400005B RID: 91
		public const string XReceiver = "X-Receiver";

		// Token: 0x0400005C RID: 92
		public const string MSExchange12 = "MSExchange12";

		// Token: 0x0400005D RID: 93
		public const string MicrosoftApprovalRequestRecallClass = "IPM.Note.Microsoft.Approval.Request.Recall";

		// Token: 0x0400005E RID: 94
		public const string XAutoResponseSuppress = "X-Auto-Response-Suppress";

		// Token: 0x0400005F RID: 95
		public const string XModerationData = "X-Moderation-Data";

		// Token: 0x04000060 RID: 96
		public const string XSendOutlookRecallReport = "X-MS-Exchange-Send-Outlook-Recall-Report";

		// Token: 0x04000061 RID: 97
		public const string XOrganizationHygienePolicy = "X-MS-Exchange-Organization-HygienePolicy";

		// Token: 0x04000062 RID: 98
		public const string XMSExchangeOrganizationHygieneReleasedFromQuarantine = "X-MS-Exchange-Organization-Hygiene-ReleasedFromQuarantine";

		// Token: 0x04000063 RID: 99
		public const string HeaderXMsReplyToMobile = "X-MS-Reply-To-Mobile";

		// Token: 0x04000064 RID: 100
		public const string XOriginatingIP = "X-Originating-IP";

		// Token: 0x04000065 RID: 101
		internal const string XMSTnefCorrelator = "X-MS-TNEF-Correlator";

		// Token: 0x04000066 RID: 102
		internal const string ThreadIndex = "Thread-Index";

		// Token: 0x04000067 RID: 103
		internal const string ThreadTopic = "Thread-Topic";

		// Token: 0x04000068 RID: 104
		internal const string TransportFlagMustDeliver = "MustDeliver";

		// Token: 0x04000069 RID: 105
		internal const string DeliveryPriority = "DeliveryPriority";

		// Token: 0x0400006A RID: 106
		internal const string AcceptLanguage = "Accept-Language";

		// Token: 0x0400006B RID: 107
		internal const string XMSExchangeOrganizationScl = "X-MS-Exchange-Organization-SCL";

		// Token: 0x0400006C RID: 108
		internal const string XMSExchangeOrganizationPcl = "X-MS-Exchange-Organization-PCL";

		// Token: 0x0400006D RID: 109
		internal const string XMSExchangeOrganizationSenderIdResult = "X-MS-Exchange-Organization-SenderIdResult";

		// Token: 0x0400006E RID: 110
		internal const string XMSExchangeOrganizationPrd = "X-MS-Exchange-Organization-PRD";

		// Token: 0x0400006F RID: 111
		internal const string XMSExchangeOrganizationAntiSpamReport = "X-MS-Exchange-Organization-Antispam-Report";

		// Token: 0x04000070 RID: 112
		internal const string XMSExchangeOrganizationDsnVersion = "X-MS-Exchange-Organization-Dsn-Version";

		// Token: 0x04000071 RID: 113
		internal const string XMSExchangeOrganizationOriginalSize = "X-MS-Exchange-Organization-OriginalSize";

		// Token: 0x04000072 RID: 114
		internal const string XMSExchangeOrganizationContentConversionOptions = "X-MS-Exchange-Organization-ContentConversionOptions";

		// Token: 0x04000073 RID: 115
		internal const string XMSExchangeOrganizationMessageSource = "X-MS-Exchange-Organization-MessageSource";

		// Token: 0x04000074 RID: 116
		internal const string XMSExchangeOrganizationPrioritization = "X-MS-Exchange-Organization-Prioritization";

		// Token: 0x04000075 RID: 117
		internal const string XMSExchangeOrganizationSpamFilterEnumeratedRisk = "X-MS-Exchange-Organization-Spam-Filter-Enumerated-Risk";

		// Token: 0x04000076 RID: 118
		internal const string XMSExchangeOrganizationAuthAs = "X-MS-Exchange-Organization-AuthAs";

		// Token: 0x04000077 RID: 119
		internal const string XMSExchangeOrganizationAuthDomain = "X-MS-Exchange-Organization-AuthDomain";

		// Token: 0x04000078 RID: 120
		internal const string XMSExchangeOrganizationAuthMechanism = "X-MS-Exchange-Organization-AuthMechanism";

		// Token: 0x04000079 RID: 121
		internal const string XMSExchangeOrganizationAuthSource = "X-MS-Exchange-Organization-AuthSource";

		// Token: 0x0400007A RID: 122
		internal const string XMSExchangeOrganizationAntispamIPv6Check = "X-MS-Exchange-Organization-Antispam-IPv6Check";

		// Token: 0x0400007B RID: 123
		internal const string XMSExchangeOrganizationOutboundIPPartition = "X-MS-Exchange-Organization-Antispam-OutboundIPPartition";

		// Token: 0x0400007C RID: 124
		public const string XMSExchangeAntispamAsyncContext = "X-MS-Exchange-Organization-Antispam-AsyncContext";

		// Token: 0x0400007D RID: 125
		public const string XMSExchangeAntispamContentFilterScanContext = "X-MS-Exchange-Organization-Antispam-ContentFilter-ScanContext";

		// Token: 0x0400007E RID: 126
		public const string XMSExchangeAntispamScanContext = "X-MS-Exchange-Organization-Antispam-ScanContext";

		// Token: 0x0400007F RID: 127
		public const string AntispamScanContextXPremTagName = "XPREM";

		// Token: 0x04000080 RID: 128
		public const char AntispamScanContextNameValuePairSeparator = ';';

		// Token: 0x04000081 RID: 129
		public const char AntispamScanContextNameValueSeparator = ':';

		// Token: 0x04000082 RID: 130
		internal const string XMSExchangeOrganizationRecipientP2Type = "X-MS-Exchange-Organization-Recipient-P2-Type";

		// Token: 0x04000083 RID: 131
		internal const string XMSExchangeOrganizationNetworkMessageId = "X-MS-Exchange-Organization-Network-Message-Id";

		// Token: 0x04000084 RID: 132
		internal const string XMSExchangeOrganizationHistory = "X-MS-Exchange-Organization-History";

		// Token: 0x04000085 RID: 133
		internal const string XMSExchangeOrganizationDisclaimerHash = "X-MS-Exchange-Organization-Disclaimer-Hash";

		// Token: 0x04000086 RID: 134
		internal const string XMSExchangeDisclaimerWrapperHeader = "X-MS-Exchange-Organization-Disclaimer-Wrapper";

		// Token: 0x04000087 RID: 135
		internal const string XMSExchangeOrganizationRulesExecutionHistoryHeader = "X-MS-Exchange-Organization-Rules-Execution-History";

		// Token: 0x04000088 RID: 136
		internal const string XMSExchangeForestSkipRuleExecution = "X-MS-Exchange-Forest-RulesExecuted";

		// Token: 0x04000089 RID: 137
		internal const string XMSExchangeTransportRulesLoop = "X-MS-Exchange-Transport-Rules-Loop";

		// Token: 0x0400008A RID: 138
		internal const string XMSExchangeTransportRulesDeferCount = "X-MS-Exchange-Transport-Rules-Defer-Count";

		// Token: 0x0400008B RID: 139
		internal const string XMSExchangeOrganizationTransportRulesFipsResult = "X-MS-Exchange-Organization-Transport-Rules-Fips-Result";

		// Token: 0x0400008C RID: 140
		internal const string XMSExchangeTransportRulesIncidentReport = "X-MS-Exchange-Transport-Rules-IncidentReport";

		// Token: 0x0400008D RID: 141
		internal const string XMSExchangeTransportRulesNotification = "X-MS-Exchange-Transport-Rules-Notification";

		// Token: 0x0400008E RID: 142
		internal const string XMSExchangeModerationLoop = "X-MS-Exchange-Moderation-Loop";

		// Token: 0x0400008F RID: 143
		internal const string XMSExchangeJournalReportHeader = "X-MS-Exchange-Organization-Journal-Report";

		// Token: 0x04000090 RID: 144
		internal const string XMSExchangeOrganizationBcc = "X-MS-Exchange-Organization-BCC";

		// Token: 0x04000091 RID: 145
		internal const string XMSExchangeJournalReportDecryptionProcessedHeader = "X-MS-Exchange-Organization-JournalReportDecryption-Processed";

		// Token: 0x04000092 RID: 146
		internal const string XMSExchangeJournaledToRecipients = "X-MS-Exchange-Organization-Journaled-To-Recipients";

		// Token: 0x04000093 RID: 147
		internal const string XMSExchangeJournalingRemoteAccounts = "X-MS-Exchange-Organization-Journaling-Remote-Accounts";

		// Token: 0x04000094 RID: 148
		internal const string XMSExchangeDoNotJournal = "X-MS-Exchange-Organization-Do-Not-Journal";

		// Token: 0x04000095 RID: 149
		internal const string XMSExchangeMapiAdminSubmission = "X-MS-Exchange-Organization-Mapi-Admin-Submission";

		// Token: 0x04000096 RID: 150
		internal const string XMSExchangeTransportPropertiesHeader = "X-MS-Exchange-Organization-Transport-Properties";

		// Token: 0x04000097 RID: 151
		internal const string XMSExchangeOrganizationId = "X-MS-Exchange-Organization-Id";

		// Token: 0x04000098 RID: 152
		internal const string XMSExchangeMatchingConnectorTenantId = "X-MS-Exchange-Organization-MatchingConnector-TenantId";

		// Token: 0x04000099 RID: 153
		internal const string XMSExchangeOrganizationDirectionalityHeader = "X-MS-Exchange-Organization-MessageDirectionality";

		// Token: 0x0400009A RID: 154
		internal const string XMSExchangeOrganizationInboundConnectorTypeHeader = "X-MS-Exchange-Organization-MessageInboundConnectorType";

		// Token: 0x0400009B RID: 155
		internal const string XMSExchangeOrganizationConnectorInfo = "X-MS-Exchange-Organization-ConnectorInfo";

		// Token: 0x0400009C RID: 156
		internal const string XMSExchangeOrganizationScopeHeader = "X-MS-Exchange-Organization-MessageScope";

		// Token: 0x0400009D RID: 157
		internal const string XMSExchangeForestScopeHeader = "X-MS-Exchange-Forest-MessageScope";

		// Token: 0x0400009E RID: 158
		internal const string XMSExchangeForestTransportDecryptionActionHeader = "X-MS-Exchange-Forest-TransportDecryption-Action";

		// Token: 0x0400009F RID: 159
		internal const string XMSExchangeOrganizationOriginalClientIPAddress = "X-MS-Exchange-Organization-OriginalClientIPAddress";

		// Token: 0x040000A0 RID: 160
		internal const string XMSExchangeOrganizationOriginalServerIPAddress = "X-MS-Exchange-Organization-OriginalServerIPAddress";

		// Token: 0x040000A1 RID: 161
		internal const string XMSQuarantineOriginalSenderHeader = "X-MS-Exchange-Organization-Original-Sender";

		// Token: 0x040000A2 RID: 162
		internal const string XMSJournalReportHeader = "X-MS-Journal-Report";

		// Token: 0x040000A3 RID: 163
		internal const string XMSGccJournalReportHeader = "X-MS-Gcc-Journal-Report";

		// Token: 0x040000A4 RID: 164
		internal const string XMSExchangeQuarantineMessageMarkerHeader = "X-MS-Exchange-Organization-Quarantine";

		// Token: 0x040000A5 RID: 165
		internal const string XMSExchangeOrganizationOriginalScl = "X-MS-Exchange-Organization-Original-SCL";

		// Token: 0x040000A6 RID: 166
		internal const string XMSExchangeOrganizationDlExpansionProhibited = "X-MS-Exchange-Organization-DL-Expansion-Prohibited";

		// Token: 0x040000A7 RID: 167
		internal const string XMSExchangeOrganizationAltRecipientProhibited = "X-MS-Exchange-Organization-Alt-Recipient-Prohibited";

		// Token: 0x040000A8 RID: 168
		internal const string XMSExchangeOrganizationJournalRecipientList = "X-MS-Exchange-Organization-JournalRecipientList";

		// Token: 0x040000A9 RID: 169
		internal const string TransportLabelHeader = "X-MS-Exchange-Organization-Classification";

		// Token: 0x040000AA RID: 170
		internal const string ClassifiedHeader = "x-microsoft-classified";

		// Token: 0x040000AB RID: 171
		internal const string ClassificationHeader = "x-microsoft-classification";

		// Token: 0x040000AC RID: 172
		internal const string ClassDescHeader = "x-microsoft-classDesc";

		// Token: 0x040000AD RID: 173
		internal const string ClassIDHeader = "x-microsoft-classID";

		// Token: 0x040000AE RID: 174
		internal const string ClassificationKeep = "X-microsoft-classKeep";

		// Token: 0x040000AF RID: 175
		internal const string StoreDriver = "StoreDriver";

		// Token: 0x040000B0 RID: 176
		internal const string XMSExchangeOrganizationDecisionMakersHeader = "X-MS-Exchange-Organization-Approval-Allowed-Decision-Makers";

		// Token: 0x040000B1 RID: 177
		internal const string XMSExchangeOrganizationAllowedActionsHeader = "X-MS-Exchange-Organization-Approval-Allowed-Actions";

		// Token: 0x040000B2 RID: 178
		internal const string XMSExchangeOrganizationApprovalRequestorHeader = "X-MS-Exchange-Organization-Approval-Requestor";

		// Token: 0x040000B3 RID: 179
		internal const string XMSExchangeOrganizationApprovalInitiatorHeader = "X-MS-Exchange-Organization-Approval-Initiator";

		// Token: 0x040000B4 RID: 180
		internal const string XMSExchangeOrganizationApprovalAttachToApprovalRequestHeader = "X-MS-Exchange-Organization-Approval-AttachToApprovalRequest";

		// Token: 0x040000B5 RID: 181
		internal const string XMSExchangeOrganizationApprovalApprovedHeader = "X-MS-Exchange-Organization-Approval-Approved";

		// Token: 0x040000B6 RID: 182
		internal const string XMSExchangeOrganizationBypassChildModerationHeader = "X-MS-Exchange-Organization-Bypass-Child-Moderation";

		// Token: 0x040000B7 RID: 183
		internal const string XMSExchangeOrganizationModerationSavedArrivalTimeHeader = "X-MS-Exchange-Organization-Moderation-SavedArrivalTime";

		// Token: 0x040000B8 RID: 184
		internal const string XMSExchangeOrganizationModerationData = "X-MS-Exchange-Organization-Moderation-Data";

		// Token: 0x040000B9 RID: 185
		internal const char ModerationSuppressNotification = '0';

		// Token: 0x040000BA RID: 186
		internal const char ModerationEnableNotification = '1';

		// Token: 0x040000BB RID: 187
		internal const string XMSExchangeOrganizationRightsProtectMessage = "X-MS-Exchange-Organization-RightsProtectMessage";

		// Token: 0x040000BC RID: 188
		internal const string XMSExchangeOrganizationE4eEncryptMessage = "X-MS-Exchange-Organization-E4eEncryptMessage";

		// Token: 0x040000BD RID: 189
		internal const string XMSExchangeOrganizationE4eDecryptMessage = "X-MS-Exchange-Organization-E4eDecryptMessage";

		// Token: 0x040000BE RID: 190
		internal const string XMSExchangeOrganizationE4eMessageOriginalSender = "X-MS-Exchange-Organization-E4eMessageOriginalSender";

		// Token: 0x040000BF RID: 191
		internal const string XMSExchangeOrganizationE4eMessageOriginalSenderOrgId = "X-MS-Exchange-Organization-E4eMessageOriginalSenderOrgId";

		// Token: 0x040000C0 RID: 192
		internal const string XMSExchangeOrganizationE4eMessageDecrypted = "X-MS-Exchange-Organization-E4eMessageDecrypted";

		// Token: 0x040000C1 RID: 193
		internal const string XMSExchangeOrganizationE4eMessageEncrypted = "X-MS-Exchange-Organization-E4eMessageEncrypted";

		// Token: 0x040000C2 RID: 194
		internal const string XMSExchangeOrganizationE4eHtmlFileGenerated = "X-MS-Exchange-Organization-E4eHtmlFileGenerated";

		// Token: 0x040000C3 RID: 195
		internal const string XMSExchangeOrganizationE4ePortal = "X-MS-Exchange-Organization-E4ePortal";

		// Token: 0x040000C4 RID: 196
		internal const string XMSExchangeOrganizationE4eReEncryptMessage = "X-MS-Exchange-Organization-E4eReEncryptMessage";

		// Token: 0x040000C5 RID: 197
		internal const string XMSExchangeOMEMessageEncrypted = "X-MS-Exchange-OMEMessageEncrypted";

		// Token: 0x040000C6 RID: 198
		internal const string XMSExchangeOutlookProtectionRuleVersion = "X-MS-Exchange-Organization-Outlook-Protection-Rule-Addin-Version";

		// Token: 0x040000C7 RID: 199
		internal const string XMSExchangeOutlookProtectionRuleConfigTimestamp = "X-MS-Exchange-Organization-Outlook-Protection-Rule-Config-Timestamp";

		// Token: 0x040000C8 RID: 200
		internal const string XMSExchangeOutlookProtectionRuleOverridden = "X-MS-Exchange-Organization-Outlook-Protection-Rule-Overridden";

		// Token: 0x040000C9 RID: 201
		internal const string XMSExchangeOrganizationContentConvertInternalMessage = "X-MS-Exchange-Organization-ContentConvertInternalMessage";

		// Token: 0x040000CA RID: 202
		internal const string XMSExchangeForestArrivalHubServer = "X-MS-Exchange-Forest-ArrivalHubServer";

		// Token: 0x040000CB RID: 203
		internal const string XMSExchangeOrganizationOriginatorOrganization = "X-MS-Exchange-Organization-OriginatorOrganization";

		// Token: 0x040000CC RID: 204
		internal const string XMSExchangeForestRoutedForHighAvailability = "X-MS-Exchange-Forest-RoutedForHighAvailability";

		// Token: 0x040000CD RID: 205
		internal const string XMSExchangeDeliveryDatabase = "X-MS-Exchange-Delivery-Database";

		// Token: 0x040000CE RID: 206
		internal const string XMSExchangeItemDeliveryDatabaseName = "X-MS-Exchange-Delivery-Database-Name";

		// Token: 0x040000CF RID: 207
		internal const string XMSExchangeOrganizationAVStampServiceName = "X-MS-Exchange-Organization-AVStamp-Service";

		// Token: 0x040000D0 RID: 208
		internal const string XMSExchangeOrganizationAVStampEnterpriseName = "X-MS-Exchange-Organization-AVStamp-Enterprise";

		// Token: 0x040000D1 RID: 209
		internal const string XMSExchangeOrganizationAVScannedByV2Stamp = "X-MS-Exchange-Organization-AVScannedByV2";

		// Token: 0x040000D2 RID: 210
		internal const string XMSExchangeOrganizationMatchedInterceptorRule = "X-MS-Exchange-Organization-Matched-Interceptor-Rule";

		// Token: 0x040000D3 RID: 211
		internal const string EncryptedSystemProbeGuidHeader = "X-FFOSystemProbe";

		// Token: 0x040000D4 RID: 212
		internal const string UnencryptedSystemProbeGuidHeader = "X-LAMNotificationId";

		// Token: 0x040000D5 RID: 213
		internal const string XMSExchangeOrganizationOutboundConnector = "X-MS-Exchange-Organization-OutboundConnector";

		// Token: 0x040000D6 RID: 214
		internal const string XMSExchangeOrganizationCatchAllOriginalRecipients = "X-MS-Exchange-Organization-CatchAll-OriginalRecipients";

		// Token: 0x040000D7 RID: 215
		internal const string XMSExchangeActiveMonitoringProbeName = "X-MS-Exchange-ActiveMonitoringProbeName";

		// Token: 0x040000D8 RID: 216
		internal const string ProbeMessageDropHeader = "X-Exchange-Probe-Drop-Message";

		// Token: 0x040000D9 RID: 217
		internal const string SystemProbeDropHeader = "X-Exchange-System-Probe-Drop";

		// Token: 0x040000DA RID: 218
		internal const string ProbeMessageDropHeaderValueEOH = "FrontEnd-EOH-250";

		// Token: 0x040000DB RID: 219
		internal const string ProbeMessageDropHeaderValueCAT = "FrontEnd-CAT-250";

		// Token: 0x040000DC RID: 220
		internal const string ProbeMessageDropHeaderValueOnCategorizedMessage = "OnCategorizedMessage";

		// Token: 0x040000DD RID: 221
		internal const string ProbeMessageDropHeaderValueOnEndOfHeaders = "OnEndOfHeaders";

		// Token: 0x040000DE RID: 222
		internal const string ProbeMessageDropHeaderValueSDD = "MailboxTransportDelivery-SDD-250";

		// Token: 0x040000DF RID: 223
		internal const string PersistProbeTraceHeader = "X-Exchange-Persist-Probe-Trace";

		// Token: 0x040000E0 RID: 224
		internal const string XMSExchangeForestGalScope = "X-MS-Exchange-Forest-GAL-Scope";

		// Token: 0x040000E1 RID: 225
		internal const string XMSExchangeAddressBookPolicy = "X-MS-Exchange-ABP-GUID";

		// Token: 0x040000E2 RID: 226
		public static readonly string AntispamScanContextXPremTagNameWithSeparator = "XPREM" + ':';

		// Token: 0x040000E3 RID: 227
		internal static readonly string[] XOriginatorOrganization = new string[]
		{
			"X-OriginatorOrg",
			"X-OriginatorOrganization"
		};

		// Token: 0x040000E4 RID: 228
		internal static readonly string PreferredXOriginatorOrganization = MimeConstant.XOriginatorOrganization[0];

		// Token: 0x02000012 RID: 18
		public enum ApprovalAllowedAction
		{
			// Token: 0x040000E6 RID: 230
			ApproveReject,
			// Token: 0x040000E7 RID: 231
			ApproveRejectComments,
			// Token: 0x040000E8 RID: 232
			ApproveRejectCommentOnApprove,
			// Token: 0x040000E9 RID: 233
			ApproveRejectCommentOnReject
		}

		// Token: 0x02000013 RID: 19
		public class MultiLevelAuthHeaderValue
		{
			// Token: 0x040000EA RID: 234
			public const string Anonymous = "Anonymous";
		}

		// Token: 0x02000014 RID: 20
		public class ApprovalAttachHeaderValue
		{
			// Token: 0x040000EB RID: 235
			public const string Never = "Never";

			// Token: 0x040000EC RID: 236
			public const string AsIs = "AsIs";

			// Token: 0x040000ED RID: 237
			public const string AsMessage = "AsMessage";
		}
	}
}
