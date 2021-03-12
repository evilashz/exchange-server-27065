using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200020F RID: 527
	internal abstract class DeliveryGroup : RoutingNextHop
	{
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600175F RID: 5983
		public abstract IEnumerable<RoutingServerInfo> AllServersNoFallback { get; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001760 RID: 5984
		public abstract string Name { get; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001761 RID: 5985
		public abstract RouteInfo PrimaryRoute { get; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0005EF8B File Offset: 0x0005D18B
		public virtual bool IsActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0005EF8E File Offset: 0x0005D18E
		public override string GetNextHopDomain(RoutingContext context)
		{
			return this.Name;
		}

		// Token: 0x06001764 RID: 5988
		public abstract IEnumerable<RoutingServerInfo> GetServersForProxyTarget(ProxyRoutingEnumeratorContext context);

		// Token: 0x06001765 RID: 5989 RVA: 0x0005EF96 File Offset: 0x0005D196
		public virtual IEnumerable<RoutingServerInfo> GetServersForShadowTarget(ProxyRoutingEnumeratorContext context, ShadowRoutingConfiguration shadowRoutingConfig)
		{
			throw new NotSupportedException("This should not be called");
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0005EFA2 File Offset: 0x0005D1A2
		public virtual bool MayContainServersOfVersion(int majorVersion)
		{
			return true;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0005EFA5 File Offset: 0x0005D1A5
		public virtual bool MayContainServersOfVersions(IList<int> majorVersions)
		{
			return true;
		}
	}
}
