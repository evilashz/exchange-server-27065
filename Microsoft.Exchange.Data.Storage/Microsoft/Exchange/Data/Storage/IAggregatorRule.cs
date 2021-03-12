using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008EB RID: 2283
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAggregatorRule
	{
		// Token: 0x060055AD RID: 21933
		void BeginAggregation();

		// Token: 0x060055AE RID: 21934
		void EndAggregation();

		// Token: 0x060055AF RID: 21935
		void AddToAggregation(IStorePropertyBag propertyBag);
	}
}
