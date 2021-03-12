using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000528 RID: 1320
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PropertyAggregationContext
	{
		// Token: 0x060038D2 RID: 14546 RVA: 0x000E8F2D File Offset: 0x000E712D
		public PropertyAggregationContext(IList<IStorePropertyBag> sources)
		{
			Util.ThrowOnNullArgument(sources, "sources");
			this.sources = sources;
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x060038D3 RID: 14547 RVA: 0x000E8F47 File Offset: 0x000E7147
		public IList<IStorePropertyBag> Sources
		{
			get
			{
				return this.sources;
			}
		}

		// Token: 0x04001E2E RID: 7726
		private readonly IList<IStorePropertyBag> sources;
	}
}
