using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002D5 RID: 725
	internal class ConnectorConfigurationSession
	{
		// Token: 0x0600202D RID: 8237 RVA: 0x0007B328 File Offset: 0x00079528
		public ConnectorConfigurationSession(OrganizationId orgId)
		{
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				throw new InvalidOperationException("Multi-tenant deployments supported only.");
			}
			if (orgId == null)
			{
				throw new ArgumentException("A tenant id must be specified.");
			}
			this.orgId = orgId;
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x0007B37C File Offset: 0x0007957C
		public static TenantOutboundConnector GetEnabledOutboundConnector(OrganizationId orgId, string connectorName)
		{
			ConnectorConfigurationSession connectorConfigurationSession = new ConnectorConfigurationSession(orgId);
			return connectorConfigurationSession.GetEnabledOutboundConnector(connectorName);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x0007B3C4 File Offset: 0x000795C4
		public static ADOperationResult TryGetEnabledOutboundConnectorByGuid(OrganizationId orgId, Guid connectorGuid, out TenantOutboundConnector matchingConnector)
		{
			IEnumerable<TenantOutboundConnector> enumerable;
			ADOperationResult outboundConnectors = ConnectorConfiguration.GetOutboundConnectors(orgId, (TenantOutboundConnector toc) => toc.IsTransportRuleScoped && connectorGuid.Equals(toc.Guid) && toc.Enabled, out enumerable);
			matchingConnector = null;
			if (outboundConnectors.Succeeded && enumerable != null)
			{
				matchingConnector = enumerable.FirstOrDefault<TenantOutboundConnector>();
			}
			return outboundConnectors;
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x0007B438 File Offset: 0x00079638
		private TenantOutboundConnector GetEnabledOutboundConnector(string connectorName)
		{
			IEnumerable<TenantOutboundConnector> enumerable;
			ADOperationResult outboundConnectors = ConnectorConfiguration.GetOutboundConnectors(this.orgId, (TenantOutboundConnector toc) => toc.IsTransportRuleScoped && string.Equals(connectorName, toc.Name, StringComparison.OrdinalIgnoreCase) && toc.Enabled, out enumerable);
			TenantOutboundConnector result;
			if (!outboundConnectors.Succeeded || enumerable == null || (result = enumerable.FirstOrDefault<TenantOutboundConnector>()) == null)
			{
				throw new OutboundConnectorNotFoundException(connectorName, this.orgId, outboundConnectors.Exception);
			}
			return result;
		}

		// Token: 0x040010DB RID: 4315
		private readonly OrganizationId orgId;
	}
}
