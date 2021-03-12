using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000216 RID: 534
	internal class ConnectorIndexMap
	{
		// Token: 0x06001796 RID: 6038 RVA: 0x0005F78A File Offset: 0x0005D98A
		public ConnectorIndexMap(DateTime timestamp)
		{
			this.timestamp = timestamp;
			this.typeToIndexMap = new Dictionary<string, ConnectorIndex>(StringComparer.OrdinalIgnoreCase);
			this.connectorRouteMap = new Dictionary<Guid, RouteInfo>();
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0005F7B4 File Offset: 0x0005D9B4
		public ConnectorMatchResult TryFindBestConnector(string addressType, string address, long messageSize, out ConnectorRoutingDestination connectorDestination)
		{
			connectorDestination = null;
			RoutingUtils.ThrowIfNullOrEmpty(addressType, "addressType");
			ConnectorIndex connectorIndex;
			if (!this.typeToIndexMap.TryGetValue(addressType, out connectorIndex))
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string, string>((long)this.GetHashCode(), "[{0}] Connector index not found for address type '{1}'. (Address is {2})", this.timestamp, addressType, address);
				return ConnectorMatchResult.NoAddressMatch;
			}
			return connectorIndex.TryFindBestConnector(address, messageSize, out connectorDestination);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0005F80C File Offset: 0x0005DA0C
		public bool TryGetConnectorNextHop(Guid connectorGuid, out RoutingNextHop nextHop)
		{
			nextHop = null;
			RouteInfo routeInfo;
			if (this.connectorRouteMap.TryGetValue(connectorGuid, out routeInfo))
			{
				nextHop = routeInfo.NextHop;
				return true;
			}
			return false;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0005F838 File Offset: 0x0005DA38
		public bool TryGetLocalSendConnector<ConnectorType>(Guid connectorGuid, out ConnectorType connector) where ConnectorType : MailGateway
		{
			connector = default(ConnectorType);
			RouteInfo routeInfo;
			return this.connectorRouteMap.TryGetValue(connectorGuid, out routeInfo) && ConnectorIndexMap.TryGetLocalSendConnector<ConnectorType>(routeInfo, out connector);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0005F868 File Offset: 0x0005DA68
		public IList<ConnectorType> GetLocalSendConnectors<ConnectorType>() where ConnectorType : MailGateway
		{
			IList<ConnectorType> list = new List<ConnectorType>();
			foreach (RouteInfo routeInfo in this.connectorRouteMap.Values)
			{
				ConnectorType item;
				if (ConnectorIndexMap.TryGetLocalSendConnector<ConnectorType>(routeInfo, out item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0005F8D4 File Offset: 0x0005DAD4
		public void AddConnector(ConnectorRoutingDestination connectorDestination)
		{
			foreach (AddressSpace addressSpace in connectorDestination.AddressSpaces)
			{
				this.AddConnector(addressSpace, connectorDestination);
			}
			this.AddNonAddressSpaceConnector(connectorDestination.ConnectorGuid, connectorDestination.RouteInfo);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0005F934 File Offset: 0x0005DB34
		public void AddNonAddressSpaceConnector(Guid connectorGuid, RouteInfo routeInfo)
		{
			TransportHelpers.AttemptAddToDictionary<Guid, RouteInfo>(this.connectorRouteMap, connectorGuid, routeInfo, new TransportHelpers.DiagnosticsHandler<Guid, RouteInfo>(RoutingUtils.LogErrorWhenAddToDictionaryFails<Guid, RouteInfo>));
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0005F950 File Offset: 0x0005DB50
		public bool QuickMatch(ConnectorIndexMap other)
		{
			return this.connectorRouteMap.Count == other.connectorRouteMap.Count && this.typeToIndexMap.Count == other.typeToIndexMap.Count;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0005F98D File Offset: 0x0005DB8D
		public bool FullMatch(ConnectorIndexMap other)
		{
			return RoutingUtils.MatchDictionaries<string, ConnectorIndex>(this.typeToIndexMap, other.typeToIndexMap, (ConnectorIndex index1, ConnectorIndex index2) => index1.Match(index2));
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0005F9C0 File Offset: 0x0005DBC0
		private static bool TryGetLocalSendConnector<ConnectorType>(RouteInfo routeInfo, out ConnectorType connector) where ConnectorType : MailGateway
		{
			connector = default(ConnectorType);
			if (routeInfo.DestinationProximity == Proximity.LocalServer)
			{
				ConnectorDeliveryHop connectorDeliveryHop = (ConnectorDeliveryHop)routeInfo.NextHop;
				connector = (connectorDeliveryHop.Connector as ConnectorType);
			}
			return connector != null;
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0005FA10 File Offset: 0x0005DC10
		private void AddConnector(AddressSpace addressSpace, ConnectorRoutingDestination connectorDestination)
		{
			RoutingDiag.Tracer.TraceDebug<DateTime, string, AddressSpace>((long)this.GetHashCode(), "[{0}] Indexing connector {1} for address space '{2}'", this.timestamp, connectorDestination.StringIdentity, addressSpace);
			ConnectorIndex connectorIndex;
			if (!this.typeToIndexMap.TryGetValue(addressSpace.Type, out connectorIndex))
			{
				if (addressSpace.IsSmtpType)
				{
					connectorIndex = new SmtpConnectorIndex(this.timestamp);
				}
				else if (addressSpace.IsX400Type)
				{
					connectorIndex = new X400ConnectorIndex(this.timestamp);
				}
				else
				{
					connectorIndex = new GenericConnectorIndex(addressSpace.Type, this.timestamp);
				}
				this.typeToIndexMap.Add(addressSpace.Type, connectorIndex);
				RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] Created connector index for address type {1}", this.timestamp, addressSpace.Type);
			}
			connectorIndex.AddConnector(addressSpace, connectorDestination);
			RoutingDiag.Tracer.TraceDebug<DateTime, string, AddressSpace>((long)this.GetHashCode(), "[{0}] Indexed connector {1} for address space '{2}'", this.timestamp, connectorDestination.StringIdentity, addressSpace);
		}

		// Token: 0x04000B90 RID: 2960
		private readonly DateTime timestamp;

		// Token: 0x04000B91 RID: 2961
		private Dictionary<string, ConnectorIndex> typeToIndexMap;

		// Token: 0x04000B92 RID: 2962
		private Dictionary<Guid, RouteInfo> connectorRouteMap;
	}
}
