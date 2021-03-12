using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x0200022A RID: 554
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchFolderTimeoutException : LocalizedException
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x00043F5F File Offset: 0x0004215F
		public SearchFolderTimeoutException(string mailbox) : base(Strings.SearchFolderTimeout(mailbox))
		{
		}
	}
}
