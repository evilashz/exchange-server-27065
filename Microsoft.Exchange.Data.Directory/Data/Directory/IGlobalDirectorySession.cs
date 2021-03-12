using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200013A RID: 314
	internal interface IGlobalDirectorySession
	{
		// Token: 0x06000CD9 RID: 3289
		string GetRedirectServer(string memberName);

		// Token: 0x06000CDA RID: 3290
		bool TryGetRedirectServer(string memberName, out string fqdn);

		// Token: 0x06000CDB RID: 3291
		string GetRedirectServer(Guid orgGuid);

		// Token: 0x06000CDC RID: 3292
		bool TryGetMSAUserMemberName(string msaUserNetID, out string msaUserMemberName);

		// Token: 0x06000CDD RID: 3293
		bool TryGetRedirectServer(Guid orgGuid, out string fqdn);

		// Token: 0x06000CDE RID: 3294
		bool TryGetDomainFlag(string domainFqdn, GlsDomainFlags flag, out bool value);

		// Token: 0x06000CDF RID: 3295
		void SetDomainFlag(string domainFqdn, GlsDomainFlags flag, bool value);

		// Token: 0x06000CE0 RID: 3296
		bool TryGetTenantFlag(Guid externalDirectoryOrganizationId, GlsTenantFlags tenantFlags, out bool value);

		// Token: 0x06000CE1 RID: 3297
		void SetTenantFlag(Guid externalDirectoryOrganizationId, GlsTenantFlags tenantFlags, bool value);

		// Token: 0x06000CE2 RID: 3298
		void AddTenant(Guid externalDirectoryOrganizationId, string resourceForestFqdn, string accountForestFqdn, string smtpNextHopDomain, GlsTenantFlags tenantFlags, string tenantContainerCN);

		// Token: 0x06000CE3 RID: 3299
		void AddTenant(Guid externalDirectoryOrganizationId, CustomerType tenantType, string ffoRegion, string ffoVersion);

		// Token: 0x06000CE4 RID: 3300
		void AddMSAUser(string msaUserNetID, string msaUserMemberName, Guid externalDirectoryOrganizationId);

		// Token: 0x06000CE5 RID: 3301
		void UpdateTenant(Guid externalDirectoryOrganizationId, string resourceForestFqdn, string accountForestFqdn, string smtpNextHopDomain, GlsTenantFlags tenantFlags, string tenantContainerCN);

		// Token: 0x06000CE6 RID: 3302
		void UpdateMSAUser(string msaUserNetID, string msaUserMemberName, Guid externalDirectoryOrganizationId);

		// Token: 0x06000CE7 RID: 3303
		void RemoveTenant(Guid externalDirectoryOrganizationId);

		// Token: 0x06000CE8 RID: 3304
		void RemoveMSAUser(string msaUserNetID);

		// Token: 0x06000CE9 RID: 3305
		bool TryGetTenantType(Guid externalDirectoryOrganizationId, out CustomerType tenantType);

		// Token: 0x06000CEA RID: 3306
		bool TryGetTenantForestsByDomain(string domainFqdn, out Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string smtpNextHopDomain, out string tenantContainerCN, out bool dataFromOfflineService);

		// Token: 0x06000CEB RID: 3307
		bool TryGetTenantForestsByOrgGuid(Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string tenantContainerCN, out bool dataFromOfflineService);

		// Token: 0x06000CEC RID: 3308
		bool TryGetTenantForestsByMSAUserNetID(string msaUserNetID, out Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string tenantContainerCN);

		// Token: 0x06000CED RID: 3309
		void SetAccountForest(Guid externalDirectoryOrganizationId, string value, string tenantContainerCN = null);

		// Token: 0x06000CEE RID: 3310
		void SetResourceForest(Guid externalDirectoryOrganizationId, string value);

		// Token: 0x06000CEF RID: 3311
		void SetTenantVersion(Guid externalDirectoryOrganizationId, string newTenantVersion);

		// Token: 0x06000CF0 RID: 3312
		bool TryGetTenantDomains(Guid externalDirectoryOrganizationId, out string[] acceptedDomainFqdns);

		// Token: 0x06000CF1 RID: 3313
		void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain);

		// Token: 0x06000CF2 RID: 3314
		void UpdateAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn);

		// Token: 0x06000CF3 RID: 3315
		void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain, bool nego2Enabled, bool oauth2ClientProfileEnabled);

		// Token: 0x06000CF4 RID: 3316
		void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain, string ffoRegion, string ffoServiceVersion);

		// Token: 0x06000CF5 RID: 3317
		void RemoveAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn);

		// Token: 0x06000CF6 RID: 3318
		void SetDomainVersion(Guid externalDirectoryOrganizationId, string domainFqdn, string newDomainVersion);

		// Token: 0x06000CF7 RID: 3319
		IEnumerable<string> GetDomainNamesProvisionedByEXO(IEnumerable<SmtpDomain> domains);

		// Token: 0x06000CF8 RID: 3320
		IAsyncResult BeginGetFfoTenantAttributionPropertiesByDomain(SmtpDomain domain, object clientAsyncState, AsyncCallback clientCallback);

		// Token: 0x06000CF9 RID: 3321
		bool TryEndGetFfoTenantAttributionPropertiesByDomain(IAsyncResult asyncResult, out string ffoRegion, out string ffoVersion, out Guid externalDirectoryOrganizationId, out string exoNextHop, out CustomerType tenantType, out DomainIPv6State ipv6Enabled, out string exoResourceForest, out string exoAccountForest, out string exoTenantContainer);

		// Token: 0x06000CFA RID: 3322
		IAsyncResult BeginGetFfoTenantAttributionPropertiesByOrgId(Guid externalDirectoryOrganizationId, object clientAsyncState, AsyncCallback clientCallback);

		// Token: 0x06000CFB RID: 3323
		bool TryEndGetFfoTenantAttributionPropertiesByOrgId(IAsyncResult asyncResult, out string ffoRegion, out string exoNextHop, out CustomerType tenantType, out string exoResourceForest, out string exoAccountForest, out string exoTenantContainer);

		// Token: 0x06000CFC RID: 3324
		bool TryGetFfoTenantProvisioningProperties(Guid externalDirectoryOrganizationId, out string version, out CustomerType tenantType, out string region);

		// Token: 0x06000CFD RID: 3325
		bool TenantExists(Guid externalDirectoryOrganizationId, Namespace namespaceToCheck);

		// Token: 0x06000CFE RID: 3326
		bool MSAUserExists(string msaUserNetID);
	}
}
