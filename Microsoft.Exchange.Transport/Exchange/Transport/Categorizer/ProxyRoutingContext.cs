using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000245 RID: 581
	internal class ProxyRoutingContext
	{
		// Token: 0x06001965 RID: 6501 RVA: 0x00066AB9 File Offset: 0x00064CB9
		public ProxyRoutingContext(RoutingTables routingTables, RoutingContextCore contextCore)
		{
			RoutingUtils.ThrowIfNull(routingTables, "routingTables");
			RoutingUtils.ThrowIfNull(contextCore, "contextCore");
			this.contextCore = contextCore;
			this.routingTables = routingTables;
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x00066AE5 File Offset: 0x00064CE5
		public RoutingContextCore Core
		{
			get
			{
				return this.contextCore;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00066AED File Offset: 0x00064CED
		public virtual int MaxRemoteSiteHubCount
		{
			get
			{
				if (!this.routingTables.ServerMap.LocalHubSiteEnabled)
				{
					return this.contextCore.Settings.ProxyRoutingMaxRemoteSiteHubCount;
				}
				return 0;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x00066B13 File Offset: 0x00064D13
		public virtual int MaxLocalSiteHubCount
		{
			get
			{
				return this.MaxTotalHubCount;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x00066B1B File Offset: 0x00064D1B
		public virtual int MaxTotalHubCount
		{
			get
			{
				return this.contextCore.Settings.ProxyRoutingMaxTotalHubCount;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x00066B2D File Offset: 0x00064D2D
		public RoutingTables RoutingTables
		{
			get
			{
				return this.routingTables;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x00066B35 File Offset: 0x00064D35
		public DateTime Timestamp
		{
			get
			{
				return this.routingTables.WhenCreated;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x00066B42 File Offset: 0x00064D42
		public bool XSiteRoutingEnabled
		{
			get
			{
				return this.MaxRemoteSiteHubCount > 0;
			}
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00066B4D File Offset: 0x00064D4D
		public bool VerifySiteRestriction(RoutingServerInfo serverInfo)
		{
			return this.XSiteRoutingEnabled || this.RoutingTables.ServerMap.IsInLocalSite(serverInfo);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00066B6A File Offset: 0x00064D6A
		public bool VerifyVersionRestriction(RoutingServerInfo serverInfo)
		{
			return this.RoutingTables.ServerMap.LocalServerVersion == serverInfo.MajorVersion || this.contextCore.ProxyRoutingAllowedTargetVersions.Contains(serverInfo.MajorVersion);
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00066B9C File Offset: 0x00064D9C
		public bool VerifyRestrictions(RoutingServerInfo serverInfo)
		{
			return this.VerifyVersionRestriction(serverInfo) && this.VerifySiteRestriction(serverInfo);
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00066BB0 File Offset: 0x00064DB0
		public virtual IEnumerable<RoutingServerInfo> GetDeliveryGroupServers(DeliveryGroup deliveryGroup, ProxyRoutingEnumeratorContext enumeratorContext)
		{
			RoutingUtils.ThrowIfNull(deliveryGroup, "deliveryGroup");
			RoutingUtils.ThrowIfNull(enumeratorContext, "enumeratorContext");
			return deliveryGroup.GetServersForProxyTarget(enumeratorContext);
		}

		// Token: 0x04000C19 RID: 3097
		private RoutingContextCore contextCore;

		// Token: 0x04000C1A RID: 3098
		private RoutingTables routingTables;
	}
}
