using System;
using System.Web;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200024D RID: 589
	internal class ProxySuggesterSidCache : BaseWebCache<SuggesterSidCompositeKey, string>
	{
		// Token: 0x06000F75 RID: 3957 RVA: 0x0004C24C File Offset: 0x0004A44C
		internal ProxySuggesterSidCache() : base("_PSCC_", SlidingOrAbsoluteTimeout.Absolute, ProxySuggesterSidCache.TimeoutInMinutes)
		{
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0004C25F File Offset: 0x0004A45F
		public static ProxySuggesterSidCache Singleton
		{
			get
			{
				return ProxySuggesterSidCache.singleton;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0004C266 File Offset: 0x0004A466
		public static int TimeoutInMinutes
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0004C269 File Offset: 0x0004A469
		public void Remove(SuggesterSidCompositeKey key)
		{
			HttpRuntime.Cache.Remove(this.BuildKey(key));
		}

		// Token: 0x04000BC0 RID: 3008
		private const string ProxySuggesterSidKeyPrefix = "_PSCC_";

		// Token: 0x04000BC1 RID: 3009
		private static ProxySuggesterSidCache singleton = new ProxySuggesterSidCache();
	}
}
