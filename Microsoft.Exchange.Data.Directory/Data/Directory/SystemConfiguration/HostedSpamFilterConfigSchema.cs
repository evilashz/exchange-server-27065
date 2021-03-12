using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000478 RID: 1144
	internal class HostedSpamFilterConfigSchema : ADConfigurationObjectSchema
	{
		// Token: 0x06003333 RID: 13107 RVA: 0x000CDCAC File Offset: 0x000CBEAC
		private static GetterDelegate AsfSettingsGetterDelegate(int slot, ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(IPropertyBag bag)
			{
				if (bag[propertyDefinition] == null)
				{
					bag[propertyDefinition] = HostedSpamFilterConfigSchema.GetDefaultAsfOptionValues();
				}
				int num = (slot % 2 == 0) ? 0 : 4;
				return (SpamFilteringOption)((bag[propertyDefinition] as byte[])[slot / 2] >> num & 15);
			};
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000CDD78 File Offset: 0x000CBF78
		private static SetterDelegate AsfSettingsSetterDelegate(int slot, ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(object value, IPropertyBag bag)
			{
				if (bag[propertyDefinition] == null)
				{
					bag[propertyDefinition] = HostedSpamFilterConfigSchema.GetDefaultAsfOptionValues();
				}
				byte[] array = bag[propertyDefinition] as byte[];
				int num = (slot % 2 == 0) ? 0 : 4;
				int num2 = (num == 0) ? 240 : 15;
				int num3 = (int)array[slot / 2];
				num3 &= num2;
				num3 |= (int)value << num;
				array[slot / 2] = (byte)num3;
				(bag as PropertyBag).MarkAsChanged(propertyDefinition);
			};
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000CDDA5 File Offset: 0x000CBFA5
		private static byte[] GetDefaultAsfOptionValues()
		{
			return new byte[32];
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000CDDB0 File Offset: 0x000CBFB0
		private static QueryFilter IsDefaultFilterBuilder(SinglePropertyFilter filter)
		{
			return new AndFilter(new QueryFilter[]
			{
				ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(HostedSpamFilterConfigSchema.SpamFilteringFlags, 64UL)),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeVersion, ExchangeObjectVersion.Exchange2007)
			});
		}

		// Token: 0x0400234C RID: 9036
		internal const int TestModeActionShift = 3;

		// Token: 0x0400234D RID: 9037
		internal const int TestModeActionLength = 3;

		// Token: 0x0400234E RID: 9038
		internal const int IsDefaultShift = 6;

		// Token: 0x0400234F RID: 9039
		internal const int EnableDigestsShift = 7;

		// Token: 0x04002350 RID: 9040
		internal const int MoveToJmfEnableHostedQuarantineShift = 8;

		// Token: 0x04002351 RID: 9041
		internal const int BccSuspiciousOutboundMailShift = 9;

		// Token: 0x04002352 RID: 9042
		internal const int NotifyOutboundSpamShift = 10;

		// Token: 0x04002353 RID: 9043
		internal const int DownloadLinkShift = 12;

		// Token: 0x04002354 RID: 9044
		internal const int HighConfidenceActionShift = 13;

		// Token: 0x04002355 RID: 9045
		internal const int HighConfidenceActionLength = 3;

		// Token: 0x04002356 RID: 9046
		internal const int MediumConfidenceActionShift = 17;

		// Token: 0x04002357 RID: 9047
		internal const int MediumConfidenceActionLength = 3;

		// Token: 0x04002358 RID: 9048
		internal const int LowConfidenceActionShift = 21;

		// Token: 0x04002359 RID: 9049
		internal const int LowConfidenceActionLength = 3;

		// Token: 0x0400235A RID: 9050
		public static readonly ADPropertyDefinition AddXHeaderValue = new ADPropertyDefinition("AddXHeaderValue", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchSpamAddHeader", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400235B RID: 9051
		public static readonly ADPropertyDefinition ModifySubjectValue = new ADPropertyDefinition("ModifySubjectValue", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchSpamModifySubject", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400235C RID: 9052
		public static readonly ADPropertyDefinition RedirectToRecipients = new ADPropertyDefinition("RedirectToRecipients", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress), "msExchSpamRedirectAddress", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400235D RID: 9053
		public static readonly ADPropertyDefinition TestModeBccToRecipients = new ADPropertyDefinition("TestModeBccToRecipients", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress), "msExchSpamAsfTestBccAddress", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400235E RID: 9054
		public static readonly ADPropertyDefinition FalsePositiveAdditionalRecipients = new ADPropertyDefinition("FalsePositiveAdditionalRecipients", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress), "msExchSpamFalsePositiveCc", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400235F RID: 9055
		public static readonly ADPropertyDefinition BccSuspiciousOutboundAdditionalRecipients = new ADPropertyDefinition("BccSuspiciousOutboundAdditionalRecipients", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress), "msExchSpamOutboundSpamCc", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002360 RID: 9056
		public static readonly ADPropertyDefinition SpamFilteringFlags = new ADPropertyDefinition("SpamFilteringFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSpamFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002361 RID: 9057
		public static readonly ADPropertyDefinition AsfSettings = new ADPropertyDefinition("AsfSettings", ExchangeObjectVersion.Exchange2007, typeof(byte[]), "msExchSpamAsfSettings", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 32)
		}, null, null);

		// Token: 0x04002362 RID: 9058
		public static readonly ADPropertyDefinition IPAllowList = new ADPropertyDefinition("IPAllowList", ExchangeObjectVersion.Exchange2007, typeof(IPRange), "msExchSpamAllowedIPRanges", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002363 RID: 9059
		public static readonly ADPropertyDefinition IPBlockList = new ADPropertyDefinition("IPBlockList", ExchangeObjectVersion.Exchange2007, typeof(IPRange), "msExchSpamBlockedIPRanges", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002364 RID: 9060
		public static readonly ADPropertyDefinition NotifyOutboundSpamRecipients = new ADPropertyDefinition("NotifyOutboundSpamRecipients", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress), "msExchSpamNotifyOutboundRecipients", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002365 RID: 9061
		public static readonly ADPropertyDefinition LanguageBlockList = new ADPropertyDefinition("LanguageBlockList", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchSpamLanguageBlockList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002366 RID: 9062
		public static readonly ADPropertyDefinition CountryBlockList = new ADPropertyDefinition("CountryBlockList", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchSpamCountryBlockList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002367 RID: 9063
		public static readonly ADPropertyDefinition QuarantineRetentionPeriod = new ADPropertyDefinition("QuarantineRetentionPeriod", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSpamQuarantineRetention", ADPropertyDefinitionFlags.PersistDefaultValue, 15, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002368 RID: 9064
		public static readonly ADPropertyDefinition DigestFrequency = new ADPropertyDefinition("DigestFrequency", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSpamDigestFrequency", ADPropertyDefinitionFlags.PersistDefaultValue, 7, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002369 RID: 9065
		public static readonly ADPropertyDefinition TestModeAction = ADObject.BitfieldProperty("TestModeAction", 3, 3, HostedSpamFilterConfigSchema.SpamFilteringFlags);

		// Token: 0x0400236A RID: 9066
		public static readonly ADPropertyDefinition IsDefault = new ADPropertyDefinition("IsDefault", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.SpamFilteringFlags
		}, new CustomFilterBuilderDelegate(HostedSpamFilterConfigSchema.IsDefaultFilterBuilder), ADObject.FlagGetterDelegate(HostedSpamFilterConfigSchema.SpamFilteringFlags, 64), ADObject.FlagSetterDelegate(HostedSpamFilterConfigSchema.SpamFilteringFlags, 64), null, null);

		// Token: 0x0400236B RID: 9067
		public static readonly ADPropertyDefinition EnableDigests = ADObject.BitfieldProperty("EnableDigests", 7, HostedSpamFilterConfigSchema.SpamFilteringFlags);

		// Token: 0x0400236C RID: 9068
		public static readonly ADPropertyDefinition MoveToJmfEnableHostedQuarantine = ADObject.BitfieldProperty("MoveToJmfEnableHostedQuarantine", 8, HostedSpamFilterConfigSchema.SpamFilteringFlags);

		// Token: 0x0400236D RID: 9069
		public static readonly ADPropertyDefinition BccSuspiciousOutboundMail = ADObject.BitfieldProperty("BccSuspiciousOutboundMail", 9, HostedSpamFilterConfigSchema.SpamFilteringFlags);

		// Token: 0x0400236E RID: 9070
		public static readonly ADPropertyDefinition NotifyOutboundSpam = ADObject.BitfieldProperty("NotifyOutboundSpam", 10, HostedSpamFilterConfigSchema.SpamFilteringFlags);

		// Token: 0x0400236F RID: 9071
		public static readonly ADPropertyDefinition DownloadLink = ADObject.BitfieldProperty("DownloadLink", 12, HostedSpamFilterConfigSchema.SpamFilteringFlags);

		// Token: 0x04002370 RID: 9072
		public static readonly ADPropertyDefinition HighConfidenceAction = ADObject.BitfieldProperty("HighConfidenceAction", 13, 3, HostedSpamFilterConfigSchema.SpamFilteringFlags);

		// Token: 0x04002371 RID: 9073
		public static readonly ADPropertyDefinition MediumConfidenceAction = ADObject.BitfieldProperty("MediumConfidenceAction", 17, 3, HostedSpamFilterConfigSchema.SpamFilteringFlags);

		// Token: 0x04002372 RID: 9074
		public static readonly ADPropertyDefinition LowConfidenceAction = ADObject.BitfieldProperty("LowConfidenceAction", 21, 3, HostedSpamFilterConfigSchema.SpamFilteringFlags);

		// Token: 0x04002373 RID: 9075
		public static readonly ADPropertyDefinition IncreaseScoreWithImageLinks = new ADPropertyDefinition("IncreaseScoreWithImageLinks", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(0, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(0, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x04002374 RID: 9076
		public static readonly ADPropertyDefinition IncreaseScoreWithNumericIps = new ADPropertyDefinition("IncreaseScoreWithNumericIps", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(1, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(1, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x04002375 RID: 9077
		public static readonly ADPropertyDefinition IncreaseScoreWithRedirectToOtherPort = new ADPropertyDefinition("IncreaseScoreWithRedirectToOtherPort", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(2, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(2, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x04002376 RID: 9078
		public static readonly ADPropertyDefinition IncreaseScoreWithBizOrInfoUrls = new ADPropertyDefinition("IncreaseScoreWithBizOrInfoUrls", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(3, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(3, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x04002377 RID: 9079
		public static readonly ADPropertyDefinition MarkAsSpamEmptyMessages = new ADPropertyDefinition("MarkAsSpamEmptyMessages", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(4, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(4, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x04002378 RID: 9080
		public static readonly ADPropertyDefinition MarkAsSpamJavaScriptInHtml = new ADPropertyDefinition("MarkAsSpamJavaScriptInHtml", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(5, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(5, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x04002379 RID: 9081
		public static readonly ADPropertyDefinition MarkAsSpamFramesInHtml = new ADPropertyDefinition("MarkAsSpamFramesInHtml", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(6, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(6, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x0400237A RID: 9082
		public static readonly ADPropertyDefinition MarkAsSpamObjectTagsInHtml = new ADPropertyDefinition("MarkAsSpamObjectTagsInHtml", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(7, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(7, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x0400237B RID: 9083
		public static readonly ADPropertyDefinition MarkAsSpamEmbedTagsInHtml = new ADPropertyDefinition("MarkAsSpamEmbedTagsInHtml", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(8, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(8, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x0400237C RID: 9084
		public static readonly ADPropertyDefinition MarkAsSpamFormTagsInHtml = new ADPropertyDefinition("MarkAsSpamFormTagsInHtml", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(9, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(9, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x0400237D RID: 9085
		public static readonly ADPropertyDefinition MarkAsSpamWebBugsInHtml = new ADPropertyDefinition("MarkAsSpamWebBugsInHtml", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(10, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(10, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x0400237E RID: 9086
		public static readonly ADPropertyDefinition MarkAsSpamSensitiveWordList = new ADPropertyDefinition("MarkAsSpamSensitiveWordList", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(11, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(11, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x0400237F RID: 9087
		public static readonly ADPropertyDefinition MarkAsSpamSpfRecordHardFail = new ADPropertyDefinition("MarkAsSpamSpfRecordHardFail", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(12, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(12, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x04002380 RID: 9088
		public static readonly ADPropertyDefinition MarkAsSpamFromAddressAuthFail = new ADPropertyDefinition("MarkAsSpamFromAddressAuthFail", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(13, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(13, HostedSpamFilterConfigSchema.AsfSettings), null, null);

		// Token: 0x04002381 RID: 9089
		public static readonly ADPropertyDefinition MarkAsSpamNdrBackscatter = new ADPropertyDefinition("MarkAsSpamNdrBackscatter", ExchangeObjectVersion.Exchange2007, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedSpamFilterConfigSchema.AsfSettings
		}, null, HostedSpamFilterConfigSchema.AsfSettingsGetterDelegate(14, HostedSpamFilterConfigSchema.AsfSettings), HostedSpamFilterConfigSchema.AsfSettingsSetterDelegate(14, HostedSpamFilterConfigSchema.AsfSettings), null, null);
	}
}
