using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200063A RID: 1594
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OrganizationIdCacheValue
	{
		// Token: 0x06004B48 RID: 19272 RVA: 0x001158F4 File Offset: 0x00113AF4
		public OrganizationIdCacheValue(OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				organizationId = OrganizationId.ForestWideOrgId;
			}
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, organizationId, organizationId, false);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, sessionSettings, 53, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\OrganizationIdCacheValue.cs");
			ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, organizationId, organizationId, false);
			adsessionSettings.IsSharedConfigChecked = true;
			IConfigurationSession tenantOrTopologyConfigurationSession2 = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, adsessionSettings, 68, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\OrganizationIdCacheValue.cs");
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, adsessionSettings, 72, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\OrganizationIdCacheValue.cs");
			this.organizationId = organizationId;
			this.federatedOrganizationIdCache = new OrganizationFederatedOrganizationIdCache(organizationId, tenantOrTopologyConfigurationSession2);
			this.federatedDomainsCache = new OrganizationFederatedDomainsCache(organizationId, this.federatedOrganizationIdCache, tenantOrTopologyConfigurationSession);
			this.acceptedDomainsCache = new OrganizationAcceptedDomainsCache(organizationId, tenantOrTopologyConfigurationSession);
			this.organizationRelationshipNonAdPropertiesCache = new OrganizationOrganizationRelationshipCache(organizationId, tenantOrTopologyConfigurationSession);
			this.availabilityConfigCache = new OrganizationAvailabilityConfigCache(organizationId, tenantOrTopologyConfigurationSession, tenantOrRootOrgRecipientSession);
			this.availabilityAddressSpaceCache = new OrganizationAvailabilityAddressSpaceCache(organizationId, tenantOrTopologyConfigurationSession);
			this.intraOrganizationConnectorCache = new OrganizationIntraOrganizationConnectorCache(organizationId, tenantOrTopologyConfigurationSession);
		}

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x06004B49 RID: 19273 RVA: 0x001159F0 File Offset: 0x00113BF0
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x06004B4A RID: 19274 RVA: 0x001159F8 File Offset: 0x00113BF8
		public FederatedOrganizationId FederatedOrganizationId
		{
			get
			{
				return this.federatedOrganizationIdCache.Value;
			}
		}

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x06004B4B RID: 19275 RVA: 0x00115A05 File Offset: 0x00113C05
		public IEnumerable<string> FederatedDomains
		{
			get
			{
				return this.federatedDomainsCache.Value;
			}
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x06004B4C RID: 19276 RVA: 0x00115A12 File Offset: 0x00113C12
		public string DefaultFederatedDomain
		{
			get
			{
				return this.federatedDomainsCache.DefaultDomain;
			}
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x00115A1F File Offset: 0x00113C1F
		public OrganizationRelationship GetOrganizationRelationship(string domain)
		{
			return this.organizationRelationshipNonAdPropertiesCache.Get(domain);
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x00115A2D File Offset: 0x00113C2D
		public IntraOrganizationConnector GetIntraOrganizationConnector(string domain)
		{
			return this.intraOrganizationConnectorCache.Get(domain);
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x06004B4F RID: 19279 RVA: 0x00115A3B File Offset: 0x00113C3B
		public IDictionary<string, AuthenticationType> NamespaceAuthenticationTypeHash
		{
			get
			{
				return this.acceptedDomainsCache.Value;
			}
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x00115A48 File Offset: 0x00113C48
		public AuthenticationType GetNamespaceAuthenticationType(string domain)
		{
			AuthenticationType result;
			if (this.NamespaceAuthenticationTypeHash.TryGetValue(domain, out result))
			{
				return result;
			}
			return AuthenticationType.Managed;
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x00115A68 File Offset: 0x00113C68
		public ADRecipient GetAvailabilityConfigOrgWideAccount()
		{
			return this.availabilityConfigCache.GetOrgWideAccountObject();
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x00115A75 File Offset: 0x00113C75
		public ADRecipient GetAvailabilityConfigPerUserAccount()
		{
			return this.availabilityConfigCache.GetPerUserAccountObject();
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x00115A82 File Offset: 0x00113C82
		public AvailabilityAddressSpace GetAvailabilityAddressSpace(string domain)
		{
			return this.availabilityAddressSpaceCache.Get(domain);
		}

		// Token: 0x040033B8 RID: 13240
		private OrganizationId organizationId;

		// Token: 0x040033B9 RID: 13241
		private OrganizationFederatedOrganizationIdCache federatedOrganizationIdCache;

		// Token: 0x040033BA RID: 13242
		private OrganizationFederatedDomainsCache federatedDomainsCache;

		// Token: 0x040033BB RID: 13243
		private OrganizationAcceptedDomainsCache acceptedDomainsCache;

		// Token: 0x040033BC RID: 13244
		private OrganizationOrganizationRelationshipCache organizationRelationshipNonAdPropertiesCache;

		// Token: 0x040033BD RID: 13245
		private OrganizationAvailabilityConfigCache availabilityConfigCache;

		// Token: 0x040033BE RID: 13246
		private OrganizationAvailabilityAddressSpaceCache availabilityAddressSpaceCache;

		// Token: 0x040033BF RID: 13247
		private OrganizationIntraOrganizationConnectorCache intraOrganizationConnectorCache;
	}
}
