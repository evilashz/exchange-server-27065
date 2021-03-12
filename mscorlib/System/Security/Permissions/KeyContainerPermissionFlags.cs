using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002E7 RID: 743
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum KeyContainerPermissionFlags
	{
		// Token: 0x04000EA3 RID: 3747
		NoFlags = 0,
		// Token: 0x04000EA4 RID: 3748
		Create = 1,
		// Token: 0x04000EA5 RID: 3749
		Open = 2,
		// Token: 0x04000EA6 RID: 3750
		Delete = 4,
		// Token: 0x04000EA7 RID: 3751
		Import = 16,
		// Token: 0x04000EA8 RID: 3752
		Export = 32,
		// Token: 0x04000EA9 RID: 3753
		Sign = 256,
		// Token: 0x04000EAA RID: 3754
		Decrypt = 512,
		// Token: 0x04000EAB RID: 3755
		ViewAcl = 4096,
		// Token: 0x04000EAC RID: 3756
		ChangeAcl = 8192,
		// Token: 0x04000EAD RID: 3757
		AllFlags = 13111
	}
}
