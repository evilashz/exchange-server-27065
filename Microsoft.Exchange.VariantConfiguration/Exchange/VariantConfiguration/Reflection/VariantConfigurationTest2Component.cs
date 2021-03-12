using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000122 RID: 290
	public sealed class VariantConfigurationTest2Component : VariantConfigurationComponent
	{
		// Token: 0x06000D88 RID: 3464 RVA: 0x00020AF8 File Offset: 0x0001ECF8
		internal VariantConfigurationTest2Component() : base("Test2")
		{
			base.Add(new VariantConfigurationSection("Test2.settings.ini", "Test2SettingsOn", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Test2.settings.ini", "Test2Settings", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Test2.settings.ini", "Test2SettingsEnterprise", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Test2.settings.ini", "Test2SettingsOff", typeof(IFeature), false));
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00020B90 File Offset: 0x0001ED90
		public VariantConfigurationSection Test2SettingsOn
		{
			get
			{
				return base["Test2SettingsOn"];
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00020B9D File Offset: 0x0001ED9D
		public VariantConfigurationSection Test2Settings
		{
			get
			{
				return base["Test2Settings"];
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00020BAA File Offset: 0x0001EDAA
		public VariantConfigurationSection Test2SettingsEnterprise
		{
			get
			{
				return base["Test2SettingsEnterprise"];
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00020BB7 File Offset: 0x0001EDB7
		public VariantConfigurationSection Test2SettingsOff
		{
			get
			{
				return base["Test2SettingsOff"];
			}
		}
	}
}
