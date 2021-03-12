using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000514 RID: 1300
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AggregationExtension
	{
		// Token: 0x060037EA RID: 14314 RVA: 0x000E2312 File Offset: 0x000E0512
		public virtual void BeforeAggregation(IList<IStorePropertyBag> sources)
		{
		}

		// Token: 0x060037EB RID: 14315
		public abstract PropertyAggregationContext GetPropertyAggregationContext(IList<IStorePropertyBag> sources);
	}
}
