using System;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000111 RID: 273
	internal class AsyncCallStateObject
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0001720C File Offset: 0x0001540C
		public AsyncCallStateObject(object callerStateObject, IPooledServiceProxy<IPolicySyncWebserviceClient> proxyToUse, TenantCookie tenantCookie = null)
		{
			ArgumentValidator.ThrowIfNull("proxyToUse", proxyToUse);
			this.CallerStateObject = callerStateObject;
			this.ProxyToUse = proxyToUse;
			this.TenantCookie = tenantCookie;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00017234 File Offset: 0x00015434
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0001723C File Offset: 0x0001543C
		public object CallerStateObject { get; private set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00017245 File Offset: 0x00015445
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x0001724D File Offset: 0x0001544D
		internal IPooledServiceProxy<IPolicySyncWebserviceClient> ProxyToUse { get; private set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00017256 File Offset: 0x00015456
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0001725E File Offset: 0x0001545E
		internal TenantCookie TenantCookie { get; private set; }
	}
}
