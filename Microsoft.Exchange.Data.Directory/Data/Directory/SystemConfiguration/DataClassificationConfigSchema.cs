using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003E9 RID: 1001
	internal class DataClassificationConfigSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001EC2 RID: 7874
		internal const int RegexGrammarLimitDefaultValue = 1;

		// Token: 0x04001EC3 RID: 7875
		internal const int DistinctRegexesDefaultValue = 20;

		// Token: 0x04001EC4 RID: 7876
		internal const int KeywordLengthDefaultValue = 50;

		// Token: 0x04001EC5 RID: 7877
		internal const int NumberOfKeywordDefaultValue = 512;

		// Token: 0x04001EC6 RID: 7878
		internal const int DistinctFunctionsDefaultValue = 10;

		// Token: 0x04001EC7 RID: 7879
		internal const int MaxAnyBlocksDefaultValue = 20;

		// Token: 0x04001EC8 RID: 7880
		internal const int MaxNestedAnyBlocksDefaultValue = 5;

		// Token: 0x04001EC9 RID: 7881
		internal const int RegexLengthDefaultValue = 500;

		// Token: 0x04001ECA RID: 7882
		internal const int RegExGrammarLimitShift = 0;

		// Token: 0x04001ECB RID: 7883
		internal const int DistinctRegExesShift = 1;

		// Token: 0x04001ECC RID: 7884
		internal const int DistinctRegExesLength = 8;

		// Token: 0x04001ECD RID: 7885
		internal const int KeywordLengthShift = 9;

		// Token: 0x04001ECE RID: 7886
		internal const int KeywordLengthLength = 9;

		// Token: 0x04001ECF RID: 7887
		internal const int NumberOfKeywordsShift = 18;

		// Token: 0x04001ED0 RID: 7888
		internal const int NumberOfKeywordsLength = 12;

		// Token: 0x04001ED1 RID: 7889
		internal const int DistinctFunctionsShift = 0;

		// Token: 0x04001ED2 RID: 7890
		internal const int DistinctFunctionsLength = 6;

		// Token: 0x04001ED3 RID: 7891
		internal const int MaxAnyBlocksShift = 6;

		// Token: 0x04001ED4 RID: 7892
		internal const int MaxAnyBlocksLength = 7;

		// Token: 0x04001ED5 RID: 7893
		internal const int MaxNestedAnyBlocksShift = 13;

		// Token: 0x04001ED6 RID: 7894
		internal const int MaxNestedAnyBlocksLength = 4;

		// Token: 0x04001ED7 RID: 7895
		internal const int RegExLengthShift = 17;

		// Token: 0x04001ED8 RID: 7896
		internal const int RegExLengthLength = 11;

		// Token: 0x04001ED9 RID: 7897
		public static readonly ADPropertyDefinition DataClassificationConfigFlags1 = new ADPropertyDefinition("DataClassificationConfigFlags1", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchSpamFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 134243369, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001EDA RID: 7898
		public static readonly ADPropertyDefinition DataClassificationConfigFlags2 = new ADPropertyDefinition("DataClassificationConfigFlags2", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchMalwareFilterConfigFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 65578250, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001EDB RID: 7899
		public static readonly ADPropertyDefinition DataClassificationConfigQuotaSettings = new ADPropertyDefinition("DataClassificationConfigQuotaSettings", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchObjectCountQuota", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001EDC RID: 7900
		internal static readonly RangedValueConstraint<int> DistinctRegexesValidRange = new RangedValueConstraint<int>(0, 200);

		// Token: 0x04001EDD RID: 7901
		internal static readonly RangedValueConstraint<int> KeywordLengthValidRange = new RangedValueConstraint<int>(0, 100);

		// Token: 0x04001EDE RID: 7902
		internal static readonly RangedValueConstraint<int> NumberOfKeywordValidRange = new RangedValueConstraint<int>(0, 2048);

		// Token: 0x04001EDF RID: 7903
		public static readonly ADPropertyDefinition RegExGrammarLimit = ADObject.BitfieldProperty("RegExGrammarLimit", 0, DataClassificationConfigSchema.DataClassificationConfigFlags1);

		// Token: 0x04001EE0 RID: 7904
		public static readonly ADPropertyDefinition DistinctRegExes = ADObject.BitfieldProperty("DistinctRegExes", 1, 8, DataClassificationConfigSchema.DataClassificationConfigFlags1, DataClassificationConfigSchema.DistinctRegexesValidRange);

		// Token: 0x04001EE1 RID: 7905
		public static readonly ADPropertyDefinition KeywordLength = ADObject.BitfieldProperty("KeywordLength", 9, 9, DataClassificationConfigSchema.DataClassificationConfigFlags1, DataClassificationConfigSchema.KeywordLengthValidRange);

		// Token: 0x04001EE2 RID: 7906
		public static readonly ADPropertyDefinition NumberOfKeywords = ADObject.BitfieldProperty("NumberOfKeywords", 18, 12, DataClassificationConfigSchema.DataClassificationConfigFlags1, DataClassificationConfigSchema.NumberOfKeywordValidRange);

		// Token: 0x04001EE3 RID: 7907
		internal static readonly RangedValueConstraint<int> DistinctFunctionsValidRange = new RangedValueConstraint<int>(0, 50);

		// Token: 0x04001EE4 RID: 7908
		internal static readonly RangedValueConstraint<int> MaxAnyBlocksValidRange = new RangedValueConstraint<int>(0, 100);

		// Token: 0x04001EE5 RID: 7909
		internal static readonly RangedValueConstraint<int> MaxNestedAnyBlocksValidRange = new RangedValueConstraint<int>(0, 10);

		// Token: 0x04001EE6 RID: 7910
		internal static readonly RangedValueConstraint<int> RegexLengthValidRange = new RangedValueConstraint<int>(0, 1024);

		// Token: 0x04001EE7 RID: 7911
		public static readonly ADPropertyDefinition DistinctFunctions = ADObject.BitfieldProperty("DistinctFunctions", 0, 6, DataClassificationConfigSchema.DataClassificationConfigFlags2, DataClassificationConfigSchema.DistinctFunctionsValidRange);

		// Token: 0x04001EE8 RID: 7912
		public static readonly ADPropertyDefinition MaxAnyBlocks = ADObject.BitfieldProperty("MaxAnyBlocks", 6, 7, DataClassificationConfigSchema.DataClassificationConfigFlags2, DataClassificationConfigSchema.MaxAnyBlocksValidRange);

		// Token: 0x04001EE9 RID: 7913
		public static readonly ADPropertyDefinition MaxNestedAnyBlocks = ADObject.BitfieldProperty("MaxNestedAnyBlocks", 13, 4, DataClassificationConfigSchema.DataClassificationConfigFlags2, DataClassificationConfigSchema.MaxNestedAnyBlocksValidRange);

		// Token: 0x04001EEA RID: 7914
		public static readonly ADPropertyDefinition RegExLength = ADObject.BitfieldProperty("RegExLength", 17, 11, DataClassificationConfigSchema.DataClassificationConfigFlags2, DataClassificationConfigSchema.RegexLengthValidRange);

		// Token: 0x04001EEB RID: 7915
		public static readonly ADPropertyDefinition MaxRulePackageSize = new ADPropertyDefinition("MaxRulePackageSize", ExchangeObjectVersion.Exchange2012, typeof(ByteQuantifiedSize), null, ADPropertyDefinitionFlags.Calculated, ByteQuantifiedSize.FromKB(150UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(500UL))
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DataClassificationConfigSchema.DataClassificationConfigQuotaSettings
		}, null, (IPropertyBag propertyBag) => DataClassificationConfig.QuotaSettingGetter(DataClassificationConfigSchema.MaxRulePackageSize, propertyBag), delegate(object value, IPropertyBag propertyBag)
		{
			DataClassificationConfig.QuotaSettingSetter(DataClassificationConfigSchema.MaxRulePackageSize, value, propertyBag);
		}, null, null);

		// Token: 0x04001EEC RID: 7916
		public static readonly ADPropertyDefinition MaxRulePackages = new ADPropertyDefinition("MaxRulePackages", ExchangeObjectVersion.Exchange2012, typeof(int), null, ADPropertyDefinitionFlags.Calculated, 10, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 350)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DataClassificationConfigSchema.DataClassificationConfigQuotaSettings
		}, null, (IPropertyBag propertyBag) => DataClassificationConfig.QuotaSettingGetter(DataClassificationConfigSchema.MaxRulePackages, propertyBag), delegate(object value, IPropertyBag propertyBag)
		{
			DataClassificationConfig.QuotaSettingSetter(DataClassificationConfigSchema.MaxRulePackages, value, propertyBag);
		}, null, null);

		// Token: 0x04001EED RID: 7917
		public static readonly ADPropertyDefinition MaxFingerprints = new ADPropertyDefinition("MaxFingerprints", ExchangeObjectVersion.Exchange2012, typeof(int), null, ADPropertyDefinitionFlags.Calculated, 100, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 500)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DataClassificationConfigSchema.DataClassificationConfigQuotaSettings
		}, null, (IPropertyBag propertyBag) => DataClassificationConfig.QuotaSettingGetter(DataClassificationConfigSchema.MaxFingerprints, propertyBag), delegate(object value, IPropertyBag propertyBag)
		{
			DataClassificationConfig.QuotaSettingSetter(DataClassificationConfigSchema.MaxFingerprints, value, propertyBag);
		}, null, null);

		// Token: 0x04001EEE RID: 7918
		public static readonly ADPropertyDefinition FingerprintThreshold = new ADPropertyDefinition("FingerprintThreshold", ExchangeObjectVersion.Exchange2012, typeof(int), null, ADPropertyDefinitionFlags.Calculated, 50, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 100)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DataClassificationConfigSchema.DataClassificationConfigQuotaSettings
		}, null, (IPropertyBag propertyBag) => DataClassificationConfig.QuotaSettingGetter(DataClassificationConfigSchema.FingerprintThreshold, propertyBag), delegate(object value, IPropertyBag propertyBag)
		{
			DataClassificationConfig.QuotaSettingSetter(DataClassificationConfigSchema.FingerprintThreshold, value, propertyBag);
		}, null, null);
	}
}
