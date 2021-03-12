using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000970 RID: 2416
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IOAuthCredentialFactory
	{
		// Token: 0x0600599A RID: 22938
		ICredentials Get(OrganizationId organizationId);
	}
}
