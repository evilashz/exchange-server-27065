using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x0200023F RID: 575
[NativeCppClass]
[StructLayout(LayoutKind.Explicit, Size = 32)]
internal struct _RPC_ASYNC_NOTIFICATION_INFO
{
	// Token: 0x04000C7C RID: 3196
	[FieldOffset(0)]
	private long <alignment\u0020member>;

	// Token: 0x02000240 RID: 576
	[CLSCompliant(false)]
	[NativeCppClass]
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	public struct <unnamed-type-APC>
	{
		// Token: 0x04000C7D RID: 3197
		private long <alignment\u0020member>;
	}

	// Token: 0x02000241 RID: 577
	[CLSCompliant(false)]
	[NativeCppClass]
	[StructLayout(LayoutKind.Sequential, Size = 32)]
	public struct <unnamed-type-IOC>
	{
		// Token: 0x04000C7E RID: 3198
		private long <alignment\u0020member>;
	}

	// Token: 0x02000242 RID: 578
	[NativeCppClass]
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	public struct <unnamed-type-HWND>
	{
		// Token: 0x04000C7F RID: 3199
		private long <alignment\u0020member>;
	}
}
