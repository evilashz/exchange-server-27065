using System;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000673 RID: 1651
	[Serializable]
	public class SettingsDagScope : SettingsServerScope
	{
		// Token: 0x06004D21 RID: 19745 RVA: 0x0011CF43 File Offset: 0x0011B143
		public SettingsDagScope()
		{
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x0011CF4B File Offset: 0x0011B14B
		public SettingsDagScope(Guid? guidMatch) : base(guidMatch)
		{
		}

		// Token: 0x17001960 RID: 6496
		// (get) Token: 0x06004D23 RID: 19747 RVA: 0x0011CF54 File Offset: 0x0011B154
		internal override int DefaultPriority
		{
			get
			{
				return 150;
			}
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x0011CF5C File Offset: 0x0011B15C
		internal override QueryFilter ConstructScopeFilter(IConfigSchema schema)
		{
			Guid? guidMatch = base.Restriction.GuidMatch;
			if (guidMatch != null)
			{
				return new ComparisonFilter(ComparisonOperator.Equal, SettingsScopeFilterSchema.DagOrServerGuid, guidMatch.Value);
			}
			return QueryFilter.False;
		}
	}
}
