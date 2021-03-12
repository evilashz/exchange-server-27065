using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.SearchService
{
	// Token: 0x02000245 RID: 581
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchTooManyMailboxesException : LocalizedException
	{
		// Token: 0x060010C6 RID: 4294 RVA: 0x0004CD5B File Offset: 0x0004AF5B
		public SearchTooManyMailboxesException(int maxNumberOfMailboxes) : base(Strings.SearchTooManyMailboxes(maxNumberOfMailboxes))
		{
		}
	}
}
