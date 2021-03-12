using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000674 RID: 1652
	[Serializable]
	public class SettingsDatabaseScope : SettingsServerScope
	{
		// Token: 0x06004D25 RID: 19749 RVA: 0x0011CF9B File Offset: 0x0011B19B
		public SettingsDatabaseScope()
		{
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x0011CFA3 File Offset: 0x0011B1A3
		public SettingsDatabaseScope(Guid? guidMatch) : base(guidMatch)
		{
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x0011CFAC File Offset: 0x0011B1AC
		public SettingsDatabaseScope(string nameMatch, string minVersion, string maxVersion) : base(nameMatch, minVersion, maxVersion)
		{
		}

		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x06004D28 RID: 19752 RVA: 0x0011CFB7 File Offset: 0x0011B1B7
		internal override int DefaultPriority
		{
			get
			{
				return 300;
			}
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x0011CFC0 File Offset: 0x0011B1C0
		internal override QueryFilter ConstructScopeFilter(IConfigSchema schema)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			Guid? guidMatch = base.Restriction.GuidMatch;
			if (guidMatch != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, SettingsScopeFilterSchema.DatabaseGuid, guidMatch.Value));
			}
			if (base.Restriction.MinServerVersion != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, SettingsScopeFilterSchema.DatabaseVersion, base.Restriction.MinServerVersion));
			}
			if (base.Restriction.MaxServerVersion != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.LessThanOrEqual, SettingsScopeFilterSchema.DatabaseVersion, base.Restriction.MaxServerVersion));
			}
			if (!string.IsNullOrEmpty(base.Restriction.NameMatch))
			{
				list.Add(new TextFilter(SettingsScopeFilterSchema.DatabaseName, base.Restriction.NameMatch, MatchOptions.WildcardString, MatchFlags.IgnoreCase));
			}
			return QueryFilter.AndTogether(list.ToArray()) ?? QueryFilter.False;
		}
	}
}
