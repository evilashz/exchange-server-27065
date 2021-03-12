using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000009 RID: 9
	internal interface ICrawlerFolderIterator
	{
		// Token: 0x06000018 RID: 24
		IEnumerable<StoreObjectId> GetFolders(MailboxSession session);
	}
}
