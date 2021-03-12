using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000D9 RID: 217
	internal class FfoSessionSettingsFactory : ADSessionSettings.SessionSettingsFactory
	{
		// Token: 0x06000810 RID: 2064 RVA: 0x0001A7E9 File Offset: 0x000189E9
		internal override ADSessionSettings FromAllTenantsPartitionId(PartitionId partitionId)
		{
			return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(ScopeSet.GetOrgWideDefaultScopeSet(OrganizationId.ForestWideOrgId), null, OrganizationId.ForestWideOrgId, null, ConfigScopes.AllTenants, PartitionId.LocalForest);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001A807 File Offset: 0x00018A07
		internal override ADSessionSettings FromAllTenantsObjectId(ADObjectId id)
		{
			return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(ScopeSet.GetOrgWideDefaultScopeSet(OrganizationId.ForestWideOrgId), id, OrganizationId.ForestWideOrgId, null, ConfigScopes.AllTenants, PartitionId.LocalForest);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001A825 File Offset: 0x00018A25
		internal override ADSessionSettings FromAllTenantsOrRootOrgAutoDetect(ADObjectId id)
		{
			if (id.DomainId == null)
			{
				return ADSessionSettings.FromRootOrgScopeSet();
			}
			if (!ADSessionSettings.IsForefrontObject(id))
			{
				return ADSessionSettings.FromRootOrgScopeSet();
			}
			return ADSessionSettings.FromAllTenantsObjectId(id);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001A849 File Offset: 0x00018A49
		internal override ADSessionSettings FromAllTenantsOrRootOrgAutoDetect(OrganizationId orgId)
		{
			if (!OrganizationId.ForestWideOrgId.Equals(orgId))
			{
				return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(ScopeSet.GetOrgWideDefaultScopeSet(OrganizationId.ForestWideOrgId), null, orgId, null, ConfigScopes.AllTenants, PartitionId.LocalForest);
			}
			return ADSessionSettings.FromRootOrgScopeSet();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001A876 File Offset: 0x00018A76
		internal override ADSessionSettings FromTenantPartitionHint(TenantPartitionHint partitionHint)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001A880 File Offset: 0x00018A80
		internal override ADSessionSettings FromExternalDirectoryOrganizationId(Guid externalDirectoryOrganizationId)
		{
			ADObjectId adobjectId = new ADObjectId(DalHelper.GetTenantDistinguishedName(externalDirectoryOrganizationId.ToString()), externalDirectoryOrganizationId);
			ADPropertyBag adpropertyBag = new ADPropertyBag();
			adpropertyBag[ADObjectSchema.ConfigurationUnit] = adobjectId;
			adpropertyBag[ADObjectSchema.OrganizationalUnitRoot] = adobjectId;
			OrganizationId organizationId = (OrganizationId)ADObject.OrganizationIdGetter(adpropertyBag);
			return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(ScopeSet.GetOrgWideDefaultScopeSet(organizationId), adobjectId, organizationId, null, ConfigScopes.TenantLocal, PartitionId.LocalForest);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001A8E4 File Offset: 0x00018AE4
		internal override ADSessionSettings FromTenantForestAndCN(string exoAccountForest, string exoTenantContainer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001A8EC File Offset: 0x00018AEC
		internal override ADSessionSettings FromTenantCUName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			GlobalConfigSession globalConfigSession = new GlobalConfigSession();
			FfoTenant tenantByName = globalConfigSession.GetTenantByName(name);
			if (tenantByName == null)
			{
				throw new CannotResolveTenantNameException(DirectoryStrings.CannotResolveTenantName(name));
			}
			ExchangeConfigurationUnit exchangeConfigurationUnit = FfoConfigurationSession.GetExchangeConfigurationUnit(tenantByName);
			return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(ScopeSet.GetOrgWideDefaultScopeSet(OrganizationId.ForestWideOrgId), null, exchangeConfigurationUnit.OrganizationId, null, ConfigScopes.TenantLocal, PartitionId.LocalForest);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001A948 File Offset: 0x00018B48
		internal override ADSessionSettings FromTenantAcceptedDomain(string domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			ExTraceGlobals.GetConnectionTracer.TraceDebug<string>((long)domain.GetHashCode(), "FromTenantAcceptedDomain(): Resolving domainName '{0}' into partition", domain);
			string text = null;
			Guid empty = Guid.Empty;
			ADAccountPartitionLocator.GetPartitionIdByAcceptedDomainName(domain, out text, out empty);
			if (empty != Guid.Empty)
			{
				return ADSessionSettings.SessionSettingsFactory.Default.FromExternalDirectoryOrganizationId(empty);
			}
			ExTraceGlobals.GetConnectionTracer.TraceDebug<string>((long)domain.GetHashCode(), "FromTenantAcceptedDomain(): Failed to resolve domainName '{0}' to partition", domain);
			throw new CannotResolveTenantNameException(DirectoryStrings.CannotResolveTenantNameByAcceptedDomain(domain));
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001A9C8 File Offset: 0x00018BC8
		internal override ADSessionSettings FromTenantMSAUser(string msaUserNetID)
		{
			throw new CannotResolveMSAUserNetIDException(DirectoryStrings.CannotFindTenantByMSAUserNetID(msaUserNetID));
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001A9D8 File Offset: 0x00018BD8
		internal override bool InDomain()
		{
			if (this.inDomain == null)
			{
				try
				{
					NativeHelpers.GetDomainName();
					this.inDomain = new bool?(true);
				}
				catch (CannotGetDomainInfoException)
				{
					this.inDomain = new bool?(false);
				}
			}
			return this.inDomain.Value;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001AA30 File Offset: 0x00018C30
		protected override OrganizationId RehomeScopingOrganizationIdIfNeeded(OrganizationId currentOrganizationId)
		{
			return currentOrganizationId;
		}

		// Token: 0x0400046F RID: 1135
		private bool? inDomain;
	}
}
