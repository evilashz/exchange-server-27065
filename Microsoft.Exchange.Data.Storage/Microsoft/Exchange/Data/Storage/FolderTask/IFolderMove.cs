using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.FolderTask
{
	// Token: 0x02000960 RID: 2400
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFolderMove
	{
		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x060058ED RID: 22765
		List<FolderInfo> CandidateFolders { get; }

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x060058EE RID: 22766
		ulong TotalSizeOccupiedByTarget { get; }

		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x060058EF RID: 22767
		ulong TotalSizeToMove { get; }
	}
}
