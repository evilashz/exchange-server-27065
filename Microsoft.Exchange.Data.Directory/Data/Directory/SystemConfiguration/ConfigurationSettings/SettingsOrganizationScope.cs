using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x0200067F RID: 1663
	[Serializable]
	public class SettingsOrganizationScope : SettingsDatabaseScope
	{
		// Token: 0x06004D90 RID: 19856 RVA: 0x0011E264 File Offset: 0x0011C464
		public SettingsOrganizationScope()
		{
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x0011E26C File Offset: 0x0011C46C
		public SettingsOrganizationScope(string nameMatch, string minVersion, string maxVersion) : base(nameMatch, minVersion, maxVersion)
		{
		}

		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x06004D92 RID: 19858 RVA: 0x0011E277 File Offset: 0x0011C477
		internal override int DefaultPriority
		{
			get
			{
				return 400;
			}
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x0011E280 File Offset: 0x0011C480
		internal override QueryFilter ConstructScopeFilter(IConfigSchema schema)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			if (base.Restriction.MinExchangeVersion != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, SettingsScopeFilterSchema.OrganizationVersion, base.Restriction.MinExchangeVersion));
			}
			if (base.Restriction.MaxExchangeVersion != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.LessThanOrEqual, SettingsScopeFilterSchema.OrganizationVersion, base.Restriction.MaxExchangeVersion));
			}
			if (!string.IsNullOrEmpty(base.Restriction.NameMatch))
			{
				list.Add(new TextFilter(SettingsScopeFilterSchema.OrganizationName, base.Restriction.NameMatch, MatchOptions.WildcardString, MatchFlags.IgnoreCase));
			}
			return QueryFilter.AndTogether(list.ToArray()) ?? QueryFilter.False;
		}
	}
}
