using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200087C RID: 2172
	internal class ServerVersionCache : LazyLookupTimeoutCache<string, int>
	{
		// Token: 0x06003E50 RID: 15952 RVA: 0x000D84D5 File Offset: 0x000D66D5
		private ServerVersionCache() : base(2, 1000, false, TimeSpan.FromDays(1.0))
		{
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x000D84F4 File Offset: 0x000D66F4
		protected override int CreateOnCacheMiss(string key, ref bool shouldAdd)
		{
			Server server = ServerVersionCache.session.FindServerByFqdn(key);
			if (server != null)
			{
				return server.VersionNumber;
			}
			return 0;
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x000D8518 File Offset: 0x000D6718
		protected override string PreprocessKey(string key)
		{
			return key.ToLowerInvariant();
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06003E53 RID: 15955 RVA: 0x000D8520 File Offset: 0x000D6720
		public static ServerVersionCache Singleton
		{
			get
			{
				return ServerVersionCache.singleton;
			}
		}

		// Token: 0x040023C8 RID: 9160
		private static readonly ServerVersionCache singleton = new ServerVersionCache();

		// Token: 0x040023C9 RID: 9161
		private static readonly ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 21, "session", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\Types\\ServerVersionCache.cs");
	}
}
