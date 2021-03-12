using System;
using System.Web;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006BA RID: 1722
	internal class ArchiveServiceDiscoveryCache : BaseWebCache<string, string>
	{
		// Token: 0x060034FF RID: 13567 RVA: 0x000BF3AB File Offset: 0x000BD5AB
		internal ArchiveServiceDiscoveryCache() : base("_XAU_", SlidingOrAbsoluteTimeout.Absolute, 10)
		{
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06003500 RID: 13568 RVA: 0x000BF3BB File Offset: 0x000BD5BB
		public static ArchiveServiceDiscoveryCache Singleton
		{
			get
			{
				return ArchiveServiceDiscoveryCache.singleton;
			}
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000BF3C2 File Offset: 0x000BD5C2
		public void Remove(string key)
		{
			HttpRuntime.Cache.Remove(this.BuildKey(key));
		}

		// Token: 0x04001DC0 RID: 7616
		private const string ArchiveServiceDiscoveryKeyPrefix = "_XAU_";

		// Token: 0x04001DC1 RID: 7617
		private const int TimeoutInMinutes = 10;

		// Token: 0x04001DC2 RID: 7618
		private static ArchiveServiceDiscoveryCache singleton = new ArchiveServiceDiscoveryCache();
	}
}
