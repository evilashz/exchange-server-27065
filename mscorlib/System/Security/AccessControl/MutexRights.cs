using System;

namespace System.Security.AccessControl
{
	// Token: 0x0200021E RID: 542
	[Flags]
	public enum MutexRights
	{
		// Token: 0x04000B48 RID: 2888
		Modify = 1,
		// Token: 0x04000B49 RID: 2889
		Delete = 65536,
		// Token: 0x04000B4A RID: 2890
		ReadPermissions = 131072,
		// Token: 0x04000B4B RID: 2891
		ChangePermissions = 262144,
		// Token: 0x04000B4C RID: 2892
		TakeOwnership = 524288,
		// Token: 0x04000B4D RID: 2893
		Synchronize = 1048576,
		// Token: 0x04000B4E RID: 2894
		FullControl = 2031617
	}
}
