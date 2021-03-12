using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x0200022C RID: 556
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchQueryEmptyException : LocalizedException
	{
		// Token: 0x06000F34 RID: 3892 RVA: 0x00043F7A File Offset: 0x0004217A
		public SearchQueryEmptyException() : base(Strings.SearchQueryEmpty)
		{
		}
	}
}
