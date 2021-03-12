using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200021D RID: 541
	internal class ConnectorDeliveryGroup : ServerCollectionDeliveryGroup
	{
		// Token: 0x060017D1 RID: 6097 RVA: 0x000609D8 File Offset: 0x0005EBD8
		public ConnectorDeliveryGroup(ADObjectId connectorId, RoutedServerCollection routedSourceServers, bool isEdgeConnector) : base(routedSourceServers)
		{
			RoutingUtils.ThrowIfNullOrEmpty(connectorId, "connectorId");
			if (routedSourceServers.ClosestProximity != Proximity.LocalADSite && routedSourceServers.ClosestProximity != Proximity.RemoteADSite)
			{
				throw new ArgumentOutOfRangeException("routedSourceServers.ClosestProximity", routedSourceServers.ClosestProximity, "routedSourceServers.ClosestProximity must be Local or Remote AD site");
			}
			if (isEdgeConnector && routedSourceServers.ClosestProximity != Proximity.LocalADSite)
			{
				throw new ArgumentException("This delivery group cannot be used for Edge connectors in remote AD sites", "isEdgeConnector");
			}
			this.connectorId = connectorId;
			this.deliveryType = (isEdgeConnector ? DeliveryType.SmtpRelayWithinAdSiteToEdge : DeliveryType.SmtpRelayToConnectorSourceServers);
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x00060A55 File Offset: 0x0005EC55
		public override string Name
		{
			get
			{
				return this.connectorId.Name;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x00060A62 File Offset: 0x0005EC62
		public override DeliveryType DeliveryType
		{
			get
			{
				return this.deliveryType;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x00060A6A File Offset: 0x0005EC6A
		public override Guid NextHopGuid
		{
			get
			{
				return this.connectorId.ObjectGuid;
			}
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00060A78 File Offset: 0x0005EC78
		public override bool Match(RoutingNextHop other)
		{
			ConnectorDeliveryGroup connectorDeliveryGroup = other as ConnectorDeliveryGroup;
			return connectorDeliveryGroup != null && !(this.connectorId.ObjectGuid != connectorDeliveryGroup.connectorId.ObjectGuid) && this.deliveryType == connectorDeliveryGroup.deliveryType && base.MatchServers(connectorDeliveryGroup);
		}

		// Token: 0x04000BA0 RID: 2976
		private ADObjectId connectorId;

		// Token: 0x04000BA1 RID: 2977
		private DeliveryType deliveryType;
	}
}
