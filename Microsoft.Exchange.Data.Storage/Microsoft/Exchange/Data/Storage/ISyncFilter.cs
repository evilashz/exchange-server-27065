using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E23 RID: 3619
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISyncFilter
	{
		// Token: 0x17002180 RID: 8576
		// (get) Token: 0x06007D23 RID: 32035
		string Id { get; }

		// Token: 0x06007D24 RID: 32036
		bool IsItemInFilter(ISyncItemId itemId);

		// Token: 0x06007D25 RID: 32037
		void UpdateFilterState(SyncOperation syncOperation);
	}
}
