using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADRootOrganizationRecipientSession
	{
		// Token: 0x06000175 RID: 373
		SecurityIdentifier GetExchangeServersUsgSid();
	}
}
