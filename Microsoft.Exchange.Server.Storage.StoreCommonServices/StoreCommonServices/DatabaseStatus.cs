using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000052 RID: 82
	[Flags]
	internal enum DatabaseStatus
	{
		// Token: 0x04000292 RID: 658
		OffLine = 0,
		// Token: 0x04000293 RID: 659
		OnLine = 1,
		// Token: 0x04000294 RID: 660
		BackupInProgress = 2,
		// Token: 0x04000295 RID: 661
		InInteg = 4,
		// Token: 0x04000296 RID: 662
		IsPublic = 8,
		// Token: 0x04000297 RID: 663
		ForRecovery = 16,
		// Token: 0x04000298 RID: 664
		Maintenance = 32,
		// Token: 0x04000299 RID: 665
		MountInProgress = 64,
		// Token: 0x0400029A RID: 666
		AttachedReadOnly = 128
	}
}
