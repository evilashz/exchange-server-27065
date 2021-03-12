using System;
using Microsoft.Exchange.TextProcessing.Boomerang;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x020000FB RID: 251
	public sealed class VariantConfigurationBoomerangComponent : VariantConfigurationComponent
	{
		// Token: 0x06000AE4 RID: 2788 RVA: 0x00019774 File Offset: 0x00017974
		internal VariantConfigurationBoomerangComponent() : base("Boomerang")
		{
			base.Add(new VariantConfigurationSection("Boomerang.settings.ini", "BoomerangSettings", typeof(IBoomerangSettings), false));
			base.Add(new VariantConfigurationSection("Boomerang.settings.ini", "BoomerangMessageId", typeof(IFeature), false));
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x000197CC File Offset: 0x000179CC
		public VariantConfigurationSection BoomerangSettings
		{
			get
			{
				return base["BoomerangSettings"];
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x000197D9 File Offset: 0x000179D9
		public VariantConfigurationSection BoomerangMessageId
		{
			get
			{
				return base["BoomerangMessageId"];
			}
		}
	}
}
