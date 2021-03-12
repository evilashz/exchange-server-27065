using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.RecipientAPI
{
	// Token: 0x020001B8 RID: 440
	internal sealed class IsInternalResolver
	{
		// Token: 0x06001240 RID: 4672 RVA: 0x000583AC File Offset: 0x000565AC
		public IsInternalResolver(OrganizationId organizationId, IsInternalResolver.GetAcceptedDomainCollectionDelegate getAcceptedDomainCollectionDelegate, IsInternalResolver.GetRemoteDomainCollectionDelegate getRemoteDomainCollectionDelegate)
		{
			if (getAcceptedDomainCollectionDelegate == null)
			{
				throw new ArgumentNullException("getAcceptedDomainCollectionDelegate");
			}
			if (getRemoteDomainCollectionDelegate == null)
			{
				throw new ArgumentNullException("getRemoteDomainCollectionDelegate");
			}
			this.organizationId = organizationId;
			this.getAcceptedDomainCollectionDelegate = getAcceptedDomainCollectionDelegate;
			this.getRemoteDomainCollectionDelegate = getRemoteDomainCollectionDelegate;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000583E5 File Offset: 0x000565E5
		public static bool IsInternal(RoutingAddress routingAddress, OrganizationId organizationId)
		{
			return routingAddress.IsValid && IsInternalResolver.IsInternal(routingAddress.DomainPart, organizationId);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000583FF File Offset: 0x000565FF
		public static bool IsInternal(RoutingDomain routingDomain, OrganizationId organizationId)
		{
			return routingDomain.IsValid() && IsInternalResolver.IsInternal(routingDomain.Domain, organizationId);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00058419 File Offset: 0x00056619
		public bool IsInternal(RoutingAddress address)
		{
			return address.IsValid && this.IsInternal(address.DomainPart);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00058433 File Offset: 0x00056633
		public bool IsInternal(RoutingDomain domain)
		{
			return domain.IsValid() && this.IsInternal(domain.Domain);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0005847C File Offset: 0x0005667C
		private static bool IsInternal(string domainStringRepresentation, OrganizationId organizationId)
		{
			IConfigurationSession session = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 145, "IsInternal", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\EdgeExtensibility\\IsInternalResolver.cs");
			ADPagedReader<Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain> acceptedDomains = null;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				acceptedDomains = session.FindAllPaged<Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain>();
			}, 3);
			foreach (Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain acceptedDomain in acceptedDomains)
			{
				if (acceptedDomain.DomainType != AcceptedDomainType.ExternalRelay)
				{
					SmtpDomainWithSubdomains smtpDomainWithSubdomains = new SmtpDomainWithSubdomains(acceptedDomain.DomainName.Domain, acceptedDomain.DomainName.IncludeSubDomains || acceptedDomain.MatchSubDomains);
					if (smtpDomainWithSubdomains.Match(domainStringRepresentation) >= 0)
					{
						return true;
					}
				}
			}
			ADPagedReader<DomainContentConfig> remoteDomains = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				remoteDomains = session.FindAllPaged<DomainContentConfig>();
			});
			foreach (DomainContentConfig domainContentConfig in remoteDomains)
			{
				if (domainContentConfig.IsInternal && domainContentConfig.DomainName.Match(domainStringRepresentation) >= 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000585C0 File Offset: 0x000567C0
		private bool IsInternal(string routingDomainStringRepr)
		{
			bool flag;
			AcceptedDomainCollection acceptedDomainCollection = this.getAcceptedDomainCollectionDelegate(this.organizationId, out flag);
			Microsoft.Exchange.Data.Transport.AcceptedDomain acceptedDomain = (acceptedDomainCollection != null) ? acceptedDomainCollection.Find(routingDomainStringRepr) : null;
			if (acceptedDomain != null && acceptedDomain.IsInCorporation)
			{
				bool flag2 = true;
				if (!flag)
				{
					flag2 = (this.organizationId.Equals(OrganizationId.ForestWideOrgId) ? (acceptedDomain.TenantId == Guid.Empty) : (this.organizationId.ConfigurationUnit.ObjectGuid == acceptedDomain.TenantId));
				}
				if (flag2)
				{
					return true;
				}
			}
			RemoteDomainCollection remoteDomainCollection = this.getRemoteDomainCollectionDelegate(this.organizationId);
			RemoteDomain remoteDomain = (remoteDomainCollection != null) ? remoteDomainCollection.Find(routingDomainStringRepr) : null;
			return remoteDomain != null && remoteDomain.IsInternal;
		}

		// Token: 0x04000A8C RID: 2700
		private const int ADOperationRetryCount = 3;

		// Token: 0x04000A8D RID: 2701
		private readonly OrganizationId organizationId;

		// Token: 0x04000A8E RID: 2702
		private readonly IsInternalResolver.GetAcceptedDomainCollectionDelegate getAcceptedDomainCollectionDelegate;

		// Token: 0x04000A8F RID: 2703
		private readonly IsInternalResolver.GetRemoteDomainCollectionDelegate getRemoteDomainCollectionDelegate;

		// Token: 0x020001B9 RID: 441
		// (Invoke) Token: 0x06001248 RID: 4680
		internal delegate AcceptedDomainCollection GetAcceptedDomainCollectionDelegate(OrganizationId organizationId, out bool scopedToOrganization);

		// Token: 0x020001BA RID: 442
		// (Invoke) Token: 0x0600124C RID: 4684
		internal delegate RemoteDomainCollection GetRemoteDomainCollectionDelegate(OrganizationId organizationId);
	}
}
