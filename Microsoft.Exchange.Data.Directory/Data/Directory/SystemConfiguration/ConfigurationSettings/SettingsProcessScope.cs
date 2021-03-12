using System;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000680 RID: 1664
	[Serializable]
	public class SettingsProcessScope : SettingsScope
	{
		// Token: 0x06004D94 RID: 19860 RVA: 0x0011E334 File Offset: 0x0011C534
		public SettingsProcessScope()
		{
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x0011E33C File Offset: 0x0011C53C
		public SettingsProcessScope(string nameMatch) : base(nameMatch, null, null)
		{
		}

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x06004D96 RID: 19862 RVA: 0x0011E347 File Offset: 0x0011C547
		internal override int DefaultPriority
		{
			get
			{
				return 250;
			}
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x0011E34E File Offset: 0x0011C54E
		internal override QueryFilter ConstructScopeFilter(IConfigSchema schema)
		{
			if (!string.IsNullOrEmpty(base.Restriction.NameMatch))
			{
				return new TextFilter(SettingsScopeFilterSchema.ProcessName, base.Restriction.NameMatch, MatchOptions.WildcardString, MatchFlags.IgnoreCase);
			}
			return QueryFilter.False;
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x0011E380 File Offset: 0x0011C580
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
			if (string.IsNullOrEmpty(base.Restriction.NameMatch))
			{
				throw new ConfigurationSettingsRestrictionExpectedException(base.GetType().Name);
			}
		}
	}
}
