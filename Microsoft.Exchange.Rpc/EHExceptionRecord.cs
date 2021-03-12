using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x02000450 RID: 1104
[NativeCppClass]
[StructLayout(LayoutKind.Sequential, Size = 64)]
internal struct EHExceptionRecord
{
	// Token: 0x0400107C RID: 4220
	private long <alignment\u0020member>;

	// Token: 0x02000451 RID: 1105
	[NativeCppClass]
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, Size = 32)]
	public struct EHParameters
	{
		// Token: 0x0400107D RID: 4221
		private long <alignment\u0020member>;
	}
}
