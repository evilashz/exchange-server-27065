using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200000A RID: 10
	internal static class TransportRuleConstants
	{
		// Token: 0x0400001D RID: 29
		public const string EtrAgentName = "Transport Rule";

		// Token: 0x0400001E RID: 30
		public const string SclHeader = "X-MS-Exchange-Organization-SCL";

		// Token: 0x0400001F RID: 31
		public const string DlpPolicyIdRuleTagName = "CP";

		// Token: 0x04000020 RID: 32
		public const string DlpPolicyNameRuleTagName = "CPN";

		// Token: 0x04000021 RID: 33
		internal const string TagIsSameUser = "isSameUser";

		// Token: 0x04000022 RID: 34
		internal const string TagIsMemberOf = "isMemberOf";

		// Token: 0x04000023 RID: 35
		internal const string TagIsPartner = "isPartner";

		// Token: 0x04000024 RID: 36
		internal const string TagIsInternal = "isInternal";

		// Token: 0x04000025 RID: 37
		internal const string TagIsExternalPartner = "isExternalPartner";

		// Token: 0x04000026 RID: 38
		internal const string TagIsMessageType = "isMessageType";

		// Token: 0x04000027 RID: 39
		internal const string TagSenderAttributeContains = "senderAttributeContains";

		// Token: 0x04000028 RID: 40
		internal const string TagSenderAttributeMatches = "senderAttributeMatches";

		// Token: 0x04000029 RID: 41
		internal const string TagSenderAttributeMatchesRegex = "senderAttributeMatchesRegex";

		// Token: 0x0400002A RID: 42
		internal const string TagRecipientAttributeContains = "recipientAttributeContains";

		// Token: 0x0400002B RID: 43
		internal const string TagRecipientAttributeMatches = "recipientAttributeMatches";

		// Token: 0x0400002C RID: 44
		internal const string TagRecipientAttributeMatchesRegex = "recipientAttributeMatchesRegex";

		// Token: 0x0400002D RID: 45
		internal const string TagAttachmentContainsWords = "attachmentContainsWords";

		// Token: 0x0400002E RID: 46
		internal const string TagAttachmentMatchesPatterns = "attachmentMatchesPatterns";

		// Token: 0x0400002F RID: 47
		internal const string TagAttachmentMatchesRegexPatterns = "attachmentMatchesRegexPatterns";

		// Token: 0x04000030 RID: 48
		internal const string TagAttachmentIsUnsupported = "attachmentIsUnsupported";

		// Token: 0x04000031 RID: 49
		internal const string TagAttachmentIsPasswordProtected = "attachmentIsPasswordProtected";

		// Token: 0x04000032 RID: 50
		internal const string TagAttachmentPropertyContains = "attachmentPropertyContains";

		// Token: 0x04000033 RID: 51
		internal const string TagSenderInRecipientList = "senderInRecipientList";

		// Token: 0x04000034 RID: 52
		internal const string TagRecipientInSenderList = "recipientInSenderList";

		// Token: 0x04000035 RID: 53
		internal const string TagFork = "fork";

		// Token: 0x04000036 RID: 54
		internal const string TagRecipient = "recipient";

		// Token: 0x04000037 RID: 55
		internal const string TagFromRecipient = "fromRecipient";

		// Token: 0x04000038 RID: 56
		internal const string TagFromList = "fromList";

		// Token: 0x04000039 RID: 57
		internal const string TagManager = "manager";

		// Token: 0x0400003A RID: 58
		internal const string TagIsSenderEvaluation = "isSenderEvaluation";

		// Token: 0x0400003B RID: 59
		internal const string TagCheckADAttributeEquality = "checkADAttributeEquality";

		// Token: 0x0400003C RID: 60
		internal const string TagManagementRelationship = "managementRelationship";

		// Token: 0x0400003D RID: 61
		internal const string TagADAttribute = "adAttribute";

		// Token: 0x0400003E RID: 62
		internal const string TagADAttributeForTextMatch = "adAttributeForTextMatch";

		// Token: 0x0400003F RID: 63
		internal const string TagADAttributeValueForTextMatch = "adAttributeValueForTextMatch";

		// Token: 0x04000040 RID: 64
		internal const string TagPartner = "partner";

		// Token: 0x04000041 RID: 65
		internal const string TagExternal = "external";

		// Token: 0x04000042 RID: 66
		internal const string TagInternal = "internal";

		// Token: 0x04000043 RID: 67
		internal const string TagExternalPartner = "externalPartner";

		// Token: 0x04000044 RID: 68
		internal const string TagExternalNonPartner = "externalNonPartner";

		// Token: 0x04000045 RID: 69
		internal const string TagRecipientContainsWords = "recipientContainsWords";

		// Token: 0x04000046 RID: 70
		internal const string TagRecipientDomainIs = "recipientDomainIs";

		// Token: 0x04000047 RID: 71
		internal const string TagRecipientMatchesPatterns = "recipientMatchesPatterns";

		// Token: 0x04000048 RID: 72
		internal const string TagRecipientMatchesRegexPatterns = "recipientMatchesRegexPatterns";

		// Token: 0x04000049 RID: 73
		internal const string TagContainsDataClassification = "containsDataClassification";

		// Token: 0x0400004A RID: 74
		internal const string TagHasSenderOverride = "hasSenderOverride";

		// Token: 0x0400004B RID: 75
		internal const string TagIpMatch = "ipMatch";

		// Token: 0x0400004C RID: 76
		internal const string TagAttachmentProcessingLimitExceeded = "attachmentProcessingLimitExceeded";

		// Token: 0x0400004D RID: 77
		internal const string TagDomainIs = "domainIs";

		// Token: 0x0400004E RID: 78
		internal const string AttributeDomain = "domain";

		// Token: 0x0400004F RID: 79
		internal const string AttributeAddress = "address";

		// Token: 0x04000050 RID: 80
		internal const string AttributeValue = "value";

		// Token: 0x04000051 RID: 81
		internal const string True = "true";

		// Token: 0x04000052 RID: 82
		internal const string False = "false";

		// Token: 0x04000053 RID: 83
		internal const string Manager = "Manager";

		// Token: 0x04000054 RID: 84
		internal const string DirectReport = "DirectReport";

		// Token: 0x04000055 RID: 85
		internal const string SenderAddressLocation = "senderAddressLocation";

		// Token: 0x04000056 RID: 86
		internal const SenderAddressLocation DefaultSenderAddressLocation = Microsoft.Exchange.MessagingPolicies.Rules.SenderAddressLocation.Header;

		// Token: 0x04000057 RID: 87
		internal const string FromScopeExternalAuth = "<>";

		// Token: 0x04000058 RID: 88
		internal const string FromScopeInternalAuth = "FromInternal";

		// Token: 0x04000059 RID: 89
		internal const string DeleteMessage = "DeleteMessage";

		// Token: 0x0400005A RID: 90
		internal const string SetPriority = "SetPriority";

		// Token: 0x0400005B RID: 91
		internal const string SetExtendedPropertyString = "SetExtendedPropertyString";

		// Token: 0x0400005C RID: 92
		internal const string AddToRecipient = "AddToRecipient";

		// Token: 0x0400005D RID: 93
		internal const string AddCcRecipient = "AddCcRecipient";

		// Token: 0x0400005E RID: 94
		internal const string AddToRecipientSmtpOnly = "AddToRecipientSmtpOnly";

		// Token: 0x0400005F RID: 95
		internal const string AddCcRecipientSmtpOnly = "AddCcRecipientSmtpOnly";

		// Token: 0x04000060 RID: 96
		internal const string AddEnvelopeRecipient = "AddEnvelopeRecipient";

		// Token: 0x04000061 RID: 97
		internal const string AddManagerAsRecipientType = "AddManagerAsRecipientType";

		// Token: 0x04000062 RID: 98
		internal const string ModerateMessageByUser = "ModerateMessageByUser";

		// Token: 0x04000063 RID: 99
		internal const string ModerateMessageByManager = "ModerateMessageByManager";

		// Token: 0x04000064 RID: 100
		internal const string RedirectMessage = "RedirectMessage";

		// Token: 0x04000065 RID: 101
		internal const string AddHeader = "AddHeader";

		// Token: 0x04000066 RID: 102
		internal const string RemoveHeader = "RemoveHeader";

		// Token: 0x04000067 RID: 103
		internal const string SetSubject = "SetSubject";

		// Token: 0x04000068 RID: 104
		internal const string PrependSubject = "PrependSubject";

		// Token: 0x04000069 RID: 105
		internal const string Halt = "Halt";

		// Token: 0x0400006A RID: 106
		internal const string SetHeader = "SetHeader";

		// Token: 0x0400006B RID: 107
		internal const string SetHeaderUniqueValue = "SetHeaderUniqueValue";

		// Token: 0x0400006C RID: 108
		internal const string ApplyHtmlDisclaimer = "ApplyHtmlDisclaimer";

		// Token: 0x0400006D RID: 109
		internal const string ApplyDisclaimerWithSeparator = "ApplyDisclaimerWithSeparator";

		// Token: 0x0400006E RID: 110
		internal const string ApplyDisclaimer = "ApplyDisclaimer";

		// Token: 0x0400006F RID: 111
		internal const string ApplyDisclaimerWithSeparatorAndReadingOrder = "ApplyDisclaimerWithSeparatorAndReadingOrder";

		// Token: 0x04000070 RID: 112
		internal const string Quarantine = "Quarantine";

		// Token: 0x04000071 RID: 113
		internal const string Disconnect = "Disconnect";

		// Token: 0x04000072 RID: 114
		internal const string RejectMessage = "RejectMessage";

		// Token: 0x04000073 RID: 115
		internal const string LogEvent = "LogEvent";

		// Token: 0x04000074 RID: 116
		internal const string RightsProtectMessage = "RightsProtectMessage";

		// Token: 0x04000075 RID: 117
		internal const string RouteMessageOutboundRequireTls = "RouteMessageOutboundRequireTls";

		// Token: 0x04000076 RID: 118
		internal const string ApplyOME = "ApplyOME";

		// Token: 0x04000077 RID: 119
		internal const string RemoveOME = "RemoveOME";

		// Token: 0x04000078 RID: 120
		internal const string RouteMessageOutboundConnector = "RouteMessageOutboundConnector";

		// Token: 0x04000079 RID: 121
		internal const string MessageTypeOof = "OOF";

		// Token: 0x0400007A RID: 122
		internal const string MessageTypeAutoForward = "AutoForward";

		// Token: 0x0400007B RID: 123
		internal const string MessageTypeEncrypted = "Encrypted";

		// Token: 0x0400007C RID: 124
		internal const string MessageTypeCalendaring = "Calendaring";

		// Token: 0x0400007D RID: 125
		internal const string MessageTypePermissionControlled = "PermissionControlled";

		// Token: 0x0400007E RID: 126
		internal const string MessageTypeVoicemail = "Voicemail";

		// Token: 0x0400007F RID: 127
		internal const string MessageTypeSigned = "Signed";

		// Token: 0x04000080 RID: 128
		internal const string MessageTypeApprovalRequest = "ApprovalRequest";

		// Token: 0x04000081 RID: 129
		internal const string MessageTypeReadReceipt = "ReadReceipt";

		// Token: 0x04000082 RID: 130
		internal const string AuditSeverityLevel = "AuditSeverityLevel";

		// Token: 0x04000083 RID: 131
		internal const string SenderNotify = "SenderNotify";

		// Token: 0x04000084 RID: 132
		internal const string GenerateIncidentReport = "GenerateIncidentReport";

		// Token: 0x04000085 RID: 133
		internal const string GenerateNotification = "GenerateNotification";

		// Token: 0x04000086 RID: 134
		internal const string ClassificationGuidHeader = "X-MS-Exchange-Organization-Classification";

		// Token: 0x04000087 RID: 135
		internal const string XMSExchangeFfoAttributedTenantId = "X-MS-Exchange-Organization-Id";

		// Token: 0x04000088 RID: 136
		internal static readonly Version VersionedContainerBaseVersion = new Version("14.00.0000.00");

		// Token: 0x0200000B RID: 11
		public static class FileTypeNames
		{
			// Token: 0x04000089 RID: 137
			public const string FileTypeExecutable = "executable";

			// Token: 0x0400008A RID: 138
			public const string FileTypeUnknown = "unknown";
		}

		// Token: 0x0200000C RID: 12
		public static class PropertyNames
		{
			// Token: 0x0400008B RID: 139
			public const string AttachmentNames = "Message.AttachmentNames";

			// Token: 0x0400008C RID: 140
			public const string AttachmentExtensions = "Message.AttachmentExtensions";

			// Token: 0x0400008D RID: 141
			public const string AttachmentTypes = "Message.AttachmentTypes";

			// Token: 0x0400008E RID: 142
			public const string MaxAttachmentSize = "Message.MaxAttachmentSize";

			// Token: 0x0400008F RID: 143
			public const string ContentCharacterSets = "Message.ContentCharacterSets";

			// Token: 0x04000090 RID: 144
			public const string SenderDomain = "Message.SenderDomain";
		}

		// Token: 0x0200000D RID: 13
		public static class AuditingValues
		{
			// Token: 0x04000091 RID: 145
			public const string RuleId = "ruleId";

			// Token: 0x04000092 RID: 146
			public const string DlpProgramId = "dlpId";

			// Token: 0x04000093 RID: 147
			public const string MatchingDataClassificationId = "dcId";

			// Token: 0x04000094 RID: 148
			public const string WhenChangedUTC = "st";

			// Token: 0x04000095 RID: 149
			public const string Action = "action";

			// Token: 0x04000096 RID: 150
			public const string SenderOverridden = "sndOverride";

			// Token: 0x04000097 RID: 151
			public const string SenderOverriddenValue = "or";

			// Token: 0x04000098 RID: 152
			public const string SenderOverriddenJustification = "just";

			// Token: 0x04000099 RID: 153
			public const string SenderOverriddenFpValue = "fp";

			// Token: 0x0400009A RID: 154
			public const string Severity = "sev";

			// Token: 0x0400009B RID: 155
			public const string DataClassificationId = "dcid";

			// Token: 0x0400009C RID: 156
			public const string DataClassificationCount = "count";

			// Token: 0x0400009D RID: 157
			public const string DataClassificationConfidence = "conf";

			// Token: 0x0400009E RID: 158
			public const string RuleMode = "mode";

			// Token: 0x0400009F RID: 159
			public const string LoadW = "LoadW";

			// Token: 0x040000A0 RID: 160
			public const string LoadC = "Loadc";

			// Token: 0x040000A1 RID: 161
			public const string ExecW = "ExecW";

			// Token: 0x040000A2 RID: 162
			public const string ExecC = "ExecC";
		}
	}
}
