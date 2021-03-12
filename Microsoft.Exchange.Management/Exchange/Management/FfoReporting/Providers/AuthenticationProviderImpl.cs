using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting.Providers
{
	// Token: 0x02000403 RID: 1027
	internal class AuthenticationProviderImpl : IAuthenticationProvider
	{
		// Token: 0x0600241C RID: 9244 RVA: 0x000903D6 File Offset: 0x0008E5D6
		public Guid GetExternalDirectoryOrganizationId(OrganizationId currentOrganizationId)
		{
			return ADHelper.GetExternalDirectoryOrganizationId(currentOrganizationId);
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000903DE File Offset: 0x0008E5DE
		public void ResolveOrganizationId(OrganizationIdParameter organization, Task task)
		{
			task.CurrentOrganizationId = ADHelper.ResolveOrganization(organization, task.CurrentOrganizationId, task.ExecutingUserOrganizationId);
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000903F8 File Offset: 0x0008E5F8
		public IConfigDataProvider CreateConfigSession(OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId)
		{
			return ADHelper.CreateConfigSession(currentOrganizationId, executingUserOrganizationId);
		}
	}
}
