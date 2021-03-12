using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000107 RID: 263
	public sealed class VariantConfigurationGlobalComponent : VariantConfigurationComponent
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x0001CB0C File Offset: 0x0001AD0C
		internal VariantConfigurationGlobalComponent() : base("Global")
		{
			base.Add(new VariantConfigurationSection("Global.settings.ini", "GlobalCriminalCompliance", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Global.settings.ini", "WindowsLiveID", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Global.settings.ini", "DistributedKeyManagement", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Global.settings.ini", "MultiTenancy", typeof(IFeature), false));
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0001CBA4 File Offset: 0x0001ADA4
		public VariantConfigurationSection GlobalCriminalCompliance
		{
			get
			{
				return base["GlobalCriminalCompliance"];
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0001CBB1 File Offset: 0x0001ADB1
		public VariantConfigurationSection WindowsLiveID
		{
			get
			{
				return base["WindowsLiveID"];
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0001CBBE File Offset: 0x0001ADBE
		public VariantConfigurationSection DistributedKeyManagement
		{
			get
			{
				return base["DistributedKeyManagement"];
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0001CBCB File Offset: 0x0001ADCB
		public VariantConfigurationSection MultiTenancy
		{
			get
			{
				return base["MultiTenancy"];
			}
		}
	}
}
