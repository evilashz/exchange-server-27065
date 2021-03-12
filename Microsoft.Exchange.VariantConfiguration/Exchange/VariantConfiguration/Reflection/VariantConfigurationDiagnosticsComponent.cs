using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000102 RID: 258
	public sealed class VariantConfigurationDiagnosticsComponent : VariantConfigurationComponent
	{
		// Token: 0x06000BC0 RID: 3008 RVA: 0x0001BD99 File Offset: 0x00019F99
		internal VariantConfigurationDiagnosticsComponent() : base("Diagnostics")
		{
			base.Add(new VariantConfigurationSection("Diagnostics.settings.ini", "TraceToHeadersLogger", typeof(IFeature), false));
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0001BDC6 File Offset: 0x00019FC6
		public VariantConfigurationSection TraceToHeadersLogger
		{
			get
			{
				return base["TraceToHeadersLogger"];
			}
		}
	}
}
