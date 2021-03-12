using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008F7 RID: 2295
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibFuncFlags
	{
		// Token: 0x040029D1 RID: 10705
		FRestricted = 1,
		// Token: 0x040029D2 RID: 10706
		FSource = 2,
		// Token: 0x040029D3 RID: 10707
		FBindable = 4,
		// Token: 0x040029D4 RID: 10708
		FRequestEdit = 8,
		// Token: 0x040029D5 RID: 10709
		FDisplayBind = 16,
		// Token: 0x040029D6 RID: 10710
		FDefaultBind = 32,
		// Token: 0x040029D7 RID: 10711
		FHidden = 64,
		// Token: 0x040029D8 RID: 10712
		FUsesGetLastError = 128,
		// Token: 0x040029D9 RID: 10713
		FDefaultCollelem = 256,
		// Token: 0x040029DA RID: 10714
		FUiDefault = 512,
		// Token: 0x040029DB RID: 10715
		FNonBrowsable = 1024,
		// Token: 0x040029DC RID: 10716
		FReplaceable = 2048,
		// Token: 0x040029DD RID: 10717
		FImmediateBind = 4096
	}
}
