using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005E2 RID: 1506
	internal class ADAMTransportConfigContainerSchema : ADContainerSchema
	{
		// Token: 0x04002FD4 RID: 12244
		internal const int DefaultShadowHeartbeatRetryCount = 12;

		// Token: 0x04002FD5 RID: 12245
		internal const int DefaultMaxRetriesForRemoteSiteShadow = 4;

		// Token: 0x04002FD6 RID: 12246
		internal const int DefaultMaxRetriesForLocalSiteShadow = 2;

		// Token: 0x04002FD7 RID: 12247
		internal static readonly EnhancedTimeSpan DefaultShadowHeartbeatTimeoutInterval = EnhancedTimeSpan.FromMinutes(15.0);

		// Token: 0x04002FD8 RID: 12248
		internal static readonly EnhancedTimeSpan DefaultShadowMessageAutoDiscardInterval = EnhancedTimeSpan.FromDays(2.0);

		// Token: 0x04002FD9 RID: 12249
		public static readonly ADPropertyDefinition TLSReceiveDomainSecureList = new ADPropertyDefinition("TLSReceiveDomainSecureList", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomain), "msExchTLSReceiveDomainSecureList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FDA RID: 12250
		public static readonly ADPropertyDefinition TLSSendDomainSecureList = new ADPropertyDefinition("TLSSendDomainSecureList", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomain), "msExchTLSSendDomainSecureList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FDB RID: 12251
		public static readonly ADPropertyDefinition GenerateCopyOfDSNFor = new ADPropertyDefinition("GenerateCopyOfDSNFor", ExchangeObjectVersion.Exchange2007, typeof(EnhancedStatusCode), "msExchDSNSendCopyToAdmin", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FDC RID: 12252
		public static readonly ADPropertyDefinition InternalSMTPServers = new ADPropertyDefinition("InternalSMTPServers", ExchangeObjectVersion.Exchange2007, typeof(IPRange), "msExchInternalSMTPServers", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FDD RID: 12253
		public static readonly ADPropertyDefinition JournalingReportNdrTo = new ADPropertyDefinition("JournalingReportNdrTo", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress), "msExchJournalingReportNdrTo", ADPropertyDefinitionFlags.None, SmtpAddress.NullReversePath, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FDE RID: 12254
		public static readonly ADPropertyDefinition Flags = new ADPropertyDefinition("TransportSettingsFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportSettingsFlags", ADPropertyDefinitionFlags.PersistDefaultValue, TopologyProvider.IsAdamTopology() ? 512 : 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FDF RID: 12255
		public static readonly ADPropertyDefinition VerifySecureSubmitEnabled = new ADPropertyDefinition("VerifySecureSubmitEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 32), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 32), null, null);

		// Token: 0x04002FE0 RID: 12256
		public static readonly ADPropertyDefinition KeepCategories = new ADPropertyDefinition("KeepCategories", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 1), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 1), null, null);

		// Token: 0x04002FE1 RID: 12257
		public static readonly ADPropertyDefinition AddressBookPolicyRoutingEnabled = new ADPropertyDefinition("AddressBookPolicyRoutingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 4), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 4), null, null);

		// Token: 0x04002FE2 RID: 12258
		public static readonly ADPropertyDefinition ConvertDisclaimerWrapperToEml = new ADPropertyDefinition("ConvertDisclaimerWrapperToEml", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 16777216), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 16777216), null, null);

		// Token: 0x04002FE3 RID: 12259
		public static readonly ADPropertyDefinition VoicemailJournalingDisabled = new ADPropertyDefinition("VoicemailJournalingDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 8), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 8), null, null);

		// Token: 0x04002FE4 RID: 12260
		public static readonly ADPropertyDefinition HeaderPromotionModeSetting = new ADPropertyDefinition("HeaderPromotionModeSetting", ExchangeObjectVersion.Exchange2007, typeof(HeaderPromotionMode), null, ADPropertyDefinitionFlags.Calculated, HeaderPromotionMode.NoCreate, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, new GetterDelegate(TransportConfigContainer.InternalHeaderPromotionModeGetter), new SetterDelegate(TransportConfigContainer.InternalHeaderPromotionModeSetter), null, null);

		// Token: 0x04002FE5 RID: 12261
		public static readonly ADPropertyDefinition PreserveReportBodypart = new ADPropertyDefinition("PreserveReportBodypart", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 131072), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 131072), null, null);

		// Token: 0x04002FE6 RID: 12262
		public static readonly ADPropertyDefinition ConvertReportToMessage = new ADPropertyDefinition("ConvertReportToMessage", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 262144), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 262144), null, null);

		// Token: 0x04002FE7 RID: 12263
		public static readonly ADPropertyDefinition DSNConversionMode = new ADPropertyDefinition("DSNConversionMode", ExchangeObjectVersion.Exchange2007, typeof(DSNConversionOption), null, ADPropertyDefinitionFlags.Calculated, DSNConversionOption.UseExchangeDSNs, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, new GetterDelegate(TransportConfigContainerSchema.DSNConversionModeGetter), new SetterDelegate(TransportConfigContainerSchema.DSNConversionModeSetter), null, null);

		// Token: 0x04002FE8 RID: 12264
		public static readonly ADPropertyDefinition DisableXexch50 = new ADPropertyDefinition("DisableXexch50", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 16), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 16), null, null);

		// Token: 0x04002FE9 RID: 12265
		public static readonly ADPropertyDefinition Rfc2231EncodingEnabled = new ADPropertyDefinition("Rfc2231EncodingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 256), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 256), null, null);

		// Token: 0x04002FEA RID: 12266
		public static readonly ADPropertyDefinition OpenDomainRoutingEnabled = new ADPropertyDefinition("OpenDomainRoutingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 1024), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 1024), null, null);

		// Token: 0x04002FEB RID: 12267
		public static readonly ADPropertyDefinition MaxSendSize = new ADPropertyDefinition("MaxSendSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "submissionContLength", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2097151UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FEC RID: 12268
		public static readonly ADPropertyDefinition ExternalDelayDsnDisabled = new ADPropertyDefinition("ExternalDelayDsnDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 65536), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 65536), null, null);

		// Token: 0x04002FED RID: 12269
		public static readonly ADPropertyDefinition ExternalDsnDefaultLanguageStr = new ADPropertyDefinition("ExternalDSNDefaultLanguageStr", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchTransportExternalDefaultLanguage", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FEE RID: 12270
		public static readonly ADPropertyDefinition ExternalDsnDefaultLanguage = new ADPropertyDefinition("ExternalDSNDefaultLanguage", ExchangeObjectVersion.Exchange2007, typeof(CultureInfo), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new DelegateConstraint(new ValidationDelegate(ConstraintDelegates.ValidateDsnDefaultCulture))
		}, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.ExternalDsnDefaultLanguageStr
		}, null, new GetterDelegate(TransportConfigContainer.ExternalDsnDefaultLanguageGetter), new SetterDelegate(TransportConfigContainer.ExternalDsnDefaultLanguageSetter), null, null);

		// Token: 0x04002FEF RID: 12271
		public static readonly ADPropertyDefinition ExternalDsnLanguageDetectionDisabled = new ADPropertyDefinition("ExternalDsnLanguageDetectionDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 2048), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 2048), null, null);

		// Token: 0x04002FF0 RID: 12272
		public static readonly ADPropertyDefinition ExternalDsnMaxMessageAttachSize = new ADPropertyDefinition("ExternalDsnMaxMessageAttachSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchTransportExternalMaxDSNMessageAttachmentSize", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromMB(10UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FF1 RID: 12273
		public static readonly ADPropertyDefinition ExternalDsnReportingAuthority = new ADPropertyDefinition("ExternalDsnReportingAuthority", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomain), "msExchTransportExternalDSNReportingAuthority", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FF2 RID: 12274
		public static readonly ADPropertyDefinition ExternalDsnSendHtmlDisabled = new ADPropertyDefinition("ExternalDsnSendHtmlDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 4096), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 4096), null, null);

		// Token: 0x04002FF3 RID: 12275
		public static readonly ADPropertyDefinition ExternalPostmasterAddress = new ADPropertyDefinition("ExternalPostmasterAddress", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress?), "msExchTransportExternalPostmasterAddress", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 320)
		}, null, null);

		// Token: 0x04002FF4 RID: 12276
		public static readonly ADPropertyDefinition InternalDelayDsnDisabled = new ADPropertyDefinition("InternalDelayDsnDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 8192), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 8192), null, null);

		// Token: 0x04002FF5 RID: 12277
		public static readonly ADPropertyDefinition InternalDsnDefaultLanguageStr = new ADPropertyDefinition("InternalDSNDefaultLanguageStr", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchTransportInternalDefaultLanguage", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FF6 RID: 12278
		public static readonly ADPropertyDefinition InternalDsnDefaultLanguage = new ADPropertyDefinition("InternalDSNDefaultLanguage", ExchangeObjectVersion.Exchange2007, typeof(CultureInfo), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new DelegateConstraint(new ValidationDelegate(ConstraintDelegates.ValidateDsnDefaultCulture))
		}, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.InternalDsnDefaultLanguageStr
		}, null, new GetterDelegate(TransportConfigContainer.InternalDsnDefaultLanguageGetter), new SetterDelegate(TransportConfigContainer.InternalDsnDefaultLanguageSetter), null, null);

		// Token: 0x04002FF7 RID: 12279
		public static readonly ADPropertyDefinition InternalDsnLanguageDetectionDisabled = new ADPropertyDefinition("InternalDsnLanguageDetectionDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 16384), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 16384), null, null);

		// Token: 0x04002FF8 RID: 12280
		public static readonly ADPropertyDefinition InternalDsnMaxMessageAttachSize = new ADPropertyDefinition("InternalDsnMaxMessageAttachSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchTransportInternalMaxDSNMessageAttachmentSize", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromMB(10UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(0UL), ByteQuantifiedSize.FromBytes(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FF9 RID: 12281
		public static readonly ADPropertyDefinition InternalDsnReportingAuthority = new ADPropertyDefinition("InternalDsnReportingAuthority", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomain), "msExchTransportInternalDSNReportingAuthority", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FFA RID: 12282
		public static readonly ADPropertyDefinition InternalDsnSendHtmlDisabled = new ADPropertyDefinition("InternalDsnSendHtmlDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 32768), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 32768), null, null);

		// Token: 0x04002FFB RID: 12283
		public static readonly ADPropertyDefinition ShadowRedundancyDisabled = new ADPropertyDefinition("ShadowRedundancyDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 512), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 512), null, null);

		// Token: 0x04002FFC RID: 12284
		public static readonly ADPropertyDefinition ShadowHeartbeatTimeoutInterval = new ADPropertyDefinition("ShadowHeartbeatTimeoutInterval", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportShadowHeartbeatTimeoutInterval", ADPropertyDefinitionFlags.PersistDefaultValue, ADAMTransportConfigContainerSchema.DefaultShadowHeartbeatTimeoutInterval, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.OneDay),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FFD RID: 12285
		public static readonly ADPropertyDefinition ShadowHeartbeatRetryCount = new ADPropertyDefinition("ShadowHeartbeatRetryCount", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportShadowHeartbeatRetryCount", ADPropertyDefinitionFlags.PersistDefaultValue, 12, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 15)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FFE RID: 12286
		public static readonly ADPropertyDefinition MaxRetriesForRemoteSiteShadow = new ADPropertyDefinition("MaxRetriesForRemoteSiteShadow", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportMaxRetriesForRemoteSiteShadow", ADPropertyDefinitionFlags.PersistDefaultValue, 4, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 255)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002FFF RID: 12287
		public static readonly ADPropertyDefinition MaxRetriesForLocalSiteShadow = new ADPropertyDefinition("MaxRetriesForLocalSiteShadow", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportMaxRetriesForLocalSiteShadow", ADPropertyDefinitionFlags.PersistDefaultValue, 2, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 255)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003000 RID: 12288
		public static readonly ADPropertyDefinition ShadowMessageAutoDiscardInterval = new ADPropertyDefinition("ShadowMessageAutoDiscardInterval", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportShadowMessageAutoDiscardInterval", ADPropertyDefinitionFlags.PersistDefaultValue, ADAMTransportConfigContainerSchema.DefaultShadowMessageAutoDiscardInterval, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromSeconds(5.0), EnhancedTimeSpan.FromDays(90.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003001 RID: 12289
		public static readonly ADPropertyDefinition HygieneSuite = new ADPropertyDefinition("HygieneSuite", ExchangeObjectVersion.Exchange2007, typeof(HygieneSuiteEnum), "msExchTransportSettingsAVFlags", ADPropertyDefinitionFlags.PersistDefaultValue, HygieneSuiteEnum.Standard, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<HygieneSuiteEnum>(HygieneSuiteEnum.Standard, HygieneSuiteEnum.Premium)
		}, null, null);

		// Token: 0x04003002 RID: 12290
		public static readonly ADPropertyDefinition MigrationEnabled = new ADPropertyDefinition("MigrationEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 1048576), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 1048576), null, null);

		// Token: 0x04003003 RID: 12291
		public static readonly ADPropertyDefinition LegacyJournalingMigrationEnabled = new ADPropertyDefinition("LegacyJournalingMigrationEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 8388608), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 8388608), null, null);

		// Token: 0x04003004 RID: 12292
		public static readonly ADPropertyDefinition LegacyArchiveJournalingEnabled = new ADPropertyDefinition("LegacyArchiveJournalingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 33554432), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 33554432), null, null);

		// Token: 0x04003005 RID: 12293
		public static readonly ADPropertyDefinition RejectMessageOnShadowFailure = new ADPropertyDefinition("RejectMessageOnShadowFailure", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 67108864), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 67108864), null, null);

		// Token: 0x04003006 RID: 12294
		public static readonly ADPropertyDefinition ShadowMessagePreferenceSetting = new ADPropertyDefinition("ShadowMessagePreferenceSetting", ExchangeObjectVersion.Exchange2007, typeof(ShadowMessagePreference), null, ADPropertyDefinitionFlags.Calculated, ShadowMessagePreference.PreferRemote, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, new GetterDelegate(TransportConfigContainer.InternalShadowMessagePreferenceGetter), new SetterDelegate(TransportConfigContainer.InternalShadowMessagePreferenceSetter), null, null);

		// Token: 0x04003007 RID: 12295
		public static readonly ADPropertyDefinition RedirectUnprovisionedUserMessagesForLegacyArchiveJournaling = new ADPropertyDefinition("RedirectUnprovisionedUserMessagesForLegacyArchiveJournaling", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 536870912), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 536870912), null, null);

		// Token: 0x04003008 RID: 12296
		public static readonly ADPropertyDefinition RedirectDLMessagesForLegacyArchiveJournaling = new ADPropertyDefinition("RedirectDLMessagesForLegacyArchiveJournaling", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 1073741824), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 1073741824), null, null);

		// Token: 0x04003009 RID: 12297
		public static readonly ADPropertyDefinition LegacyArchiveLiveJournalingEnabled = new ADPropertyDefinition("LegacyArchiveLiveJournalingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 2), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 2), null, null);

		// Token: 0x0400300A RID: 12298
		public static readonly ADPropertyDefinition JournalReportDLMemberSubstitutionEnabled = new ADPropertyDefinition("JournalReportDLMemberSubstitutionEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 64), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 64), null, null);

		// Token: 0x0400300B RID: 12299
		public static readonly ADPropertyDefinition JournalArchivingEnabled = new ADPropertyDefinition("JournalArchivingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADAMTransportConfigContainerSchema.Flags
		}, null, ADObject.FlagGetterDelegate(ADAMTransportConfigContainerSchema.Flags, 128), ADObject.FlagSetterDelegate(ADAMTransportConfigContainerSchema.Flags, 128), null, null);

		// Token: 0x0400300C RID: 12300
		public static readonly ADPropertyDefinition SafetyNetHoldTime = new ADPropertyDefinition("SafetyNetHoldTime", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportDumpsterHoldTime", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(7.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromSeconds(15.0), EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);
	}
}
