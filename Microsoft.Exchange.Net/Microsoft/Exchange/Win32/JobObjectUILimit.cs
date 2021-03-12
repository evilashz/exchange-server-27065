using System;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B41 RID: 2881
	[Flags]
	internal enum JobObjectUILimit : uint
	{
		// Token: 0x040035E7 RID: 13799
		Default = 0U,
		// Token: 0x040035E8 RID: 13800
		Handles = 1U,
		// Token: 0x040035E9 RID: 13801
		ReadClipboard = 2U,
		// Token: 0x040035EA RID: 13802
		SystemParameters = 8U,
		// Token: 0x040035EB RID: 13803
		WriteClipboard = 4U,
		// Token: 0x040035EC RID: 13804
		Desktop = 64U,
		// Token: 0x040035ED RID: 13805
		DisplaySettings = 16U,
		// Token: 0x040035EE RID: 13806
		ExitWindows = 128U,
		// Token: 0x040035EF RID: 13807
		GlobalAtoms = 32U
	}
}
