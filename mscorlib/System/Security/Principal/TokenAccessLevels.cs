using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x020002FA RID: 762
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TokenAccessLevels
	{
		// Token: 0x04000F58 RID: 3928
		AssignPrimary = 1,
		// Token: 0x04000F59 RID: 3929
		Duplicate = 2,
		// Token: 0x04000F5A RID: 3930
		Impersonate = 4,
		// Token: 0x04000F5B RID: 3931
		Query = 8,
		// Token: 0x04000F5C RID: 3932
		QuerySource = 16,
		// Token: 0x04000F5D RID: 3933
		AdjustPrivileges = 32,
		// Token: 0x04000F5E RID: 3934
		AdjustGroups = 64,
		// Token: 0x04000F5F RID: 3935
		AdjustDefault = 128,
		// Token: 0x04000F60 RID: 3936
		AdjustSessionId = 256,
		// Token: 0x04000F61 RID: 3937
		Read = 131080,
		// Token: 0x04000F62 RID: 3938
		Write = 131296,
		// Token: 0x04000F63 RID: 3939
		AllAccess = 983551,
		// Token: 0x04000F64 RID: 3940
		MaximumAllowed = 33554432
	}
}
