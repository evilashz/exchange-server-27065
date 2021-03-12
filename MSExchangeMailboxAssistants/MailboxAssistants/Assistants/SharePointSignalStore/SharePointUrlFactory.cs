using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SharePointSignalStore;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x0200022B RID: 555
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SharePointUrlFactory : ISharePointUrlFactory
	{
		// Token: 0x060014F9 RID: 5369 RVA: 0x00078340 File Offset: 0x00076540
		public ISharePointUrl CreateADWithDictFallbackSharePointUrl(ISharePointUrl adSharePointUrl, ISharePointUrl fallbackSharePointUrl, ILogger logger)
		{
			return new ADWithDictFallbackSharePointUrl(adSharePointUrl, fallbackSharePointUrl, logger);
		}
	}
}
