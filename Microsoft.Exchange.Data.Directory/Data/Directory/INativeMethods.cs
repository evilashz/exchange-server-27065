using System;
using System.Collections.Specialized;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200015F RID: 351
	internal interface INativeMethods
	{
		// Token: 0x06000F28 RID: 3880
		string GetSiteNameHookable(bool throwOnErrorNoSite = false);

		// Token: 0x06000F29 RID: 3881
		string GetDomainNameHookable();

		// Token: 0x06000F2A RID: 3882
		string GetForestNameHookable();

		// Token: 0x06000F2B RID: 3883
		StringCollection FindAllDomainControllersHookable(string domainFqdn, string siteName);

		// Token: 0x06000F2C RID: 3884
		StringCollection FindAllGlobalCatalogsHookable(string forestFqdn, string siteName = null);

		// Token: 0x06000F2D RID: 3885
		string ResetSecureChannelDCForDomainHookable(string domainFqdn, bool throwOnError = true);

		// Token: 0x06000F2E RID: 3886
		string GetSecureChannelDCForDomainHookable(string domainFqdn, bool throwOnError = true);
	}
}
