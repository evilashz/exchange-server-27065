using System;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000676 RID: 1654
	[Serializable]
	public class SettingsGenericScope : SettingsScope
	{
		// Token: 0x06004D2F RID: 19759 RVA: 0x0011D0DB File Offset: 0x0011B2DB
		public SettingsGenericScope()
		{
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x0011D0E3 File Offset: 0x0011B2E3
		public SettingsGenericScope(string scopeType, string nameMatch) : base(scopeType, nameMatch)
		{
		}

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x06004D31 RID: 19761 RVA: 0x0011D0ED File Offset: 0x0011B2ED
		internal override int DefaultPriority
		{
			get
			{
				return 600;
			}
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x0011D0F4 File Offset: 0x0011B2F4
		internal override QueryFilter ConstructScopeFilter(IConfigSchema schema)
		{
			if (!string.IsNullOrEmpty(base.Restriction.SubType))
			{
				SettingsScopeFilterSchema schemaInstance = SettingsScopeFilterSchema.GetSchemaInstance(schema);
				PropertyDefinition propertyDefinition = schemaInstance.LookupSchemaProperty(base.Restriction.SubType);
				if (propertyDefinition != null)
				{
					return new TextFilter(propertyDefinition, base.Restriction.NameMatch, MatchOptions.WildcardString, MatchFlags.IgnoreCase);
				}
			}
			return QueryFilter.False;
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0011D148 File Offset: 0x0011B348
		internal override void Validate(IConfigSchema schema)
		{
			if (base.Restriction == null)
			{
				throw new ConfigurationSettingsRestrictionExpectedException(base.GetType().Name);
			}
			if (string.IsNullOrEmpty(base.Restriction.SubType))
			{
				throw new ConfigurationSettingsRestrictionMissingProperty(base.GetType().Name, "SubType");
			}
			if (!string.IsNullOrEmpty(base.Restriction.MinVersion))
			{
				throw new ConfigurationSettingsRestrictionExtraProperty(base.GetType().Name, "MinVersion");
			}
			if (!string.IsNullOrEmpty(base.Restriction.MaxVersion))
			{
				throw new ConfigurationSettingsRestrictionExtraProperty(base.GetType().Name, "MaxVersion");
			}
			if (schema != null)
			{
				schema.ParseAndValidateScopeValue(base.Restriction.SubType, base.Restriction.NameMatch);
			}
		}
	}
}
