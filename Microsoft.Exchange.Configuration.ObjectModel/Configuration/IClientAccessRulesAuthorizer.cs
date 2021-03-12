using System;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration
{
	// Token: 0x020000B2 RID: 178
	public interface IClientAccessRulesAuthorizer
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600072C RID: 1836
		ClientAccessProtocol Protocol { get; }

		// Token: 0x0600072D RID: 1837
		void SafeAppendGenericInfo(HttpContext httpContext, string key, string value);

		// Token: 0x0600072E RID: 1838
		void ResponseToError(HttpContext httpContext);

		// Token: 0x0600072F RID: 1839
		OrganizationId GetUserOrganization();
	}
}
