using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000835 RID: 2101
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IContentIndexingSession
	{
		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06004E37 RID: 20023
		// (set) Token: 0x06004E38 RID: 20024
		bool EnableWordBreak { get; set; }

		// Token: 0x06004E39 RID: 20025
		void OnBeforeItemChange(ItemChangeOperation operation, ICoreItem coreItem);
	}
}
