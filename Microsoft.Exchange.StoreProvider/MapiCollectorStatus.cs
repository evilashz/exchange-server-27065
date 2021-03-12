using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200007D RID: 125
	internal enum MapiCollectorStatus
	{
		// Token: 0x040004D2 RID: 1234
		Success,
		// Token: 0x040004D3 RID: 1235
		SyncClientChangeNewer = 264225,
		// Token: 0x040004D4 RID: 1236
		SyncObjectDeleted = -2147219456,
		// Token: 0x040004D5 RID: 1237
		SyncIgnore,
		// Token: 0x040004D6 RID: 1238
		SyncConflict,
		// Token: 0x040004D7 RID: 1239
		NotFound = -2147221233,
		// Token: 0x040004D8 RID: 1240
		ObjectModified = -2147221239,
		// Token: 0x040004D9 RID: 1241
		ObjectDeleted,
		// Token: 0x040004DA RID: 1242
		Failed = -2147467259
	}
}
