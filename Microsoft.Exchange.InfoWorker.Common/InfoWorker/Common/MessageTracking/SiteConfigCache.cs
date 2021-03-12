using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x0200030B RID: 779
	internal sealed class SiteConfigCache : LazyLookupTimeoutCacheWithDiagnostics<ADObjectId, SiteConfigCache.Item>
	{
		// Token: 0x0600170C RID: 5900 RVA: 0x0006B1A5 File Offset: 0x000693A5
		public SiteConfigCache() : base(2, 20, false, TimeSpan.FromHours(1.0))
		{
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0006B1C0 File Offset: 0x000693C0
		protected override SiteConfigCache.Item Create(ADObjectId key, ref bool shouldAdd)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<ADObjectId>(this.GetHashCode(), "SiteConfigCache miss, searching for {0}", key);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), OrganizationId.ForestWideOrgId, null, false), 116, "Create", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MessageTracking\\Caching\\SiteConfigCache.cs");
			shouldAdd = true;
			return SiteConfigCache.GetSiteConfiguration(tenantOrTopologyConfigurationSession, key);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0006B218 File Offset: 0x00069418
		public static SiteConfigCache.Item GetSiteConfiguration(IConfigurationSession globalConfigSession, ADObjectId siteId)
		{
			List<ServerInfo> casServerInfos;
			int arg = ServerInfo.GetCASServersInSite(globalConfigSession, siteId, out casServerInfos);
			TraceWrapper.SearchLibraryTracer.TraceDebug<int>(0, "Added {0} CAS servers for site", arg);
			List<string> hubServerFqdns;
			arg = ServerInfo.GetHubServersInSite(globalConfigSession, siteId, out hubServerFqdns);
			TraceWrapper.SearchLibraryTracer.TraceDebug<int>(0, "Added {0} HUB servers for site", arg);
			return new SiteConfigCache.Item(casServerInfos, hubServerFqdns);
		}

		// Token: 0x0200030C RID: 780
		internal sealed class Item
		{
			// Token: 0x0600170F RID: 5903 RVA: 0x0006B264 File Offset: 0x00069464
			public Item(List<ServerInfo> casServerInfos, List<string> hubServerFqdns)
			{
				if (casServerInfos == null)
				{
					throw new NullReferenceException("casServerInfos must not be null");
				}
				if (hubServerFqdns == null)
				{
					throw new NullReferenceException("hubServerFqdns must not be null");
				}
				this.CasServerInfos = casServerInfos;
				this.HubServerFqdns = hubServerFqdns;
				this.HubServerTable = new HashSet<string>();
				foreach (string text in this.HubServerFqdns)
				{
					this.HubServerTable.Add(text);
					int num = text.IndexOf('.');
					string text2 = null;
					if (num != -1)
					{
						text2 = text.Substring(0, num);
					}
					if (!string.IsNullOrEmpty(text2))
					{
						this.HubServerTable.Add(text2);
					}
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x06001710 RID: 5904 RVA: 0x0006B328 File Offset: 0x00069528
			// (set) Token: 0x06001711 RID: 5905 RVA: 0x0006B330 File Offset: 0x00069530
			public List<ServerInfo> CasServerInfos { get; private set; }

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x06001712 RID: 5906 RVA: 0x0006B339 File Offset: 0x00069539
			// (set) Token: 0x06001713 RID: 5907 RVA: 0x0006B341 File Offset: 0x00069541
			public List<string> HubServerFqdns { get; private set; }

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001714 RID: 5908 RVA: 0x0006B34A File Offset: 0x0006954A
			// (set) Token: 0x06001715 RID: 5909 RVA: 0x0006B352 File Offset: 0x00069552
			public HashSet<string> HubServerTable { get; private set; }
		}
	}
}
