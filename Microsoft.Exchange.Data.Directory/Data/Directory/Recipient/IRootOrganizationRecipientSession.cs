using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000208 RID: 520
	internal interface IRootOrganizationRecipientSession : IRecipientSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x06001B4C RID: 6988
		SecurityIdentifier GetExchangeServersUsgSid();
	}
}
