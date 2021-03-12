using System;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000688 RID: 1672
	[Serializable]
	public class SettingsUserScope : SettingsScope
	{
		// Token: 0x06004DD4 RID: 19924 RVA: 0x0011EF19 File Offset: 0x0011D119
		public SettingsUserScope()
		{
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x0011EF21 File Offset: 0x0011D121
		public SettingsUserScope(Guid? guidMatch) : base(guidMatch)
		{
		}

		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x06004DD6 RID: 19926 RVA: 0x0011EF2A File Offset: 0x0011D12A
		internal override int DefaultPriority
		{
			get
			{
				return 500;
			}
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x0011EF34 File Offset: 0x0011D134
		internal override QueryFilter ConstructScopeFilter(IConfigSchema schema)
		{
			Guid? guidMatch = base.Restriction.GuidMatch;
			if (guidMatch != null)
			{
				return new ComparisonFilter(ComparisonOperator.Equal, SettingsScopeFilterSchema.MailboxGuid, guidMatch.Value);
			}
			return QueryFilter.False;
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x0011EF74 File Offset: 0x0011D174
		internal override void Validate(IConfigSchema schema)
		{
			if (base.Restriction == null)
			{
				throw new ConfigurationSettingsRestrictionExpectedException(base.GetType().Name);
			}
			if (!string.IsNullOrEmpty(base.Restriction.SubType))
			{
				throw new ConfigurationSettingsRestrictionExtraProperty(base.GetType().Name, "SubType");
			}
			if (!string.IsNullOrEmpty(base.Restriction.MinVersion))
			{
				throw new ConfigurationSettingsRestrictionExtraProperty(base.GetType().Name, "MinVersion");
			}
			if (!string.IsNullOrEmpty(base.Restriction.MaxVersion))
			{
				throw new ConfigurationSettingsRestrictionExtraProperty(base.GetType().Name, "MaxVersion");
			}
			if (base.Restriction.GuidMatch == null)
			{
				throw new ConfigurationSettingsRestrictionExpectedException(base.GetType().Name);
			}
		}
	}
}
