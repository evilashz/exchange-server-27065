using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000473 RID: 1139
	internal class HostedContentFilterPolicySchema : ADConfigurationObjectSchema
	{
		// Token: 0x060032C3 RID: 12995 RVA: 0x000CC684 File Offset: 0x000CA884
		private static int AsfSettingsGetterFunc(int slot, ProviderPropertyDefinition propertyDefinition, IPropertyBag bag)
		{
			if (bag[propertyDefinition] == null)
			{
				bag[propertyDefinition] = HostedContentFilterPolicySchema.GetDefaultAsfOptionValues();
			}
			int num = (slot % 2 == 0) ? 0 : 4;
			return (bag[propertyDefinition] as byte[])[slot / 2] >> num & 15;
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000CC710 File Offset: 0x000CA910
		private static GetterDelegate AsfSettingsGetterDelegateInt32(int slot, ProviderPropertyDefinition propertyDefinition, Func<int, int> modifier)
		{
			return delegate(IPropertyBag bag)
			{
				int num = HostedContentFilterPolicySchema.AsfSettingsGetterFunc(slot, propertyDefinition, bag);
				if (modifier == null)
				{
					return num;
				}
				return modifier(num);
			};
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000CC768 File Offset: 0x000CA968
		private static GetterDelegate AsfSettingsGetterDelegate(int slot, ProviderPropertyDefinition propertyDefinition)
		{
			return (IPropertyBag bag) => (SpamFilteringOption)HostedContentFilterPolicySchema.AsfSettingsGetterFunc(slot, propertyDefinition, bag);
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000CC834 File Offset: 0x000CAA34
		private static SetterDelegate AsfSettingsSetterDelegate(int slot, ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(object value, IPropertyBag bag)
			{
				if (bag[propertyDefinition] == null)
				{
					bag[propertyDefinition] = HostedContentFilterPolicySchema.GetDefaultAsfOptionValues();
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

		// Token: 0x060032C7 RID: 12999 RVA: 0x000CC864 File Offset: 0x000CAA64
		private static byte[] GetDefaultAsfOptionValues()
		{
			byte[] array = new byte[32];
			array[7] = 16;
			return array;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000CC880 File Offset: 0x000CAA80
		private static QueryFilter IsDefaultFilterBuilder(SinglePropertyFilter filter)
		{
			return new AndFilter(new QueryFilter[]
			{
				ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(HostedContentFilterPolicySchema.SpamFilteringFlags, 64UL)),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeVersion, ExchangeObjectVersion.Exchange2012)
			});
		}

		// Token: 0x040022F9 RID: 8953
		internal const int AsfSettingsByteArrayLengthMinValue = 1;

		// Token: 0x040022FA RID: 8954
		internal const int AsfSettingsByteArrayLengthMaxValue = 32;

		// Token: 0x040022FB RID: 8955
		internal const int EndUserSpamNotificationFrequencyMinValue = 1;

		// Token: 0x040022FC RID: 8956
		internal const int EndUserSpamNotificationFrequencyMaxValue = 15;

		// Token: 0x040022FD RID: 8957
		internal const int EndUserSpamNotificationFrequencyDefaultValue = 3;

		// Token: 0x040022FE RID: 8958
		internal const int QuarantineRetentionPeriodMinValue = 1;

		// Token: 0x040022FF RID: 8959
		internal const int QuarantineRetentionPeriodMaxValue = 15;

		// Token: 0x04002300 RID: 8960
		internal const int QuarantineRetentionPeriodDefaultValue = 15;

		// Token: 0x04002301 RID: 8961
		internal const int EndUserSpamNotificationLimitDefaultValue = 0;

		// Token: 0x04002302 RID: 8962
		internal const int SpamFilteringFlagsDefaultValue = 0;

		// Token: 0x04002303 RID: 8963
		internal const int BulkThresholdDefaultValue = 6;

		// Token: 0x04002304 RID: 8964
		internal const int BulkThresholdMinValue = 1;

		// Token: 0x04002305 RID: 8965
		internal const int BulkThresholdMaxValue = 9;

		// Token: 0x04002306 RID: 8966
		internal const int TestModeActionShift = 3;

		// Token: 0x04002307 RID: 8967
		internal const int TestModeActionLength = 3;

		// Token: 0x04002308 RID: 8968
		internal const int IsDefaultShift = 6;

		// Token: 0x04002309 RID: 8969
		internal const int EnableDigestsShift = 7;

		// Token: 0x0400230A RID: 8970
		internal const int DownloadLinkShift = 12;

		// Token: 0x0400230B RID: 8971
		internal const int HighConfidenceSpamActionShift = 13;

		// Token: 0x0400230C RID: 8972
		internal const int HighConfidenceSpamActionLength = 3;

		// Token: 0x0400230D RID: 8973
		internal const int SpamActionShift = 21;

		// Token: 0x0400230E RID: 8974
		internal const int SpamActionLength = 3;

		// Token: 0x0400230F RID: 8975
		internal const int EnableRegionBlockListShift = 25;

		// Token: 0x04002310 RID: 8976
		internal const int EnableLanguageBlockListShift = 26;

		// Token: 0x04002311 RID: 8977
		public static readonly ADPropertyDefinition AddXHeaderValue = new ADPropertyDefinition("AddXHeaderValue", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchSpamAddHeader", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002312 RID: 8978
		public static readonly ADPropertyDefinition ModifySubjectValue = new ADPropertyDefinition("ModifySubjectValue", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchSpamModifySubject", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002313 RID: 8979
		public static readonly ADPropertyDefinition RedirectToRecipients = new ADPropertyDefinition("RedirectToRecipients", ExchangeObjectVersion.Exchange2012, typeof(SmtpAddress), "msExchSpamRedirectAddress", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002314 RID: 8980
		public static readonly ADPropertyDefinition TestModeBccToRecipients = new ADPropertyDefinition("TestModeBccToRecipients", ExchangeObjectVersion.Exchange2012, typeof(SmtpAddress), "msExchSpamAsfTestBccAddress", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002315 RID: 8981
		public static readonly ADPropertyDefinition FalsePositiveAdditionalRecipients = new ADPropertyDefinition("FalsePositiveAdditionalRecipients", ExchangeObjectVersion.Exchange2012, typeof(SmtpAddress), "msExchSpamFalsePositiveCc", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002316 RID: 8982
		public static readonly ADPropertyDefinition SpamFilteringFlags = new ADPropertyDefinition("SpamFilteringFlags", ExchangeObjectVersion.Exchange2012, typeof(int), "msExchSpamFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002317 RID: 8983
		public static readonly ADPropertyDefinition AsfSettings = new ADPropertyDefinition("AsfSettings", ExchangeObjectVersion.Exchange2012, typeof(byte[]), "msExchSpamAsfSettings", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 32)
		}, null, null);

		// Token: 0x04002318 RID: 8984
		public static readonly ADPropertyDefinition LanguageBlockList = new ADPropertyDefinition("LanguageBlockList", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchSpamLanguageBlockList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002319 RID: 8985
		public static readonly ADPropertyDefinition RegionBlockList = new ADPropertyDefinition("RegionBlockList", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchSpamCountryBlockList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400231A RID: 8986
		public static readonly ADPropertyDefinition QuarantineRetentionPeriod = new ADPropertyDefinition("QuarantineRetentionPeriod", ExchangeObjectVersion.Exchange2012, typeof(int), "msExchSpamQuarantineRetention", ADPropertyDefinitionFlags.PersistDefaultValue, 15, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 15)
		}, null, null);

		// Token: 0x0400231B RID: 8987
		public static readonly ADPropertyDefinition EndUserSpamNotificationFrequency = new ADPropertyDefinition("EndUserSpamNotificationFrequency", ExchangeObjectVersion.Exchange2012, typeof(int), "msExchSpamDigestFrequency", ADPropertyDefinitionFlags.PersistDefaultValue, 3, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 15)
		}, null, null);

		// Token: 0x0400231C RID: 8988
		public static readonly ADPropertyDefinition EndUserSpamNotificationCustomFromAddress = new ADPropertyDefinition("EndUserSpamNotificationCustomFromAddress", ExchangeObjectVersion.Exchange2012, typeof(SmtpAddress), "msExchMalwareFilterConfigInternalSubject", ADPropertyDefinitionFlags.None, SmtpAddress.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400231D RID: 8989
		public static readonly ADPropertyDefinition EndUserSpamNotificationCustomFromName = new ADPropertyDefinition("EndUserSpamNotificationCustomFromName", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchMalwareFilterConfigFromName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400231E RID: 8990
		public static readonly ADPropertyDefinition EndUserSpamNotificationCustomSubject = new ADPropertyDefinition("EndUserSpamNotificationCustomSubject", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchMalwareFilterConfigExternalSubject", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400231F RID: 8991
		public static readonly ADPropertyDefinition EndUserSpamNotificationLimit = new ADPropertyDefinition("EndUserSpamNotificationLimit", ExchangeObjectVersion.Exchange2012, typeof(int), "msExchMalwareFilterConfigFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002320 RID: 8992
		public static readonly ADPropertyDefinition EndUserSpamNotificationLanguage = new ADPropertyDefinition("EndUserSpamNotificationLanguage", ExchangeObjectVersion.Exchange2012, typeof(EsnLanguage), "msExchMalwareFilteringFlags", ADPropertyDefinitionFlags.PersistDefaultValue, EsnLanguage.Default, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002321 RID: 8993
		public static readonly ADPropertyDefinition TestModeAction = ADObject.BitfieldProperty("TestModeAction", 3, 3, HostedContentFilterPolicySchema.SpamFilteringFlags);

		// Token: 0x04002322 RID: 8994
		public static readonly ADPropertyDefinition IsDefault = new ADPropertyDefinition("IsDefault", ExchangeObjectVersion.Exchange2012, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.SpamFilteringFlags
		}, new CustomFilterBuilderDelegate(HostedContentFilterPolicySchema.IsDefaultFilterBuilder), ADObject.FlagGetterDelegate(HostedContentFilterPolicySchema.SpamFilteringFlags, 64), ADObject.FlagSetterDelegate(HostedContentFilterPolicySchema.SpamFilteringFlags, 64), null, null);

		// Token: 0x04002323 RID: 8995
		public static readonly ADPropertyDefinition EnableEndUserSpamNotifications = ADObject.BitfieldProperty("EnableEndUserSpamNotifications", 7, HostedContentFilterPolicySchema.SpamFilteringFlags);

		// Token: 0x04002324 RID: 8996
		public static readonly ADPropertyDefinition DownloadLink = ADObject.BitfieldProperty("DownloadLink", 12, HostedContentFilterPolicySchema.SpamFilteringFlags);

		// Token: 0x04002325 RID: 8997
		public static readonly ADPropertyDefinition HighConfidenceSpamAction = ADObject.BitfieldProperty("HighConfidenceSpamAction", 13, 3, HostedContentFilterPolicySchema.SpamFilteringFlags);

		// Token: 0x04002326 RID: 8998
		public static readonly ADPropertyDefinition SpamAction = ADObject.BitfieldProperty("SpamAction", 21, 3, HostedContentFilterPolicySchema.SpamFilteringFlags);

		// Token: 0x04002327 RID: 8999
		public static readonly ADPropertyDefinition EnableRegionBlockList = ADObject.BitfieldProperty("EnableRegionBlockList", 25, HostedContentFilterPolicySchema.SpamFilteringFlags);

		// Token: 0x04002328 RID: 9000
		public static readonly ADPropertyDefinition EnableLanguageBlockList = ADObject.BitfieldProperty("EnableLanguageBlockList", 26, HostedContentFilterPolicySchema.SpamFilteringFlags);

		// Token: 0x04002329 RID: 9001
		public static readonly ADPropertyDefinition IncreaseScoreWithImageLinks = new ADPropertyDefinition("IncreaseScoreWithImageLinks", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(0, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(0, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x0400232A RID: 9002
		public static readonly ADPropertyDefinition IncreaseScoreWithNumericIps = new ADPropertyDefinition("IncreaseScoreWithNumericIps", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(1, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(1, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x0400232B RID: 9003
		public static readonly ADPropertyDefinition IncreaseScoreWithRedirectToOtherPort = new ADPropertyDefinition("IncreaseScoreWithRedirectToOtherPort", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(2, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(2, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x0400232C RID: 9004
		public static readonly ADPropertyDefinition IncreaseScoreWithBizOrInfoUrls = new ADPropertyDefinition("IncreaseScoreWithBizOrInfoUrls", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(3, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(3, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x0400232D RID: 9005
		public static readonly ADPropertyDefinition MarkAsSpamEmptyMessages = new ADPropertyDefinition("MarkAsSpamEmptyMessages", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(4, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(4, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x0400232E RID: 9006
		public static readonly ADPropertyDefinition MarkAsSpamJavaScriptInHtml = new ADPropertyDefinition("MarkAsSpamJavaScriptInHtml", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(5, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(5, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x0400232F RID: 9007
		public static readonly ADPropertyDefinition MarkAsSpamFramesInHtml = new ADPropertyDefinition("MarkAsSpamFramesInHtml", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(6, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(6, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x04002330 RID: 9008
		public static readonly ADPropertyDefinition MarkAsSpamObjectTagsInHtml = new ADPropertyDefinition("MarkAsSpamObjectTagsInHtml", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(7, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(7, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x04002331 RID: 9009
		public static readonly ADPropertyDefinition MarkAsSpamEmbedTagsInHtml = new ADPropertyDefinition("MarkAsSpamEmbedTagsInHtml", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(8, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(8, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x04002332 RID: 9010
		public static readonly ADPropertyDefinition MarkAsSpamFormTagsInHtml = new ADPropertyDefinition("MarkAsSpamFormTagsInHtml", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(9, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(9, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x04002333 RID: 9011
		public static readonly ADPropertyDefinition MarkAsSpamWebBugsInHtml = new ADPropertyDefinition("MarkAsSpamWebBugsInHtml", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(10, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(10, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x04002334 RID: 9012
		public static readonly ADPropertyDefinition MarkAsSpamSensitiveWordList = new ADPropertyDefinition("MarkAsSpamSensitiveWordList", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(11, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(11, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x04002335 RID: 9013
		public static readonly ADPropertyDefinition MarkAsSpamSpfRecordHardFail = new ADPropertyDefinition("MarkAsSpamSpfRecordHardFail", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(12, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(12, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x04002336 RID: 9014
		public static readonly ADPropertyDefinition MarkAsSpamFromAddressAuthFail = new ADPropertyDefinition("MarkAsSpamFromAddressAuthFail", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(13, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(13, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x04002337 RID: 9015
		public static readonly ADPropertyDefinition MarkAsSpamNdrBackscatter = new ADPropertyDefinition("MarkAsSpamNdrBackscatter", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(14, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(14, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x04002338 RID: 9016
		public static readonly ADPropertyDefinition MarkAsSpamBulkMail = new ADPropertyDefinition("MarkAsSpamBulkMail", ExchangeObjectVersion.Exchange2012, typeof(SpamFilteringOption), null, ADPropertyDefinitionFlags.Calculated, SpamFilteringOption.Off, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegate(15, HostedContentFilterPolicySchema.AsfSettings), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(15, HostedContentFilterPolicySchema.AsfSettings), null, null);

		// Token: 0x04002339 RID: 9017
		public static readonly ADPropertyDefinition BulkThreshold = new ADPropertyDefinition("BulkThreshold", ExchangeObjectVersion.Exchange2012, typeof(int), null, ADPropertyDefinitionFlags.Calculated, 6, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 9)
		}, new ProviderPropertyDefinition[]
		{
			HostedContentFilterPolicySchema.AsfSettings
		}, null, HostedContentFilterPolicySchema.AsfSettingsGetterDelegateInt32(16, HostedContentFilterPolicySchema.AsfSettings, delegate(int v)
		{
			if (v == 0)
			{
				return 6;
			}
			return v;
		}), HostedContentFilterPolicySchema.AsfSettingsSetterDelegate(16, HostedContentFilterPolicySchema.AsfSettings), null, null);
	}
}
