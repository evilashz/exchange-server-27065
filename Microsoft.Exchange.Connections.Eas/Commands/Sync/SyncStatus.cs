using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.Sync
{
	// Token: 0x02000070 RID: 112
	[Flags]
	public enum SyncStatus
	{
		// Token: 0x040001BE RID: 446
		Success = 1,
		// Token: 0x040001BF RID: 447
		InvalidSyncKey = 1027,
		// Token: 0x040001C0 RID: 448
		ProtocolError = 4100,
		// Token: 0x040001C1 RID: 449
		ServerError = 517,
		// Token: 0x040001C2 RID: 450
		ErrorInClientServerConversion = 4102,
		// Token: 0x040001C3 RID: 451
		Conflict = 263,
		// Token: 0x040001C4 RID: 452
		SyncItemNotFound = 4104,
		// Token: 0x040001C5 RID: 453
		OutOfDisk = 265,
		// Token: 0x040001C6 RID: 454
		FolderHierarchyChanged = 2060,
		// Token: 0x040001C7 RID: 455
		IncompleteSyncCommand = 4109,
		// Token: 0x040001C8 RID: 456
		InvalidWaitTime = 4110,
		// Token: 0x040001C9 RID: 457
		SyncTooManyFolders = 4111,
		// Token: 0x040001CA RID: 458
		Retry = 272,
		// Token: 0x040001CB RID: 459
		ServerBusy = 8302,
		// Token: 0x040001CC RID: 460
		CompositeStatusError = 510
	}
}
