using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200064F RID: 1615
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class LocalSiteCache
	{
		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x06004BDE RID: 19422 RVA: 0x00118122 File Offset: 0x00116322
		public static ADSite LocalSite
		{
			get
			{
				LocalSiteCache.InitializeIfNeeded();
				return LocalSiteCache.localSite;
			}
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x0011812E File Offset: 0x0011632E
		private static void InitializeIfNeeded()
		{
			if (LocalSiteCache.localSite == null || LocalSiteCache.nextRefresh < DateTime.UtcNow)
			{
				LocalSiteCache.ReadLocalSite();
				LocalSiteCache.nextRefresh = DateTime.UtcNow + LocalSiteCache.RefreshInterval;
			}
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x00118164 File Offset: 0x00116364
		private static void ReadLocalSite()
		{
			LocalSiteCache.Tracer.TraceDebug(0L, "LocalSiteCache: reading local site object");
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 70, "ReadLocalSite", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\LocalSiteCache.cs");
			LocalSiteCache.localSite = topologyConfigurationSession.GetLocalSite();
		}

		// Token: 0x0400340D RID: 13325
		private static readonly Trace Tracer = ExTraceGlobals.SystemConfigurationCacheTracer;

		// Token: 0x0400340E RID: 13326
		private static readonly TimeSpan RefreshInterval = TimeSpan.FromHours(1.0);

		// Token: 0x0400340F RID: 13327
		private static DateTime nextRefresh;

		// Token: 0x04003410 RID: 13328
		private static ADSite localSite;
	}
}
