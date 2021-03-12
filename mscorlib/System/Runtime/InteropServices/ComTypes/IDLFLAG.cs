using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A12 RID: 2578
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum IDLFLAG : short
	{
		// Token: 0x04002CE6 RID: 11494
		[__DynamicallyInvokable]
		IDLFLAG_NONE = 0,
		// Token: 0x04002CE7 RID: 11495
		[__DynamicallyInvokable]
		IDLFLAG_FIN = 1,
		// Token: 0x04002CE8 RID: 11496
		[__DynamicallyInvokable]
		IDLFLAG_FOUT = 2,
		// Token: 0x04002CE9 RID: 11497
		[__DynamicallyInvokable]
		IDLFLAG_FLCID = 4,
		// Token: 0x04002CEA RID: 11498
		[__DynamicallyInvokable]
		IDLFLAG_FRETVAL = 8
	}
}
