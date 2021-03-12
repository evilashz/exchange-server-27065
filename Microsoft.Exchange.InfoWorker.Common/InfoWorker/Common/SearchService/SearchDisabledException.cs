using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.SearchService
{
	// Token: 0x02000243 RID: 579
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchDisabledException : LocalizedException
	{
		// Token: 0x060010C4 RID: 4292 RVA: 0x0004CD40 File Offset: 0x0004AF40
		public SearchDisabledException() : base(Strings.SearchDisabled)
		{
		}
	}
}
