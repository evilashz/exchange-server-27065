using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000237 RID: 567
	[Flags]
	internal enum DelegateSaveState
	{
		// Token: 0x040010E0 RID: 4320
		None = 0,
		// Token: 0x040010E1 RID: 4321
		DelegateForwardingRule = 1,
		// Token: 0x040010E2 RID: 4322
		ADSendOnBehalf = 2,
		// Token: 0x040010E3 RID: 4323
		FolderPermissions = 4,
		// Token: 0x040010E4 RID: 4324
		FreeBusyDelegateInfo = 8,
		// Token: 0x040010E5 RID: 4325
		RestoreDelegateForwardingRule = 16,
		// Token: 0x040010E6 RID: 4326
		RestoreADSendOnBehalf = 32,
		// Token: 0x040010E7 RID: 4327
		RestoreFolderPermissions = 64,
		// Token: 0x040010E8 RID: 4328
		RestoreFreeBusyDelegateInfo = 128
	}
}
