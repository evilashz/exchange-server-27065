using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000261 RID: 609
	internal class RoutingTables
	{
		// Token: 0x06001A55 RID: 6741 RVA: 0x0006C098 File Offset: 0x0006A298
		public RoutingTables(RoutingTopologyBase topologyConfig, RoutingContextCore context, ITenantDagQuota tenantDagQuota, bool forcedReload)
		{
			RoutingUtils.ThrowIfNull(topologyConfig, "topologyConfig");
			RoutingUtils.ThrowIfNull(context, "context");
			this.PopulateTables(topologyConfig, context, tenantDagQuota, forcedReload);
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x0006C0C1 File Offset: 0x0006A2C1
		public RoutingServerInfoMap ServerMap
		{
			get
			{
				return this.serverMap;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0006C0C9 File Offset: 0x0006A2C9
		public DatabaseRouteMap DatabaseMap
		{
			get
			{
				return this.databaseMap;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x0006C0D1 File Offset: 0x0006A2D1
		public ConnectorIndexMap ConnectorMap
		{
			get
			{
				return this.connectorIndexMap;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x0006C0D9 File Offset: 0x0006A2D9
		public DateTime WhenCreated
		{
			get
			{
				return this.whenCreated;
			}
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0006C0E4 File Offset: 0x0006A2E4
		public bool Match(RoutingTables other)
		{
			RoutingUtils.ThrowIfNull(other, "other");
			if (object.ReferenceEquals(this, other))
			{
				throw new InvalidOperationException("An instance of RoutingTables should not be compared with itself");
			}
			return RoutingUtils.NullMatch(this.serverMap, other.serverMap) && RoutingUtils.NullMatch(this.databaseMap, other.databaseMap) && RoutingUtils.NullMatch(this.connectorIndexMap, other.connectorIndexMap) && (this.serverMap == null || this.serverMap.QuickMatch(other.serverMap)) && (this.databaseMap == null || this.databaseMap.QuickMatch(other.databaseMap)) && (this.connectorIndexMap == null || this.connectorIndexMap.QuickMatch(other.connectorIndexMap)) && (this.serverMap == null || this.serverMap.FullMatch(other.serverMap)) && (this.databaseMap == null || this.databaseMap.FullMatch(other.databaseMap)) && (this.connectorIndexMap == null || this.connectorIndexMap.FullMatch(other.connectorIndexMap));
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0006C1F9 File Offset: 0x0006A3F9
		public bool TryGetDiagnosticInfo(bool verbose, DiagnosableParameters parameters, out XElement diagnosticInfo)
		{
			return this.databaseMap.TryGetDiagnosticInfo(verbose, parameters, out diagnosticInfo);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0006C20C File Offset: 0x0006A40C
		private void PopulateTables(RoutingTopologyBase topologyConfig, RoutingContextCore contextCore, ITenantDagQuota tenantDagQuota, bool forcedReload)
		{
			this.whenCreated = topologyConfig.WhenCreated;
			if (contextCore.ServerRoutingSupported)
			{
				this.serverMap = new RoutingServerInfoMap(topologyConfig, contextCore);
			}
			RouteCalculatorContext context = new RouteCalculatorContext(contextCore, topologyConfig, this.serverMap);
			if (contextCore.ConnectorRoutingSupported)
			{
				this.PopulateConnectors(context);
			}
			if (contextCore.ServerRoutingSupported)
			{
				this.PopulateDatabaseRoutes(context, tenantDagQuota, forcedReload);
			}
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x0006C26C File Offset: 0x0006A46C
		private void PopulateConnectors(RouteCalculatorContext context)
		{
			this.connectorIndexMap = new ConnectorIndexMap(this.whenCreated);
			foreach (MailGateway mailGateway in context.TopologyConfig.SendConnectors)
			{
				RouteInfo routeInfo;
				IList<AddressSpace> addressSpaces;
				if (ConnectorRouteFactory.TryCalculateConnectorRoute(mailGateway, mailGateway.HomeMtaServerId, context, out routeInfo, out addressSpaces))
				{
					this.connectorIndexMap.AddConnector(new ConnectorRoutingDestination(mailGateway, addressSpaces, routeInfo));
				}
			}
			if (context.Core.ServerRoutingSupported)
			{
				foreach (KeyValuePair<Guid, RouteInfo> keyValuePair in this.serverMap.RoutingGroupRelayMap.RoutesToRGConnectors)
				{
					this.connectorIndexMap.AddNonAddressSpaceConnector(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0006C360 File Offset: 0x0006A560
		private void PopulateDatabaseRoutes(RouteCalculatorContext context, ITenantDagQuota tenantDagQuota, bool forcedReload)
		{
			this.databaseMap = new DatabaseRouteMap(context, tenantDagQuota, forcedReload);
		}

		// Token: 0x04000C87 RID: 3207
		private DateTime whenCreated;

		// Token: 0x04000C88 RID: 3208
		private RoutingServerInfoMap serverMap;

		// Token: 0x04000C89 RID: 3209
		private DatabaseRouteMap databaseMap;

		// Token: 0x04000C8A RID: 3210
		private ConnectorIndexMap connectorIndexMap;
	}
}
