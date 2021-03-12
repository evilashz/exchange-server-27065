using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002E1 RID: 737
	internal struct ServerInfo
	{
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00065A09 File Offset: 0x00063C09
		internal string Key
		{
			get
			{
				return this.lowerCaseFqdn;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x00065A11 File Offset: 0x00063C11
		internal ServerStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x00065A19 File Offset: 0x00063C19
		internal ADObjectId ServerSiteId
		{
			get
			{
				return this.serverSiteId;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x00065A21 File Offset: 0x00063C21
		internal ADObjectId DatabaseAvailabilityGroup
		{
			get
			{
				return this.databaseAvailabilityGroup;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00065A29 File Offset: 0x00063C29
		internal bool IsSearchable
		{
			get
			{
				return ServerStatus.Searchable == this.status;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00065A34 File Offset: 0x00063C34
		internal ulong Roles
		{
			get
			{
				return this.roles;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00065A3C File Offset: 0x00063C3C
		internal ServerVersion AdminDisplayVersion
		{
			get
			{
				return this.adminDisplayVersion;
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00065A44 File Offset: 0x00063C44
		internal ServerInfo(string lowerCaseFqdn, ServerStatus status, ADObjectId serverSiteId, ADObjectId databaseAvailabilityGroup, ServerVersion version, ulong roles)
		{
			this.lowerCaseFqdn = lowerCaseFqdn;
			this.status = status;
			this.serverSiteId = serverSiteId;
			this.databaseAvailabilityGroup = databaseAvailabilityGroup;
			this.adminDisplayVersion = version;
			this.roles = roles;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00065A73 File Offset: 0x00063C73
		public override string ToString()
		{
			return this.status.ToString() + ":" + (this.lowerCaseFqdn ?? "null");
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00065AA0 File Offset: 0x00063CA0
		internal static int GetHubServersInSite(IConfigurationSession session, ADObjectId site, out List<string> serverFqdns)
		{
			serverFqdns = new List<string>(0);
			IEnumerable<ServerInfo> serversInSiteByRole = ServerInfo.GetServersInSiteByRole(session, ServerInfo.HubRoleFilter, site);
			foreach (ServerInfo serverInfo in serversInSiteByRole)
			{
				serverFqdns.Add(serverInfo.lowerCaseFqdn);
			}
			return serverFqdns.Count;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00065B0C File Offset: 0x00063D0C
		internal static int GetCASServersInSite(IConfigurationSession session, ADObjectId site, out List<ServerInfo> serverInfoList)
		{
			IEnumerable<ServerInfo> serversInSiteByRole = ServerInfo.GetServersInSiteByRole(session, ServerInfo.CASRoleFilter, site);
			serverInfoList = new List<ServerInfo>(serversInSiteByRole);
			return serverInfoList.Count;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00065B38 File Offset: 0x00063D38
		internal static ServerInfo GetServerByName(string serverName, ulong roleMask, ITopologyConfigurationSession session)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "null/empty servername search, returning not-found", new object[0]);
				return ServerInfo.NotFound;
			}
			Server server;
			if (serverName.Contains("."))
			{
				server = session.FindServerByFqdn(serverName);
			}
			else
			{
				server = session.FindServerByName(serverName);
			}
			if (server == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(0, "Server name: {0} is not found", serverName);
				return ServerInfo.NotFound;
			}
			if (((long)server.CurrentServerRole & (long)roleMask) != 0L)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, ulong>(0, "Found server: {0} with roles: {1}", server.Fqdn, (ulong)((long)server.CurrentServerRole));
				return ServerInfo.CreateServerInfoFromServer(server);
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<string, ulong, ulong>(0, "Server name: {0} roles do not match. Searched for: {1}, actual: {2}", serverName, roleMask, (ulong)((long)server.CurrentServerRole));
			if (!server.IsE14OrLater)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(0, "Server is legacy Exchange Server", new object[0]);
				return ServerInfo.LegacyExchangeServer;
			}
			return ServerInfo.NotFound;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00065E88 File Offset: 0x00064088
		private static IEnumerable<ServerInfo> GetServersInSiteByRole(IConfigurationSession session, QueryFilter roleFilter, ADObjectId site)
		{
			ComparisonFilter userSiteFilter = new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, site);
			AndFilter queryFilter = new AndFilter(new QueryFilter[]
			{
				userSiteFilter,
				roleFilter
			});
			ADPagedReader<Server> servers = session.FindPaged<Server>(null, QueryScope.SubTree, queryFilter, null, 0);
			foreach (Server server in servers)
			{
				ServerInfo serverInfo = ServerInfo.CreateServerInfoFromServer(server);
				if (!serverInfo.IsSearchable)
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<string>(0, "Server: {0}, not searchable", serverInfo.Key);
				}
				else if (string.IsNullOrEmpty(server.Fqdn))
				{
					TraceWrapper.SearchLibraryTracer.TraceError(0, "Null/empty server-name, skipping", new object[0]);
				}
				else
				{
					yield return serverInfo;
				}
			}
			yield break;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00065EB4 File Offset: 0x000640B4
		private static ServerInfo CreateServerInfoFromServer(Server server)
		{
			if (string.IsNullOrEmpty(server.Fqdn))
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "Null or Empty FQDN", new object[0]);
				return ServerInfo.NotFound;
			}
			if (!server.IsE14OrLater)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "Pre-E14 server {0}", server.Fqdn);
				return ServerInfo.LegacyExchangeServer;
			}
			ServerVersion serverVersion = server.AdminDisplayVersion;
			if (serverVersion == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "Could not get build for server {0}", server.Fqdn);
				return ServerInfo.NotExchangeServer;
			}
			string text = server.Fqdn.ToLowerInvariant();
			return new ServerInfo(text, ServerStatus.Searchable, server.ServerSite, server.DatabaseAvailabilityGroup, serverVersion, (ulong)((long)server.CurrentServerRole));
		}

		// Token: 0x04000DEE RID: 3566
		private static readonly BitMaskAndFilter HubRoleFilter = new BitMaskAndFilter(ServerSchema.CurrentServerRole, 32UL);

		// Token: 0x04000DEF RID: 3567
		private static readonly BitMaskAndFilter CASRoleFilter = new BitMaskAndFilter(ServerSchema.CurrentServerRole, 4UL);

		// Token: 0x04000DF0 RID: 3568
		internal static readonly ServerInfo NotFound = new ServerInfo(null, ServerStatus.NotFound, null, null, null, 0UL);

		// Token: 0x04000DF1 RID: 3569
		internal static readonly ServerInfo NotExchangeServer = new ServerInfo(null, ServerStatus.NotExchangeServer, null, null, null, 0UL);

		// Token: 0x04000DF2 RID: 3570
		internal static readonly ServerInfo LegacyExchangeServer = new ServerInfo(null, ServerStatus.LegacyExchangeServer, null, null, null, 0UL);

		// Token: 0x04000DF3 RID: 3571
		private string lowerCaseFqdn;

		// Token: 0x04000DF4 RID: 3572
		private ServerStatus status;

		// Token: 0x04000DF5 RID: 3573
		private ADObjectId serverSiteId;

		// Token: 0x04000DF6 RID: 3574
		private ADObjectId databaseAvailabilityGroup;

		// Token: 0x04000DF7 RID: 3575
		private ulong roles;

		// Token: 0x04000DF8 RID: 3576
		private ServerVersion adminDisplayVersion;
	}
}
