using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F3C RID: 3900
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAuditRecordStrategy<T>
	{
		// Token: 0x1700238B RID: 9099
		// (get) Token: 0x06008617 RID: 34327
		SortBy[] QuerySortOrder { get; }

		// Token: 0x1700238C RID: 9100
		// (get) Token: 0x06008618 RID: 34328
		PropertyDefinition[] Columns { get; }

		// Token: 0x06008619 RID: 34329
		bool RecordFilter(IReadOnlyPropertyBag propertyBag, out bool stopNow);

		// Token: 0x0600861A RID: 34330
		T Convert(IReadOnlyPropertyBag propertyBag);
	}
}
