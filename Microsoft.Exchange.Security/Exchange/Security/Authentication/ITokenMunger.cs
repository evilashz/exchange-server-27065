using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000118 RID: 280
	internal interface ITokenMunger
	{
		// Token: 0x06000919 RID: 2329
		ClientSecurityContext MungeToken(ClientSecurityContext clientSecurityContext, OrganizationId tenantOrganizationId);

		// Token: 0x0600091A RID: 2330
		bool TryMungeToken(ClientSecurityContext clientSecurityContext, OrganizationId tenantOrganizationId, SecurityIdentifier slaveAccountSid, out ClientSecurityContext mungedClientSecurityContext);
	}
}
