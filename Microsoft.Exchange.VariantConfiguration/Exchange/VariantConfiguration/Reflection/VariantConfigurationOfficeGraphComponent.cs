using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000117 RID: 279
	public sealed class VariantConfigurationOfficeGraphComponent : VariantConfigurationComponent
	{
		// Token: 0x06000CB3 RID: 3251 RVA: 0x0001E66C File Offset: 0x0001C86C
		internal VariantConfigurationOfficeGraphComponent() : base("OfficeGraph")
		{
			base.Add(new VariantConfigurationSection("OfficeGraph.settings.ini", "OfficeGraphGenerateSignals", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("OfficeGraph.settings.ini", "OfficeGraphAgent", typeof(IFeature), false));
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
		public VariantConfigurationSection OfficeGraphGenerateSignals
		{
			get
			{
				return base["OfficeGraphGenerateSignals"];
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0001E6D1 File Offset: 0x0001C8D1
		public VariantConfigurationSection OfficeGraphAgent
		{
			get
			{
				return base["OfficeGraphAgent"];
			}
		}
	}
}
