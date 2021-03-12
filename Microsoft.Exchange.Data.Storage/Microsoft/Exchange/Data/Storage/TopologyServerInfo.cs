using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D74 RID: 3444
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TopologyServerInfo
	{
		// Token: 0x060076F7 RID: 30455 RVA: 0x0020D8BC File Offset: 0x0020BABC
		public TopologyServerInfo(Site site, TopologyServer server)
		{
			this.Site = site;
			this.DistinguishedName = server.DistinguishedName;
			this.ServerFullyQualifiedDomainName = server.Fqdn;
			this.VersionNumber = server.VersionNumber;
			this.AdminDisplayVersionNumber = server.AdminDisplayVersion;
			this.Role = server.CurrentServerRole;
			this.IsOutOfService = ((bool)server[ActiveDirectoryServerSchema.IsOutOfService] || !ServerComponentStates.IsServerOnline(server.ComponentStates));
		}

		// Token: 0x17001FD1 RID: 8145
		// (get) Token: 0x060076F8 RID: 30456 RVA: 0x0020D93B File Offset: 0x0020BB3B
		// (set) Token: 0x060076F9 RID: 30457 RVA: 0x0020D943 File Offset: 0x0020BB43
		public string DistinguishedName { get; private set; }

		// Token: 0x17001FD2 RID: 8146
		// (get) Token: 0x060076FA RID: 30458 RVA: 0x0020D94C File Offset: 0x0020BB4C
		// (set) Token: 0x060076FB RID: 30459 RVA: 0x0020D954 File Offset: 0x0020BB54
		public Site Site { get; private set; }

		// Token: 0x17001FD3 RID: 8147
		// (get) Token: 0x060076FC RID: 30460 RVA: 0x0020D95D File Offset: 0x0020BB5D
		// (set) Token: 0x060076FD RID: 30461 RVA: 0x0020D965 File Offset: 0x0020BB65
		public string ServerFullyQualifiedDomainName { get; private set; }

		// Token: 0x17001FD4 RID: 8148
		// (get) Token: 0x060076FE RID: 30462 RVA: 0x0020D96E File Offset: 0x0020BB6E
		// (set) Token: 0x060076FF RID: 30463 RVA: 0x0020D976 File Offset: 0x0020BB76
		public int VersionNumber { get; private set; }

		// Token: 0x17001FD5 RID: 8149
		// (get) Token: 0x06007700 RID: 30464 RVA: 0x0020D97F File Offset: 0x0020BB7F
		// (set) Token: 0x06007701 RID: 30465 RVA: 0x0020D987 File Offset: 0x0020BB87
		public ServerVersion AdminDisplayVersionNumber { get; private set; }

		// Token: 0x17001FD6 RID: 8150
		// (get) Token: 0x06007702 RID: 30466 RVA: 0x0020D990 File Offset: 0x0020BB90
		// (set) Token: 0x06007703 RID: 30467 RVA: 0x0020D998 File Offset: 0x0020BB98
		public ServerRole Role { get; private set; }

		// Token: 0x17001FD7 RID: 8151
		// (get) Token: 0x06007704 RID: 30468 RVA: 0x0020D9A1 File Offset: 0x0020BBA1
		// (set) Token: 0x06007705 RID: 30469 RVA: 0x0020D9A9 File Offset: 0x0020BBA9
		public bool IsOutOfService { get; private set; }

		// Token: 0x06007706 RID: 30470 RVA: 0x0020D9B4 File Offset: 0x0020BBB4
		internal static TopologyServerInfo Get(TopologyServer server, ServiceTopology.All all)
		{
			TopologyServerInfo topologyServerInfo;
			if (!all.Servers.TryGetValue(server.DistinguishedName, out topologyServerInfo))
			{
				Site site = Site.Get(server.TopologySite, all);
				topologyServerInfo = new TopologyServerInfo(site, server);
				all.Servers.Add(topologyServerInfo.DistinguishedName, topologyServerInfo);
			}
			return topologyServerInfo;
		}
	}
}
