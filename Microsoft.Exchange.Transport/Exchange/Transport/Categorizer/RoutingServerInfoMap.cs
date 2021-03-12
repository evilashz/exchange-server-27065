using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000267 RID: 615
	internal class RoutingServerInfoMap
	{
		// Token: 0x06001AC9 RID: 6857 RVA: 0x0006DAA4 File Offset: 0x0006BCA4
		public RoutingServerInfoMap(RoutingTopologyBase topologyConfig, RoutingContextCore contextCore)
		{
			this.localServer = topologyConfig.LocalServer;
			this.whenCreated = topologyConfig.WhenCreated;
			this.serversByGuid = new Dictionary<Guid, RoutingServerInfo>(64);
			this.serversByDN = new Dictionary<string, RoutingServerInfo>(64, StringComparer.OrdinalIgnoreCase);
			this.serversByFqdn = new Dictionary<string, RoutingServerInfo>(64, StringComparer.OrdinalIgnoreCase);
			this.serversByLegacyDN = new Dictionary<string, RoutingServerInfo>(64, StringComparer.OrdinalIgnoreCase);
			this.externalPostmasterAddresses = new List<RoutingAddress>();
			foreach (TopologyServer topologyServer in topologyConfig.Servers)
			{
				if (!this.ShouldExcludeTopologyServer(topologyServer))
				{
					RoutingServerInfo routingServerInfo = new RoutingServerInfo(new RoutingMiniServer(topologyServer));
					if (TransportHelpers.AttemptAddToDictionary<Guid, RoutingServerInfo>(this.serversByGuid, topologyServer.Guid, routingServerInfo, new TransportHelpers.DiagnosticsHandler<Guid, RoutingServerInfo>(RoutingUtils.LogErrorWhenAddToDictionaryFails<Guid, RoutingServerInfo>)) && TransportHelpers.AttemptAddToDictionary<string, RoutingServerInfo>(this.serversByDN, topologyServer.DistinguishedName, routingServerInfo, new TransportHelpers.DiagnosticsHandler<string, RoutingServerInfo>(RoutingUtils.LogErrorWhenAddToDictionaryFails<string, RoutingServerInfo>)) && TransportHelpers.AttemptAddToDictionary<string, RoutingServerInfo>(this.serversByFqdn, topologyServer.Fqdn, routingServerInfo, new TransportHelpers.DiagnosticsHandler<string, RoutingServerInfo>(RoutingUtils.LogErrorWhenAddToDictionaryFails<string, RoutingServerInfo>)) && TransportHelpers.AttemptAddToDictionary<string, RoutingServerInfo>(this.serversByLegacyDN, topologyServer.ExchangeLegacyDN, routingServerInfo, new TransportHelpers.DiagnosticsHandler<string, RoutingServerInfo>(RoutingUtils.LogErrorWhenAddToDictionaryFails<string, RoutingServerInfo>)))
					{
						if (contextCore.ConnectorRoutingSupported && routingServerInfo.IsFrontendTransportServer && this.IsInLocalSite(routingServerInfo) && (contextCore.Settings.OutboundProxyRoutingXVersionEnabled || routingServerInfo.IsSameVersionAs(this.localServer)) && contextCore.VerifyFrontendComponentStateRestriction(routingServerInfo))
						{
							RoutingUtils.AddItemToLazyList<RoutingServerInfo>(routingServerInfo, contextCore.Settings.RandomLoadBalancingOffsetEnabled, ref this.frontendServersInLocalSite);
						}
					}
					else
					{
						this.serversByGuid.Remove(topologyServer.Guid);
						this.serversByDN.Remove(topologyServer.DistinguishedName);
						this.serversByFqdn.Remove(topologyServer.Fqdn);
					}
				}
				this.AddExternalPostmasterAddress(topologyServer);
			}
			this.externalPostmasterAddresses = new ReadOnlyCollection<RoutingAddress>(this.externalPostmasterAddresses);
			this.siteRelayMap = new ADSiteRelayMap(topologyConfig, this, contextCore);
			this.routingGroupRelayMap = new RoutingGroupRelayMap(topologyConfig, this, contextCore);
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0006DCCC File Offset: 0x0006BECC
		public bool LocalDagExists
		{
			get
			{
				return this.localServer.DatabaseAvailabilityGroup != null;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x0006DCDF File Offset: 0x0006BEDF
		public int LocalServerVersion
		{
			get
			{
				return this.localServer.MajorVersion;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x0006DCEC File Offset: 0x0006BEEC
		public bool LocalHubSiteEnabled
		{
			get
			{
				return this.localServer.TopologySite.HubSiteEnabled;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x0006DCFE File Offset: 0x0006BEFE
		public ADSiteRelayMap SiteRelayMap
		{
			get
			{
				return this.siteRelayMap;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x0006DD06 File Offset: 0x0006BF06
		public RoutingGroupRelayMap RoutingGroupRelayMap
		{
			get
			{
				return this.routingGroupRelayMap;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0006DD0E File Offset: 0x0006BF0E
		public DateTime WhenCreated
		{
			get
			{
				return this.whenCreated;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x0006DD16 File Offset: 0x0006BF16
		public IList<RoutingAddress> ExternalPostmasterAddresses
		{
			get
			{
				return this.externalPostmasterAddresses;
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0006DD1E File Offset: 0x0006BF1E
		public bool IsLocalServer(RoutingServerInfo serverInfo)
		{
			return serverInfo.IsSameServerAs(this.localServer);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0006DD2C File Offset: 0x0006BF2C
		public bool IsInLocalSite(RoutingServerInfo serverInfo)
		{
			return serverInfo.IsInSameSite(this.localServer);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0006DD3C File Offset: 0x0006BF3C
		public bool IsInLocalDag(RoutingServerInfo serverInfo)
		{
			ADObjectId databaseAvailabilityGroup = this.localServer.DatabaseAvailabilityGroup;
			ADObjectId databaseAvailabilityGroup2 = serverInfo.DatabaseAvailabilityGroup;
			return databaseAvailabilityGroup != null && databaseAvailabilityGroup2 != null && databaseAvailabilityGroup.ObjectGuid.Equals(databaseAvailabilityGroup2.ObjectGuid);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0006DD78 File Offset: 0x0006BF78
		public bool TryGetServerInfo(ADObjectId serverId, out RoutingServerInfo serverInfo)
		{
			RoutingUtils.ThrowIfNullOrEmpty(serverId, "serverId");
			return this.TryGetServerInfo(serverId.ObjectGuid, out serverInfo);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0006DD92 File Offset: 0x0006BF92
		public bool TryGetServerInfo(Guid serverGuid, out RoutingServerInfo serverInfo)
		{
			return this.serversByGuid.TryGetValue(serverGuid, out serverInfo);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0006DDA1 File Offset: 0x0006BFA1
		public bool TryGetServerInfoByDN(string serverDN, out RoutingServerInfo serverInfo)
		{
			RoutingUtils.ThrowIfNullOrEmpty(serverDN, "serverDN");
			return this.serversByDN.TryGetValue(serverDN, out serverInfo);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0006DDBB File Offset: 0x0006BFBB
		public bool TryGetServerInfoByFqdn(string serverFqdn, out RoutingServerInfo serverInfo)
		{
			RoutingUtils.ThrowIfNullOrEmpty(serverFqdn, "serverFqdn");
			return this.serversByFqdn.TryGetValue(serverFqdn, out serverInfo);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0006DDD5 File Offset: 0x0006BFD5
		public bool TryGetServerInfoByLegacyDN(string serverLegacyDN, out RoutingServerInfo serverInfo)
		{
			RoutingUtils.ThrowIfNullOrEmpty(serverLegacyDN, "serverLegacyDN");
			return this.serversByLegacyDN.TryGetValue(serverLegacyDN, out serverInfo);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0006DE01 File Offset: 0x0006C001
		public IEnumerable<RoutingServerInfo> GetHubTransportServers()
		{
			return this.GetServers((RoutingServerInfo server) => server.IsExchange2007OrLater && server.IsHubTransportServer);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0006DE26 File Offset: 0x0006C026
		public bool TryGetServerRoute(RoutingServerInfo serverInfo, out RouteInfo routeInfo)
		{
			return this.TryGetServerRoute(serverInfo, this.GetProximity(serverInfo), out routeInfo) != Proximity.None;
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0006DE41 File Offset: 0x0006C041
		public bool TryGetServerRoute(ADObjectId serverId, out RoutingServerInfo serverInfo, out RouteInfo routeInfo)
		{
			return this.TryGetServerRouteByDN(serverId.DistinguishedName, Proximity.None, out serverInfo, out routeInfo) != Proximity.None;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0006DE60 File Offset: 0x0006C060
		public bool TryGetServerRouteByDN(string serverDN, out RoutingServerInfo serverInfo, out RouteInfo routeInfo)
		{
			return this.TryGetServerRouteByDN(serverDN, Proximity.None, out serverInfo, out routeInfo) != Proximity.None;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0006DE7C File Offset: 0x0006C07C
		public bool TryCreateRoutedServerCollectionForClosestProximity(ICollection<ADObjectId> serverIds, RoutingContextCore contextCore, out RoutedServerCollection collection, out List<ADObjectId> unknownServerIds, out List<RoutingServerInfo> unroutedServers, out List<RoutingServerInfo> nonActiveServers)
		{
			collection = null;
			unknownServerIds = null;
			unroutedServers = null;
			nonActiveServers = null;
			foreach (ADObjectId adobjectId in serverIds)
			{
				Proximity proximity = (collection == null) ? Proximity.None : collection.ClosestProximity;
				RoutingServerInfo routingServerInfo;
				RouteInfo routeInfo;
				Proximity proximity2 = this.TryGetServerRouteByDN(adobjectId.DistinguishedName, proximity, out routingServerInfo, out routeInfo);
				if (routingServerInfo == null)
				{
					RoutingUtils.AddItemToLazyList<ADObjectId>(adobjectId, ref unknownServerIds);
				}
				else if (routingServerInfo.IsHubTransportServer && !contextCore.VerifyHubComponentStateRestriction(routingServerInfo))
				{
					RoutingUtils.AddItemToLazyList<RoutingServerInfo>(routingServerInfo, ref nonActiveServers);
				}
				else if (proximity2 == Proximity.None)
				{
					RoutingUtils.AddItemToLazyList<RoutingServerInfo>(routingServerInfo, ref unroutedServers);
				}
				else if (collection == null)
				{
					collection = new RoutedServerCollection(routeInfo, routingServerInfo, contextCore);
				}
				else if (proximity2 <= proximity)
				{
					collection.AddServerForRoute(routeInfo, routingServerInfo, proximity2 < proximity, contextCore);
					if (proximity2 == Proximity.LocalServer)
					{
						return true;
					}
				}
			}
			return collection != null;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0006DF70 File Offset: 0x0006C170
		public bool TryCreateRoutedServerCollection(ICollection<string> serverFqdns, RoutingContextCore contextCore, out RoutedServerCollection collection, out List<string> unknownServers, out List<RoutingServerInfo> unroutedServers)
		{
			collection = null;
			unknownServers = null;
			unroutedServers = null;
			foreach (string text in serverFqdns)
			{
				RoutingServerInfo routingServerInfo;
				RouteInfo routeInfo;
				Proximity proximity = this.TryGetServerRouteByFqdn(text, out routingServerInfo, out routeInfo);
				if (proximity == Proximity.None)
				{
					if (routingServerInfo == null)
					{
						RoutingUtils.AddItemToLazyList<string>(text, ref unknownServers);
					}
					else
					{
						RoutingUtils.AddItemToLazyList<RoutingServerInfo>(routingServerInfo, ref unroutedServers);
					}
				}
				else if (collection == null)
				{
					collection = new RoutedServerCollection(routeInfo, routingServerInfo, contextCore);
				}
				else
				{
					collection.AddServerForRoute(routeInfo, routingServerInfo, contextCore);
				}
			}
			return collection != null;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0006E010 File Offset: 0x0006C210
		public bool TryGetLoadBalancedFrontendServersInLocalSite(out IEnumerable<RoutingServerInfo> servers)
		{
			if (this.frontendServersInLocalSite == null)
			{
				servers = null;
				return false;
			}
			servers = this.frontendServersInLocalSite.LoadBalancedCollection;
			return true;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0006E030 File Offset: 0x0006C230
		public bool QuickMatch(RoutingServerInfoMap other)
		{
			return this.serversByGuid.Count == other.serversByGuid.Count && this.siteRelayMap.QuickMatch(other.siteRelayMap) && this.routingGroupRelayMap.QuickMatch(other.routingGroupRelayMap) && this.QuickFrontendServersInLocalSiteMatch(other.frontendServersInLocalSite);
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0006E089 File Offset: 0x0006C289
		public bool FullMatch(RoutingServerInfoMap other)
		{
			return this.FullServersMatch(other) && this.siteRelayMap.FullMatch(other.siteRelayMap) && this.routingGroupRelayMap.FullMatch(other.routingGroupRelayMap);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0006E0BC File Offset: 0x0006C2BC
		private Proximity TryGetServerRouteByDN(string serverDN, Proximity maxProximity, out RoutingServerInfo serverInfo, out RouteInfo routeInfo)
		{
			routeInfo = null;
			RoutingUtils.ThrowIfNullOrEmpty(serverDN, "serverDN");
			if (!this.TryGetServerInfoByDN(serverDN, out serverInfo))
			{
				return Proximity.None;
			}
			Proximity proximity = this.GetProximity(serverInfo);
			if (proximity > maxProximity)
			{
				return proximity;
			}
			return this.TryGetServerRoute(serverInfo, proximity, out routeInfo);
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0006E102 File Offset: 0x0006C302
		private Proximity TryGetServerRouteByFqdn(string serverFqdn, out RoutingServerInfo serverInfo, out RouteInfo routeInfo)
		{
			routeInfo = null;
			RoutingUtils.ThrowIfNullOrEmpty(serverFqdn, "serverFqdn");
			if (!this.TryGetServerInfoByFqdn(serverFqdn, out serverInfo))
			{
				return Proximity.None;
			}
			return this.TryGetServerRoute(serverInfo, this.GetProximity(serverInfo), out routeInfo);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0006E134 File Offset: 0x0006C334
		private Proximity TryGetServerRoute(RoutingServerInfo serverInfo, Proximity serverProximity, out RouteInfo routeInfo)
		{
			routeInfo = null;
			switch (serverProximity)
			{
			case Proximity.LocalServer:
				routeInfo = RouteInfo.LocalServerRoute;
				break;
			case Proximity.LocalADSite:
				routeInfo = RouteInfo.LocalSiteRoute;
				break;
			case Proximity.RemoteADSite:
				this.siteRelayMap.TryGetRouteInfo(serverInfo.ADSite, out routeInfo);
				break;
			case Proximity.RemoteRoutingGroup:
				if (this.routingGroupRelayMap != null)
				{
					this.routingGroupRelayMap.TryGetRouteInfo(serverInfo.HomeRoutingGroup, out routeInfo);
				}
				break;
			default:
				throw new ArgumentOutOfRangeException("serverProximity", serverProximity, "Unexpected server proximity value: " + serverProximity);
			}
			if (routeInfo != null)
			{
				return routeInfo.DestinationProximity;
			}
			return Proximity.None;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0006E1D4 File Offset: 0x0006C3D4
		private Proximity GetProximity(RoutingServerInfo serverInfo)
		{
			if (this.IsLocalServer(serverInfo))
			{
				return Proximity.LocalServer;
			}
			if (!serverInfo.IsExchange2007OrLater)
			{
				return Proximity.RemoteRoutingGroup;
			}
			if (!this.IsInLocalSite(serverInfo))
			{
				return Proximity.RemoteADSite;
			}
			return Proximity.LocalADSite;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0006E3A4 File Offset: 0x0006C5A4
		private IEnumerable<RoutingServerInfo> GetServers(Predicate<RoutingServerInfo> filter)
		{
			foreach (RoutingServerInfo server in this.serversByGuid.Values)
			{
				if (filter(server))
				{
					yield return server;
				}
			}
			yield break;
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0006E3C8 File Offset: 0x0006C5C8
		private bool ShouldExcludeTopologyServer(TopologyServer server)
		{
			if (server.IsExchange2007OrLater && !server.IsFrontendTransportServer && !server.IsHubTransportServer && !server.IsMailboxServer && !server.IsEdgeServer)
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] Skipping server {1} because it does not contain roles relevant to routing.", this.whenCreated, server.DistinguishedName);
				return true;
			}
			if (string.IsNullOrEmpty(server.Fqdn))
			{
				RoutingDiag.Tracer.TraceError<DateTime, string>((long)this.GetHashCode(), "[{0}] No FQDN for Server object with DN: {1}. Skipping it.", this.whenCreated, server.DistinguishedName);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoServerFqdn, null, new object[]
				{
					server.DistinguishedName,
					this.whenCreated
				});
				return true;
			}
			if (server.IsExchange2007OrLater)
			{
				if (server.TopologySite == null)
				{
					RoutingDiag.Tracer.TraceError<DateTime, string>((long)this.GetHashCode(), "[{0}] AD site for server '{1}' was not determined. Skipping the server.", this.whenCreated, server.Fqdn);
					RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoServerAdSite, null, new object[]
					{
						server.Fqdn,
						this.whenCreated
					});
					return true;
				}
			}
			else if (server.HomeRoutingGroup == null)
			{
				RoutingDiag.Tracer.TraceError<DateTime, string>((long)this.GetHashCode(), "[{0}] Routing group for server '{1}' was not determined. Skipping the server.", this.whenCreated, server.Fqdn);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoServerRg, null, new object[]
				{
					server.Fqdn,
					this.whenCreated
				});
				return true;
			}
			return false;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0006E548 File Offset: 0x0006C748
		private void AddExternalPostmasterAddress(TopologyServer server)
		{
			RoutingAddress item;
			if (RoutingUtils.TryConvertToRoutingAddress(server.ExternalPostmasterAddress, out item) && !this.externalPostmasterAddresses.Contains(item))
			{
				this.externalPostmasterAddresses.Add(item);
			}
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0006E57E File Offset: 0x0006C77E
		private bool QuickFrontendServersInLocalSiteMatch(ListLoadBalancer<RoutingServerInfo> other)
		{
			return RoutingUtils.NullMatch(this.frontendServersInLocalSite, other) && (this.frontendServersInLocalSite == null || this.frontendServersInLocalSite.Count == other.Count);
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0006E5B6 File Offset: 0x0006C7B6
		private bool FullServersMatch(RoutingServerInfoMap other)
		{
			return RoutingUtils.MatchDictionaries<Guid, RoutingServerInfo>(this.serversByGuid, other.serversByGuid, (RoutingServerInfo serverInfo1, RoutingServerInfo serverInfo2) => serverInfo1.Match(serverInfo2));
		}

		// Token: 0x04000CBA RID: 3258
		private const int InitialCapacity = 64;

		// Token: 0x04000CBB RID: 3259
		private readonly DateTime whenCreated;

		// Token: 0x04000CBC RID: 3260
		private readonly Dictionary<Guid, RoutingServerInfo> serversByGuid;

		// Token: 0x04000CBD RID: 3261
		private readonly Dictionary<string, RoutingServerInfo> serversByDN;

		// Token: 0x04000CBE RID: 3262
		private readonly Dictionary<string, RoutingServerInfo> serversByFqdn;

		// Token: 0x04000CBF RID: 3263
		private readonly Dictionary<string, RoutingServerInfo> serversByLegacyDN;

		// Token: 0x04000CC0 RID: 3264
		private readonly TopologyServer localServer;

		// Token: 0x04000CC1 RID: 3265
		private readonly ADSiteRelayMap siteRelayMap;

		// Token: 0x04000CC2 RID: 3266
		private readonly RoutingGroupRelayMap routingGroupRelayMap;

		// Token: 0x04000CC3 RID: 3267
		private readonly ListLoadBalancer<RoutingServerInfo> frontendServersInLocalSite;

		// Token: 0x04000CC4 RID: 3268
		private readonly IList<RoutingAddress> externalPostmasterAddresses;
	}
}
