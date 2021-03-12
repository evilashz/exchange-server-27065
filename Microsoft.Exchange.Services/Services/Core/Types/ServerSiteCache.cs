using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200024F RID: 591
	internal class ServerSiteCache : LazyLookupTimeoutCache<string, ADObjectId>
	{
		// Token: 0x06000F7D RID: 3965 RVA: 0x0004C2B1 File Offset: 0x0004A4B1
		private ServerSiteCache() : base(2, 1000, false, TimeSpan.FromDays(1.0))
		{
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0004C2D0 File Offset: 0x0004A4D0
		protected override ADObjectId CreateOnCacheMiss(string key, ref bool shouldAdd)
		{
			Server server = ServerSiteCache.session.FindServerByFqdn(key);
			if (server != null)
			{
				return server.ServerSite;
			}
			return null;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0004C2F4 File Offset: 0x0004A4F4
		protected override string PreprocessKey(string key)
		{
			return key.ToLowerInvariant();
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0004C2FC File Offset: 0x0004A4FC
		public static ServerSiteCache Singleton
		{
			get
			{
				return ServerSiteCache.singleton;
			}
		}

		// Token: 0x04000BC4 RID: 3012
		private static readonly ServerSiteCache singleton = new ServerSiteCache();

		// Token: 0x04000BC5 RID: 3013
		private static readonly ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 21, "session", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\RequestProxying\\ServerSiteCache.cs");
	}
}
