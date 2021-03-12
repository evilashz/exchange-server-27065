using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000610 RID: 1552
	internal sealed class UMMailboxPolicySchema : MailboxPolicySchema
	{
		// Token: 0x040032E1 RID: 13025
		public static readonly ADPropertyDefinition AllowCommonPatternsValue = new ADPropertyDefinition("AllowCommonPatterns", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchUMPinPolicyDisallowCommonPatterns", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 1)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032E2 RID: 13026
		public static readonly ADPropertyDefinition AllowedInCountryOrRegionGroups = SharedPropertyDefinitions.AllowedInCountryOrRegionGroups;

		// Token: 0x040032E3 RID: 13027
		public static readonly ADPropertyDefinition AllowedInternationalGroups = SharedPropertyDefinitions.AllowedInternationalGroups;

		// Token: 0x040032E4 RID: 13028
		public static readonly ADPropertyDefinition AllowDialPlanSubscribers = new ADPropertyDefinition("AllowDialPlanSubscribers", ExchangeObjectVersion.Exchange2007, typeof(bool), "msExchUMDialPlanSubscribersAllowed", ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032E5 RID: 13029
		public static readonly ADPropertyDefinition AllowExtensions = new ADPropertyDefinition("AllowExtensions", ExchangeObjectVersion.Exchange2007, typeof(bool), "msExchUMExtensionLengthNumbersAllowed", ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032E6 RID: 13030
		public static readonly ADPropertyDefinition AssociatedUsers = new ADPropertyDefinition("AssociatedUsers", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchUMTemplateBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032E7 RID: 13031
		public static readonly ADPropertyDefinition FaxMessageText = new ADPropertyDefinition("FaxMessageText", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchUMFaxMessageText", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, null, null);

		// Token: 0x040032E8 RID: 13032
		public static readonly ADPropertyDefinition LogonFailuresBeforePINReset = new ADPropertyDefinition("LogonFailuresBeforePINReset", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<int>), "msExchUMLogonFailuresBeforePINReset", ADPropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(1, 999)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032E9 RID: 13033
		public static readonly ADPropertyDefinition MaxGreetingDuration = new ADPropertyDefinition("MaxGreetingDuration", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchUMMaxGreetingDuration", ADPropertyDefinitionFlags.PersistDefaultValue, 5, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 10)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032EA RID: 13034
		public static readonly ADPropertyDefinition MaxLogonAttempts = new ADPropertyDefinition("MaxLogonAttempts", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<int>), "msExchUMPinPolicyAccountLockoutFailures", ADPropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(1, 999)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032EB RID: 13035
		public static readonly ADPropertyDefinition MinPINLength = new ADPropertyDefinition("MinPINLength", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchUMPinPolicyMinPasswordLength", ADPropertyDefinitionFlags.PersistDefaultValue, 6, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(4, 24)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032EC RID: 13036
		public static readonly ADPropertyDefinition PINHistoryCount = new ADPropertyDefinition("PINHistoryCount", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchUMPinPolicyNumberOfPreviousPasswordsDisallowed", ADPropertyDefinitionFlags.PersistDefaultValue, 5, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 20)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032ED RID: 13037
		public static readonly ADPropertyDefinition PINLifetime = new ADPropertyDefinition("PINLifetime", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<EnhancedTimeSpan>), "msExchUMPinPolicyExpiryDays", ADPropertyDefinitionFlags.None, Unlimited<EnhancedTimeSpan>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneDay, EnhancedTimeSpan.FromDays(999.0)),
			new UnlimitedEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032EE RID: 13038
		public static readonly ADPropertyDefinition ProtectUnauthenticatedVoiceMail = new ADPropertyDefinition("ProtectUnauthenticatedVoiceMail", ExchangeObjectVersion.Exchange2010, typeof(DRMProtectionOptions), "msExchUMProtectUnauthenticatedVoiceMail", ADPropertyDefinitionFlags.PersistDefaultValue, DRMProtectionOptions.None, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(DRMProtectionOptions))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032EF RID: 13039
		public static readonly ADPropertyDefinition ProtectAuthenticatedVoiceMail = new ADPropertyDefinition("ProtectAuthenticatedVoiceMail", ExchangeObjectVersion.Exchange2010, typeof(DRMProtectionOptions), "msExchUMProtectAuthenticatedVoiceMail", ADPropertyDefinitionFlags.PersistDefaultValue, DRMProtectionOptions.None, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(DRMProtectionOptions))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032F0 RID: 13040
		public static readonly ADPropertyDefinition ProtectedVoiceMailText = new ADPropertyDefinition("ProtectedVoiceMailText", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchUMProtectedVoiceMailText", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, null, null);

		// Token: 0x040032F1 RID: 13041
		public static readonly ADPropertyDefinition RequireProtectedPlayOnPhone = new ADPropertyDefinition("RequireProtectedPlayOnPhone", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchUMRequireProtectedPlayOnPhone", ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032F2 RID: 13042
		public static readonly ADPropertyDefinition ResetPINText = new ADPropertyDefinition("ResetPINText", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchUMResetPinText", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, null, null);

		// Token: 0x040032F3 RID: 13043
		public static readonly ADPropertyDefinition SourceForestPolicyNames = new ADPropertyDefinition("SourceForestPolicyNames", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchUMSourceForestPolicyNames", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032F4 RID: 13044
		public static readonly ADPropertyDefinition UMEnabledText = new ADPropertyDefinition("UMEnabledText", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchUMEnabledText", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, null, null);

		// Token: 0x040032F5 RID: 13045
		public static readonly ADPropertyDefinition VoiceMailText = new ADPropertyDefinition("VoiceMailText", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchUMVoiceMailText", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, null, null);

		// Token: 0x040032F6 RID: 13046
		public static readonly ADPropertyDefinition UMDialPlan = new ADPropertyDefinition("UMDialPlan", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchUMMailboxPolicyDialPlanLink", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032F7 RID: 13047
		public static readonly ADPropertyDefinition UMEnabledFlagsBits = new ADPropertyDefinition("UMEnabledFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchUMEnabledFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032F8 RID: 13048
		public static readonly ADPropertyDefinition UMEnabledFlags2Bits = new ADPropertyDefinition("UMEnabledFlags2", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchUMEnabledFlags2", ADPropertyDefinitionFlags.PersistDefaultValue, 65534, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032F9 RID: 13049
		public static readonly ADPropertyDefinition VoiceMailPreviewPartnerAddress = new ADPropertyDefinition("VoiceMailPreviewPartnerAddress", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress?), "msExchVoiceMailPreviewPartnerAddress", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032FA RID: 13050
		public static readonly ADPropertyDefinition VoiceMailPreviewPartnerAssignedID = new ADPropertyDefinition("VoiceMailPreviewPartnerAssignedID", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchVoiceMailPreviewPartnerAssignedID", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032FB RID: 13051
		public static readonly ADPropertyDefinition VoiceMailPreviewPartnerMaxDeliveryDelay = new ADPropertyDefinition("VoiceMailPreviewPartnerMaxDeliveryDelay", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchVoiceMailPreviewPartnerMaxDeliveryDelay", ADPropertyDefinitionFlags.PersistDefaultValue, 1200, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(300, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032FC RID: 13052
		public static readonly ADPropertyDefinition VoiceMailPreviewPartnerMaxMessageDuration = new ADPropertyDefinition("VoiceMailPreviewPartnerMaxMessageDuration", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchVoiceMailPreviewPartnerMaxMessageDuration", ADPropertyDefinitionFlags.PersistDefaultValue, 180, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(60, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040032FD RID: 13053
		public static readonly ADPropertyDefinition AllowCommonPatterns = new ADPropertyDefinition("AllowCommonPatterns", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
		{
			UMMailboxPolicySchema.AllowCommonPatternsValue
		}, null, (IPropertyBag propertyBag) => 0 != (int)propertyBag[UMMailboxPolicySchema.AllowCommonPatternsValue], delegate(object value, IPropertyBag propertyBag)
		{
			propertyBag[UMMailboxPolicySchema.AllowCommonPatternsValue] = (((bool)value) ? 1 : 0);
		}, null, null);

		// Token: 0x040032FE RID: 13054
		public static readonly ADPropertyDefinition AllowMissedCallNotifications = new ADPropertyDefinition("AllowMissedCallNotifications", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlagsBits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlagsBits, 1), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlagsBits, 1), null, null);

		// Token: 0x040032FF RID: 13055
		public static readonly ADPropertyDefinition AllowVirtualNumber = new ADPropertyDefinition("AllowVirtualNumber", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlagsBits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlagsBits, 4), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlagsBits, 4), null, null);

		// Token: 0x04003300 RID: 13056
		public static readonly ADPropertyDefinition AllowFax = new ADPropertyDefinition("AllowFax", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 1), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 1), null, null);

		// Token: 0x04003301 RID: 13057
		public static readonly ADPropertyDefinition AllowTUIAccessToCalendar = new ADPropertyDefinition("AllowTUIAccessToCalendar", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 2), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 2), null, null);

		// Token: 0x04003302 RID: 13058
		public static readonly ADPropertyDefinition AllowTUIAccessToEmail = new ADPropertyDefinition("AllowTUIAccessToEmail", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 4), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 4), null, null);

		// Token: 0x04003303 RID: 13059
		public static readonly ADPropertyDefinition AllowSubscriberAccess = new ADPropertyDefinition("AllowSubscriberAccess", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 8), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 8), null, null);

		// Token: 0x04003304 RID: 13060
		public static readonly ADPropertyDefinition AllowTUIAccessToDirectory = new ADPropertyDefinition("AllowTUIAccessToDirectory", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 16), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 16), null, null);

		// Token: 0x04003305 RID: 13061
		public static readonly ADPropertyDefinition AllowTUIAccessToPersonalContacts = new ADPropertyDefinition("AllowTUIAccessToPersonalContacts", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 1024), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 1024), null, null);

		// Token: 0x04003306 RID: 13062
		public static readonly ADPropertyDefinition AllowASR = new ADPropertyDefinition("AllowASR", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 32), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 32), null, null);

		// Token: 0x04003307 RID: 13063
		public static readonly ADPropertyDefinition AllowPlayOnPhone = new ADPropertyDefinition("AllowPlayOnPhone", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 64), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 64), null, null);

		// Token: 0x04003308 RID: 13064
		public static readonly ADPropertyDefinition AllowVoiceMailPreview = new ADPropertyDefinition("AllowVoiceMailPreview", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 128), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 128), null, null);

		// Token: 0x04003309 RID: 13065
		public static readonly ADPropertyDefinition AllowPersonalAutoAttendant = new ADPropertyDefinition("AllowPersonalAutoAttendant", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 256), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 256), null, null);

		// Token: 0x0400330A RID: 13066
		public static readonly ADPropertyDefinition AllowMessageWaitingIndicator = new ADPropertyDefinition("AllowMessageWaitingIndicator", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 512), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 512), null, null);

		// Token: 0x0400330B RID: 13067
		public static readonly ADPropertyDefinition AllowSMSNotification = new ADPropertyDefinition("AllowSMSNotification", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 2048), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 2048), null, null);

		// Token: 0x0400330C RID: 13068
		public static readonly ADPropertyDefinition AllowPinlessVoiceMailAccess = new ADPropertyDefinition("AllowPinlessVoiceMailAccess", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlagsBits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlagsBits, 8), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlagsBits, 8), null, null);

		// Token: 0x0400330D RID: 13069
		public static readonly ADPropertyDefinition AllowVoiceResponseToOtherMessageTypes = new ADPropertyDefinition("AllowVoiceResponseToOtherMessageTypes", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 4096), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 4096), null, null);

		// Token: 0x0400330E RID: 13070
		public static readonly ADPropertyDefinition FaxServerURI = new ADPropertyDefinition("FaxServerURI", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchUMFaxServerURI", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 256)
		}, null, null);

		// Token: 0x0400330F RID: 13071
		public static readonly ADPropertyDefinition AllowVoiceMailAnalysis = new ADPropertyDefinition("AllowVoiceMailAnalysis", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlagsBits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlagsBits, 16), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlagsBits, 16), null, null);

		// Token: 0x04003310 RID: 13072
		public static readonly ADPropertyDefinition InformCallerOfVoiceMailAnalysis = new ADPropertyDefinition("InformCallerOfVoiceMailAnalysis", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 8192), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 8192), null, null);

		// Token: 0x04003311 RID: 13073
		public static readonly ADPropertyDefinition AllowVoiceNotification = new ADPropertyDefinition("AllowVoiceNotification", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			UMMailboxPolicySchema.UMEnabledFlags2Bits
		}, null, ADObject.FlagGetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 16384), ADObject.FlagSetterDelegate(UMMailboxPolicySchema.UMEnabledFlags2Bits, 16384), null, null);
	}
}
