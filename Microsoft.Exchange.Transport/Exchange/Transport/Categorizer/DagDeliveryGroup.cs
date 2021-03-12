using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200021F RID: 543
	internal class DagDeliveryGroup : MailboxDeliveryGroupBase
	{
		// Token: 0x060017E2 RID: 6114 RVA: 0x00060C20 File Offset: 0x0005EE20
		public DagDeliveryGroup(ADObjectId dagId, RouteInfo routeInfo, RoutingServerInfo serverInfo, bool isLocalDeliveryGroup, RoutingContextCore contextCore) : base(new RoutedServerCollection(routeInfo, serverInfo, contextCore), DeliveryType.SmtpRelayToDag, dagId.Name, dagId.ObjectGuid, serverInfo.MajorVersion, isLocalDeliveryGroup)
		{
			RoutingUtils.ThrowIfNullOrEmpty(dagId, "dagId");
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00060C52 File Offset: 0x0005EE52
		public void AddServer(RouteInfo routeInfo, RoutingServerInfo serverInfo, RoutingContextCore contextCore)
		{
			base.AddServerInternal(routeInfo, serverInfo, contextCore);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00060C60 File Offset: 0x0005EE60
		public override bool TryGetDatabaseRouteInfo(MiniDatabase database, RoutingServerInfo owningServerInfo, RouteCalculatorContext context, out RouteInfo databaseRouteInfo)
		{
			databaseRouteInfo = null;
			RouteInfo primaryRoute = base.RoutedServerCollection.PrimaryRoute;
			if (primaryRoute.DestinationProximity != Proximity.RemoteADSite || base.RoutedServerCollection.ServerGroupCount <= 1 || (!primaryRoute.HasMandatoryTopologyHop && context.Core.Settings.DestinationRoutingToRemoteSitesEnabled))
			{
				return base.TryGetDatabaseRouteInfo(database, owningServerInfo, context, out databaseRouteInfo);
			}
			if (context.ServerMap.TryGetServerRoute(owningServerInfo, out databaseRouteInfo))
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string, string>((long)this.GetHashCode(), "[{0}] Broke up the DAG for Database '{1}'; the route destination name is '{2}'", context.Timestamp, database.DistinguishedName, databaseRouteInfo.DestinationName);
				return true;
			}
			RoutingDiag.Tracer.TraceError<DateTime, string, string>((long)this.GetHashCode(), "[{0}] Owning server '{1}' for Database '{2}' does not have a route", context.Timestamp, owningServerInfo.Id.DistinguishedName, database.DistinguishedName);
			RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoRouteToOwningServer, owningServerInfo.Id.DistinguishedName, new object[]
			{
				owningServerInfo.Id.DistinguishedName,
				database.DistinguishedName,
				context.Timestamp
			});
			databaseRouteInfo = null;
			return false;
		}
	}
}
