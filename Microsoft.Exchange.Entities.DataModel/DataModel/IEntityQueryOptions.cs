using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x0200007D RID: 125
	public interface IEntityQueryOptions
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000366 RID: 870
		int? Skip { get; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000367 RID: 871
		IReadOnlyList<OrderByClause> OrderBy { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000368 RID: 872
		int? Take { get; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000369 RID: 873
		Expression Filter { get; }
	}
}
