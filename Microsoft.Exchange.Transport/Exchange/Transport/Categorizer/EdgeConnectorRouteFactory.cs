using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000225 RID: 549
	internal static class EdgeConnectorRouteFactory
	{
		// Token: 0x0600181C RID: 6172 RVA: 0x00062580 File Offset: 0x00060780
		public static bool TryCalculateConnectorRoute(SendConnector connector, RouteCalculatorContext context, out RouteInfo routeInfo, out IList<AddressSpace> addressSpaces)
		{
			routeInfo = null;
			addressSpaces = null;
			SmtpSendConnectorConfig smtpSendConnectorConfig = connector as SmtpSendConnectorConfig;
			if (smtpSendConnectorConfig == null)
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string>(0L, "[{0}] Skipping non-SMTP connector on Edge: {1}", context.Timestamp, connector.DistinguishedName);
				return false;
			}
			IList<AddressSpace> list;
			if (!EdgeConnectorRouteFactory.TryExpandAddressSpaces(smtpSendConnectorConfig, context, out list))
			{
				return false;
			}
			ConnectorDeliveryHop nextHop;
			if (!smtpSendConnectorConfig.DNSRoutingEnabled)
			{
				IList<SmartHost> expandedSmartHosts;
				string expandedSmartHostsString;
				if (!EdgeConnectorRouteFactory.TryExpandSmartHosts(smtpSendConnectorConfig, context, out expandedSmartHosts, out expandedSmartHostsString))
				{
					return false;
				}
				nextHop = new ConnectorDeliveryHop(smtpSendConnectorConfig, expandedSmartHosts, expandedSmartHostsString, context.Core);
			}
			else
			{
				nextHop = new ConnectorDeliveryHop(connector, context.Core);
			}
			routeInfo = RouteInfo.CreateForLocalServer(connector.Name, nextHop);
			addressSpaces = list;
			RoutingDiag.Tracer.TraceDebug<DateTime, string, RouteInfo>(0L, "[{0}] Send connector <{1}> has the following route: {2}", context.Timestamp, connector.DistinguishedName, routeInfo);
			return true;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00062634 File Offset: 0x00060834
		private static bool TryExpandAddressSpaces(SmtpSendConnectorConfig connector, RouteCalculatorContext context, out IList<AddressSpace> expandedAddressSpaces)
		{
			expandedAddressSpaces = null;
			ICollection<string> edgeToHubAcceptedDomains = context.Core.EdgeDependencies.EdgeToHubAcceptedDomains;
			if (EdgeConnectorRouteFactory.IsAutomatic(connector.AddressSpaces[0]))
			{
				if (edgeToHubAcceptedDomains.Count == 0)
				{
					RoutingDiag.Tracer.TraceError<DateTime, string>(0L, "[{0}] Skipping auto address space connector due to the absence of accepted domains: {1}", context.Timestamp, connector.DistinguishedName);
					return false;
				}
				expandedAddressSpaces = new List<AddressSpace>(edgeToHubAcceptedDomains.Count);
				using (IEnumerator<string> enumerator = edgeToHubAcceptedDomains.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string addressSpace = enumerator.Current;
						AddressSpace item = new AddressSpace("smtp", addressSpace, connector.AddressSpaces[0].Cost);
						expandedAddressSpaces.Add(item);
					}
					return true;
				}
			}
			expandedAddressSpaces = EdgeConnectorRouteFactory.FilterList<AddressSpace>(connector.AddressSpaces, new Predicate<AddressSpace>(EdgeConnectorRouteFactory.IsAutomatic));
			if (expandedAddressSpaces == null)
			{
				expandedAddressSpaces = connector.AddressSpaces;
			}
			else
			{
				RoutingDiag.Tracer.TraceError<DateTime, string>(0L, "[{0}] Auto and regular address spaces detected in connector <{1}>; skipping the auto address spaces", context.Timestamp, connector.DistinguishedName);
			}
			return true;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0006273C File Offset: 0x0006093C
		private static bool TryExpandSmartHosts(SmtpSendConnectorConfig connector, RouteCalculatorContext context, out IList<SmartHost> expandedSmartHosts, out string expandedSmartHostsString)
		{
			expandedSmartHosts = null;
			expandedSmartHostsString = null;
			ICollection<Server> hubServersOnEdge = context.TopologyConfig.HubServersOnEdge;
			if (!EdgeConnectorRouteFactory.IsAutomatic(connector.SmartHosts[0]))
			{
				expandedSmartHosts = EdgeConnectorRouteFactory.FilterList<SmartHost>(connector.SmartHosts, new Predicate<SmartHost>(EdgeConnectorRouteFactory.IsAutomatic));
				if (expandedSmartHosts == null)
				{
					expandedSmartHosts = connector.SmartHosts;
					expandedSmartHostsString = connector.SmartHostsString;
				}
				else
				{
					RoutingDiag.Tracer.TraceError<DateTime, string>(0L, "[{0}] Auto and regular smart hosts detected in connector <{1}>; skipping the auto smart hosts", context.Timestamp, connector.DistinguishedName);
					expandedSmartHostsString = connector.SmartHostsString.Replace(EdgeConnectorRouteFactory.AutoSmartHostInString, string.Empty);
				}
				return true;
			}
			if (hubServersOnEdge.Count == 0)
			{
				RoutingDiag.Tracer.TraceError<DateTime, string>(0L, "[{0}] Skipping auto smart host connector due to the absence of Hub servers: {1}", context.Timestamp, connector.DistinguishedName);
				return false;
			}
			expandedSmartHosts = new List<SmartHost>(hubServersOnEdge.Count);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Server server in hubServersOnEdge)
			{
				if (string.IsNullOrEmpty(server.Fqdn))
				{
					RoutingDiag.Tracer.TraceError<DateTime, string>(0L, "[{0}] No FQDN for Hub Server object with DN: {1}. Skipping it from Smart Hosts expansion.", context.Timestamp, server.DistinguishedName);
				}
				else
				{
					expandedSmartHosts.Add(new SmartHost(server.Fqdn));
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(server.Fqdn);
				}
			}
			if (expandedSmartHosts.Count > 0)
			{
				expandedSmartHostsString = stringBuilder.ToString();
				return true;
			}
			RoutingDiag.Tracer.TraceError<DateTime, string>(0L, "[{0}] No valid Hub Server for smart hosts expansion of connector {1}. Skipping the connector.", context.Timestamp, connector.DistinguishedName);
			expandedSmartHostsString = null;
			return false;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000628D8 File Offset: 0x00060AD8
		private static bool IsAutomatic(AddressSpace addressSpace)
		{
			return addressSpace.Address.Equals("--");
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000628EA File Offset: 0x00060AEA
		private static bool IsAutomatic(SmartHost smartHost)
		{
			return smartHost.Equals(EdgeConnectorRouteFactory.AutomaticEdgeToHubSmartHost);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000628F8 File Offset: 0x00060AF8
		private static IList<T> FilterList<T>(IList<T> list, Predicate<T> filterOut)
		{
			IList<T> list2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				if (filterOut(list[i]))
				{
					if (list2 == null)
					{
						list2 = new List<T>(list.Count - 1);
						for (int j = 0; j < i; j++)
						{
							list2.Add(list[j]);
						}
					}
				}
				else if (list2 != null)
				{
					list2.Add(list[i]);
				}
			}
			return list2;
		}

		// Token: 0x04000BC6 RID: 3014
		private const string AutomaticEdgeToHubAddressSpace = "--";

		// Token: 0x04000BC7 RID: 3015
		private const char SmartHostDelimiter = ',';

		// Token: 0x04000BC8 RID: 3016
		private static readonly SmartHost AutomaticEdgeToHubSmartHost = new SmartHost("--");

		// Token: 0x04000BC9 RID: 3017
		private static readonly string AutoSmartHostInString = ',' + EdgeConnectorRouteFactory.AutomaticEdgeToHubSmartHost;
	}
}
