using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200005F RID: 95
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupSort : SortBy
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x000384C8 File Offset: 0x000366C8
		public GroupSort(PropertyDefinition columnDefinition, SortOrder sortOrder, Aggregate aggregate) : base(columnDefinition, sortOrder)
		{
			EnumValidator.ThrowIfInvalid<Aggregate>(aggregate, "aggregate");
			this.aggregate = aggregate;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x000384E4 File Offset: 0x000366E4
		public Aggregate Aggregate
		{
			get
			{
				return this.aggregate;
			}
		}

		// Token: 0x040001E7 RID: 487
		private readonly Aggregate aggregate;
	}
}
