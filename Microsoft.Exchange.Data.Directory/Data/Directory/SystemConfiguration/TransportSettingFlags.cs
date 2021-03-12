using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005DF RID: 1503
	[Flags]
	internal enum TransportSettingFlags
	{
		// Token: 0x04002FAD RID: 12205
		Empty = 0,
		// Token: 0x04002FAE RID: 12206
		KeepCategories = 1,
		// Token: 0x04002FAF RID: 12207
		LegacyArchiveLiveJournalingEnabled = 2,
		// Token: 0x04002FB0 RID: 12208
		AddressBookPolicyRoutingEnabled = 4,
		// Token: 0x04002FB1 RID: 12209
		VoicemailJournalingDisabled = 8,
		// Token: 0x04002FB2 RID: 12210
		DisableXexch50 = 16,
		// Token: 0x04002FB3 RID: 12211
		VerifySecureSubmitEnabled = 32,
		// Token: 0x04002FB4 RID: 12212
		JournalReportDLMemberSubstitutionEnabled = 64,
		// Token: 0x04002FB5 RID: 12213
		JournalArchivingEnabled = 128,
		// Token: 0x04002FB6 RID: 12214
		Rfc2231EncodingEnabled = 256,
		// Token: 0x04002FB7 RID: 12215
		ShadowRedundancyDisabled = 512,
		// Token: 0x04002FB8 RID: 12216
		OpenDomainRoutingEnabled = 1024,
		// Token: 0x04002FB9 RID: 12217
		ExternalDsnLanguageDetectionDisabled = 2048,
		// Token: 0x04002FBA RID: 12218
		ExternalDsnSendHtmlDisabled = 4096,
		// Token: 0x04002FBB RID: 12219
		InternalDelayDsnDisabled = 8192,
		// Token: 0x04002FBC RID: 12220
		InternalDsnLanguageDetectionDisabled = 16384,
		// Token: 0x04002FBD RID: 12221
		InternalDsnSendHtmlDisabled = 32768,
		// Token: 0x04002FBE RID: 12222
		ExternalDelayDsnDisabled = 65536,
		// Token: 0x04002FBF RID: 12223
		PreserveReportBodypart = 131072,
		// Token: 0x04002FC0 RID: 12224
		ConvertReportToMessage = 262144,
		// Token: 0x04002FC1 RID: 12225
		MigrationEnabled = 1048576,
		// Token: 0x04002FC2 RID: 12226
		HeaderPromotionModeSetting = 6291456,
		// Token: 0x04002FC3 RID: 12227
		LegacyJournalingMigrationEnabled = 8388608,
		// Token: 0x04002FC4 RID: 12228
		ConvertDisclaimerWrapperToEml = 16777216,
		// Token: 0x04002FC5 RID: 12229
		LegacyArchiveJournalingEnabled = 33554432,
		// Token: 0x04002FC6 RID: 12230
		RejectMessageOnShadowFailure = 67108864,
		// Token: 0x04002FC7 RID: 12231
		RedirectUnprovisionedUserMessagesForLegacyArchiveJournaling = 536870912,
		// Token: 0x04002FC8 RID: 12232
		ShadowMessagePreferenceSetting = 402653184,
		// Token: 0x04002FC9 RID: 12233
		RedirectDLMessagesForLegacyArchiveJournaling = 1073741824,
		// Token: 0x04002FCA RID: 12234
		All = 2146959359
	}
}
