using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000209 RID: 521
	internal class ADSiteRelayMap : TopologyRelayMap<ADSiteRelayMap.ADTopologyPath>
	{
		// Token: 0x06001721 RID: 5921 RVA: 0x0005DD28 File Offset: 0x0005BF28
		public ADSiteRelayMap(RoutingTopologyBase topologyConfig, RoutingServerInfoMap serverMap, RoutingContextCore contextCore) : base(topologyConfig.Sites.Count, topologyConfig.WhenCreated)
		{
			ITopologySite topologySite = topologyConfig.LocalServer.TopologySite;
			if (topologyConfig.Sites.Count != 1)
			{
				base.CalculateRoutes(topologySite, contextCore);
				this.PopulateSitesWithHubServers(topologyConfig, serverMap);
				this.Normalize(contextCore);
				this.MapRoutesBySiteGuid();
				return;
			}
			if (topologyConfig.Sites[0] != topologySite)
			{
				throw new InvalidOperationException("Sites property does not contain LocalSite");
			}
			base.DebugTrace("No remote AD sites found; ADSiteRelayMap is empty");
			this.routesByGuid = new Dictionary<Guid, RouteInfo>();
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0005DDB4 File Offset: 0x0005BFB4
		public bool TryGetRouteInfo(ADObjectId siteId, out RouteInfo routeInfo)
		{
			RoutingUtils.ThrowIfNullOrEmpty(siteId, "siteId");
			return this.routesByGuid.TryGetValue(siteId.ObjectGuid, out routeInfo);
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0005DDD4 File Offset: 0x0005BFD4
		public bool TryGetPath(Guid siteGuid, out ADSiteRelayMap.ADTopologyPath path)
		{
			path = null;
			RouteInfo routeInfo;
			if (!this.routesByGuid.TryGetValue(siteGuid, out routeInfo))
			{
				return false;
			}
			path = ((ADSiteRelayMap.SiteNextHop)routeInfo.NextHop).TargetPath;
			return true;
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0005DE0C File Offset: 0x0005C00C
		public bool TryGetNextHop(NextHopSolutionKey nextHopKey, out RoutingNextHop nextHop)
		{
			nextHop = null;
			RouteInfo routeInfo;
			if (!this.routesByGuid.TryGetValue(nextHopKey.NextHopConnector, out routeInfo))
			{
				RoutingDiag.Tracer.TraceError<DateTime, NextHopSolutionKey>(0L, "[{0}] Target AD Site is not found for next hop key <{1}>", this.timestamp, nextHopKey);
				return false;
			}
			nextHop = routeInfo.NextHop;
			return true;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0005DE55 File Offset: 0x0005C055
		public bool QuickMatch(ADSiteRelayMap other)
		{
			return this.routesByGuid.Count == other.routesByGuid.Count;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0005DE6F File Offset: 0x0005C06F
		public bool FullMatch(ADSiteRelayMap other)
		{
			return RoutingUtils.MatchRouteDictionaries<Guid>(this.routesByGuid, other.routesByGuid);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0005DE82 File Offset: 0x0005C082
		protected override ADSiteRelayMap.ADTopologyPath CreateTopologyPath(ADSiteRelayMap.ADTopologyPath prePath, ITopologySite targetSite, ITopologySiteLink link, RoutingContextCore contextCore)
		{
			return new ADSiteRelayMap.ADTopologyPath(prePath, this.timestamp, targetSite, link, contextCore);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0005DE94 File Offset: 0x0005C094
		private void PopulateSitesWithHubServers(RoutingTopologyBase topologyConfig, RoutingServerInfoMap serverMap)
		{
			base.DebugTrace("Populating remote AD sites with Hub servers");
			Dictionary<Guid, TopologySite> dictionary = new Dictionary<Guid, TopologySite>(topologyConfig.Sites.Count);
			foreach (TopologySite topologySite in topologyConfig.Sites)
			{
				TransportHelpers.AttemptAddToDictionary<Guid, TopologySite>(dictionary, topologySite.Guid, topologySite, new TransportHelpers.DiagnosticsHandler<Guid, TopologySite>(RoutingUtils.LogErrorWhenAddToDictionaryFails<Guid, TopologySite>));
			}
			foreach (RoutingServerInfo routingServerInfo in serverMap.GetHubTransportServers())
			{
				if (!serverMap.IsInLocalSite(routingServerInfo))
				{
					TopologySite key;
					ADSiteRelayMap.ADTopologyPath adtopologyPath;
					if (dictionary.TryGetValue(routingServerInfo.ADSite.ObjectGuid, out key) && this.routes.TryGetValue(key, out adtopologyPath))
					{
						adtopologyPath.TargetSite.AddHubServer(routingServerInfo);
						RoutingDiag.Tracer.TraceDebug<DateTime, string, string>((long)this.GetHashCode(), "[{0}] Added Hub server {1} to target AD site {2}", this.timestamp, routingServerInfo.Fqdn, routingServerInfo.ADSite.Name);
					}
					else
					{
						RoutingDiag.Tracer.TraceError<DateTime, string, string>((long)this.GetHashCode(), "[{0}] Route for Hub server {1} from AD site {2} not found while populating sites with Hub servers", this.timestamp, routingServerInfo.Fqdn, routingServerInfo.ADSite.Name);
						RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoRouteToAdSite, null, new object[]
						{
							routingServerInfo.ADSite.Name,
							this.timestamp,
							routingServerInfo.Fqdn
						});
					}
				}
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0005E034 File Offset: 0x0005C234
		private void Normalize(RoutingContextCore context)
		{
			base.DebugTrace("Normalizing AD site relay map");
			List<ITopologySite> list = new List<ITopologySite>();
			foreach (KeyValuePair<ITopologySite, ADSiteRelayMap.ADTopologyPath> keyValuePair in this.routes)
			{
				if (keyValuePair.Value.TargetSite.HasHubServers)
				{
					keyValuePair.Value.TargetSite.UpdateSiteStateAndRemoveInactiveServers(context);
				}
				else
				{
					list.Add(keyValuePair.Key);
				}
			}
			if (list.Count > 0)
			{
				foreach (ITopologySite topologySite in list)
				{
					RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] Removing AD site {1} from the relay map because it does not have any Hub servers", this.timestamp, topologySite.Name);
					this.routes.Remove(topologySite);
				}
				foreach (ADSiteRelayMap.ADTopologyPath adtopologyPath in this.routes.Values)
				{
					adtopologyPath.Normalize(this.timestamp);
				}
			}
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0005E188 File Offset: 0x0005C388
		private void MapRoutesBySiteGuid()
		{
			this.routesByGuid = new Dictionary<Guid, RouteInfo>(this.routes.Count);
			foreach (ADSiteRelayMap.ADTopologyPath adtopologyPath in this.routes.Values)
			{
				ADSiteRelayMap.SiteNextHop nextHop = new ADSiteRelayMap.SiteNextHop(adtopologyPath.NextHopPath);
				RouteInfo routeInfo = RouteInfo.CreateForRemoteSite(adtopologyPath.TargetSite.Name, nextHop, adtopologyPath.MaxMessageSize, adtopologyPath.TotalCost);
				adtopologyPath.RouteInfo = routeInfo;
				TransportHelpers.AttemptAddToDictionary<Guid, RouteInfo>(this.routesByGuid, adtopologyPath.TargetSite.Guid, routeInfo, new TransportHelpers.DiagnosticsHandler<Guid, RouteInfo>(RoutingUtils.LogErrorWhenAddToDictionaryFails<Guid, RouteInfo>));
			}
		}

		// Token: 0x04000B6E RID: 2926
		public const int BinaryBackoffThreshold = 4;

		// Token: 0x04000B6F RID: 2927
		private Dictionary<Guid, RouteInfo> routesByGuid;

		// Token: 0x0200020B RID: 523
		internal class ADTopologyPath : TopologyPath
		{
			// Token: 0x06001730 RID: 5936 RVA: 0x0005E260 File Offset: 0x0005C460
			public ADTopologyPath(ADSiteRelayMap.ADTopologyPath prePath, DateTime timestamp, ITopologySite targetSite, ITopologySiteLink link, RoutingContextCore contextCore)
			{
				this.targetSite = new ADSiteRelayMap.TargetSite(targetSite, contextCore);
				this.prePath = prePath;
				this.link = link;
				this.totalCost = link.Cost;
				this.maxMessageSize = (long)((link.AbsoluteMaxMessageSize > 9223372036854775807UL) ? 9223372036854775807UL : link.AbsoluteMaxMessageSize);
				if (prePath != null)
				{
					this.totalCost += prePath.totalCost;
					this.SetHubPath(timestamp);
					if (prePath.MaxMessageSize < this.maxMessageSize)
					{
						this.maxMessageSize = prePath.MaxMessageSize;
					}
				}
			}

			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x06001731 RID: 5937 RVA: 0x0005E2FE File Offset: 0x0005C4FE
			public ADSiteRelayMap.TargetSite TargetSite
			{
				get
				{
					return this.targetSite;
				}
			}

			// Token: 0x1700061E RID: 1566
			// (get) Token: 0x06001732 RID: 5938 RVA: 0x0005E306 File Offset: 0x0005C506
			public ADSiteRelayMap.ADTopologyPath NextHopPath
			{
				get
				{
					if (this.hubPath == null)
					{
						return this;
					}
					return this.hubPath;
				}
			}

			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x06001733 RID: 5939 RVA: 0x0005E318 File Offset: 0x0005C518
			public ADSiteRelayMap.TargetSite NextHopSite
			{
				get
				{
					return this.NextHopPath.targetSite;
				}
			}

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x06001734 RID: 5940 RVA: 0x0005E325 File Offset: 0x0005C525
			public override int TotalCost
			{
				get
				{
					return this.totalCost;
				}
			}

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x06001735 RID: 5941 RVA: 0x0005E32D File Offset: 0x0005C52D
			public long MaxMessageSize
			{
				get
				{
					return this.maxMessageSize;
				}
			}

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x06001736 RID: 5942 RVA: 0x0005E335 File Offset: 0x0005C535
			// (set) Token: 0x06001737 RID: 5943 RVA: 0x0005E33D File Offset: 0x0005C53D
			public RouteInfo RouteInfo
			{
				get
				{
					return this.routeInfo;
				}
				set
				{
					this.routeInfo = value;
				}
			}

			// Token: 0x06001738 RID: 5944 RVA: 0x0005E348 File Offset: 0x0005C548
			public static ADSiteRelayMap.ADTopologyPath GetCommonPath(ADSiteRelayMap.ADTopologyPath path1, ADSiteRelayMap.ADTopologyPath path2)
			{
				if (object.ReferenceEquals(path1, path2))
				{
					return path1;
				}
				int num = path1.SegmentCount();
				int num2 = path2.SegmentCount();
				if (num != num2)
				{
					ADSiteRelayMap.ADTopologyPath adtopologyPath;
					int num3;
					if (num > num2)
					{
						adtopologyPath = path1;
						num3 = num - num2;
					}
					else
					{
						adtopologyPath = path2;
						num3 = num2 - num;
					}
					while (num3-- > 0)
					{
						adtopologyPath = adtopologyPath.prePath;
					}
					if (num > num2)
					{
						path1 = adtopologyPath;
					}
					else
					{
						path2 = adtopologyPath;
					}
				}
				while (!object.ReferenceEquals(path1, path2))
				{
					path1 = path1.prePath;
					path2 = path2.prePath;
				}
				return path1;
			}

			// Token: 0x06001739 RID: 5945 RVA: 0x0005E3BD File Offset: 0x0005C5BD
			public Guid FirstHopSiteGuid()
			{
				if (this.prePath != null)
				{
					return this.prePath.FirstHopSiteGuid();
				}
				return this.targetSite.Guid;
			}

			// Token: 0x0600173A RID: 5946 RVA: 0x0005E3E0 File Offset: 0x0005C5E0
			public override string ToString()
			{
				return string.Format("{0} Link:'{1}',cost={2} Site:'{3}'", new object[]
				{
					this.prePath ?? "Site:<Local>",
					this.link.Name,
					this.link.Cost,
					this.targetSite.Site.Name
				});
			}

			// Token: 0x0600173B RID: 5947 RVA: 0x0005E448 File Offset: 0x0005C648
			public override void ReplaceIfBetter(TopologyPath newPrePath, ITopologySiteLink newLink, DateTime timestamp)
			{
				ADSiteRelayMap.ADTopologyPath adtopologyPath = (ADSiteRelayMap.ADTopologyPath)newPrePath;
				int num = this.TotalCost;
				int num2 = this.SegmentCount();
				int num3 = newLink.Cost;
				int num4 = 1;
				if (adtopologyPath != null)
				{
					num3 += adtopologyPath.TotalCost;
					num4 += adtopologyPath.SegmentCount();
				}
				if (num > num3 || (num == num3 && (num2 > num4 || (num2 == num4 && num2 > 1 && RoutingUtils.CompareNames(this.prePath.targetSite.Site.Name, adtopologyPath.targetSite.Site.Name) > 0))))
				{
					this.prePath = adtopologyPath;
					this.link = newLink;
					this.totalCost = num3;
					this.maxMessageSize = (long)((newLink.AbsoluteMaxMessageSize > 9223372036854775807UL) ? 9223372036854775807UL : newLink.AbsoluteMaxMessageSize);
					if (adtopologyPath != null && adtopologyPath.MaxMessageSize < this.maxMessageSize)
					{
						this.maxMessageSize = adtopologyPath.MaxMessageSize;
					}
					RoutingDiag.Tracer.TraceDebug<DateTime, ADSiteRelayMap.ADTopologyPath>((long)this.GetHashCode(), "[{0}] [LCP] Replaced with better path: {1}", timestamp, this);
					this.SetHubPath(timestamp);
				}
			}

			// Token: 0x0600173C RID: 5948 RVA: 0x0005E550 File Offset: 0x0005C750
			public void Normalize(DateTime timestamp)
			{
				while (this.prePath != null && !this.prePath.targetSite.HasHubServers)
				{
					RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "[{0}] Removing site '{1}' from the path to site '{2}' and combining links '{3}' and '{4}'", new object[]
					{
						timestamp,
						this.prePath.targetSite.Name,
						this.targetSite.Name,
						this.prePath.link.Name,
						this.link.Name
					});
					this.link = new ADSiteRelayMap.CombinedLink(this.link, this.prePath.link);
					this.prePath = this.prePath.prePath;
				}
				if (this.prePath != null && !this.prePath.normalized)
				{
					this.prePath.Normalize(timestamp);
				}
				this.normalized = true;
				this.SetHubPath(timestamp);
				RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] Normalized path to AD site '{1}'", timestamp, this.targetSite.Name);
			}

			// Token: 0x0600173D RID: 5949 RVA: 0x0005E87C File Offset: 0x0005CA7C
			public IEnumerable<RoutingServerInfo> GetLoadBalancedHubServers()
			{
				foreach (ADSiteRelayMap.TargetSite site in this.GetBackoffSites())
				{
					foreach (RoutingServerInfo serverInfo in site.LoadBalancedHubServers)
					{
						yield return serverInfo;
					}
				}
				yield break;
			}

			// Token: 0x0600173E RID: 5950 RVA: 0x0005E89C File Offset: 0x0005CA9C
			public bool Match(ADSiteRelayMap.ADTopologyPath other)
			{
				return this.NextHopSite.Guid == other.NextHopSite.Guid && this.TargetSite.Match(other.TargetSite) && RoutingUtils.NullMatch(this.prePath, other.prePath);
			}

			// Token: 0x0600173F RID: 5951 RVA: 0x0005E8EC File Offset: 0x0005CAEC
			private int SegmentCount()
			{
				int num = 0;
				for (ADSiteRelayMap.ADTopologyPath adtopologyPath = this; adtopologyPath != null; adtopologyPath = adtopologyPath.prePath)
				{
					num++;
				}
				return num;
			}

			// Token: 0x06001740 RID: 5952 RVA: 0x0005EAA8 File Offset: 0x0005CCA8
			private IEnumerable<ADSiteRelayMap.TargetSite> GetBackoffSites()
			{
				int segmentsLeft = this.SegmentCount();
				int numSkip = 0;
				bool destinationSite = true;
				for (ADSiteRelayMap.ADTopologyPath path = this; path != null; path = path.prePath)
				{
					segmentsLeft--;
					if (numSkip == 0)
					{
						if (destinationSite || path.targetSite.IsActive)
						{
							if (segmentsLeft > 4)
							{
								numSkip = segmentsLeft / 2;
								if (segmentsLeft - numSkip < 4)
								{
									numSkip = segmentsLeft - 4;
								}
							}
							destinationSite = false;
							yield return path.targetSite;
						}
					}
					else
					{
						numSkip--;
					}
				}
				yield break;
			}

			// Token: 0x06001741 RID: 5953 RVA: 0x0005EAC8 File Offset: 0x0005CCC8
			private void SetHubPath(DateTime timestamp)
			{
				this.hubPath = null;
				if (this.prePath != null)
				{
					if (this.prePath.hubPath != null)
					{
						this.hubPath = this.prePath.hubPath;
					}
					else
					{
						TopologySite site = this.prePath.TargetSite.Site;
						if (site.HubSiteEnabled)
						{
							this.hubPath = this.prePath;
						}
					}
				}
				if (this.hubPath != null)
				{
					RoutingDiag.Tracer.TraceDebug<DateTime, string, string>((long)this.GetHashCode(), "[{0}] Path to AD site '{1}' contains hub site {2}", timestamp, this.targetSite.Name, this.hubPath.targetSite.Name);
					return;
				}
				RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] Path to AD site '{1}' does not contain a hub site", timestamp, this.targetSite.Name);
			}

			// Token: 0x04000B71 RID: 2929
			private ADSiteRelayMap.TargetSite targetSite;

			// Token: 0x04000B72 RID: 2930
			private ADSiteRelayMap.ADTopologyPath prePath;

			// Token: 0x04000B73 RID: 2931
			private ITopologySiteLink link;

			// Token: 0x04000B74 RID: 2932
			private int totalCost;

			// Token: 0x04000B75 RID: 2933
			private long maxMessageSize;

			// Token: 0x04000B76 RID: 2934
			private bool normalized;

			// Token: 0x04000B77 RID: 2935
			private ADSiteRelayMap.ADTopologyPath hubPath;

			// Token: 0x04000B78 RID: 2936
			private RouteInfo routeInfo;
		}

		// Token: 0x0200020C RID: 524
		internal class TargetSite
		{
			// Token: 0x06001742 RID: 5954 RVA: 0x0005EB86 File Offset: 0x0005CD86
			public TargetSite(ITopologySite site, RoutingContextCore contextCore)
			{
				this.Site = (site as TopologySite);
				this.hubServers = new ListLoadBalancer<RoutingServerInfo>(contextCore.Settings.RandomLoadBalancingOffsetEnabled);
			}

			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x06001743 RID: 5955 RVA: 0x0005EBB0 File Offset: 0x0005CDB0
			public string Name
			{
				get
				{
					return this.Site.Name;
				}
			}

			// Token: 0x17000624 RID: 1572
			// (get) Token: 0x06001744 RID: 5956 RVA: 0x0005EBBD File Offset: 0x0005CDBD
			public Guid Guid
			{
				get
				{
					return this.Site.Guid;
				}
			}

			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x06001745 RID: 5957 RVA: 0x0005EBCA File Offset: 0x0005CDCA
			public ADObjectId Id
			{
				get
				{
					return this.Site.Id;
				}
			}

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x06001746 RID: 5958 RVA: 0x0005EBD7 File Offset: 0x0005CDD7
			public bool HasHubServers
			{
				get
				{
					return !this.hubServers.IsEmpty;
				}
			}

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x06001747 RID: 5959 RVA: 0x0005EBE7 File Offset: 0x0005CDE7
			public int HubServerCount
			{
				get
				{
					return this.hubServers.Count;
				}
			}

			// Token: 0x17000628 RID: 1576
			// (get) Token: 0x06001748 RID: 5960 RVA: 0x0005EBF4 File Offset: 0x0005CDF4
			public ICollection<RoutingServerInfo> HubServers
			{
				get
				{
					return this.hubServers.NonLoadBalancedCollection;
				}
			}

			// Token: 0x17000629 RID: 1577
			// (get) Token: 0x06001749 RID: 5961 RVA: 0x0005EC01 File Offset: 0x0005CE01
			public ICollection<RoutingServerInfo> LoadBalancedHubServers
			{
				get
				{
					return this.hubServers.LoadBalancedCollection;
				}
			}

			// Token: 0x1700062A RID: 1578
			// (get) Token: 0x0600174A RID: 5962 RVA: 0x0005EC0E File Offset: 0x0005CE0E
			public bool IsActive
			{
				get
				{
					if (this.isActive == null)
					{
						throw new InvalidOperationException("IsActive not computed");
					}
					return this.isActive.Value;
				}
			}

			// Token: 0x0600174B RID: 5963 RVA: 0x0005EC33 File Offset: 0x0005CE33
			public void AddHubServer(RoutingServerInfo server)
			{
				this.hubServers.AddItem(server);
			}

			// Token: 0x0600174C RID: 5964 RVA: 0x0005EC44 File Offset: 0x0005CE44
			public void UpdateSiteStateAndRemoveInactiveServers(RoutingContextCore context)
			{
				bool flag = false;
				List<RoutingServerInfo> list = null;
				foreach (RoutingServerInfo routingServerInfo in this.hubServers.NonLoadBalancedCollection)
				{
					if (context.VerifyHubComponentStateRestriction(routingServerInfo))
					{
						flag = true;
					}
					else
					{
						RoutingUtils.AddItemToLazyList<RoutingServerInfo>(routingServerInfo, ref list);
					}
				}
				if (flag && list != null)
				{
					foreach (RoutingServerInfo item in list)
					{
						this.hubServers.RemoveItem(item);
					}
				}
				this.isActive = new bool?(flag);
			}

			// Token: 0x0600174D RID: 5965 RVA: 0x0005ED04 File Offset: 0x0005CF04
			public bool Match(ADSiteRelayMap.TargetSite other)
			{
				return this.Site.InboundMailEnabled == other.Site.InboundMailEnabled && RoutingUtils.MatchStrings(this.Site.Name, other.Site.Name);
			}

			// Token: 0x04000B79 RID: 2937
			public readonly TopologySite Site;

			// Token: 0x04000B7A RID: 2938
			private ListLoadBalancer<RoutingServerInfo> hubServers;

			// Token: 0x04000B7B RID: 2939
			private bool? isActive;
		}

		// Token: 0x0200020D RID: 525
		private class CombinedLink : ITopologySiteLink
		{
			// Token: 0x0600174E RID: 5966 RVA: 0x0005ED3B File Offset: 0x0005CF3B
			public CombinedLink(ITopologySiteLink link1, ITopologySiteLink link2)
			{
				this.link1 = link1;
				this.link2 = link2;
			}

			// Token: 0x1700062B RID: 1579
			// (get) Token: 0x0600174F RID: 5967 RVA: 0x0005ED51 File Offset: 0x0005CF51
			public string Name
			{
				get
				{
					return string.Format("Combined({0}, {1})", this.link1.Name, this.link2.Name);
				}
			}

			// Token: 0x1700062C RID: 1580
			// (get) Token: 0x06001750 RID: 5968 RVA: 0x0005ED73 File Offset: 0x0005CF73
			public int Cost
			{
				get
				{
					return this.link1.Cost + this.link2.Cost;
				}
			}

			// Token: 0x1700062D RID: 1581
			// (get) Token: 0x06001751 RID: 5969 RVA: 0x0005ED8C File Offset: 0x0005CF8C
			public Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				get
				{
					if (this.link1.MaxMessageSize.CompareTo(this.link2.MaxMessageSize) >= 0)
					{
						return this.link2.MaxMessageSize;
					}
					return this.link1.MaxMessageSize;
				}
			}

			// Token: 0x1700062E RID: 1582
			// (get) Token: 0x06001752 RID: 5970 RVA: 0x0005EDD1 File Offset: 0x0005CFD1
			public ulong AbsoluteMaxMessageSize
			{
				get
				{
					return Math.Min(this.link1.AbsoluteMaxMessageSize, this.link2.AbsoluteMaxMessageSize);
				}
			}

			// Token: 0x1700062F RID: 1583
			// (get) Token: 0x06001753 RID: 5971 RVA: 0x0005EDEE File Offset: 0x0005CFEE
			public ReadOnlyCollection<ITopologySite> TopologySites
			{
				get
				{
					return null;
				}
			}

			// Token: 0x04000B7C RID: 2940
			private ITopologySiteLink link1;

			// Token: 0x04000B7D RID: 2941
			private ITopologySiteLink link2;
		}

		// Token: 0x02000210 RID: 528
		private class SiteNextHop : DeliveryGroup
		{
			// Token: 0x06001769 RID: 5993 RVA: 0x0005EFB0 File Offset: 0x0005D1B0
			public SiteNextHop(ADSiteRelayMap.ADTopologyPath targetPath)
			{
				RoutingUtils.ThrowIfNull(targetPath, "targetPath");
				this.targetPath = targetPath;
			}

			// Token: 0x17000638 RID: 1592
			// (get) Token: 0x0600176A RID: 5994 RVA: 0x0005EFCA File Offset: 0x0005D1CA
			public ADSiteRelayMap.ADTopologyPath TargetPath
			{
				get
				{
					return this.targetPath;
				}
			}

			// Token: 0x17000639 RID: 1593
			// (get) Token: 0x0600176B RID: 5995 RVA: 0x0005EFD2 File Offset: 0x0005D1D2
			public override IEnumerable<RoutingServerInfo> AllServersNoFallback
			{
				get
				{
					return this.targetPath.TargetSite.HubServers;
				}
			}

			// Token: 0x1700063A RID: 1594
			// (get) Token: 0x0600176C RID: 5996 RVA: 0x0005EFE4 File Offset: 0x0005D1E4
			public override string Name
			{
				get
				{
					return this.targetPath.TargetSite.Name;
				}
			}

			// Token: 0x1700063B RID: 1595
			// (get) Token: 0x0600176D RID: 5997 RVA: 0x0005EFF6 File Offset: 0x0005D1F6
			public override bool IsActive
			{
				get
				{
					return this.targetPath.TargetSite.IsActive;
				}
			}

			// Token: 0x1700063C RID: 1596
			// (get) Token: 0x0600176E RID: 5998 RVA: 0x0005F008 File Offset: 0x0005D208
			public override DeliveryType DeliveryType
			{
				get
				{
					return DeliveryType.SmtpRelayToRemoteAdSite;
				}
			}

			// Token: 0x1700063D RID: 1597
			// (get) Token: 0x0600176F RID: 5999 RVA: 0x0005F00B File Offset: 0x0005D20B
			public override Guid NextHopGuid
			{
				get
				{
					return this.targetPath.TargetSite.Guid;
				}
			}

			// Token: 0x1700063E RID: 1598
			// (get) Token: 0x06001770 RID: 6000 RVA: 0x0005F01D File Offset: 0x0005D21D
			public override bool IsMandatoryTopologyHop
			{
				get
				{
					return this.targetPath.TargetSite.Site.HubSiteEnabled;
				}
			}

			// Token: 0x1700063F RID: 1599
			// (get) Token: 0x06001771 RID: 6001 RVA: 0x0005F034 File Offset: 0x0005D234
			public override RouteInfo PrimaryRoute
			{
				get
				{
					return this.targetPath.NextHopPath.RouteInfo;
				}
			}

			// Token: 0x06001772 RID: 6002 RVA: 0x0005F046 File Offset: 0x0005D246
			public override IEnumerable<INextHopServer> GetLoadBalancedNextHopServers(string nextHopDomain)
			{
				return this.targetPath.GetLoadBalancedHubServers();
			}

			// Token: 0x06001773 RID: 6003 RVA: 0x0005F06C File Offset: 0x0005D26C
			public override IEnumerable<RoutingServerInfo> GetServersForProxyTarget(ProxyRoutingEnumeratorContext context)
			{
				return context.PostLoadbalanceFilter(from serverInfo in this.targetPath.TargetSite.LoadBalancedHubServers
				where context.PreLoadbalanceFilter(serverInfo)
				select serverInfo, new bool?(true));
			}

			// Token: 0x06001774 RID: 6004 RVA: 0x0005F0B8 File Offset: 0x0005D2B8
			public override bool Match(RoutingNextHop other)
			{
				ADSiteRelayMap.SiteNextHop siteNextHop = other as ADSiteRelayMap.SiteNextHop;
				return siteNextHop != null && this.targetPath.Match(siteNextHop.targetPath);
			}

			// Token: 0x04000B7E RID: 2942
			private ADSiteRelayMap.ADTopologyPath targetPath;
		}
	}
}
