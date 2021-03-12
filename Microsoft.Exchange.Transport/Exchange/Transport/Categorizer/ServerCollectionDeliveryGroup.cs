using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200021C RID: 540
	internal abstract class ServerCollectionDeliveryGroup : DeliveryGroup
	{
		// Token: 0x060017C8 RID: 6088 RVA: 0x00060939 File Offset: 0x0005EB39
		protected ServerCollectionDeliveryGroup(RoutedServerCollection routedServerCollection)
		{
			RoutingUtils.ThrowIfNull(routedServerCollection, "routedServerCollection");
			this.routedServerCollection = routedServerCollection;
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x00060953 File Offset: 0x0005EB53
		public override IEnumerable<RoutingServerInfo> AllServersNoFallback
		{
			get
			{
				return this.routedServerCollection.AllServers;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x00060960 File Offset: 0x0005EB60
		public override RouteInfo PrimaryRoute
		{
			get
			{
				return this.routedServerCollection.PrimaryRoute;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x0006096D File Offset: 0x0005EB6D
		protected RoutedServerCollection RoutedServerCollection
		{
			get
			{
				return this.routedServerCollection;
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00060975 File Offset: 0x0005EB75
		public override IEnumerable<INextHopServer> GetLoadBalancedNextHopServers(string nextHopDomain)
		{
			return this.routedServerCollection.AllServersLoadBalanced;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00060982 File Offset: 0x0005EB82
		public override IEnumerable<RoutingServerInfo> GetServersForProxyTarget(ProxyRoutingEnumeratorContext context)
		{
			return this.routedServerCollection.GetServersForProxyTarget(context);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00060990 File Offset: 0x0005EB90
		public override IEnumerable<RoutingServerInfo> GetServersForShadowTarget(ProxyRoutingEnumeratorContext context, ShadowRoutingConfiguration shadowRoutingConfig)
		{
			return this.routedServerCollection.GetServersForShadowTarget(context, shadowRoutingConfig);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0006099F File Offset: 0x0005EB9F
		public override bool Match(RoutingNextHop other)
		{
			return !(base.GetType() != other.GetType()) && this.MatchServers((ServerCollectionDeliveryGroup)other);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x000609C2 File Offset: 0x0005EBC2
		protected bool MatchServers(ServerCollectionDeliveryGroup other)
		{
			return this.routedServerCollection.MatchServers(other.routedServerCollection);
		}

		// Token: 0x04000B9F RID: 2975
		private RoutedServerCollection routedServerCollection;
	}
}
