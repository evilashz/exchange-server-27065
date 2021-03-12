using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200025A RID: 602
	internal class RoutingGroupRelayMap : TopologyRelayMap<RoutingGroupRelayMap.RGTopologyPath>
	{
		// Token: 0x06001A0C RID: 6668 RVA: 0x0006A304 File Offset: 0x00068504
		public RoutingGroupRelayMap(RoutingTopologyBase topologyConfig, RoutingServerInfoMap serverMap, RoutingContextCore contextCore) : base(topologyConfig.RoutingGroups.Count, topologyConfig.WhenCreated)
		{
			RoutingGroupRelayMap.RGTopologySite localSite = this.CalculateRGTopology(topologyConfig, serverMap, contextCore);
			base.CalculateRoutes(localSite, contextCore);
			this.MapRoutesByRoutingGroupDN();
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x0006A340 File Offset: 0x00068540
		public IEnumerable<KeyValuePair<Guid, RouteInfo>> RoutesToRGConnectors
		{
			get
			{
				return this.routesToRGConnectors;
			}
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0006A348 File Offset: 0x00068548
		public static void ValidateTopologyConfig(RoutingTopologyBase topologyConfig)
		{
			if (topologyConfig.RoutingGroups.Count == 0)
			{
				RoutingDiag.Tracer.TraceError<DateTime>(0L, "[{0}] No Routing Groups found", topologyConfig.WhenCreated);
				throw new TransientRoutingException(Strings.RoutingNoRoutingGroups);
			}
			if (topologyConfig.LocalServer.HomeRoutingGroup == null)
			{
				RoutingDiag.Tracer.TraceError<DateTime>(0L, "[{0}] Local server is not a member of any RG", topologyConfig.WhenCreated);
				throw new TransientRoutingException(Strings.RoutingLocalRgNotSet);
			}
			bool flag = false;
			Guid objectGuid = topologyConfig.LocalServer.HomeRoutingGroup.ObjectGuid;
			foreach (RoutingGroup routingGroup in topologyConfig.RoutingGroups)
			{
				if (objectGuid.Equals(routingGroup.Guid))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				RoutingDiag.Tracer.TraceError<DateTime, ADObjectId>(0L, "[{0}] Local RG object '{1}' not found.", topologyConfig.WhenCreated, topologyConfig.LocalServer.HomeRoutingGroup);
				throw new TransientRoutingException(Strings.RoutingNoLocalRgObject);
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0006A444 File Offset: 0x00068644
		public bool TryGetRouteInfo(ADObjectId routingGroupId, out RouteInfo routeInfo)
		{
			routeInfo = null;
			RoutingUtils.ThrowIfNull(routingGroupId, "routingGroupId");
			RoutingUtils.ThrowIfNullOrEmpty(routingGroupId.DistinguishedName, "routingGroupId.DistinguishedName");
			return this.routesByDN.TryGetValue(routingGroupId.DistinguishedName, out routeInfo);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0006A476 File Offset: 0x00068676
		public bool QuickMatch(RoutingGroupRelayMap other)
		{
			return this.routesByDN.Count == other.routesByDN.Count && this.routesToRGConnectors.Count == other.routesToRGConnectors.Count;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0006A4AA File Offset: 0x000686AA
		public bool FullMatch(RoutingGroupRelayMap other)
		{
			return RoutingUtils.MatchRouteDictionaries<string>(this.routesByDN, other.routesByDN, NextHopMatch.GuidOnly) && RoutingUtils.MatchRouteDictionaries<Guid>(this.routesToRGConnectors, other.routesToRGConnectors);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0006A4D3 File Offset: 0x000686D3
		protected override RoutingGroupRelayMap.RGTopologyPath CreateTopologyPath(RoutingGroupRelayMap.RGTopologyPath prePath, ITopologySite targetSite, ITopologySiteLink link, RoutingContextCore contextCore)
		{
			return new RoutingGroupRelayMap.RGTopologyPath(prePath, targetSite, link, this.routesToRGConnectors);
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0006A4E4 File Offset: 0x000686E4
		private RoutingGroupRelayMap.RGTopologySite CalculateRGTopology(RoutingTopologyBase topologyConfig, RoutingServerInfoMap serverMap, RoutingContextCore contextCore)
		{
			base.DebugTrace("Calculating routing group topology");
			Dictionary<string, RoutingGroupRelayMap.RGTopologySite> dictionary = new Dictionary<string, RoutingGroupRelayMap.RGTopologySite>(topologyConfig.RoutingGroups.Count, StringComparer.OrdinalIgnoreCase);
			Dictionary<Guid, RoutingGroupRelayMap.RGTopologySite> dictionary2 = new Dictionary<Guid, RoutingGroupRelayMap.RGTopologySite>(topologyConfig.RoutingGroups.Count);
			foreach (RoutingGroup routingGroup in topologyConfig.RoutingGroups)
			{
				RoutingGroupRelayMap.RGTopologySite valueToAdd = new RoutingGroupRelayMap.RGTopologySite(routingGroup);
				if (TransportHelpers.AttemptAddToDictionary<string, RoutingGroupRelayMap.RGTopologySite>(dictionary, routingGroup.DistinguishedName, valueToAdd, new TransportHelpers.DiagnosticsHandler<string, RoutingGroupRelayMap.RGTopologySite>(RoutingUtils.LogErrorWhenAddToDictionaryFails<string, RoutingGroupRelayMap.RGTopologySite>)) && !TransportHelpers.AttemptAddToDictionary<Guid, RoutingGroupRelayMap.RGTopologySite>(dictionary2, routingGroup.Guid, valueToAdd, new TransportHelpers.DiagnosticsHandler<Guid, RoutingGroupRelayMap.RGTopologySite>(RoutingUtils.LogErrorWhenAddToDictionaryFails<Guid, RoutingGroupRelayMap.RGTopologySite>)))
				{
					dictionary.Remove(routingGroup.DistinguishedName);
				}
			}
			ADObjectId homeRoutingGroup = topologyConfig.LocalServer.HomeRoutingGroup;
			RoutingGroupRelayMap.RGTopologySite rgtopologySite = dictionary2[homeRoutingGroup.ObjectGuid];
			this.routesToRGConnectors = new Dictionary<Guid, RouteInfo>();
			foreach (RoutingGroupConnector connector in topologyConfig.RoutingGroupConnectors)
			{
				this.AddRGConnector(connector, rgtopologySite, dictionary, topologyConfig, serverMap, contextCore);
			}
			foreach (MailGateway mailGateway in topologyConfig.SendConnectors)
			{
				if (!MultiValuedPropertyBase.IsNullOrEmpty(mailGateway.ConnectedDomains))
				{
					this.AddConnectorWithConnectedDomains(mailGateway, rgtopologySite, dictionary, dictionary2);
				}
			}
			base.DebugTrace("Calculated routing group topology");
			return rgtopologySite;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0006A680 File Offset: 0x00068880
		private void AddRGConnector(RoutingGroupConnector connector, RoutingGroupRelayMap.RGTopologySite localRoutingGroup, Dictionary<string, RoutingGroupRelayMap.RGTopologySite> routingGroupsByDN, RoutingTopologyBase topologyConfig, RoutingServerInfoMap serverMap, RoutingContextCore contextCore)
		{
			RoutingGroupRelayMap.RGTopologySite rgtopologySite = null;
			if (!routingGroupsByDN.TryGetValue(connector.SourceRoutingGroup.DistinguishedName, out rgtopologySite))
			{
				RoutingDiag.Tracer.TraceError<DateTime, string, string>((long)this.GetHashCode(), "[{0}] Source routing group '{1}' not found for routing group connector '{2}'; skipping the connector.", this.timestamp, connector.SourceRoutingGroup.DistinguishedName, connector.DistinguishedName);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoSourceRgForRgConnector, null, new object[]
				{
					connector.SourceRoutingGroup.DistinguishedName,
					connector.DistinguishedName,
					this.timestamp
				});
				return;
			}
			RoutingGroupRelayMap.RGTopologySite rgtopologySite2 = null;
			ADObjectId targetRoutingGroup = connector.TargetRoutingGroup;
			string text;
			if (targetRoutingGroup != null)
			{
				RoutingUtils.ThrowIfEmpty(targetRoutingGroup, "rgc.TargetRoutingGroup");
				text = targetRoutingGroup.DistinguishedName;
				routingGroupsByDN.TryGetValue(text, out rgtopologySite2);
			}
			else
			{
				text = "<<null>>";
			}
			if (rgtopologySite2 == null)
			{
				RoutingDiag.Tracer.TraceError<DateTime, string, string>((long)this.GetHashCode(), "[{0}] Target routing group '{1}' not found for routing group connector '{2}'; skipping the connector.", this.timestamp, text, connector.DistinguishedName);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoTargetRgForRgConnector, null, new object[]
				{
					text,
					connector.DistinguishedName,
					this.timestamp
				});
				return;
			}
			if (object.ReferenceEquals(localRoutingGroup, rgtopologySite))
			{
				RouteInfo valueToAdd = null;
				if (!this.TryCalculateRGConnectorRoute(connector, topologyConfig, serverMap, contextCore, out valueToAdd))
				{
					return;
				}
				TransportHelpers.AttemptAddToDictionary<Guid, RouteInfo>(this.routesToRGConnectors, connector.Guid, valueToAdd, new TransportHelpers.DiagnosticsHandler<Guid, RouteInfo>(RoutingUtils.LogErrorWhenAddToDictionaryFails<Guid, RouteInfo>));
			}
			RoutingGroupRelayMap.RGTopologyLink rgtopologyLink = new RoutingGroupRelayMap.RGTopologyLink(connector, rgtopologySite2, connector.Cost);
			rgtopologySite.AddLink(rgtopologyLink);
			RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "[{0}] Added link for routing group connector '{1}'; source RG '{2}'; target RG '{3}'; cost={4}.", new object[]
			{
				this.timestamp,
				connector.DistinguishedName,
				rgtopologySite.Name,
				rgtopologySite2.Name,
				rgtopologyLink.Cost
			});
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0006A858 File Offset: 0x00068A58
		private void AddConnectorWithConnectedDomains(MailGateway connector, RoutingGroupRelayMap.RGTopologySite localRoutingGroup, Dictionary<string, RoutingGroupRelayMap.RGTopologySite> routingGroupsByDN, Dictionary<Guid, RoutingGroupRelayMap.RGTopologySite> routingGroupsByGuid)
		{
			RoutingGroupRelayMap.RGTopologySite rgtopologySite;
			if (!routingGroupsByDN.TryGetValue(connector.SourceRoutingGroup.DistinguishedName, out rgtopologySite))
			{
				RoutingDiag.Tracer.TraceError<DateTime, string, string>((long)this.GetHashCode(), "[{0}] Source routing group '{1}' not found for non-routing-group connector '{2}'; skipping the connector.", this.timestamp, connector.SourceRoutingGroup.DistinguishedName, connector.DistinguishedName);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoSourceRgForNonRgConnector, null, new object[]
				{
					connector.SourceRoutingGroup.DistinguishedName,
					connector.DistinguishedName,
					this.timestamp
				});
				return;
			}
			if (object.ReferenceEquals(localRoutingGroup, rgtopologySite))
			{
				RoutingDiag.Tracer.TraceError<DateTime, string, string>((long)this.GetHashCode(), "[{0}] Source routing group '{1}' is local for non-routing-group connector '{2}'; skipping the connector.", this.timestamp, rgtopologySite.Name, connector.DistinguishedName);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingLocalConnectorWithConnectedDomains, null, new object[]
				{
					rgtopologySite.Name,
					connector.DistinguishedName,
					this.timestamp
				});
				return;
			}
			foreach (ConnectedDomain connectedDomain in connector.ConnectedDomains)
			{
				RoutingGroupRelayMap.RGTopologySite rgtopologySite2;
				if (!routingGroupsByGuid.TryGetValue(connectedDomain.RoutingGroupGuid, out rgtopologySite2))
				{
					RoutingDiag.Tracer.TraceError<DateTime, Guid, string>((long)this.GetHashCode(), "[{0}] Target routing group '{1}' not found for non-routing-group connector '{2}'; skipping the connector.", this.timestamp, connectedDomain.RoutingGroupGuid, connector.DistinguishedName);
					RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoConnectedRg, null, new object[]
					{
						connectedDomain.RoutingGroupGuid,
						connector.DistinguishedName,
						this.timestamp
					});
					break;
				}
				RoutingGroupRelayMap.RGTopologyLink rgtopologyLink = new RoutingGroupRelayMap.RGTopologyLink(connector, rgtopologySite2, connectedDomain.Cost);
				rgtopologySite.AddLink(rgtopologyLink);
				RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "[{0}] Added link for non-routing-group connector '{1}'; source RG '{2}'; target RG '{3}'; cost={4}.", new object[]
				{
					this.timestamp,
					connector.DistinguishedName,
					rgtopologySite.Name,
					rgtopologySite2.Name,
					rgtopologyLink.Cost
				});
			}
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0006AAA0 File Offset: 0x00068CA0
		private void MapRoutesByRoutingGroupDN()
		{
			this.routesByDN = new Dictionary<string, RouteInfo>(this.routes.Count);
			foreach (RoutingGroupRelayMap.RGTopologyPath rgtopologyPath in this.routes.Values)
			{
				RouteInfo valueToAdd = RouteInfo.CreateForRemoteRG(rgtopologyPath.TargetObjectId.DistinguishedName, rgtopologyPath.MaxMessageSize, rgtopologyPath.TotalCost, rgtopologyPath.FirstRGConnectorRoute);
				TransportHelpers.AttemptAddToDictionary<string, RouteInfo>(this.routesByDN, rgtopologyPath.TargetObjectId.DistinguishedName, valueToAdd, new TransportHelpers.DiagnosticsHandler<string, RouteInfo>(RoutingUtils.LogErrorWhenAddToDictionaryFails<string, RouteInfo>));
			}
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0006AB50 File Offset: 0x00068D50
		private bool TryCalculateRGConnectorRoute(RoutingGroupConnector connector, RoutingTopologyBase topologyConfig, RoutingServerInfoMap serverMap, RoutingContextCore contextCore, out RouteInfo routeInfo)
		{
			ADObjectId id = topologyConfig.LocalServer.Id;
			if (!ConnectorRouteFactory.TryCalculateConnectorRoute(connector, id, serverMap, contextCore, out routeInfo))
			{
				return false;
			}
			if (routeInfo.DestinationProximity == Proximity.LocalServer)
			{
				ListLoadBalancer<INextHopServer> targetBridgeheads;
				if (!this.TryGetTargetBridgeheads(connector, serverMap, contextCore, out targetBridgeheads))
				{
					routeInfo = null;
					return false;
				}
				((ConnectorDeliveryHop)routeInfo.NextHop).SetTargetBridgeheads(targetBridgeheads);
			}
			return true;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0006ABAC File Offset: 0x00068DAC
		private bool TryGetTargetBridgeheads(RoutingGroupConnector connector, RoutingServerInfoMap serverMap, RoutingContextCore contextCore, out ListLoadBalancer<INextHopServer> targetBridgeheads)
		{
			targetBridgeheads = null;
			if (!MultiValuedPropertyBase.IsNullOrEmpty(connector.TargetTransportServers))
			{
				foreach (ADObjectId adobjectId in connector.TargetTransportServers)
				{
					RoutingServerInfo item;
					if (serverMap.TryGetServerInfoByDN(adobjectId.DistinguishedName, out item))
					{
						RoutingUtils.AddItemToLazyList<INextHopServer>(item, contextCore.Settings.RandomLoadBalancingOffsetEnabled, ref targetBridgeheads);
					}
					else
					{
						RoutingDiag.Tracer.TraceError<DateTime, string, string>((long)this.GetHashCode(), "[{0}] Target Bridgehead server '{1}' not found for RG connector '{2}'. Skipping the server.", this.timestamp, adobjectId.DistinguishedName, connector.DistinguishedName);
						RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoTargetBhServer, null, new object[]
						{
							adobjectId.DistinguishedName,
							connector.DistinguishedName,
							this.timestamp
						});
					}
				}
			}
			if (targetBridgeheads == null)
			{
				RoutingDiag.Tracer.TraceError<DateTime, string>((long)this.GetHashCode(), "[{0}] No target Bridgehead servers found for RG connector '{1}'. Skipping the connector.", this.timestamp, connector.DistinguishedName);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoTargetBhServers, null, new object[]
				{
					connector.DistinguishedName,
					this.timestamp
				});
				return false;
			}
			return true;
		}

		// Token: 0x04000C6C RID: 3180
		private Dictionary<string, RouteInfo> routesByDN;

		// Token: 0x04000C6D RID: 3181
		private Dictionary<Guid, RouteInfo> routesToRGConnectors;

		// Token: 0x0200025B RID: 603
		internal class RGTopologyPath : TopologyPath
		{
			// Token: 0x06001A19 RID: 6681 RVA: 0x0006ACF8 File Offset: 0x00068EF8
			public RGTopologyPath(RoutingGroupRelayMap.RGTopologyPath prePath, ITopologySite targetRoutingGroup, ITopologySiteLink connector, Dictionary<Guid, RouteInfo> routesToRGConnectors)
			{
				this.targetRoutingGroup = (RoutingGroupRelayMap.RGTopologySite)targetRoutingGroup;
				this.routesToRGConnectors = routesToRGConnectors;
				if (prePath != null)
				{
					this.firstRGConnectorRoute = prePath.firstRGConnectorRoute;
					this.totalCost = prePath.totalCost + connector.Cost;
					this.maxMessageSize = Math.Min(prePath.MaxMessageSize, (long)connector.AbsoluteMaxMessageSize);
					return;
				}
				this.firstRGConnectorRoute = this.routesToRGConnectors[((RoutingGroupRelayMap.RGTopologyLink)connector).ConnectorGuid];
				this.totalCost = connector.Cost;
				this.maxMessageSize = Math.Min((long)connector.AbsoluteMaxMessageSize, this.firstRGConnectorRoute.MaxMessageSize);
			}

			// Token: 0x170006DF RID: 1759
			// (get) Token: 0x06001A1A RID: 6682 RVA: 0x0006AD9D File Offset: 0x00068F9D
			public override int TotalCost
			{
				get
				{
					return this.totalCost;
				}
			}

			// Token: 0x170006E0 RID: 1760
			// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0006ADA5 File Offset: 0x00068FA5
			public long MaxMessageSize
			{
				get
				{
					return this.maxMessageSize;
				}
			}

			// Token: 0x170006E1 RID: 1761
			// (get) Token: 0x06001A1C RID: 6684 RVA: 0x0006ADAD File Offset: 0x00068FAD
			public ADObjectId TargetObjectId
			{
				get
				{
					return this.targetRoutingGroup.ObjectId;
				}
			}

			// Token: 0x170006E2 RID: 1762
			// (get) Token: 0x06001A1D RID: 6685 RVA: 0x0006ADBA File Offset: 0x00068FBA
			public RouteInfo FirstRGConnectorRoute
			{
				get
				{
					return this.firstRGConnectorRoute;
				}
			}

			// Token: 0x06001A1E RID: 6686 RVA: 0x0006ADC4 File Offset: 0x00068FC4
			public override void ReplaceIfBetter(TopologyPath newPrePath, ITopologySiteLink newLink, DateTime timestamp)
			{
				RoutingGroupRelayMap.RGTopologyPath rgtopologyPath = (RoutingGroupRelayMap.RGTopologyPath)newPrePath;
				int num = this.TotalCost;
				int num2 = newLink.Cost;
				if (rgtopologyPath != null)
				{
					num2 += rgtopologyPath.TotalCost;
				}
				if (num2 > num)
				{
					return;
				}
				RouteInfo other;
				if (rgtopologyPath != null)
				{
					other = rgtopologyPath.firstRGConnectorRoute;
				}
				else
				{
					other = this.routesToRGConnectors[((RoutingGroupRelayMap.RGTopologyLink)newLink).ConnectorGuid];
				}
				if (num2 == num && this.firstRGConnectorRoute.CompareTo(other, RouteComparison.CompareNames) <= 0)
				{
					return;
				}
				this.firstRGConnectorRoute = other;
				this.totalCost = num2;
				this.maxMessageSize = (long)newLink.AbsoluteMaxMessageSize;
				if (rgtopologyPath != null)
				{
					this.maxMessageSize = Math.Min(this.maxMessageSize, rgtopologyPath.MaxMessageSize);
				}
				RoutingDiag.Tracer.TraceDebug<DateTime, RoutingGroupRelayMap.RGTopologyPath, long>((long)this.GetHashCode(), "[{0}] [LCP] Replaced with better RG path: {1}, MaxMessageSize:{2}", timestamp, this, this.maxMessageSize);
			}

			// Token: 0x04000C6E RID: 3182
			private RoutingGroupRelayMap.RGTopologySite targetRoutingGroup;

			// Token: 0x04000C6F RID: 3183
			private RouteInfo firstRGConnectorRoute;

			// Token: 0x04000C70 RID: 3184
			private int totalCost;

			// Token: 0x04000C71 RID: 3185
			private long maxMessageSize;

			// Token: 0x04000C72 RID: 3186
			private Dictionary<Guid, RouteInfo> routesToRGConnectors;
		}

		// Token: 0x0200025C RID: 604
		internal class RGTopologySite : ITopologySite
		{
			// Token: 0x06001A1F RID: 6687 RVA: 0x0006AE81 File Offset: 0x00069081
			public RGTopologySite(RoutingGroup rg)
			{
				this.rg = rg;
				this.connectors = new List<ITopologySiteLink>();
				this.siteLinks = new ReadOnlyCollection<ITopologySiteLink>(this.connectors);
			}

			// Token: 0x170006E3 RID: 1763
			// (get) Token: 0x06001A20 RID: 6688 RVA: 0x0006AEAC File Offset: 0x000690AC
			public Guid Guid
			{
				get
				{
					return this.rg.Guid;
				}
			}

			// Token: 0x170006E4 RID: 1764
			// (get) Token: 0x06001A21 RID: 6689 RVA: 0x0006AEB9 File Offset: 0x000690B9
			public string Name
			{
				get
				{
					return this.rg.DistinguishedName;
				}
			}

			// Token: 0x170006E5 RID: 1765
			// (get) Token: 0x06001A22 RID: 6690 RVA: 0x0006AEC6 File Offset: 0x000690C6
			public ADObjectId ObjectId
			{
				get
				{
					return this.rg.Id;
				}
			}

			// Token: 0x170006E6 RID: 1766
			// (get) Token: 0x06001A23 RID: 6691 RVA: 0x0006AED3 File Offset: 0x000690D3
			public ReadOnlyCollection<ITopologySiteLink> TopologySiteLinks
			{
				get
				{
					return this.siteLinks;
				}
			}

			// Token: 0x06001A24 RID: 6692 RVA: 0x0006AEDB File Offset: 0x000690DB
			public void AddLink(RoutingGroupRelayMap.RGTopologyLink connector)
			{
				this.connectors.Add(connector);
			}

			// Token: 0x04000C73 RID: 3187
			private RoutingGroup rg;

			// Token: 0x04000C74 RID: 3188
			private List<ITopologySiteLink> connectors;

			// Token: 0x04000C75 RID: 3189
			private ReadOnlyCollection<ITopologySiteLink> siteLinks;
		}

		// Token: 0x0200025D RID: 605
		internal class RGTopologyLink : ITopologySiteLink
		{
			// Token: 0x06001A25 RID: 6693 RVA: 0x0006AEEC File Offset: 0x000690EC
			public RGTopologyLink(SendConnector connector, RoutingGroupRelayMap.RGTopologySite targetRoutingGroup, int cost)
			{
				this.connector = connector;
				this.cost = cost;
				this.topologySites = new ReadOnlyCollection<ITopologySite>(new ITopologySite[]
				{
					targetRoutingGroup
				});
			}

			// Token: 0x170006E7 RID: 1767
			// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0006AF24 File Offset: 0x00069124
			public string Name
			{
				get
				{
					return this.connector.DistinguishedName;
				}
			}

			// Token: 0x170006E8 RID: 1768
			// (get) Token: 0x06001A27 RID: 6695 RVA: 0x0006AF31 File Offset: 0x00069131
			public int Cost
			{
				get
				{
					return this.cost;
				}
			}

			// Token: 0x170006E9 RID: 1769
			// (get) Token: 0x06001A28 RID: 6696 RVA: 0x0006AF39 File Offset: 0x00069139
			public Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				get
				{
					return this.connector.MaxMessageSize;
				}
			}

			// Token: 0x170006EA RID: 1770
			// (get) Token: 0x06001A29 RID: 6697 RVA: 0x0006AF46 File Offset: 0x00069146
			public ulong AbsoluteMaxMessageSize
			{
				get
				{
					return this.connector.AbsoluteMaxMessageSize;
				}
			}

			// Token: 0x170006EB RID: 1771
			// (get) Token: 0x06001A2A RID: 6698 RVA: 0x0006AF53 File Offset: 0x00069153
			public ReadOnlyCollection<ITopologySite> TopologySites
			{
				get
				{
					return this.topologySites;
				}
			}

			// Token: 0x170006EC RID: 1772
			// (get) Token: 0x06001A2B RID: 6699 RVA: 0x0006AF5B File Offset: 0x0006915B
			public Guid ConnectorGuid
			{
				get
				{
					return this.connector.Guid;
				}
			}

			// Token: 0x04000C76 RID: 3190
			private readonly int cost;

			// Token: 0x04000C77 RID: 3191
			private SendConnector connector;

			// Token: 0x04000C78 RID: 3192
			private ReadOnlyCollection<ITopologySite> topologySites;
		}
	}
}
