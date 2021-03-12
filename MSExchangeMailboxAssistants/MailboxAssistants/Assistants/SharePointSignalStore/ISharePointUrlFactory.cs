using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SharePointSignalStore;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x02000224 RID: 548
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISharePointUrlFactory
	{
		// Token: 0x060014BA RID: 5306
		ISharePointUrl CreateADWithDictFallbackSharePointUrl(ISharePointUrl defaultSharePointUrl, ISharePointUrl fallbackSharePointUrl, ILogger logger);
	}
}
