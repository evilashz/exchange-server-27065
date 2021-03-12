using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000E0 RID: 224
	[Flags]
	public enum SaveMessageChangesFlags : byte
	{
		// Token: 0x040005A3 RID: 1443
		None = 0,
		// Token: 0x040005A4 RID: 1444
		IMAPIDChange = 1,
		// Token: 0x040005A5 RID: 1445
		ForceSave = 2,
		// Token: 0x040005A6 RID: 1446
		SkipMailboxQuotaCheck = 4,
		// Token: 0x040005A7 RID: 1447
		SkipFolderQuotaCheck = 8,
		// Token: 0x040005A8 RID: 1448
		SkipQuotaCheck = 12,
		// Token: 0x040005A9 RID: 1449
		NonFatalDuplicateKey = 16,
		// Token: 0x040005AA RID: 1450
		ForceCreatedEventForCopy = 32,
		// Token: 0x040005AB RID: 1451
		TimerEventFired = 64
	}
}
