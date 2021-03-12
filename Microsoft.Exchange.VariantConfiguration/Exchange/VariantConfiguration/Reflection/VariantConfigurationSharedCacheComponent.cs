using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x0200011F RID: 287
	public sealed class VariantConfigurationSharedCacheComponent : VariantConfigurationComponent
	{
		// Token: 0x06000D7D RID: 3453 RVA: 0x00020950 File Offset: 0x0001EB50
		internal VariantConfigurationSharedCacheComponent() : base("SharedCache")
		{
			base.Add(new VariantConfigurationSection("SharedCache.settings.ini", "UsePersistenceForCafe", typeof(IFeature), false));
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0002097D File Offset: 0x0001EB7D
		public VariantConfigurationSection UsePersistenceForCafe
		{
			get
			{
				return base["UsePersistenceForCafe"];
			}
		}
	}
}
