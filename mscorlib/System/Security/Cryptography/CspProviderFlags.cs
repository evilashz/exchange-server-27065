using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000253 RID: 595
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum CspProviderFlags
	{
		// Token: 0x04000BFD RID: 3069
		NoFlags = 0,
		// Token: 0x04000BFE RID: 3070
		UseMachineKeyStore = 1,
		// Token: 0x04000BFF RID: 3071
		UseDefaultKeyContainer = 2,
		// Token: 0x04000C00 RID: 3072
		UseNonExportableKey = 4,
		// Token: 0x04000C01 RID: 3073
		UseExistingKey = 8,
		// Token: 0x04000C02 RID: 3074
		UseArchivableKey = 16,
		// Token: 0x04000C03 RID: 3075
		UseUserProtectedKey = 32,
		// Token: 0x04000C04 RID: 3076
		NoPrompt = 64,
		// Token: 0x04000C05 RID: 3077
		CreateEphemeralKey = 128
	}
}
