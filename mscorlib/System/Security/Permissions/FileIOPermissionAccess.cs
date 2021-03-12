using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002B4 RID: 692
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FileIOPermissionAccess
	{
		// Token: 0x04000DC2 RID: 3522
		NoAccess = 0,
		// Token: 0x04000DC3 RID: 3523
		Read = 1,
		// Token: 0x04000DC4 RID: 3524
		Write = 2,
		// Token: 0x04000DC5 RID: 3525
		Append = 4,
		// Token: 0x04000DC6 RID: 3526
		PathDiscovery = 8,
		// Token: 0x04000DC7 RID: 3527
		AllAccess = 15
	}
}
