using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000089 RID: 137
	[Flags]
	internal enum MapiExtendedEventFlags : ulong
	{
		// Token: 0x0400054C RID: 1356
		None = 0UL,
		// Token: 0x0400054D RID: 1357
		NoReminderPropertyModified = 1UL,
		// Token: 0x0400054E RID: 1358
		NoContentIndexingPropertyModified = 2UL,
		// Token: 0x0400054F RID: 1359
		RetentionTagModified = 4UL,
		// Token: 0x04000550 RID: 1360
		RetentionPropertiesModified = 8UL,
		// Token: 0x04000551 RID: 1361
		MoveDestination = 16UL,
		// Token: 0x04000552 RID: 1362
		ExcludeFromHierarchy = 32UL,
		// Token: 0x04000553 RID: 1363
		AppointmentTimeNotModified = 64UL,
		// Token: 0x04000554 RID: 1364
		AppointmentFreeBusyNotModified = 128UL,
		// Token: 0x04000555 RID: 1365
		IrmRestrictedItem = 256UL,
		// Token: 0x04000556 RID: 1366
		PublicFolderMailbox = 512UL,
		// Token: 0x04000557 RID: 1367
		FolderPermissionChanged = 2048UL,
		// Token: 0x04000558 RID: 1368
		NonIPMFolder = 1073741824UL,
		// Token: 0x04000559 RID: 1369
		IPMFolder = 2147483648UL,
		// Token: 0x0400055A RID: 1370
		NeedGroupExpansion = 4294967296UL,
		// Token: 0x0400055B RID: 1371
		InferenceProcessingNeeded = 8589934592UL,
		// Token: 0x0400055C RID: 1372
		ModernRemindersChanged = 17179869184UL,
		// Token: 0x0400055D RID: 1373
		FolderIsNotPartOfContentIndexing = 34359738368UL,
		// Token: 0x0400055E RID: 1374
		TimerEventFired = 68719476736UL,
		// Token: 0x0400055F RID: 1375
		InternalAccessFolder = 137438953472UL
	}
}
