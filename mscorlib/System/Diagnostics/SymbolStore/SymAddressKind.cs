using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003D8 RID: 984
	[ComVisible(true)]
	[Serializable]
	public enum SymAddressKind
	{
		// Token: 0x04001634 RID: 5684
		ILOffset = 1,
		// Token: 0x04001635 RID: 5685
		NativeRVA,
		// Token: 0x04001636 RID: 5686
		NativeRegister,
		// Token: 0x04001637 RID: 5687
		NativeRegisterRelative,
		// Token: 0x04001638 RID: 5688
		NativeOffset,
		// Token: 0x04001639 RID: 5689
		NativeRegisterRegister,
		// Token: 0x0400163A RID: 5690
		NativeRegisterStack,
		// Token: 0x0400163B RID: 5691
		NativeStackRegister,
		// Token: 0x0400163C RID: 5692
		BitField,
		// Token: 0x0400163D RID: 5693
		NativeSectionOffset
	}
}
