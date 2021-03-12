using System;

namespace Microsoft.Exchange.Security.RightsManagement.Protectors
{
	// Token: 0x020009AE RID: 2478
	internal enum MsoIpiStatus
	{
		// Token: 0x04002DD8 RID: 11736
		Unknown,
		// Token: 0x04002DD9 RID: 11737
		ProtectSuccess,
		// Token: 0x04002DDA RID: 11738
		UnprotectSuccess,
		// Token: 0x04002DDB RID: 11739
		AlreadyProtected,
		// Token: 0x04002DDC RID: 11740
		CantProtect,
		// Token: 0x04002DDD RID: 11741
		AlreadyUnprotected,
		// Token: 0x04002DDE RID: 11742
		CantUnprotect,
		// Token: 0x04002DDF RID: 11743
		NotOwner,
		// Token: 0x04002DE0 RID: 11744
		NotMyFile = 9,
		// Token: 0x04002DE1 RID: 11745
		FileCorrupt,
		// Token: 0x04002DE2 RID: 11746
		PlatformIrmFailed,
		// Token: 0x04002DE3 RID: 11747
		BadInstall
	}
}
