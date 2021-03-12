using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.SearchService
{
	// Token: 0x02000244 RID: 580
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchOverBudgetException : LocalizedException
	{
		// Token: 0x060010C5 RID: 4293 RVA: 0x0004CD4D File Offset: 0x0004AF4D
		public SearchOverBudgetException(int budget) : base(Strings.SearchOverBudget(budget))
		{
		}
	}
}
