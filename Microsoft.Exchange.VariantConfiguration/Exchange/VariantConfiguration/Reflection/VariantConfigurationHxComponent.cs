using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x0200010A RID: 266
	public sealed class VariantConfigurationHxComponent : VariantConfigurationComponent
	{
		// Token: 0x06000C18 RID: 3096 RVA: 0x0001CC4C File Offset: 0x0001AE4C
		internal VariantConfigurationHxComponent() : base("Hx")
		{
			base.Add(new VariantConfigurationSection("Hx.settings.ini", "SmartSyncWebSockets", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Hx.settings.ini", "EnforceDevicePolicy", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Hx.settings.ini", "Irm", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Hx.settings.ini", "ServiceAvailable", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Hx.settings.ini", "ClientSettingsPane", typeof(IFeature), false));
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0001CD04 File Offset: 0x0001AF04
		public VariantConfigurationSection SmartSyncWebSockets
		{
			get
			{
				return base["SmartSyncWebSockets"];
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0001CD11 File Offset: 0x0001AF11
		public VariantConfigurationSection EnforceDevicePolicy
		{
			get
			{
				return base["EnforceDevicePolicy"];
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0001CD1E File Offset: 0x0001AF1E
		public VariantConfigurationSection Irm
		{
			get
			{
				return base["Irm"];
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0001CD2B File Offset: 0x0001AF2B
		public VariantConfigurationSection ServiceAvailable
		{
			get
			{
				return base["ServiceAvailable"];
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0001CD38 File Offset: 0x0001AF38
		public VariantConfigurationSection ClientSettingsPane
		{
			get
			{
				return base["ClientSettingsPane"];
			}
		}
	}
}
