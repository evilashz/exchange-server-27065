using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000116 RID: 278
	public sealed class VariantConfigurationOABComponent : VariantConfigurationComponent
	{
		// Token: 0x06000CAC RID: 3244 RVA: 0x0001E544 File Offset: 0x0001C744
		internal VariantConfigurationOABComponent() : base("OAB")
		{
			base.Add(new VariantConfigurationSection("OAB.settings.ini", "LinkedOABGenMailboxes", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OAB.settings.ini", "SkipServiceTopologyDiscovery", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OAB.settings.ini", "EnforceManifestVersionMatch", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OAB.settings.ini", "SharedTemplateFiles", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OAB.settings.ini", "GenerateRequestedOABsOnly", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OAB.settings.ini", "OabHttpClientAccessRulesEnabled", typeof(IFeature), false));
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0001E61C File Offset: 0x0001C81C
		public VariantConfigurationSection LinkedOABGenMailboxes
		{
			get
			{
				return base["LinkedOABGenMailboxes"];
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0001E629 File Offset: 0x0001C829
		public VariantConfigurationSection SkipServiceTopologyDiscovery
		{
			get
			{
				return base["SkipServiceTopologyDiscovery"];
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0001E636 File Offset: 0x0001C836
		public VariantConfigurationSection EnforceManifestVersionMatch
		{
			get
			{
				return base["EnforceManifestVersionMatch"];
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0001E643 File Offset: 0x0001C843
		public VariantConfigurationSection SharedTemplateFiles
		{
			get
			{
				return base["SharedTemplateFiles"];
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0001E650 File Offset: 0x0001C850
		public VariantConfigurationSection GenerateRequestedOABsOnly
		{
			get
			{
				return base["GenerateRequestedOABsOnly"];
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0001E65D File Offset: 0x0001C85D
		public VariantConfigurationSection OabHttpClientAccessRulesEnabled
		{
			get
			{
				return base["OabHttpClientAccessRulesEnabled"];
			}
		}
	}
}
