using System;
using Microsoft.Exchange.VariantConfiguration.Settings;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000126 RID: 294
	public sealed class VariantConfigurationVariantConfigComponent : VariantConfigurationComponent
	{
		// Token: 0x06000DC4 RID: 3524 RVA: 0x00021528 File Offset: 0x0001F728
		internal VariantConfigurationVariantConfigComponent() : base("VariantConfig")
		{
			base.Add(new VariantConfigurationSection("VariantConfig.settings.ini", "Microsoft", typeof(IOrganizationNameSettings), false));
			base.Add(new VariantConfigurationSection("VariantConfig.settings.ini", "InternalAccess", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("VariantConfig.settings.ini", "SettingOverrideSync", typeof(IOverrideSyncSettings), false));
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x000215A0 File Offset: 0x0001F7A0
		public VariantConfigurationSection Microsoft
		{
			get
			{
				return base["Microsoft"];
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x000215AD File Offset: 0x0001F7AD
		public VariantConfigurationSection InternalAccess
		{
			get
			{
				return base["InternalAccess"];
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x000215BA File Offset: 0x0001F7BA
		public VariantConfigurationSection SettingOverrideSync
		{
			get
			{
				return base["SettingOverrideSync"];
			}
		}
	}
}
