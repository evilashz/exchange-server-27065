using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002AF RID: 687
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum EnvironmentPermissionAccess
	{
		// Token: 0x04000DB4 RID: 3508
		NoAccess = 0,
		// Token: 0x04000DB5 RID: 3509
		Read = 1,
		// Token: 0x04000DB6 RID: 3510
		Write = 2,
		// Token: 0x04000DB7 RID: 3511
		AllAccess = 3
	}
}
