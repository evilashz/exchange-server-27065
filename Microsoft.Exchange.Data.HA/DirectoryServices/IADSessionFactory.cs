using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADSessionFactory
	{
		// Token: 0x06000180 RID: 384
		IADToplogyConfigurationSession CreateIgnoreInvalidRootOrgSession(bool readOnly = true);

		// Token: 0x06000181 RID: 385
		IADToplogyConfigurationSession CreatePartiallyConsistentRootOrgSession(bool readOnly = true);

		// Token: 0x06000182 RID: 386
		IADToplogyConfigurationSession CreateFullyConsistentRootOrgSession(bool readOnly = true);

		// Token: 0x06000183 RID: 387
		IADRootOrganizationRecipientSession CreateIgnoreInvalidRootOrgRecipientSession();
	}
}
