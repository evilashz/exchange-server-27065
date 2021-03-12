using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000213 RID: 531
	internal abstract class ConnectorIndex
	{
		// Token: 0x06001789 RID: 6025 RVA: 0x0005F4F5 File Offset: 0x0005D6F5
		public ConnectorIndex(DateTime timestamp)
		{
			this.Timestamp = timestamp;
		}

		// Token: 0x0600178A RID: 6026
		public abstract ConnectorMatchResult TryFindBestConnector(string address, long messageSize, out ConnectorRoutingDestination connectorDestination);

		// Token: 0x0600178B RID: 6027
		public abstract void AddConnector(AddressSpace addressSpace, ConnectorRoutingDestination connectorDestination);

		// Token: 0x0600178C RID: 6028
		public abstract bool Match(ConnectorIndex other);

		// Token: 0x04000B86 RID: 2950
		protected readonly DateTime Timestamp;

		// Token: 0x02000214 RID: 532
		protected sealed class ConnectorWithCost
		{
			// Token: 0x0600178D RID: 6029 RVA: 0x0005F504 File Offset: 0x0005D704
			public ConnectorWithCost(ConnectorRoutingDestination connectorDestination, AddressSpace addressSpace)
			{
				RoutingUtils.ThrowIfNull(connectorDestination, "connectorDestination");
				RoutingUtils.ThrowIfNull(addressSpace, "addressSpace");
				this.ConnectorDestination = connectorDestination;
				this.AddressSpace = addressSpace;
			}

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x0600178E RID: 6030 RVA: 0x0005F530 File Offset: 0x0005D730
			public int Cost
			{
				get
				{
					return this.AddressSpace.Cost;
				}
			}

			// Token: 0x0600178F RID: 6031 RVA: 0x0005F540 File Offset: 0x0005D740
			public static void InsertConnector(ConnectorRoutingDestination connectorDestination, AddressSpace addressSpace, IList<ConnectorIndex.ConnectorWithCost> connectorList)
			{
				ConnectorIndex.ConnectorWithCost connectorWithCost = new ConnectorIndex.ConnectorWithCost(connectorDestination, addressSpace);
				int num = 0;
				while (num < connectorList.Count && connectorWithCost.CompareTo(connectorList[num]) >= 0)
				{
					num++;
				}
				connectorList.Insert(num, connectorWithCost);
			}

			// Token: 0x06001790 RID: 6032 RVA: 0x0005F580 File Offset: 0x0005D780
			public static int GetConnectorForMessageSize(IList<ConnectorIndex.ConnectorWithCost> connectorList, long messageSize, DateTime timestamp, string addressType, string address)
			{
				for (int i = 0; i < connectorList.Count; i++)
				{
					long maxMessageSize = connectorList[i].ConnectorDestination.MaxMessageSize;
					if (messageSize <= maxMessageSize)
					{
						return i;
					}
					RoutingDiag.Tracer.TraceDebug(0L, "[{0}] Skipped connector {1} for address '{2}:{3}' because the message size {4} is over the limit of {5}", new object[]
					{
						timestamp,
						connectorList[i].ConnectorDestination.StringIdentity,
						addressType,
						address,
						messageSize,
						maxMessageSize
					});
				}
				return -1;
			}

			// Token: 0x06001791 RID: 6033 RVA: 0x0005F608 File Offset: 0x0005D808
			public static ConnectorMatchResult TryGetConnectorForMessageSize(IList<ConnectorIndex.ConnectorWithCost> connectorList, long messageSize, DateTime timestamp, string addressType, string address, out ConnectorRoutingDestination matchingConnector)
			{
				matchingConnector = null;
				if (connectorList == null)
				{
					return ConnectorMatchResult.NoAddressMatch;
				}
				int connectorForMessageSize = ConnectorIndex.ConnectorWithCost.GetConnectorForMessageSize(connectorList, messageSize, timestamp, addressType, address);
				if (connectorForMessageSize < 0)
				{
					return ConnectorMatchResult.MaxMessageSizeExceeded;
				}
				matchingConnector = connectorList[connectorForMessageSize].ConnectorDestination;
				return ConnectorMatchResult.Success;
			}

			// Token: 0x06001792 RID: 6034 RVA: 0x0005F649 File Offset: 0x0005D849
			public static bool MatchLists(List<ConnectorIndex.ConnectorWithCost> l1, List<ConnectorIndex.ConnectorWithCost> l2)
			{
				return RoutingUtils.MatchOrderedLists<ConnectorIndex.ConnectorWithCost>(l1, l2, (ConnectorIndex.ConnectorWithCost c1, ConnectorIndex.ConnectorWithCost c2) => c1.Match(c2));
			}

			// Token: 0x06001793 RID: 6035 RVA: 0x0005F670 File Offset: 0x0005D870
			public int CompareTo(ConnectorIndex.ConnectorWithCost other)
			{
				RouteInfo routeInfo = this.ConnectorDestination.RouteInfo;
				RouteInfo routeInfo2 = other.ConnectorDestination.RouteInfo;
				Proximity destinationProximity = routeInfo.DestinationProximity;
				Proximity destinationProximity2 = routeInfo2.DestinationProximity;
				int num = this.Cost;
				int num2 = other.Cost;
				if (destinationProximity != destinationProximity2)
				{
					if (destinationProximity == Proximity.RemoteRoutingGroup)
					{
						return 1;
					}
					if (destinationProximity2 == Proximity.RemoteRoutingGroup)
					{
						return -1;
					}
				}
				else if (destinationProximity == Proximity.RemoteRoutingGroup)
				{
					num += routeInfo.RGRelayCost;
					num2 += routeInfo2.RGRelayCost;
					if (num != num2)
					{
						return num - num2;
					}
					int num3 = routeInfo.CompareTo(routeInfo2, RouteComparison.IgnoreRGCosts);
					if (num3 != 0)
					{
						return num3;
					}
					return RoutingUtils.CompareNames(this.ConnectorDestination.ConnectorName, other.ConnectorDestination.ConnectorName);
				}
				if (destinationProximity == Proximity.RemoteADSite)
				{
					num += routeInfo.SiteRelayCost;
				}
				if (destinationProximity2 == Proximity.RemoteADSite)
				{
					num2 += routeInfo2.SiteRelayCost;
				}
				if (num != num2)
				{
					return num - num2;
				}
				if (destinationProximity == destinationProximity2)
				{
					return RoutingUtils.CompareNames(this.ConnectorDestination.ConnectorName, other.ConnectorDestination.ConnectorName);
				}
				if (destinationProximity >= destinationProximity2)
				{
					return 1;
				}
				return -1;
			}

			// Token: 0x06001794 RID: 6036 RVA: 0x0005F767 File Offset: 0x0005D967
			private bool Match(ConnectorIndex.ConnectorWithCost other)
			{
				return this.Cost == other.Cost && this.ConnectorDestination.Match(other.ConnectorDestination);
			}

			// Token: 0x04000B87 RID: 2951
			public readonly ConnectorRoutingDestination ConnectorDestination;

			// Token: 0x04000B88 RID: 2952
			public readonly AddressSpace AddressSpace;
		}
	}
}
