using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000124 RID: 292
	internal interface IGlobalLocatorServiceWriter
	{
		// Token: 0x06000C38 RID: 3128
		void SaveTenant(Guid tenantId, KeyValuePair<TenantProperty, PropertyValue>[] properties);

		// Token: 0x06000C39 RID: 3129
		void SaveDomain(SmtpDomain domain, string domainKey, Guid tenantId, KeyValuePair<DomainProperty, PropertyValue>[] properties);

		// Token: 0x06000C3A RID: 3130
		void SaveDomain(SmtpDomain domain, bool isInitialDomain, Guid tenantId, KeyValuePair<DomainProperty, PropertyValue>[] properties);

		// Token: 0x06000C3B RID: 3131
		void DeleteTenant(Guid tenantId, Namespace ns);

		// Token: 0x06000C3C RID: 3132
		void DeleteDomain(SmtpDomain domain, Guid tenantId, Namespace ns);

		// Token: 0x06000C3D RID: 3133
		IAsyncResult BeginSaveTenant(Guid tenantId, KeyValuePair<TenantProperty, PropertyValue>[] properties, AsyncCallback callback, object asyncState);

		// Token: 0x06000C3E RID: 3134
		IAsyncResult BeginSaveDomain(SmtpDomain domain, string domainKey, Guid tenantId, KeyValuePair<DomainProperty, PropertyValue>[] properties, AsyncCallback callback, object asyncState);

		// Token: 0x06000C3F RID: 3135
		IAsyncResult BeginDeleteTenant(Guid tenantId, Namespace ns, AsyncCallback callback, object asyncState);

		// Token: 0x06000C40 RID: 3136
		IAsyncResult BeginDeleteDomain(SmtpDomain domain, Guid tenantId, Namespace ns, AsyncCallback callback, object asyncState);

		// Token: 0x06000C41 RID: 3137
		void EndSaveTenant(IAsyncResult asyncResult);

		// Token: 0x06000C42 RID: 3138
		void EndSaveDomain(IAsyncResult asyncResult);

		// Token: 0x06000C43 RID: 3139
		void EndDeleteTenant(IAsyncResult asyncResult);

		// Token: 0x06000C44 RID: 3140
		void EndDeleteDomain(IAsyncResult asyncResult);
	}
}
