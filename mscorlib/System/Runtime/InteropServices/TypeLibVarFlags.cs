using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008F8 RID: 2296
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibVarFlags
	{
		// Token: 0x040029DF RID: 10719
		FReadOnly = 1,
		// Token: 0x040029E0 RID: 10720
		FSource = 2,
		// Token: 0x040029E1 RID: 10721
		FBindable = 4,
		// Token: 0x040029E2 RID: 10722
		FRequestEdit = 8,
		// Token: 0x040029E3 RID: 10723
		FDisplayBind = 16,
		// Token: 0x040029E4 RID: 10724
		FDefaultBind = 32,
		// Token: 0x040029E5 RID: 10725
		FHidden = 64,
		// Token: 0x040029E6 RID: 10726
		FRestricted = 128,
		// Token: 0x040029E7 RID: 10727
		FDefaultCollelem = 256,
		// Token: 0x040029E8 RID: 10728
		FUiDefault = 512,
		// Token: 0x040029E9 RID: 10729
		FNonBrowsable = 1024,
		// Token: 0x040029EA RID: 10730
		FReplaceable = 2048,
		// Token: 0x040029EB RID: 10731
		FImmediateBind = 4096
	}
}
