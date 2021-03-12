using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002AD RID: 685
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum X509KeyStorageFlags
	{
		// Token: 0x04000D9E RID: 3486
		DefaultKeySet = 0,
		// Token: 0x04000D9F RID: 3487
		UserKeySet = 1,
		// Token: 0x04000DA0 RID: 3488
		MachineKeySet = 2,
		// Token: 0x04000DA1 RID: 3489
		Exportable = 4,
		// Token: 0x04000DA2 RID: 3490
		UserProtected = 8,
		// Token: 0x04000DA3 RID: 3491
		PersistKeySet = 16,
		// Token: 0x04000DA4 RID: 3492
		EphemeralKeySet = 32
	}
}
