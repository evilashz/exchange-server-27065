using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Data.Directory.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000263 RID: 611
	internal class RoutingTopology : RoutingTopologyBase
	{
		// Token: 0x06001A72 RID: 6770 RVA: 0x0006CE0B File Offset: 0x0006B00B
		public RoutingTopology(DatabaseLoader databaseLoader, RoutingContextCore context)
		{
			this.databaseLoader = databaseLoader;
			this.routingTopologyCacheEnabled = context.Settings.RoutingTopologyCacheEnabled;
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x0006CE2B File Offset: 0x0006B02B
		public override TopologyServer LocalServer
		{
			get
			{
				return this.siteTopology.LocalServer;
			}
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0006CE38 File Offset: 0x0006B038
		public override IEnumerable<MiniDatabase> GetDatabases(bool forcedReload)
		{
			if (this.databases == null)
			{
				this.databases = this.databaseLoader.GetDatabases(base.WhenCreated, forcedReload);
			}
			return this.databases;
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x0006CE60 File Offset: 0x0006B060
		public override IEnumerable<TopologyServer> Servers
		{
			get
			{
				return this.siteTopology.AllTopologyServers;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0006CE6D File Offset: 0x0006B06D
		public override IList<TopologySite> Sites
		{
			get
			{
				return this.siteTopology.AllTopologySites;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x0006CE7A File Offset: 0x0006B07A
		public override IList<MailGateway> SendConnectors
		{
			get
			{
				return this.sendConnectors;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x0006CE82 File Offset: 0x0006B082
		public override IList<PublicFolderTree> PublicFolderTrees
		{
			get
			{
				return this.publicFolderTrees;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0006CE8A File Offset: 0x0006B08A
		public override IList<RoutingGroup> RoutingGroups
		{
			get
			{
				return this.routingGroups;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0006CE92 File Offset: 0x0006B092
		public override IList<RoutingGroupConnector> RoutingGroupConnectors
		{
			get
			{
				return this.routingGroupConnectors;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0006CE9A File Offset: 0x0006B09A
		public override IList<Server> HubServersOnEdge
		{
			get
			{
				throw new NotSupportedException("HubServersOnEdge property is not supported");
			}
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0006CEA6 File Offset: 0x0006B0A6
		public override void LogData(RoutingTableLogger logger)
		{
			logger.WriteStartElement("RoutingTopology");
			this.LogServers(logger);
			this.LogSiteTopology(logger);
			this.LogRoutingGroupTopology(logger);
			base.LogSendConnectors(logger);
			this.LogDatabases(logger);
			this.LogPublicFolderTrees(logger);
			logger.WriteEndElement();
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0006CEE4 File Offset: 0x0006B0E4
		protected override void PreLoadInternal()
		{
			RoutingDiag.Tracer.TraceDebug<DateTime>((long)this.GetHashCode(), "[{0}] Discovering topology", base.WhenCreated);
			if (this.routingTopologyCacheEnabled)
			{
				RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "Getting routing topology from Service Cache.");
				using (TopologyServiceClient topologyServiceClient = TopologyServiceClient.CreateClient("localhost"))
				{
					byte[][] exchangeTopology = topologyServiceClient.GetExchangeTopology(DateTime.MinValue, ExchangeTopologyScope.ServerAndSiteTopology, false);
					ExchangeTopologyDiscovery.Simple topology = ExchangeTopologyDiscovery.Simple.Deserialize(exchangeTopology);
					ExchangeTopologyDiscovery topologyDiscovery = ExchangeTopologyDiscovery.Simple.CreateFrom(topology);
					this.siteTopology = ExchangeTopologyDiscovery.Populate(topologyDiscovery);
					goto IL_A1;
				}
			}
			RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "Getting routing topology from AD.");
			this.siteTopology = ExchangeTopology.Discover(base.Session, ExchangeTopologyScope.ServerAndSiteTopology);
			IL_A1:
			this.sendConnectors = base.LoadAll<MailGateway>();
			this.publicFolderTrees = base.LoadAll<PublicFolderTree>();
			this.routingGroups = base.LoadAll<RoutingGroup>();
			this.routingGroupConnectors = base.LoadAll<RoutingGroupConnector>();
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0006CFD4 File Offset: 0x0006B1D4
		protected override void RegisterForADNotifications(ADNotificationCallback callback, IList<ADNotificationRequestCookie> cookies)
		{
			cookies.Add(TransportADNotificationAdapter.Instance.RegisterForMailGatewayNotifications(base.RootId, callback));
			cookies.Add(TransportADNotificationAdapter.Instance.RegisterForDatabaseNotifications(base.RootId, callback));
			cookies.Add(TransportADNotificationAdapter.RegisterForNonDeletedNotifications<PublicFolderTree>(base.RootId, callback));
			cookies.Add(TransportADNotificationAdapter.RegisterForNonDeletedNotifications<RoutingGroup>(base.RootId, callback));
			cookies.Add(TransportADNotificationAdapter.RegisterForNonDeletedNotifications<RoutingGroupConnector>(base.RootId, callback));
			cookies.Add(TransportADNotificationAdapter.RegisterForNonDeletedNotifications<AdministrativeGroup>(base.RootId, callback));
			cookies.Add(TransportADNotificationAdapter.RegisterForNonDeletedNotifications<StorageGroup>(base.RootId, callback));
			ADObjectId childId = ADSession.GetConfigurationNamingContextForLocalForest().GetChildId("CN", "Sites");
			ADObjectId childId2 = childId.GetChildId("CN", "Inter-Site Transports").GetChildId("CN", "IP");
			cookies.Add(TransportADNotificationAdapter.Instance.RegisterForADSiteNotifications(childId, callback));
			cookies.Add(TransportADNotificationAdapter.Instance.RegisterForADSiteLinkNotifications(childId2, callback));
			cookies.Add(TransportADNotificationAdapter.Instance.RegisterForExchangeServerNotifications(null, callback));
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0006D0D4 File Offset: 0x0006B2D4
		protected override void Validate()
		{
			if (this.siteTopology.LocalServer == null)
			{
				RoutingDiag.Tracer.TraceError<DateTime>((long)this.GetHashCode(), "[{0}] Topology Discovery returned null LocalServer", base.WhenCreated);
				throw new TransientRoutingException(Strings.RoutingNoLocalServer);
			}
			if (this.siteTopology.AllTopologySites.Count == 0)
			{
				RoutingDiag.Tracer.TraceError<DateTime>(0L, "[{0}] No AD sites found", base.WhenCreated);
				throw new TransientRoutingException(Strings.RoutingNoAdSites);
			}
			if (this.siteTopology.LocalServer.TopologySite == null)
			{
				RoutingDiag.Tracer.TraceError<DateTime, string>(0L, "[{0}] Unable to determine local AD site from local server {1}", base.WhenCreated, this.siteTopology.LocalServer.Fqdn);
				throw new TransientRoutingException(Strings.RoutingNoLocalAdSite);
			}
			RoutingGroupRelayMap.ValidateTopologyConfig(this);
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0006D194 File Offset: 0x0006B394
		private void LogServers(RoutingTableLogger logger)
		{
			logger.WriteStartElement("ExchangeServers");
			foreach (TopologyServer server in this.siteTopology.AllTopologyServers)
			{
				logger.WriteTopologyServer(server);
			}
			logger.WriteEndElement();
			logger.WriteADReference("LocalServerId", this.LocalServer.Id);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0006D214 File Offset: 0x0006B414
		private void LogSiteTopology(RoutingTableLogger logger)
		{
			logger.WriteStartElement("TopologySites");
			foreach (TopologySite site in this.Sites)
			{
				logger.WriteADSite(site);
			}
			logger.WriteEndElement();
			logger.WriteStartElement("TopologySiteLinks");
			foreach (TopologySiteLink link in this.siteTopology.AllTopologySiteLinks)
			{
				logger.WriteADSiteLink(link);
			}
			logger.WriteEndElement();
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0006D2CC File Offset: 0x0006B4CC
		private void LogRoutingGroupTopology(RoutingTableLogger logger)
		{
			logger.WriteStartElement("RoutingGroups");
			foreach (RoutingGroup routingGroup in this.RoutingGroups)
			{
				logger.WriteRoutingGroup(routingGroup);
			}
			logger.WriteEndElement();
			logger.WriteStartElement("RoutingGroupConnectors");
			foreach (RoutingGroupConnector rgc in this.RoutingGroupConnectors)
			{
				logger.WriteRoutingGroupConnector(rgc);
			}
			logger.WriteEndElement();
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0006D378 File Offset: 0x0006B578
		private void LogDatabases(RoutingTableLogger logger)
		{
			if (this.databases == null)
			{
				throw new InvalidOperationException("Databases have not been read from AD yet");
			}
			logger.WriteStartElement("Databases");
			foreach (MiniDatabase database in this.databases)
			{
				logger.WriteDatabase(database);
			}
			logger.WriteEndElement();
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0006D3EC File Offset: 0x0006B5EC
		private void LogPublicFolderTrees(RoutingTableLogger logger)
		{
			logger.WriteStartElement("PublicFolderTrees");
			foreach (PublicFolderTree publicFolderTree in this.publicFolderTrees)
			{
				logger.WritePublicFolderTree(publicFolderTree);
			}
			logger.WriteEndElement();
		}

		// Token: 0x04000C9A RID: 3226
		private ExchangeTopology siteTopology;

		// Token: 0x04000C9B RID: 3227
		private IList<MailGateway> sendConnectors;

		// Token: 0x04000C9C RID: 3228
		private IList<PublicFolderTree> publicFolderTrees;

		// Token: 0x04000C9D RID: 3229
		private IList<RoutingGroup> routingGroups;

		// Token: 0x04000C9E RID: 3230
		private IList<RoutingGroupConnector> routingGroupConnectors;

		// Token: 0x04000C9F RID: 3231
		private IEnumerable<MiniDatabase> databases;

		// Token: 0x04000CA0 RID: 3232
		private readonly DatabaseLoader databaseLoader;

		// Token: 0x04000CA1 RID: 3233
		private readonly bool routingTopologyCacheEnabled;
	}
}
