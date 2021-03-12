using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200024D RID: 589
	internal class RoutedServerCollection
	{
		// Token: 0x0600199C RID: 6556 RVA: 0x00067F20 File Offset: 0x00066120
		public RoutedServerCollection(RouteInfo routeInfo, RoutingServerInfo serverInfo, RoutingContextCore contextCore)
		{
			RoutingUtils.ThrowIfNull(routeInfo, "routeInfo");
			RoutingUtils.ThrowIfNull(serverInfo, "serverInfo");
			RoutingUtils.ThrowIfNull(contextCore, "contextCore");
			this.serversByRoutes = new SortedList<RouteInfo, ListLoadBalancer<RoutingServerInfo>>(RoutedServerCollection.routeComparer);
			this.AddServerForRoute(routeInfo, serverInfo, contextCore);
			this.ServerSelectStrategyForProxyTarget = contextCore.Settings.ProxyRoutingServerSelectStrategy;
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x00067F7E File Offset: 0x0006617E
		public Proximity ClosestProximity
		{
			get
			{
				return this.PrimaryRoute.DestinationProximity;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x00067F8B File Offset: 0x0006618B
		// (set) Token: 0x0600199F RID: 6559 RVA: 0x00067F93 File Offset: 0x00066193
		public RoutedServerSelectStrategy ServerSelectStrategyForProxyTarget
		{
			get
			{
				return this.serverSelectStrategyForProxyTarget;
			}
			private set
			{
				this.serverSelectStrategyForProxyTarget = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00067F9C File Offset: 0x0006619C
		public RouteInfo PrimaryRoute
		{
			get
			{
				return this.serversByRoutes.Keys[0];
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00067FAF File Offset: 0x000661AF
		public int ServerGroupCount
		{
			get
			{
				return this.serversByRoutes.Count;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x00067FBC File Offset: 0x000661BC
		public IList<RoutingServerInfo> PrimaryRouteServers
		{
			get
			{
				return this.serversByRoutes.Values[0].NonLoadBalancedList;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x000681E4 File Offset: 0x000663E4
		public IEnumerable<RoutingServerInfo> AllServers
		{
			get
			{
				foreach (ListLoadBalancer<RoutingServerInfo> serverList in this.AllServerLists)
				{
					foreach (RoutingServerInfo serverInfo in serverList.NonLoadBalancedList)
					{
						yield return serverInfo;
					}
				}
				yield break;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x00068418 File Offset: 0x00066618
		public IEnumerable<RoutingServerInfo> AllServersLoadBalanced
		{
			get
			{
				foreach (ListLoadBalancer<RoutingServerInfo> serverList in this.AllServerLists)
				{
					foreach (RoutingServerInfo serverInfo in serverList.LoadBalancedCollection)
					{
						yield return serverInfo;
					}
				}
				yield break;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x00068435 File Offset: 0x00066635
		private IEnumerable<ListLoadBalancer<RoutingServerInfo>> AllServerLists
		{
			get
			{
				return this.serversByRoutes.Values;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x00068442 File Offset: 0x00066642
		private RoutingServerInfo LocalServer
		{
			get
			{
				if (this.PrimaryRoute.DestinationProximity != Proximity.LocalServer)
				{
					throw new InvalidOperationException("Local server is not present in this RoutedServerCollection");
				}
				return this.PrimaryRouteServers[0];
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x00068468 File Offset: 0x00066668
		private int LocalSiteRouteCount
		{
			get
			{
				int num = 0;
				while (num < this.serversByRoutes.Count && this.serversByRoutes.Keys[num].InLocalADSite)
				{
					num++;
				}
				if (num > 2)
				{
					throw new InvalidOperationException("LocalSiteRouteCount must be between 0 and 2; actual value: " + num);
				}
				return num;
			}
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x000684C0 File Offset: 0x000666C0
		public IEnumerable<RoutingServerInfo> GetServersForProxyTarget(ProxyRoutingEnumeratorContext context)
		{
			switch (this.ServerSelectStrategyForProxyTarget)
			{
			case RoutedServerSelectStrategy.FavorCloserProximity:
				return this.GetServersByProximity(context);
			case RoutedServerSelectStrategy.FavorLoadBalance:
				return this.GetAllServersLoadBalanced(context);
			default:
				throw new NotImplementedException("Only FavorCloserProximity and FavorLoadBalance are supported.");
			}
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00068808 File Offset: 0x00066A08
		public IEnumerable<RoutingServerInfo> GetServersForShadowTarget(ProxyRoutingEnumeratorContext context, ShadowRoutingConfiguration shadowRoutingConfig)
		{
			if (context.RemainingServerCount != 0)
			{
				int localSiteRouteCount = this.LocalSiteRouteCount;
				int remoteSiteRouteCount = this.serversByRoutes.Count - localSiteRouteCount;
				if (shadowRoutingConfig.ShadowMessagePreference != ShadowMessagePreference.LocalOnly && remoteSiteRouteCount > 0)
				{
					foreach (RoutingServerInfo serverInfo in context.PostLoadbalanceFilter(this.GetRemoteSiteServersForProxyTarget(localSiteRouteCount, context), new bool?(true)))
					{
						yield return serverInfo;
					}
				}
				if (shadowRoutingConfig.ShadowMessagePreference != ShadowMessagePreference.RemoteOnly)
				{
					foreach (RoutingServerInfo serverInfo2 in context.PostLoadbalanceFilter(this.GetLocalSiteServersForProxyTarget(localSiteRouteCount, context), new bool?(false)))
					{
						yield return serverInfo2;
					}
				}
			}
			yield break;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00068833 File Offset: 0x00066A33
		public void AddServerForRoute(RouteInfo routeInfo, RoutingServerInfo serverInfo, RoutingContextCore contextCore)
		{
			this.AddServerForRoute(routeInfo, serverInfo, false, contextCore);
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00068840 File Offset: 0x00066A40
		public void AddServerForRoute(RouteInfo routeInfo, RoutingServerInfo serverInfo, bool replaceExistingData, RoutingContextCore contextCore)
		{
			RoutingUtils.ThrowIfNull(routeInfo, "routeInfo");
			RoutingUtils.ThrowIfNull(serverInfo, "serverInfo");
			RoutingUtils.ThrowIfNull(contextCore, "contextCore");
			if (replaceExistingData)
			{
				this.serversByRoutes.Clear();
			}
			int num = this.serversByRoutes.IndexOfKey(routeInfo);
			if (num == -1)
			{
				ListLoadBalancer<RoutingServerInfo> listLoadBalancer = new ListLoadBalancer<RoutingServerInfo>(contextCore.Settings.RandomLoadBalancingOffsetEnabled);
				listLoadBalancer.AddItem(serverInfo);
				this.serversByRoutes.Add(routeInfo, listLoadBalancer);
				return;
			}
			this.serversByRoutes.Values[num].AddItem(serverInfo);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000688CC File Offset: 0x00066ACC
		public int TrimRoutes(Proximity maxProximity, int maxCost, long minMaxMessageSize)
		{
			int num = this.serversByRoutes.Count;
			int num2 = 0;
			while (--num > 0)
			{
				RouteInfo routeInfo = this.serversByRoutes.Keys[num];
				if (routeInfo.HasMandatoryTopologyHop || routeInfo.DestinationProximity > maxProximity || routeInfo.SiteRelayCost > maxCost || routeInfo.MaxMessageSize < minMaxMessageSize)
				{
					this.serversByRoutes.RemoveAt(num);
					num2++;
				}
			}
			return num2;
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00068954 File Offset: 0x00066B54
		public bool MatchServers(RoutedServerCollection other)
		{
			if (this.serversByRoutes.Count != other.serversByRoutes.Count)
			{
				return false;
			}
			for (int i = 0; i < this.serversByRoutes.Count; i++)
			{
				if (!RoutingUtils.MatchLists<RoutingServerInfo>(this.serversByRoutes.Values[i], other.serversByRoutes.Values[i], (RoutingServerInfo server1, RoutingServerInfo server2) => server1.Id.ObjectGuid == server2.Id.ObjectGuid))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x000689DC File Offset: 0x00066BDC
		public bool GetStateAndRemoveInactiveServersIfStateIsActive(RoutingContextCore context)
		{
			List<RouteInfo> list = null;
			List<KeyValuePair<RouteInfo, List<RoutingServerInfo>>> list2 = null;
			bool flag = false;
			foreach (KeyValuePair<RouteInfo, ListLoadBalancer<RoutingServerInfo>> keyValuePair in this.serversByRoutes)
			{
				bool flag2 = false;
				List<RoutingServerInfo> value = null;
				foreach (RoutingServerInfo routingServerInfo in keyValuePair.Value.NonLoadBalancedCollection)
				{
					if (context.VerifyHubComponentStateRestriction(routingServerInfo))
					{
						flag = true;
						flag2 = true;
					}
					else
					{
						RoutingUtils.AddItemToLazyList<RoutingServerInfo>(routingServerInfo, ref value);
					}
				}
				if (flag2)
				{
					RoutingUtils.AddItemToLazyList<KeyValuePair<RouteInfo, List<RoutingServerInfo>>>(new KeyValuePair<RouteInfo, List<RoutingServerInfo>>(keyValuePair.Key, value), ref list2);
				}
				else
				{
					RoutingUtils.AddItemToLazyList<RouteInfo>(keyValuePair.Key, ref list);
				}
			}
			if (flag)
			{
				if (list != null)
				{
					foreach (RouteInfo key in list)
					{
						this.serversByRoutes.Remove(key);
					}
				}
				if (list2 != null)
				{
					foreach (KeyValuePair<RouteInfo, List<RoutingServerInfo>> keyValuePair2 in list2)
					{
						if (keyValuePair2.Value != null)
						{
							foreach (RoutingServerInfo item in keyValuePair2.Value)
							{
								this.serversByRoutes[keyValuePair2.Key].RemoveItem(item);
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00068BAC File Offset: 0x00066DAC
		private static bool TryGetNextServerIndex(LoadBalancedCollection<RoutingServerInfo> serverList, int offset, ProxyRoutingEnumeratorContext context, out int index)
		{
			index = -1;
			for (int i = offset; i < serverList.Count; i++)
			{
				if (context.PreLoadbalanceFilter(serverList[i]))
				{
					index = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00068BE4 File Offset: 0x00066DE4
		private IEnumerable<RoutingServerInfo> GetAllServersLoadBalanced(ProxyRoutingEnumeratorContext context)
		{
			return context.PostLoadbalanceFilter(this.GetAllSitesServersForProxyTarget(context), null);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00068EE4 File Offset: 0x000670E4
		private IEnumerable<RoutingServerInfo> GetServersByProximity(ProxyRoutingEnumeratorContext context)
		{
			if (context.RemainingServerCount != 0)
			{
				int localSiteRouteCount = this.LocalSiteRouteCount;
				foreach (RoutingServerInfo serverInfo in context.PostLoadbalanceFilter(this.GetLocalSiteServersForProxyTarget(localSiteRouteCount, context), new bool?(false)))
				{
					yield return serverInfo;
				}
				if (localSiteRouteCount != this.serversByRoutes.Count && context.RemoteSiteRemainingServerCount != 0)
				{
					foreach (RoutingServerInfo serverInfo2 in context.PostLoadbalanceFilter(this.GetRemoteSiteServersForProxyTarget(localSiteRouteCount, context), new bool?(true)))
					{
						yield return serverInfo2;
					}
				}
			}
			yield break;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000690D8 File Offset: 0x000672D8
		private IEnumerable<RoutingServerInfo> GetAllSitesServersForProxyTarget(ProxyRoutingEnumeratorContext context)
		{
			if (context.RemainingServerCount != 0)
			{
				List<RoutingServerInfo> allServerList = new List<RoutingServerInfo>(this.AllServers);
				foreach (RoutingServerInfo serverInfo in RoutingUtils.RandomShuffleEnumerate<RoutingServerInfo>(allServerList))
				{
					if (context.PreLoadbalanceFilter(serverInfo))
					{
						yield return serverInfo;
					}
				}
			}
			yield break;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00069340 File Offset: 0x00067540
		private IEnumerable<RoutingServerInfo> GetLocalSiteServersForProxyTarget(int localSiteRouteCount, ProxyRoutingEnumeratorContext context)
		{
			if (localSiteRouteCount != 0 && context.RemainingServerCount != 0)
			{
				LoadBalancedCollection<RoutingServerInfo> localSiteServerList = this.serversByRoutes.Values[localSiteRouteCount - 1].LoadBalancedCollection;
				int indexForLocalServer = -1;
				if (localSiteRouteCount == 2)
				{
					indexForLocalServer = RoutingUtils.GetRandomNumber(localSiteServerList.Count + 1);
				}
				for (int i = 0; i < localSiteServerList.Count; i++)
				{
					if (i == indexForLocalServer && context.PreLoadbalanceFilter(this.LocalServer))
					{
						yield return this.LocalServer;
					}
					if (context.PreLoadbalanceFilter(localSiteServerList[i]))
					{
						yield return localSiteServerList[i];
					}
				}
				if (indexForLocalServer == localSiteServerList.Count && context.PreLoadbalanceFilter(this.LocalServer))
				{
					yield return this.LocalServer;
				}
			}
			yield break;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x000695F8 File Offset: 0x000677F8
		private IEnumerable<RoutingServerInfo> GetRemoteSiteServersForProxyTarget(int localSiteRouteCount, ProxyRoutingEnumeratorContext context)
		{
			int remoteSiteCount = this.serversByRoutes.Count - localSiteRouteCount;
			LoadBalancedCollection<RoutingServerInfo>[] loadBalancedServerLists = new LoadBalancedCollection<RoutingServerInfo>[remoteSiteCount];
			int[] serverListIndices = new int[remoteSiteCount];
			int i = 0;
			foreach (ListLoadBalancer<RoutingServerInfo> listLoadBalancer in RoutingUtils.RandomShiftEnumerate<ListLoadBalancer<RoutingServerInfo>>(this.serversByRoutes.Values, localSiteRouteCount))
			{
				LoadBalancedCollection<RoutingServerInfo>[] array = loadBalancedServerLists;
				int num;
				i = (num = i) + 1;
				array[num] = listLoadBalancer.LoadBalancedCollection;
			}
			bool haveMoreServers;
			do
			{
				haveMoreServers = false;
				for (i = 0; i < remoteSiteCount; i++)
				{
					if (loadBalancedServerLists[i] != null)
					{
						int serverIndex = -1;
						if (RoutedServerCollection.TryGetNextServerIndex(loadBalancedServerLists[i], serverListIndices[i], context, out serverIndex))
						{
							yield return loadBalancedServerLists[i][serverIndex];
						}
						if (serverIndex != -1 && serverIndex < loadBalancedServerLists[i].Count - 1)
						{
							serverListIndices[i] = serverIndex + 1;
							haveMoreServers = true;
						}
						else
						{
							loadBalancedServerLists[i] = null;
						}
					}
				}
			}
			while (haveMoreServers);
			yield break;
		}

		// Token: 0x04000C33 RID: 3123
		private static readonly RouteInfo.Comparer routeComparer = new RouteInfo.Comparer(RouteComparison.CompareNames | RouteComparison.CompareRestrictions);

		// Token: 0x04000C34 RID: 3124
		private SortedList<RouteInfo, ListLoadBalancer<RoutingServerInfo>> serversByRoutes;

		// Token: 0x04000C35 RID: 3125
		private RoutedServerSelectStrategy serverSelectStrategyForProxyTarget;
	}
}
