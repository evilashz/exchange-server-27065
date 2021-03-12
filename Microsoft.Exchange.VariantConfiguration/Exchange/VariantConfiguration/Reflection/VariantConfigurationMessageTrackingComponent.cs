using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000112 RID: 274
	public sealed class VariantConfigurationMessageTrackingComponent : VariantConfigurationComponent
	{
		// Token: 0x06000C96 RID: 3222 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
		internal VariantConfigurationMessageTrackingComponent() : base("MessageTracking")
		{
			base.Add(new VariantConfigurationSection("MessageTracking.settings.ini", "StatsLogging", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MessageTracking.settings.ini", "QueueViewerDiagnostics", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MessageTracking.settings.ini", "AllowDebugMode", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MessageTracking.settings.ini", "UseBackEndLocator", typeof(IFeature), false));
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0001E258 File Offset: 0x0001C458
		public VariantConfigurationSection StatsLogging
		{
			get
			{
				return base["StatsLogging"];
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0001E265 File Offset: 0x0001C465
		public VariantConfigurationSection QueueViewerDiagnostics
		{
			get
			{
				return base["QueueViewerDiagnostics"];
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0001E272 File Offset: 0x0001C472
		public VariantConfigurationSection AllowDebugMode
		{
			get
			{
				return base["AllowDebugMode"];
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0001E27F File Offset: 0x0001C47F
		public VariantConfigurationSection UseBackEndLocator
		{
			get
			{
				return base["UseBackEndLocator"];
			}
		}
	}
}
