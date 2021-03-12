using System;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000111 RID: 273
	internal interface IGlobalLocatorServiceReader
	{
		// Token: 0x06000B7F RID: 2943
		bool TenantExists(Guid tenantId, Namespace[] ns);

		// Token: 0x06000B80 RID: 2944
		bool DomainExists(SmtpDomain domain, Namespace[] ns);

		// Token: 0x06000B81 RID: 2945
		FindTenantResult FindTenant(Guid tenantId, TenantProperty[] tenantProperties);

		// Token: 0x06000B82 RID: 2946
		FindDomainResult FindDomain(SmtpDomain domain, DomainProperty[] domainProperties, TenantProperty[] tenantProperties);

		// Token: 0x06000B83 RID: 2947
		FindDomainsResult FindDomains(SmtpDomain[] domains, DomainProperty[] domainProperties, TenantProperty[] tenantProperties);

		// Token: 0x06000B84 RID: 2948
		IAsyncResult BeginTenantExists(Guid tenantId, Namespace[] ns, AsyncCallback callback, object asyncState);

		// Token: 0x06000B85 RID: 2949
		IAsyncResult BeginDomainExists(SmtpDomain domain, Namespace[] ns, AsyncCallback callback, object asyncState);

		// Token: 0x06000B86 RID: 2950
		IAsyncResult BeginFindTenant(Guid tenantId, TenantProperty[] tenantProperties, AsyncCallback callback, object asyncState);

		// Token: 0x06000B87 RID: 2951
		IAsyncResult BeginFindDomain(SmtpDomain domain, DomainProperty[] domainProperties, TenantProperty[] tenantProperties, AsyncCallback callback, object asyncState);

		// Token: 0x06000B88 RID: 2952
		IAsyncResult BeginFindDomains(SmtpDomain[] domains, DomainProperty[] domainProperties, TenantProperty[] tenantProperties, AsyncCallback callback, object asyncState);

		// Token: 0x06000B89 RID: 2953
		bool EndTenantExists(IAsyncResult asyncResult);

		// Token: 0x06000B8A RID: 2954
		bool EndDomainExists(IAsyncResult asyncResult);

		// Token: 0x06000B8B RID: 2955
		FindTenantResult EndFindTenant(IAsyncResult asyncResult);

		// Token: 0x06000B8C RID: 2956
		FindDomainResult EndFindDomain(IAsyncResult asyncResult);

		// Token: 0x06000B8D RID: 2957
		FindDomainsResult EndFindDomains(IAsyncResult asyncResult);
	}
}
