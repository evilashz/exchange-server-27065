using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200008F RID: 143
	internal interface ITenantConfigurationSession : IConfigurationSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x06000745 RID: 1861
		AcceptedDomain[] FindAllAcceptedDomainsInOrg(ADObjectId organizationCU);

		// Token: 0x06000746 RID: 1862
		ExchangeConfigurationUnit[] FindAllOpenConfigurationUnits(bool excludeFull);

		// Token: 0x06000747 RID: 1863
		ExchangeConfigurationUnit[] FindSharedConfiguration(SharedConfigurationInfo sharedConfigInfo, bool enabledSharedOrgOnly);

		// Token: 0x06000748 RID: 1864
		ExchangeConfigurationUnit[] FindSharedConfigurationByOrganizationId(OrganizationId tinyTenantId);

		// Token: 0x06000749 RID: 1865
		ADRawEntry[] FindDeletedADRawEntryByUsnRange(ADObjectId lastKnownParentId, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties);

		// Token: 0x0600074A RID: 1866
		ExchangeConfigurationUnit GetExchangeConfigurationUnitByExternalId(string externalDirectoryOrganizationId);

		// Token: 0x0600074B RID: 1867
		ExchangeConfigurationUnit GetExchangeConfigurationUnitByExternalId(Guid externalDirectoryOrganizationId);

		// Token: 0x0600074C RID: 1868
		ExchangeConfigurationUnit GetExchangeConfigurationUnitByName(string organizationName);

		// Token: 0x0600074D RID: 1869
		ADObjectId GetExchangeConfigurationUnitIdByName(string organizationName);

		// Token: 0x0600074E RID: 1870
		ExchangeConfigurationUnit GetExchangeConfigurationUnitByNameOrAcceptedDomain(string organizationName);

		// Token: 0x0600074F RID: 1871
		ExchangeConfigurationUnit GetExchangeConfigurationUnitByUserNetID(string userNetID);

		// Token: 0x06000750 RID: 1872
		OrganizationId GetOrganizationIdFromOrgNameOrAcceptedDomain(string domainName);

		// Token: 0x06000751 RID: 1873
		OrganizationId GetOrganizationIdFromExternalDirectoryOrgId(Guid externalDirectoryOrgId);

		// Token: 0x06000752 RID: 1874
		MsoTenantCookieContainer GetMsoTenantCookieContainer(Guid contextId);

		// Token: 0x06000753 RID: 1875
		Result<ADRawEntry>[] ReadMultipleOrganizationProperties(ADObjectId[] organizationOUIds, PropertyDefinition[] properties);

		// Token: 0x06000754 RID: 1876
		T GetDefaultFilteringConfiguration<T>() where T : ADConfigurationObject, new();

		// Token: 0x06000755 RID: 1877
		bool IsTenantLockedOut();
	}
}
