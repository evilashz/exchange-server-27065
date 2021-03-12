using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x0200030D RID: 781
	internal sealed class TransportServerConfigCache : LazyLookupTimeoutCacheWithDiagnostics<string, ServerInfo>
	{
		// Token: 0x06001716 RID: 5910 RVA: 0x0006B35B File Offset: 0x0006955B
		public TransportServerConfigCache() : base(2, 100, false, TimeSpan.FromHours(5.0))
		{
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0006B375 File Offset: 0x00069575
		protected override string PreprocessKey(string key)
		{
			return key.ToUpperInvariant();
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0006B380 File Offset: 0x00069580
		protected override ServerInfo Create(string key, ref bool shouldAdd)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "TransportServerConfigCache miss, searching for {0}", key);
			ITopologyConfigurationSession globalConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), OrganizationId.ForestWideOrgId, null, false), 74, "Create", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MessageTracking\\Caching\\TransportServerConfigCache.cs");
			shouldAdd = true;
			return TransportServerConfigCache.FindServer(globalConfigSession, key, 34UL);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0006B3DC File Offset: 0x000695DC
		public ServerInfo FindServer(string serverNameOrFqdn, ulong roleMask)
		{
			ServerInfo result = base.Get(serverNameOrFqdn);
			if ((roleMask & result.Roles) != 0UL)
			{
				return result;
			}
			if (result.Status == ServerStatus.LegacyExchangeServer)
			{
				return ServerInfo.LegacyExchangeServer;
			}
			return ServerInfo.NotFound;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0006B415 File Offset: 0x00069615
		public static ServerInfo FindServer(ITopologyConfigurationSession globalConfigSession, string serverNameOrFqdn, ulong roleMask)
		{
			return ServerInfo.GetServerByName(serverNameOrFqdn, roleMask, globalConfigSession);
		}

		// Token: 0x04000EBC RID: 3772
		private const ulong MailboxAndHubRoleMask = 34UL;
	}
}
