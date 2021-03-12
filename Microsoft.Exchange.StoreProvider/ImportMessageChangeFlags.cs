using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200006A RID: 106
	[Flags]
	internal enum ImportMessageChangeFlags
	{
		// Token: 0x0400047C RID: 1148
		None = 0,
		// Token: 0x0400047D RID: 1149
		Associated = 16,
		// Token: 0x0400047E RID: 1150
		FailOnConflict = 64,
		// Token: 0x0400047F RID: 1151
		NewMessage = 2048
	}
}
