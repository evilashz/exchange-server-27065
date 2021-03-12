using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002ED RID: 749
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum RegistryPermissionAccess
	{
		// Token: 0x04000EBD RID: 3773
		NoAccess = 0,
		// Token: 0x04000EBE RID: 3774
		Read = 1,
		// Token: 0x04000EBF RID: 3775
		Write = 2,
		// Token: 0x04000EC0 RID: 3776
		Create = 4,
		// Token: 0x04000EC1 RID: 3777
		AllAccess = 7
	}
}
