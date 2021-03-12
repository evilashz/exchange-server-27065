using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002B2 RID: 690
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FileDialogPermissionAccess
	{
		// Token: 0x04000DBC RID: 3516
		None = 0,
		// Token: 0x04000DBD RID: 3517
		Open = 1,
		// Token: 0x04000DBE RID: 3518
		Save = 2,
		// Token: 0x04000DBF RID: 3519
		OpenSave = 3
	}
}
