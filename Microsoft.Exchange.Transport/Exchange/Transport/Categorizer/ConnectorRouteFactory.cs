using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000217 RID: 535
	internal static class ConnectorRouteFactory
	{
		// Token: 0x060017A2 RID: 6050 RVA: 0x0005FAF4 File Offset: 0x0005DCF4
		public static bool TryCalculateConnectorRoute(MailGateway connector, ADObjectId fallbackSourceServerId, RouteCalculatorContext context, out RouteInfo routeInfo, out IList<AddressSpace> addressSpaces)
		{
			routeInfo = null;
			addressSpaces = null;
			if (ConnectorRouteFactory.ShouldIgnoreConnector(connector, context.Timestamp))
			{
				return false;
			}
			if (context.Core.IsEdgeMode)
			{
				return EdgeConnectorRouteFactory.TryCalculateConnectorRoute(connector, context, out routeInfo, out addressSpaces);
			}
			if (!ConnectorRouteFactory.TryCalculateConnectorRoute(connector, fallbackSourceServerId, context.ServerMap, context.Core, out routeInfo))
			{
				return false;
			}
			addressSpaces = connector.AddressSpaces;
			return true;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0005FB54 File Offset: 0x0005DD54
		public static bool TryCalculateConnectorRoute(SendConnector connector, ADObjectId fallbackSourceServerId, RoutingServerInfoMap serverMap, RoutingContextCore contextCore, out RouteInfo routeInfo)
		{
			routeInfo = null;
			RoutedServerCollection routedServerCollection;
			if (!ConnectorRouteFactory.TryGetRoutedSourceServers(connector, fallbackSourceServerId, serverMap, contextCore, out routedServerCollection))
			{
				return false;
			}
			if (!ConnectorRouteFactory.IsConnectorInScope(connector, routedServerCollection.ClosestProximity, serverMap.WhenCreated))
			{
				return false;
			}
			bool isEdgeConnector = (routedServerCollection.ClosestProximity == Proximity.LocalADSite || routedServerCollection.ClosestProximity == Proximity.RemoteADSite) && routedServerCollection.PrimaryRouteServers[0].IsEdgeTransportServer;
			switch (routedServerCollection.ClosestProximity)
			{
			case Proximity.LocalServer:
				routeInfo = RouteInfo.CreateForLocalServer(connector.Name, new ConnectorDeliveryHop(connector, contextCore));
				break;
			case Proximity.LocalADSite:
				routeInfo = ConnectorRouteFactory.GetRouteForLocalSite(routedServerCollection, connector, isEdgeConnector);
				break;
			case Proximity.RemoteADSite:
				routeInfo = ConnectorRouteFactory.GetRouteForRemoteSites(routedServerCollection, connector, isEdgeConnector, contextCore, serverMap.WhenCreated);
				break;
			case Proximity.RemoteRoutingGroup:
				routeInfo = routedServerCollection.PrimaryRoute;
				break;
			default:
				throw new InvalidOperationException("Unexpected closest proximity value: " + routedServerCollection.ClosestProximity);
			}
			RoutingDiag.Tracer.TraceDebug<DateTime, string, RouteInfo>(0L, "[{0}] Send connector <{1}> has the following route: {2}", serverMap.WhenCreated, connector.DistinguishedName, routeInfo);
			return true;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0005FC50 File Offset: 0x0005DE50
		private static RouteInfo GetRouteForLocalSite(RoutedServerCollection routedSourceServers, SendConnector connector, bool isEdgeConnector)
		{
			RoutingNextHop nextHop = new ConnectorDeliveryGroup(connector.Id, routedSourceServers, isEdgeConnector);
			return RouteInfo.CreateForLocalSite(connector.Name, nextHop);
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0005FC78 File Offset: 0x0005DE78
		private static RouteInfo GetRouteForRemoteSites(RoutedServerCollection routedSourceServers, SendConnector connector, bool isEdgeConnector, RoutingContextCore contextCore, DateTime traceTimestamp)
		{
			RouteInfo primaryRoute = routedSourceServers.PrimaryRoute;
			if (isEdgeConnector)
			{
				return primaryRoute;
			}
			if (primaryRoute.HasMandatoryTopologyHop || !contextCore.Settings.DestinationRoutingToRemoteSitesEnabled)
			{
				return primaryRoute;
			}
			long minMaxMessageSize = Math.Min(primaryRoute.MaxMessageSize, (long)connector.AbsoluteMaxMessageSize);
			int num = routedSourceServers.TrimRoutes(primaryRoute.DestinationProximity, primaryRoute.SiteRelayCost, minMaxMessageSize);
			if (num > 0)
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, int, string>(0L, "[{0}] Removed {1} AD Sites from Delivery Group for send connector <{2}> because they have higher costs, intermediate hops or lower size restrictions than the primary site", traceTimestamp, num, connector.DistinguishedName);
			}
			RoutingNextHop nextHop = new ConnectorDeliveryGroup(connector.Id, routedSourceServers, isEdgeConnector);
			return RouteInfo.CreateForRemoteSite(connector.Name, nextHop, primaryRoute.MaxMessageSize, primaryRoute.SiteRelayCost);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0005FD14 File Offset: 0x0005DF14
		private static bool ShouldIgnoreConnector(MailGateway connector, DateTime traceTimestamp)
		{
			if (!connector.Enabled)
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string>(0L, "[{0}] Skipping disabled connector: {1}", traceTimestamp, connector.DistinguishedName);
				return true;
			}
			if (MultiValuedPropertyBase.IsNullOrEmpty(connector.AddressSpaces))
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string>(0L, "[{0}] Skipping null-or-empty address space connector: {1}", traceTimestamp, connector.DistinguishedName);
				return true;
			}
			SmtpSendConnectorConfig smtpSendConnectorConfig = connector as SmtpSendConnectorConfig;
			if (smtpSendConnectorConfig == null)
			{
				return false;
			}
			if (!smtpSendConnectorConfig.DNSRoutingEnabled && MultiValuedPropertyBase.IsNullOrEmpty(smtpSendConnectorConfig.SmartHosts))
			{
				RoutingDiag.Tracer.TraceError<DateTime, string, string>(0L, "[{0}] Skipping connector <{1}> because its smart hosts string '{2}' is invalid.", traceTimestamp, connector.DistinguishedName, smtpSendConnectorConfig.SmartHostsString);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingInvalidSmarthosts, null, new object[]
				{
					smtpSendConnectorConfig.SmartHostsString,
					connector.DistinguishedName,
					traceTimestamp
				});
				return true;
			}
			return false;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0005FDE0 File Offset: 0x0005DFE0
		private static bool TryGetRoutedSourceServers(SendConnector connector, ADObjectId fallbackSourceServerId, RoutingServerInfoMap serverMap, RoutingContextCore contextCore, out RoutedServerCollection routedSourceServers)
		{
			routedSourceServers = null;
			IList<ADObjectId> list;
			if (!ConnectorRouteFactory.TryGetSourceServerIds(connector, fallbackSourceServerId, serverMap.WhenCreated, out list))
			{
				return false;
			}
			List<ADObjectId> list2;
			List<RoutingServerInfo> list3;
			List<RoutingServerInfo> list4;
			bool flag = serverMap.TryCreateRoutedServerCollectionForClosestProximity(list, contextCore, out routedSourceServers, out list2, out list3, out list4);
			if (list2 != null)
			{
				foreach (ADObjectId adobjectId in list2)
				{
					if (!adobjectId.IsDeleted)
					{
						ConnectorRouteFactory.LogUnroutedServer(adobjectId, connector, serverMap.WhenCreated);
					}
				}
			}
			if (list3 != null)
			{
				foreach (RoutingServerInfo serverInfo in list3)
				{
					ConnectorRouteFactory.LogUnroutedServer(serverInfo, connector, serverMap.WhenCreated);
				}
			}
			if (!flag)
			{
				RoutingDiag.Tracer.TraceError(0L, "[{0}] No route found to connector '{1}'. Total source server count: {2}; unknown source server count: {3}; unrouted source server count: {4}; non-active source server count: {5}", new object[]
				{
					serverMap.WhenCreated,
					connector.DistinguishedName,
					list.Count,
					(list2 == null) ? 0 : list2.Count,
					(list3 == null) ? 0 : list3.Count,
					(list4 == null) ? 0 : list4.Count
				});
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoRouteToConnector, null, new object[]
				{
					connector.DistinguishedName,
					serverMap.WhenCreated,
					list.Count,
					(list2 == null) ? 0 : list2.Count,
					(list3 == null) ? 0 : list3.Count,
					(list4 == null) ? 0 : list4.Count
				});
				return false;
			}
			return true;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0005FFC4 File Offset: 0x0005E1C4
		private static bool TryGetSourceServerIds(SendConnector connector, ADObjectId fallbackSourceServerId, DateTime traceTimestamp, out IList<ADObjectId> sourceServerIds)
		{
			sourceServerIds = connector.SourceTransportServers;
			if (sourceServerIds == null || sourceServerIds.Count == 0)
			{
				if (fallbackSourceServerId == null)
				{
					RoutingDiag.Tracer.TraceError<DateTime, string>(0L, "[{0}] No source transport servers and fallback server set for connector '{1}'. Connector has no route.", traceTimestamp, connector.DistinguishedName);
					RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoSourceBhServers, null, new object[]
					{
						connector.DistinguishedName,
						traceTimestamp
					});
					return false;
				}
				sourceServerIds = new ADObjectId[]
				{
					fallbackSourceServerId
				};
			}
			return true;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00060040 File Offset: 0x0005E240
		private static bool IsConnectorInScope(SendConnector connector, Proximity proximity, DateTime traceTimestamp)
		{
			MailGateway mailGateway = connector as MailGateway;
			if (mailGateway == null)
			{
				return true;
			}
			if (!mailGateway.IsScopedConnector)
			{
				return true;
			}
			if (proximity == Proximity.RemoteRoutingGroup)
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string>(0L, "[{0}] Connector <{1}> is out of scope for local Routing Group. Skipping the connector.", traceTimestamp, connector.DistinguishedName);
				return false;
			}
			if (proximity != Proximity.LocalServer && proximity != Proximity.LocalADSite)
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string>(0L, "[{0}] Connector <{1}> is out of scope for local AD Site. Skipping the connector.", traceTimestamp, connector.DistinguishedName);
				return false;
			}
			return true;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000600A4 File Offset: 0x0005E2A4
		private static void LogUnroutedServer(ADObjectId serverId, SendConnector connector, DateTime traceTimestamp)
		{
			RoutingDiag.Tracer.TraceError<DateTime, string, string>(0L, "[{0}] Server object not found for source transport server '{1}' for connector '{2}'. Skipping the server.", traceTimestamp, serverId.DistinguishedName, connector.DistinguishedName);
			RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoSourceBhRoute, null, new object[]
			{
				serverId.DistinguishedName,
				connector.DistinguishedName,
				traceTimestamp
			});
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00060104 File Offset: 0x0005E304
		private static void LogUnroutedServer(RoutingServerInfo serverInfo, SendConnector connector, DateTime traceTimestamp)
		{
			RoutingDiag.Tracer.TraceError<DateTime, string, string>(0L, "[{0}] No route to source transport server '{1}' for connector '{2}'. Skipping the server.", traceTimestamp, serverInfo.Id.DistinguishedName, connector.DistinguishedName);
			RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoSourceBhRoute, null, new object[]
			{
				serverInfo.Id.DistinguishedName,
				connector.DistinguishedName,
				traceTimestamp
			});
		}
	}
}
