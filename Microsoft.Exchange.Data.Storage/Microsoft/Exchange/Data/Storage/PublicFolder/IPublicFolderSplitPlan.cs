using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000940 RID: 2368
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPublicFolderSplitPlan
	{
		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x0600583B RID: 22587
		// (set) Token: 0x0600583C RID: 22588
		List<SplitPlanFolder> FoldersToSplit { get; set; }

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x0600583D RID: 22589
		// (set) Token: 0x0600583E RID: 22590
		ulong TotalSizeOccupied { get; set; }

		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x0600583F RID: 22591
		// (set) Token: 0x06005840 RID: 22592
		ulong TotalSizeToSplit { get; set; }
	}
}
