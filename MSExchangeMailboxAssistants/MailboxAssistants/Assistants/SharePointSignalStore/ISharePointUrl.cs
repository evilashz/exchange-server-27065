using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x0200021F RID: 543
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISharePointUrl
	{
		// Token: 0x060014A8 RID: 5288
		string GetUrl(IExchangePrincipal userIdentity, IRecipientSession recipientSession);
	}
}
