using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200024A RID: 586
	internal class RedirectGroup : ServerCollectionDeliveryGroup
	{
		// Token: 0x06001991 RID: 6545 RVA: 0x00067DE1 File Offset: 0x00065FE1
		public RedirectGroup(RoutedServerCollection routedServerCollection) : base(routedServerCollection)
		{
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x00067DEA File Offset: 0x00065FEA
		public override DeliveryType DeliveryType
		{
			get
			{
				return DeliveryType.SmtpRelayToServers;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x00067DEE File Offset: 0x00065FEE
		public override Guid NextHopGuid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x00067DF5 File Offset: 0x00065FF5
		public override string Name
		{
			get
			{
				return "RedirectGroup";
			}
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00067E04 File Offset: 0x00066004
		public static bool TryGetNextHop(NextHopSolutionKey nextHopKey, RoutingTables routingTables, RoutingContextCore contextCore, out RoutingNextHop nextHop)
		{
			nextHop = null;
			RoutedServerCollection routedServerCollection;
			List<string> list;
			List<RoutingServerInfo> list2;
			if (!routingTables.ServerMap.TryCreateRoutedServerCollection(nextHopKey.NextHopDomain.Split(new char[]
			{
				';'
			}), contextCore, out routedServerCollection, out list, out list2))
			{
				RoutingDiag.Tracer.TraceError<DateTime, NextHopSolutionKey>(0L, "[{0}] No server information for next hop key <{1}>", routingTables.WhenCreated, nextHopKey);
				return false;
			}
			if (!routedServerCollection.AllServers.All((RoutingServerInfo server) => server.IsHubTransportServer))
			{
				RoutingDiag.Tracer.TraceError<DateTime, NextHopSolutionKey>(0L, "[{0}] Target server is not a Hub Transport server for next hop key <{1}>", routingTables.WhenCreated, nextHopKey);
				return false;
			}
			nextHop = new RedirectGroup(routedServerCollection);
			return true;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00067EB0 File Offset: 0x000660B0
		public override string GetNextHopDomain(RoutingContext context)
		{
			return string.Join(";", from server in base.RoutedServerCollection.AllServers
			select server.Fqdn);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00067EE9 File Offset: 0x000660E9
		public override bool Match(RoutingNextHop other)
		{
			throw new NotSupportedException("RedirectGroup is an on-demand-created delivery group that is not expected to be in Routing Tables; hence, it does not support the Match() operation.");
		}
	}
}
