using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000251 RID: 593
	internal class RouteInfo
	{
		// Token: 0x060019B7 RID: 6583 RVA: 0x00069630 File Offset: 0x00067830
		private RouteInfo(string destinationName, RoutingNextHop nextHop, long maxMessageSize, Proximity siteBasedProximity, int siteRelayCost, int routingGroupRelayCost)
		{
			RoutingUtils.ThrowIfNullOrEmpty(destinationName, "destinationName");
			if (maxMessageSize < 0L)
			{
				throw new ArgumentOutOfRangeException("maxMessageSize", maxMessageSize, "maxMessageSize must not be a negative value");
			}
			this.DestinationName = destinationName;
			this.NextHop = nextHop;
			this.MaxMessageSize = maxMessageSize;
			this.siteBasedProximity = siteBasedProximity;
			this.SiteRelayCost = siteRelayCost;
			this.RGRelayCost = routingGroupRelayCost;
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00069696 File Offset: 0x00067896
		public Proximity DestinationProximity
		{
			get
			{
				if (this.RGRelayCost != -1)
				{
					return Proximity.RemoteRoutingGroup;
				}
				return this.siteBasedProximity;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x000696A9 File Offset: 0x000678A9
		public bool HasMandatoryTopologyHop
		{
			get
			{
				return this.NextHop != null && (this.RGRelayCost != -1 || this.NextHop.IsMandatoryTopologyHop);
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x000696CC File Offset: 0x000678CC
		public bool InLocalADSite
		{
			get
			{
				Proximity destinationProximity = this.DestinationProximity;
				return destinationProximity == Proximity.LocalADSite || destinationProximity == Proximity.LocalServer;
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000696EA File Offset: 0x000678EA
		public static RouteInfo CreateForLocalServer(string destinationName, RoutingNextHop nextHop)
		{
			return new RouteInfo(destinationName, nextHop, long.MaxValue, Proximity.LocalServer, -1, -1);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000696FF File Offset: 0x000678FF
		public static RouteInfo CreateForLocalSite(string destinationName, RoutingNextHop nextHop)
		{
			return new RouteInfo(destinationName, nextHop, long.MaxValue, Proximity.LocalADSite, -1, -1);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00069714 File Offset: 0x00067914
		public static RouteInfo CreateForRemoteSite(string destinationName, RoutingNextHop nextHop, long maxMessageSize, int cost)
		{
			RoutingUtils.ThrowIfNull(nextHop, "nextHop");
			if (cost == -1)
			{
				throw new ArgumentOutOfRangeException("cost", "Route cost must be provided");
			}
			return new RouteInfo(destinationName, nextHop, maxMessageSize, Proximity.RemoteADSite, cost, -1);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00069740 File Offset: 0x00067940
		public static RouteInfo CreateForRemoteRG(string destinationName, long maxMessageSize, int routingGroupRelayCost, RouteInfo firstRGConnectorRouteInfo)
		{
			RoutingUtils.ThrowIfNull(firstRGConnectorRouteInfo, "firstRGConnectorRouteInfo");
			if (routingGroupRelayCost == -1)
			{
				throw new ArgumentOutOfRangeException("routingGroupRelayCost", "routingGroupRelayCost must be provided");
			}
			if (firstRGConnectorRouteInfo.DestinationProximity == Proximity.RemoteRoutingGroup)
			{
				throw new ArgumentOutOfRangeException("firstRGConnectorRouteInfo.DestinationProximity", "First RG connector for any route must be in the local RG");
			}
			if (firstRGConnectorRouteInfo.NextHop == null)
			{
				throw new ArgumentOutOfRangeException("firstRGConnectorRouteInfo.NextHop", "First RG connector next hop must not be null");
			}
			return new RouteInfo(destinationName, firstRGConnectorRouteInfo.NextHop, maxMessageSize, firstRGConnectorRouteInfo.DestinationProximity, firstRGConnectorRouteInfo.SiteRelayCost, routingGroupRelayCost);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000697B7 File Offset: 0x000679B7
		public static RouteInfo CreateForUnroutableDestination(string destinationName, RoutingNextHop nextHop)
		{
			RoutingUtils.ThrowIfNull(nextHop, "nextHop");
			return new RouteInfo(destinationName, nextHop, long.MaxValue, Proximity.None, -1, -1);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000697DC File Offset: 0x000679DC
		public int CompareTo(RouteInfo other, RouteComparison options)
		{
			RoutingUtils.ThrowIfNull(other, "other");
			if (object.ReferenceEquals(this, other))
			{
				return 0;
			}
			if (this.DestinationProximity != other.DestinationProximity)
			{
				if (this.DestinationProximity >= other.DestinationProximity)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if ((options & RouteComparison.IgnoreRGCosts) == RouteComparison.None && this.RGRelayCost != other.RGRelayCost)
				{
					return this.RGRelayCost - other.RGRelayCost;
				}
				if (this.siteBasedProximity != other.siteBasedProximity)
				{
					if (this.siteBasedProximity >= other.siteBasedProximity)
					{
						return 1;
					}
					return -1;
				}
				else
				{
					if (this.SiteRelayCost != other.SiteRelayCost)
					{
						return this.SiteRelayCost - other.SiteRelayCost;
					}
					if ((options & RouteComparison.CompareRestrictions) != RouteComparison.None && this.MaxMessageSize != other.MaxMessageSize)
					{
						if (other.MaxMessageSize >= this.MaxMessageSize)
						{
							return 1;
						}
						return -1;
					}
					else
					{
						if ((options & RouteComparison.CompareNames) != RouteComparison.None)
						{
							return RoutingUtils.CompareNames(this.DestinationName, other.DestinationName);
						}
						return 0;
					}
				}
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000698BC File Offset: 0x00067ABC
		public bool Match(RouteInfo other, NextHopMatch nextHopMatch)
		{
			if (this.siteBasedProximity != other.siteBasedProximity || this.SiteRelayCost != other.SiteRelayCost || this.RGRelayCost != other.RGRelayCost || this.MaxMessageSize != other.MaxMessageSize || !RoutingUtils.MatchStrings(this.DestinationName, other.DestinationName) || !RoutingUtils.NullMatch(this.NextHop, other.NextHop))
			{
				return false;
			}
			if (this.NextHop == null)
			{
				return true;
			}
			switch (nextHopMatch)
			{
			case NextHopMatch.Full:
				return this.NextHop.Match(other.NextHop);
			case NextHopMatch.GuidOnly:
				return this.NextHop.NextHopGuid == other.NextHop.NextHopGuid;
			default:
				throw new ArgumentOutOfRangeException("nextHopMatch", nextHopMatch, "Unexpected nextHopMatch: " + nextHopMatch);
			}
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00069994 File Offset: 0x00067B94
		public RouteInfo ReplaceNextHop(RoutingNextHop newNextHop, string newName)
		{
			RoutingUtils.ThrowIfNull(newNextHop, "newNextHop");
			RoutingUtils.ThrowIfNullOrEmpty(newName, "newName");
			if (this.HasMandatoryTopologyHop)
			{
				return this;
			}
			return new RouteInfo(newName, newNextHop, this.MaxMessageSize, this.siteBasedProximity, this.SiteRelayCost, this.RGRelayCost);
		}

		// Token: 0x04000C45 RID: 3141
		public const int NoCost = -1;

		// Token: 0x04000C46 RID: 3142
		public static readonly RouteInfo LocalServerRoute = RouteInfo.CreateForLocalServer("<Local Server>", null);

		// Token: 0x04000C47 RID: 3143
		public static readonly RouteInfo LocalSiteRoute = RouteInfo.CreateForLocalSite("<Local AD Site>", null);

		// Token: 0x04000C48 RID: 3144
		public readonly string DestinationName;

		// Token: 0x04000C49 RID: 3145
		public readonly RoutingNextHop NextHop;

		// Token: 0x04000C4A RID: 3146
		public readonly long MaxMessageSize;

		// Token: 0x04000C4B RID: 3147
		public readonly int SiteRelayCost;

		// Token: 0x04000C4C RID: 3148
		public readonly int RGRelayCost;

		// Token: 0x04000C4D RID: 3149
		private readonly Proximity siteBasedProximity;

		// Token: 0x02000252 RID: 594
		public class Comparer : IComparer<RouteInfo>
		{
			// Token: 0x060019C4 RID: 6596 RVA: 0x00069A02 File Offset: 0x00067C02
			public Comparer(RouteComparison options)
			{
				this.options = options;
			}

			// Token: 0x060019C5 RID: 6597 RVA: 0x00069A11 File Offset: 0x00067C11
			public int Compare(RouteInfo x, RouteInfo y)
			{
				RoutingUtils.ThrowIfNull(x, "x");
				return x.CompareTo(y, this.options);
			}

			// Token: 0x04000C4E RID: 3150
			private RouteComparison options;
		}
	}
}
