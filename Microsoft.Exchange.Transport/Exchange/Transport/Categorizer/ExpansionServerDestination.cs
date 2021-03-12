using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000233 RID: 563
	internal class ExpansionServerDestination : RoutingDestination
	{
		// Token: 0x060018DF RID: 6367 RVA: 0x00064F68 File Offset: 0x00063168
		private ExpansionServerDestination(ADObjectId serverId, RouteInfo routeInfo) : base(routeInfo)
		{
			this.serverId = serverId;
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x00064F78 File Offset: 0x00063178
		public override MailRecipientType DestinationType
		{
			get
			{
				return MailRecipientType.DistributionGroup;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x00064F7B File Offset: 0x0006317B
		public override string StringIdentity
		{
			get
			{
				return this.serverId.DistinguishedName;
			}
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x00064F88 File Offset: 0x00063188
		public static RoutingDestination GetRoutingDestination(ADObjectId serverId, RoutingContext context)
		{
			if (serverId == null)
			{
				throw new ArgumentNullException("serverId", "Expansion Server cannot be null for Distribution Group recipients that reach Routing");
			}
			RoutingUtils.ThrowIfNullOrEmpty(serverId.DistinguishedName, "serverId.DistinguishedName");
			RoutingServerInfo routingServerInfo;
			RouteInfo routeInfo;
			if (!context.RoutingTables.ServerMap.TryGetServerRouteByDN(serverId.DistinguishedName, out routingServerInfo, out routeInfo))
			{
				if (routingServerInfo == null)
				{
					RoutingDiag.Tracer.TraceError<DateTime, string, string>(0L, "[{0}] No server information for DG expansion server <{1}> for recipient {2}; the recipient will be placed in Unreachable queue", context.Timestamp, serverId.DistinguishedName, context.CurrentRecipient.Email.ToString());
				}
				else
				{
					RoutingDiag.Tracer.TraceError<DateTime, string, string>(0L, "[{0}] No route to DG expansion server <{1}> for recipient {2}; the recipient will be placed in Unreachable queue", context.Timestamp, serverId.DistinguishedName, context.CurrentRecipient.Email.ToString());
				}
				return ExpansionServerDestination.NoRouteToServer;
			}
			if (routingServerInfo.IsExchange2007OrLater && !routingServerInfo.IsHubTransportServer)
			{
				RoutingDiag.Tracer.TraceError<DateTime, string, string>(0L, "[{0}] DG expansion server <{1}> for recipient {2} is not a Hub Transport server; the recipient will be placed in Unreachable queue", context.Timestamp, serverId.DistinguishedName, context.CurrentRecipient.Email.ToString());
				return ExpansionServerDestination.NonHubExpansionServer;
			}
			if (routeInfo.DestinationProximity == Proximity.LocalServer)
			{
				throw new InvalidOperationException("DG with local expansion server reached routing: " + context.CurrentRecipient.Email.ToString());
			}
			if (!routeInfo.HasMandatoryTopologyHop && (routeInfo.DestinationProximity != Proximity.RemoteADSite || context.Core.Settings.DestinationRoutingToRemoteSitesEnabled))
			{
				routeInfo = routeInfo.ReplaceNextHop(new RedirectGroup(new RoutedServerCollection(routeInfo, routingServerInfo, context.Core)), context.CurrentRecipient.Email.ToString());
			}
			return new ExpansionServerDestination(serverId, routeInfo);
		}

		// Token: 0x04000BF8 RID: 3064
		private static readonly UnroutableDestination NoRouteToServer = new UnroutableDestination(MailRecipientType.DistributionGroup, "<No Route To Expansion Server>", UnreachableNextHop.NoRouteToServer);

		// Token: 0x04000BF9 RID: 3065
		private static readonly UnroutableDestination NonHubExpansionServer = new UnroutableDestination(MailRecipientType.DistributionGroup, "<Non-Hub Expansion Server>", UnreachableNextHop.NonHubExpansionServer);

		// Token: 0x04000BFA RID: 3066
		private ADObjectId serverId;
	}
}
