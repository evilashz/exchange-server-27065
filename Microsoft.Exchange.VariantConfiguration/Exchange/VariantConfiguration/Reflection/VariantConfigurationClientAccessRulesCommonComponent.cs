using System;
using Microsoft.Exchange.Flighting;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x020000FE RID: 254
	public sealed class VariantConfigurationClientAccessRulesCommonComponent : VariantConfigurationComponent
	{
		// Token: 0x06000B0C RID: 2828 RVA: 0x00019E44 File Offset: 0x00018044
		internal VariantConfigurationClientAccessRulesCommonComponent() : base("ClientAccessRulesCommon")
		{
			base.Add(new VariantConfigurationSection("ClientAccessRulesCommon.settings.ini", "ImplicitAllowLocalClientAccessRulesEnabled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ClientAccessRulesCommon.settings.ini", "ClientAccessRulesCacheExpiryTime", typeof(ICacheExpiryTimeInMinutes), false));
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00019E9C File Offset: 0x0001809C
		public VariantConfigurationSection ImplicitAllowLocalClientAccessRulesEnabled
		{
			get
			{
				return base["ImplicitAllowLocalClientAccessRulesEnabled"];
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00019EA9 File Offset: 0x000180A9
		public VariantConfigurationSection ClientAccessRulesCacheExpiryTime
		{
			get
			{
				return base["ClientAccessRulesCacheExpiryTime"];
			}
		}
	}
}
