using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.SearchService
{
	// Token: 0x02000246 RID: 582
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchTooManyKeywordsException : LocalizedException
	{
		// Token: 0x060010C7 RID: 4295 RVA: 0x0004CD69 File Offset: 0x0004AF69
		public SearchTooManyKeywordsException(int maxNumberOfKeywords) : base(Strings.SearchTooManyKeywords(maxNumberOfKeywords))
		{
		}
	}
}
