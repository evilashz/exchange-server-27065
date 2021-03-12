using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000249 RID: 585
	internal class RedirectDestination : RoutingDestination
	{
		// Token: 0x0600198A RID: 6538 RVA: 0x00067BFC File Offset: 0x00065DFC
		private RedirectDestination(RouteInfo primaryRoute, RoutedServerCollection routedServerCollection) : base(primaryRoute)
		{
			RoutingUtils.ThrowIfNull(routedServerCollection, "routedServerCollection");
			this.redirectGroup = new RedirectGroup(routedServerCollection);
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			string format = "Redirect={0}";
			object[] array = new object[1];
			array[0] = string.Join(";", from server in routedServerCollection.AllServers
			select server.Fqdn);
			this.stringIdentity = string.Format(invariantCulture, format, array);
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x00067C79 File Offset: 0x00065E79
		public override MailRecipientType DestinationType
		{
			get
			{
				return MailRecipientType.DistributionGroup;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x00067C7C File Offset: 0x00065E7C
		public override string StringIdentity
		{
			get
			{
				return this.stringIdentity;
			}
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00067C84 File Offset: 0x00065E84
		public static RoutingDestination GetRoutingDestination(List<string> targetHostFqdns, RoutingContext context)
		{
			RoutingUtils.ThrowIfNull(targetHostFqdns, "targetHostFqdns");
			RoutedServerCollection routedServerCollection;
			List<string> list;
			List<RoutingServerInfo> list2;
			bool flag = context.RoutingTables.ServerMap.TryCreateRoutedServerCollection(targetHostFqdns, context.Core, out routedServerCollection, out list, out list2);
			if (list != null && list.Count > 0)
			{
				RoutingDiag.Tracer.TraceWarning<DateTime, string, string>(0L, "[{0}] Unknown servers specified as redirect hosts <{1}> for recipient {2}", context.Timestamp, string.Join(", ", targetHostFqdns), context.CurrentRecipient.Email.ToString());
			}
			if (list2 != null && list2.Count > 0)
			{
				RoutingDiag.Tracer.TraceWarning<DateTime, string, string>(0L, "[{0}] Unreachable servers specified as redirect hosts <{1}> for recipient {2}", context.Timestamp, string.Join<RoutingServerInfo>(", ", list2), context.CurrentRecipient.Email.ToString());
			}
			if (flag)
			{
				return new RedirectDestination(routedServerCollection.PrimaryRoute, routedServerCollection);
			}
			RoutingDiag.Tracer.TraceError<DateTime, string, string>(0L, "[{0}] No server information for redirect hosts <{1}> for recipient {2}; the message will get into the unreachable queue.", context.Timestamp, string.Join(", ", targetHostFqdns), context.CurrentRecipient.Email.ToString());
			if (list2 == null || list2.Count <= 0)
			{
				return RedirectDestination.UnknownServer;
			}
			return RedirectDestination.NoRouteToServer;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00067DAD File Offset: 0x00065FAD
		public override RoutingNextHop GetNextHop(RoutingContext context)
		{
			return this.redirectGroup;
		}

		// Token: 0x04000C26 RID: 3110
		private static readonly UnroutableDestination NoRouteToServer = new UnroutableDestination(MailRecipientType.DistributionGroup, "<No Route to Redirection Server>", UnreachableNextHop.NoRouteToServer);

		// Token: 0x04000C27 RID: 3111
		private static readonly UnroutableDestination UnknownServer = new UnroutableDestination(MailRecipientType.DistributionGroup, "<Unknown Redirection Server>", UnreachableNextHop.NonHubExpansionServer);

		// Token: 0x04000C28 RID: 3112
		private readonly RedirectGroup redirectGroup;

		// Token: 0x04000C29 RID: 3113
		private readonly string stringIdentity;
	}
}
