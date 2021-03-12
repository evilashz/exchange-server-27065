using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002F2 RID: 754
	internal static class TransportIsInternalResolver
	{
		// Token: 0x0600215F RID: 8543 RVA: 0x0007E6A8 File Offset: 0x0007C8A8
		public static bool IsInternal(OrganizationId organizationId, RoutingAddress routingAddress, bool acceptedDomainsOnly = false)
		{
			IsInternalResolver isInternalResolver = TransportIsInternalResolver.CreateIsInternalResolver(organizationId, acceptedDomainsOnly);
			return isInternalResolver.IsInternal(routingAddress);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0007E6C4 File Offset: 0x0007C8C4
		public static bool IsInternal(OrganizationId organizationId, RoutingDomain routingDomain, bool acceptedDomainsOnly = false)
		{
			IsInternalResolver isInternalResolver = TransportIsInternalResolver.CreateIsInternalResolver(organizationId, acceptedDomainsOnly);
			return isInternalResolver.IsInternal(routingDomain);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0007E6E0 File Offset: 0x0007C8E0
		private static IsInternalResolver CreateIsInternalResolver(OrganizationId organizationId, bool acceptedDomainsOnly)
		{
			return new IsInternalResolver(organizationId, new IsInternalResolver.GetAcceptedDomainCollectionDelegate(TransportIsInternalResolver.GetAcceptedDomainCollection), acceptedDomainsOnly ? new IsInternalResolver.GetRemoteDomainCollectionDelegate(TransportIsInternalResolver.GetEmptyRemoteDomainCollection) : new IsInternalResolver.GetRemoteDomainCollectionDelegate(TransportIsInternalResolver.GetRemoteDomainCollection));
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0007E714 File Offset: 0x0007C914
		private static AcceptedDomainCollection GetAcceptedDomainCollection(OrganizationId organizationId, out bool scopedToOrganization)
		{
			scopedToOrganization = true;
			PerTenantAcceptedDomainTable perTenantAcceptedDomainTable;
			if (Components.Configuration.TryGetAcceptedDomainTable(organizationId, out perTenantAcceptedDomainTable))
			{
				return perTenantAcceptedDomainTable.AcceptedDomainTable;
			}
			return null;
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0007E73C File Offset: 0x0007C93C
		private static RemoteDomainCollection GetRemoteDomainCollection(OrganizationId organizationId)
		{
			PerTenantRemoteDomainTable perTenantRemoteDomainTable;
			if (Components.Configuration.TryGetRemoteDomainTable(organizationId, out perTenantRemoteDomainTable))
			{
				return perTenantRemoteDomainTable.RemoteDomainTable;
			}
			return null;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x0007E760 File Offset: 0x0007C960
		private static RemoteDomainCollection GetEmptyRemoteDomainCollection(OrganizationId organizationId)
		{
			return RemoteDomainMap.Empty;
		}
	}
}
