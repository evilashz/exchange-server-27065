using System;

namespace System.Security.AccessControl
{
	// Token: 0x02000214 RID: 532
	[Flags]
	public enum EventWaitHandleRights
	{
		// Token: 0x04000B27 RID: 2855
		Modify = 2,
		// Token: 0x04000B28 RID: 2856
		Delete = 65536,
		// Token: 0x04000B29 RID: 2857
		ReadPermissions = 131072,
		// Token: 0x04000B2A RID: 2858
		ChangePermissions = 262144,
		// Token: 0x04000B2B RID: 2859
		TakeOwnership = 524288,
		// Token: 0x04000B2C RID: 2860
		Synchronize = 1048576,
		// Token: 0x04000B2D RID: 2861
		FullControl = 2031619
	}
}
