using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Common.Internal;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x0200004B RID: 75
	internal sealed class ReplicationTopology
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00009B51 File Offset: 0x00007D51
		public Server LocalHub
		{
			get
			{
				return this.localHub;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00009B59 File Offset: 0x00007D59
		public IConfigurationSession ConfigSession
		{
			get
			{
				return this.configSession;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00009B61 File Offset: 0x00007D61
		public IRecipientSession RecipientSession
		{
			get
			{
				return this.recipientSession;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00009B69 File Offset: 0x00007D69
		public List<Server> SiteEdgeServers
		{
			get
			{
				return this.siteEdgeServers;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00009B71 File Offset: 0x00007D71
		public List<Server> SiteHubServers
		{
			get
			{
				return this.siteHubServers;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00009B79 File Offset: 0x00007D79
		public EdgeSyncServiceConfig EdgeSyncServiceConfig
		{
			get
			{
				return this.edgeSyncServiceConfig;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00009B81 File Offset: 0x00007D81
		public ADSite LocalSite
		{
			get
			{
				return this.localSite;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009B8C File Offset: 0x00007D8C
		private ReplicationTopology(IConfigurationSession configSession, Server localHub, ADSite localSite, EdgeSyncServiceConfig edgeSyncServiceConfig)
		{
			this.configSession = configSession;
			this.localHub = localHub;
			this.localSite = localSite;
			this.edgeSyncServiceConfig = edgeSyncServiceConfig;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009D3C File Offset: 0x00007F3C
		public static bool TryLoadLocalSiteTopology(string domainController, out ReplicationTopology topology, out Exception exception)
		{
			topology = null;
			exception = null;
			ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 154, "TryLoadLocalSiteTopology", "f:\\15.00.1497\\sources\\dev\\EdgeSync\\src\\Common\\Validation\\ReplicationTopology.cs");
			ADSite localSite = null;
			EdgeSyncServiceConfig edgeSyncServiceConfig = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				localSite = session.GetLocalSite();
				if (localSite == null)
				{
					throw new ADTransientException(Strings.CannotGetLocalSite);
				}
				edgeSyncServiceConfig = session.Read<EdgeSyncServiceConfig>(localSite.Id.GetChildId("EdgeSyncService"));
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				exception = adoperationResult.Exception;
				return false;
			}
			if (edgeSyncServiceConfig == null)
			{
				topology = new ReplicationTopology(session, null, localSite, null);
				return true;
			}
			ReplicationTopology resultTopology = null;
			adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				Server server = session.FindLocalServer();
				resultTopology = new ReplicationTopology(session, server, localSite, edgeSyncServiceConfig);
				QueryFilter filter = Util.BuildServerFilterForSite(localSite.Id);
				ADPagedReader<Server> adpagedReader = session.FindPaged<Server>(null, QueryScope.SubTree, filter, null, 0);
				resultTopology.siteEdgeServers.Clear();
				resultTopology.siteHubServers.Clear();
				foreach (Server server2 in adpagedReader)
				{
					if (server2.IsEdgeServer)
					{
						resultTopology.siteEdgeServers.Add(server2);
					}
					if (server2.IsHubTransportServer)
					{
						resultTopology.siteHubServers.Add(server2);
					}
				}
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				exception = adoperationResult.Exception;
				return false;
			}
			topology = resultTopology;
			return true;
		}

		// Token: 0x0400014B RID: 331
		private readonly Server localHub;

		// Token: 0x0400014C RID: 332
		private readonly ADSite localSite;

		// Token: 0x0400014D RID: 333
		private readonly EdgeSyncServiceConfig edgeSyncServiceConfig;

		// Token: 0x0400014E RID: 334
		private readonly List<Server> siteEdgeServers = new List<Server>();

		// Token: 0x0400014F RID: 335
		private readonly List<Server> siteHubServers = new List<Server>();

		// Token: 0x04000150 RID: 336
		private readonly IConfigurationSession configSession;

		// Token: 0x04000151 RID: 337
		private readonly IRecipientSession recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 66, "recipientSession", "f:\\15.00.1497\\sources\\dev\\EdgeSync\\src\\Common\\Validation\\ReplicationTopology.cs");
	}
}
