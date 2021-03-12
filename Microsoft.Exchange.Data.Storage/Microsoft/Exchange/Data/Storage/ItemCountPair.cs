using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000709 RID: 1801
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct ItemCountPair
	{
		// Token: 0x06004752 RID: 18258 RVA: 0x0012F858 File Offset: 0x0012DA58
		public ItemCountPair(long itemCount, long unreadItemCount)
		{
			this.ItemCount = itemCount;
			this.UnreadItemCount = unreadItemCount;
		}

		// Token: 0x04002701 RID: 9985
		public readonly long ItemCount;

		// Token: 0x04002702 RID: 9986
		public readonly long UnreadItemCount;
	}
}
