using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200024E RID: 590
	internal class ProxyTokenCache : BaseWebCache<string, SerializedSecurityAccessToken>
	{
		// Token: 0x06000F7A RID: 3962 RVA: 0x0004C289 File Offset: 0x0004A489
		public ProxyTokenCache() : base("_PTC_", SlidingOrAbsoluteTimeout.Absolute, ProxySuggesterSidCache.TimeoutInMinutes + 1)
		{
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0004C29E File Offset: 0x0004A49E
		public static ProxyTokenCache Singleton
		{
			get
			{
				return ProxyTokenCache.singleton;
			}
		}

		// Token: 0x04000BC2 RID: 3010
		private const string ProxyTokenCachePrefix = "_PTC_";

		// Token: 0x04000BC3 RID: 3011
		private static readonly ProxyTokenCache singleton = new ProxyTokenCache();
	}
}
