using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200021E RID: 542
	internal abstract class MailboxDeliveryGroupBase : ServerCollectionDeliveryGroup
	{
		// Token: 0x060017D6 RID: 6102 RVA: 0x00060AC5 File Offset: 0x0005ECC5
		protected MailboxDeliveryGroupBase(RoutedServerCollection routedServerCollection, DeliveryType deliveryType, string nextHopDomain, Guid nextHopGuid, int version, bool isLocalDeliveryGroup) : base(routedServerCollection)
		{
			this.key = new NextHopSolutionKey(deliveryType, nextHopDomain, nextHopGuid, isLocalDeliveryGroup);
			this.version = version;
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x00060AE7 File Offset: 0x0005ECE7
		public override string Name
		{
			get
			{
				return this.key.NextHopDomain;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x00060AF4 File Offset: 0x0005ECF4
		public override bool IsActive
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

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x00060B1C File Offset: 0x0005ED1C
		public override DeliveryType DeliveryType
		{
			get
			{
				return this.key.NextHopType.DeliveryType;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x00060B3C File Offset: 0x0005ED3C
		public override Guid NextHopGuid
		{
			get
			{
				return this.key.NextHopConnector;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x00060B49 File Offset: 0x0005ED49
		public bool IsLocalDeliveryGroup
		{
			get
			{
				return this.key.IsLocalDeliveryGroupRelay;
			}
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00060B58 File Offset: 0x0005ED58
		public virtual bool TryGetDatabaseRouteInfo(MiniDatabase database, RoutingServerInfo owningServerInfo, RouteCalculatorContext context, out RouteInfo databaseRouteInfo)
		{
			databaseRouteInfo = null;
			if (this.databaseRouteInfo == null)
			{
				RouteInfo primaryRoute = base.RoutedServerCollection.PrimaryRoute;
				if (primaryRoute.DestinationProximity == Proximity.RemoteADSite && !context.Core.Settings.DestinationRoutingToRemoteSitesEnabled)
				{
					this.databaseRouteInfo = primaryRoute;
				}
				else
				{
					this.databaseRouteInfo = primaryRoute.ReplaceNextHop(this, this.key.NextHopDomain);
				}
			}
			databaseRouteInfo = this.databaseRouteInfo;
			return true;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00060BC3 File Offset: 0x0005EDC3
		public override bool MayContainServersOfVersions(IList<int> majorVersions)
		{
			return majorVersions.Contains(this.version);
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00060BD1 File Offset: 0x0005EDD1
		public override bool MayContainServersOfVersion(int majorVersion)
		{
			return this.version == majorVersion;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00060BDC File Offset: 0x0005EDDC
		public void UpdateIfGroupIsActiveAndRemoveInactiveServers(RoutingContextCore context)
		{
			this.isActive = new bool?(base.RoutedServerCollection.GetStateAndRemoveInactiveServersIfStateIsActive(context));
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00060BF5 File Offset: 0x0005EDF5
		protected override NextHopSolutionKey GetNextHopSolutionKey(RoutingContext context)
		{
			return this.key;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00060BFD File Offset: 0x0005EDFD
		protected void AddServerInternal(RouteInfo routeInfo, RoutingServerInfo serverInfo, RoutingContextCore contextCore)
		{
			if (this.databaseRouteInfo != null)
			{
				throw new InvalidOperationException("Cannot add servers after database route is calculated");
			}
			base.RoutedServerCollection.AddServerForRoute(routeInfo, serverInfo, contextCore);
		}

		// Token: 0x04000BA2 RID: 2978
		private readonly int version;

		// Token: 0x04000BA3 RID: 2979
		private NextHopSolutionKey key;

		// Token: 0x04000BA4 RID: 2980
		private RouteInfo databaseRouteInfo;

		// Token: 0x04000BA5 RID: 2981
		private bool? isActive;
	}
}
