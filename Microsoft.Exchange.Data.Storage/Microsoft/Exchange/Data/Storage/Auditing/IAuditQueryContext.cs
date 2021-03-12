using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F40 RID: 3904
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAuditQueryContext<TFilter> : IDisposable
	{
		// Token: 0x06008632 RID: 34354
		IAsyncResult BeginAuditLogQuery(TFilter queryFilter, int maximumResultsCount);

		// Token: 0x06008633 RID: 34355
		IEnumerable<T> EndAuditLogQuery<T>(IAsyncResult asyncResult, IAuditQueryStrategy<T> queryStrategy);
	}
}
