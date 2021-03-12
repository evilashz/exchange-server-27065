using System;
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003E8 RID: 1000
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class DataClassificationConfig : ADConfigurationObject
	{
		// Token: 0x06002DE9 RID: 11753 RVA: 0x000BB340 File Offset: 0x000B9540
		internal static object QuotaSettingGetter(ADPropertyDefinition adPropertyDefinition, IPropertyBag propertyBag)
		{
			if (adPropertyDefinition == null)
			{
				throw new ArgumentNullException("adPropertyDefinition");
			}
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[DataClassificationConfigSchema.DataClassificationConfigQuotaSettings];
			string quotaSettingIdentifier = adPropertyDefinition.Name + ':';
			object result = adPropertyDefinition.DefaultValue;
			if (multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				string text = multiValuedProperty.FirstOrDefault((string item) => item.StartsWith(quotaSettingIdentifier, StringComparison.Ordinal));
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						result = ValueConvertor.ConvertValueFromString(text.Substring(quotaSettingIdentifier.Length), adPropertyDefinition.Type, null);
					}
					catch (FormatException ex)
					{
						PropertyValidationError error = new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(adPropertyDefinition.Name, ex.Message), adPropertyDefinition, propertyBag[DataClassificationConfigSchema.DataClassificationConfigQuotaSettings]);
						throw new DataValidationException(error, ex);
					}
				}
			}
			return result;
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000BB428 File Offset: 0x000B9628
		internal static void QuotaSettingSetter(ADPropertyDefinition adPropertyDefinition, object quota, IPropertyBag propertyBag)
		{
			if (adPropertyDefinition == null)
			{
				throw new ArgumentNullException("adPropertyDefinition");
			}
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[DataClassificationConfigSchema.DataClassificationConfigQuotaSettings];
			string text = adPropertyDefinition.Name + ':';
			if (multiValuedProperty != null && multiValuedProperty.Count != 0)
			{
				for (int i = multiValuedProperty.Count - 1; i >= 0; i--)
				{
					if (string.IsNullOrEmpty(multiValuedProperty[i]) || multiValuedProperty[i].StartsWith(text, StringComparison.Ordinal))
					{
						multiValuedProperty.RemoveAt(i);
					}
				}
			}
			if (!object.Equals(quota, adPropertyDefinition.DefaultValue))
			{
				string arg = ValueConvertor.ConvertValueToString(quota, null);
				string item = string.Format("{0}{1}", text, arg);
				multiValuedProperty.Add(item);
			}
			propertyBag[DataClassificationConfigSchema.DataClassificationConfigQuotaSettings] = multiValuedProperty;
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000BB4E2 File Offset: 0x000B96E2
		internal override ADObjectSchema Schema
		{
			get
			{
				return DataClassificationConfig.schema;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x000BB4E9 File Offset: 0x000B96E9
		internal override ADObjectId ParentPath
		{
			get
			{
				return DataClassificationConfig.parentPath;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x000BB4F0 File Offset: 0x000B96F0
		internal override string MostDerivedObjectClass
		{
			get
			{
				return DataClassificationConfig.ldapName;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x000BB4F7 File Offset: 0x000B96F7
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000BB4FE File Offset: 0x000B96FE
		// (set) Token: 0x06002DF0 RID: 11760 RVA: 0x000BB510 File Offset: 0x000B9710
		[Parameter]
		public bool RegExGrammarLimit
		{
			get
			{
				return (bool)this[DataClassificationConfigSchema.RegExGrammarLimit];
			}
			set
			{
				this[DataClassificationConfigSchema.RegExGrammarLimit] = value;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x000BB523 File Offset: 0x000B9723
		// (set) Token: 0x06002DF2 RID: 11762 RVA: 0x000BB535 File Offset: 0x000B9735
		[Parameter]
		public int DistinctRegExes
		{
			get
			{
				return (int)this[DataClassificationConfigSchema.DistinctRegExes];
			}
			set
			{
				this[DataClassificationConfigSchema.DistinctRegExes] = value;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x000BB548 File Offset: 0x000B9748
		// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x000BB55A File Offset: 0x000B975A
		[Parameter]
		public int KeywordLength
		{
			get
			{
				return (int)this[DataClassificationConfigSchema.KeywordLength];
			}
			set
			{
				this[DataClassificationConfigSchema.KeywordLength] = value;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x000BB56D File Offset: 0x000B976D
		// (set) Token: 0x06002DF6 RID: 11766 RVA: 0x000BB57F File Offset: 0x000B977F
		[Parameter]
		public int NumberOfKeywords
		{
			get
			{
				return (int)this[DataClassificationConfigSchema.NumberOfKeywords];
			}
			set
			{
				this[DataClassificationConfigSchema.NumberOfKeywords] = value;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x000BB592 File Offset: 0x000B9792
		// (set) Token: 0x06002DF8 RID: 11768 RVA: 0x000BB5A4 File Offset: 0x000B97A4
		[Parameter]
		public int DistinctFunctions
		{
			get
			{
				return (int)this[DataClassificationConfigSchema.DistinctFunctions];
			}
			set
			{
				this[DataClassificationConfigSchema.DistinctFunctions] = value;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x000BB5B7 File Offset: 0x000B97B7
		// (set) Token: 0x06002DFA RID: 11770 RVA: 0x000BB5C9 File Offset: 0x000B97C9
		[Parameter]
		public int MaxAnyBlocks
		{
			get
			{
				return (int)this[DataClassificationConfigSchema.MaxAnyBlocks];
			}
			set
			{
				this[DataClassificationConfigSchema.MaxAnyBlocks] = value;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06002DFB RID: 11771 RVA: 0x000BB5DC File Offset: 0x000B97DC
		// (set) Token: 0x06002DFC RID: 11772 RVA: 0x000BB5EE File Offset: 0x000B97EE
		[Parameter]
		public int MaxNestedAnyBlocks
		{
			get
			{
				return (int)this[DataClassificationConfigSchema.MaxNestedAnyBlocks];
			}
			set
			{
				this[DataClassificationConfigSchema.MaxNestedAnyBlocks] = value;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06002DFD RID: 11773 RVA: 0x000BB601 File Offset: 0x000B9801
		// (set) Token: 0x06002DFE RID: 11774 RVA: 0x000BB613 File Offset: 0x000B9813
		[Parameter]
		public int RegExLength
		{
			get
			{
				return (int)this[DataClassificationConfigSchema.RegExLength];
			}
			set
			{
				this[DataClassificationConfigSchema.RegExLength] = value;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06002DFF RID: 11775 RVA: 0x000BB626 File Offset: 0x000B9826
		// (set) Token: 0x06002E00 RID: 11776 RVA: 0x000BB638 File Offset: 0x000B9838
		[Parameter]
		public ByteQuantifiedSize MaxRulePackageSize
		{
			get
			{
				return (ByteQuantifiedSize)this[DataClassificationConfigSchema.MaxRulePackageSize];
			}
			set
			{
				this[DataClassificationConfigSchema.MaxRulePackageSize] = value;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x000BB64B File Offset: 0x000B984B
		// (set) Token: 0x06002E02 RID: 11778 RVA: 0x000BB65D File Offset: 0x000B985D
		[Parameter]
		public int MaxRulePackages
		{
			get
			{
				return (int)this[DataClassificationConfigSchema.MaxRulePackages];
			}
			set
			{
				this[DataClassificationConfigSchema.MaxRulePackages] = value;
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06002E03 RID: 11779 RVA: 0x000BB670 File Offset: 0x000B9870
		// (set) Token: 0x06002E04 RID: 11780 RVA: 0x000BB682 File Offset: 0x000B9882
		[Parameter]
		public int MaxFingerprints
		{
			get
			{
				return (int)this[DataClassificationConfigSchema.MaxFingerprints];
			}
			set
			{
				this[DataClassificationConfigSchema.MaxFingerprints] = value;
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x000BB695 File Offset: 0x000B9895
		// (set) Token: 0x06002E06 RID: 11782 RVA: 0x000BB6A7 File Offset: 0x000B98A7
		[Parameter]
		public int FingerprintThreshold
		{
			get
			{
				return (int)this[DataClassificationConfigSchema.FingerprintThreshold];
			}
			set
			{
				this[DataClassificationConfigSchema.FingerprintThreshold] = value;
			}
		}

		// Token: 0x04001EBD RID: 7869
		private const char Separator = ':';

		// Token: 0x04001EBE RID: 7870
		internal const string ContainerName = "Default Data Config";

		// Token: 0x04001EBF RID: 7871
		private static readonly string ldapName = "msExchDataClassificationConfig";

		// Token: 0x04001EC0 RID: 7872
		private static readonly ADObjectId parentPath = new ADObjectId("CN=Data Classification");

		// Token: 0x04001EC1 RID: 7873
		private static readonly DataClassificationConfigSchema schema = ObjectSchema.GetInstance<DataClassificationConfigSchema>();
	}
}
