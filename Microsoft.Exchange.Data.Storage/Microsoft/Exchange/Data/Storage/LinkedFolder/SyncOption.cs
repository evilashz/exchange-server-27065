using System;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x020009A4 RID: 2468
	[Flags]
	internal enum SyncOption : uint
	{
		// Token: 0x04003248 RID: 12872
		Default = 0U,
		// Token: 0x04003249 RID: 12873
		CurrentDocumentLibsOnly = 1U,
		// Token: 0x0400324A RID: 12874
		RefreshTeamMailboxCacheEntry = 2U,
		// Token: 0x0400324B RID: 12875
		IgnoreNextAllowedSyncTimeRestriction = 4U,
		// Token: 0x0400324C RID: 12876
		FullSync = 8U
	}
}
