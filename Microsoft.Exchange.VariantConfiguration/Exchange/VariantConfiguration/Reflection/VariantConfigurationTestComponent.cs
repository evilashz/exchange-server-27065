using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000121 RID: 289
	public sealed class VariantConfigurationTestComponent : VariantConfigurationComponent
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x00020A2C File Offset: 0x0001EC2C
		internal VariantConfigurationTestComponent() : base("Test")
		{
			base.Add(new VariantConfigurationSection("Test.settings.ini", "TestSettingsEnterprise", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Test.settings.ini", "TestSettings", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Test.settings.ini", "TestSettingsOn", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Test.settings.ini", "TestSettingsOff", typeof(IFeature), false));
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00020AC4 File Offset: 0x0001ECC4
		public VariantConfigurationSection TestSettingsEnterprise
		{
			get
			{
				return base["TestSettingsEnterprise"];
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00020AD1 File Offset: 0x0001ECD1
		public VariantConfigurationSection TestSettings
		{
			get
			{
				return base["TestSettings"];
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00020ADE File Offset: 0x0001ECDE
		public VariantConfigurationSection TestSettingsOn
		{
			get
			{
				return base["TestSettingsOn"];
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00020AEB File Offset: 0x0001ECEB
		public VariantConfigurationSection TestSettingsOff
		{
			get
			{
				return base["TestSettingsOff"];
			}
		}
	}
}
