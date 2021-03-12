using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000672 RID: 1650
	[Serializable]
	public class SettingsServerScope : SettingsScope
	{
		// Token: 0x06004D1B RID: 19739 RVA: 0x0011CD4E File Offset: 0x0011AF4E
		public SettingsServerScope()
		{
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x0011CD56 File Offset: 0x0011AF56
		public SettingsServerScope(Guid? guidMatch) : base(guidMatch)
		{
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x0011CD5F File Offset: 0x0011AF5F
		public SettingsServerScope(string nameMatch, string minVersion, string maxVersion) : base(nameMatch, minVersion, maxVersion)
		{
		}

		// Token: 0x1700195F RID: 6495
		// (get) Token: 0x06004D1E RID: 19742 RVA: 0x0011CD6A File Offset: 0x0011AF6A
		internal override int DefaultPriority
		{
			get
			{
				return 200;
			}
		}

		// Token: 0x06004D1F RID: 19743 RVA: 0x0011CD74 File Offset: 0x0011AF74
		internal override QueryFilter ConstructScopeFilter(IConfigSchema schema)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			Guid? guidMatch = base.Restriction.GuidMatch;
			if (guidMatch != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, SettingsScopeFilterSchema.ServerGuid, guidMatch.Value));
			}
			if (base.Restriction.MinServerVersion != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, SettingsScopeFilterSchema.ServerVersion, base.Restriction.MinServerVersion));
			}
			if (base.Restriction.MaxServerVersion != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.LessThanOrEqual, SettingsScopeFilterSchema.ServerVersion, base.Restriction.MaxServerVersion));
			}
			if (!string.IsNullOrEmpty(base.Restriction.NameMatch))
			{
				list.Add(new TextFilter(SettingsScopeFilterSchema.ServerName, base.Restriction.NameMatch, MatchOptions.WildcardString, MatchFlags.IgnoreCase));
			}
			return QueryFilter.AndTogether(list.ToArray()) ?? QueryFilter.False;
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x0011CE5C File Offset: 0x0011B05C
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
			bool flag = false;
			if (!string.IsNullOrEmpty(base.Restriction.MinVersion))
			{
				SettingsScopeRestriction.ValidateAsServerVersion(base.Restriction.MinVersion);
				flag = true;
			}
			if (!string.IsNullOrEmpty(base.Restriction.MaxVersion))
			{
				SettingsScopeRestriction.ValidateAsServerVersion(base.Restriction.MaxVersion);
				flag = true;
			}
			if (!string.IsNullOrEmpty(base.Restriction.NameMatch))
			{
				SettingsScopeRestriction.ValidateNameMatch(base.Restriction.NameMatch);
				flag = true;
			}
			if (base.Restriction.GuidMatch != null)
			{
				flag = true;
			}
			if (!flag)
			{
				throw new ConfigurationSettingsRestrictionExpectedException(base.GetType().Name);
			}
		}
	}
}
