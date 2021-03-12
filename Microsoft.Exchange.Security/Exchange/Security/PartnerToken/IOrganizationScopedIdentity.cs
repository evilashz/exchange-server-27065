using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Security.PartnerToken
{
	// Token: 0x02000093 RID: 147
	internal interface IOrganizationScopedIdentity : IIdentity
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004F2 RID: 1266
		OrganizationId OrganizationId { get; }

		// Token: 0x060004F3 RID: 1267
		IStandardBudget AcquireBudget();
	}
}
