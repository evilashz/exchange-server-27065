using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000018 RID: 24
	[Flags]
	internal enum ConnectionFlags
	{
		// Token: 0x0400003F RID: 63
		None = 0,
		// Token: 0x04000040 RID: 64
		UseAdminPrivilege = 1,
		// Token: 0x04000041 RID: 65
		UseDelegatedAuthPrivilege = 256,
		// Token: 0x04000042 RID: 66
		UseTransportPrivilege = 1024,
		// Token: 0x04000043 RID: 67
		UseReadOnlyPrivilege = 2048,
		// Token: 0x04000044 RID: 68
		UseReadWritePrivilege = 4096,
		// Token: 0x04000045 RID: 69
		IgnoreNoPublicFolders = 32768,
		// Token: 0x04000046 RID: 70
		RemoteSystemService = 4194304
	}
}
