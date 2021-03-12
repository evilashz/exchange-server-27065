using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002D5 RID: 725
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum ReflectionPermissionFlag
	{
		// Token: 0x04000E5B RID: 3675
		NoFlags = 0,
		// Token: 0x04000E5C RID: 3676
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		TypeInformation = 1,
		// Token: 0x04000E5D RID: 3677
		MemberAccess = 2,
		// Token: 0x04000E5E RID: 3678
		[Obsolete("This permission is no longer used by the CLR.")]
		ReflectionEmit = 4,
		// Token: 0x04000E5F RID: 3679
		[ComVisible(false)]
		RestrictedMemberAccess = 8,
		// Token: 0x04000E60 RID: 3680
		[Obsolete("This permission has been deprecated. Use PermissionState.Unrestricted to get full access.")]
		AllFlags = 7
	}
}
