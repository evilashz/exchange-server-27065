using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.FfoReporting.Providers
{
	// Token: 0x02000402 RID: 1026
	internal interface IAuthenticationProvider
	{
		// Token: 0x06002419 RID: 9241
		Guid GetExternalDirectoryOrganizationId(OrganizationId currentOrganizationId);

		// Token: 0x0600241A RID: 9242
		void ResolveOrganizationId(OrganizationIdParameter organization, Task task);

		// Token: 0x0600241B RID: 9243
		IConfigDataProvider CreateConfigSession(OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId);
	}
}
