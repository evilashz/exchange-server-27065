using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x020000CE RID: 206
[NativeCppClass]
[StructLayout(LayoutKind.Sequential, Size = 64)]
internal struct _TP_CALLBACK_ENVIRON_V1
{
	// Token: 0x04000213 RID: 531
	private long <alignment\u0020member>;

	// Token: 0x020000CF RID: 207
	[CLSCompliant(false)]
	[NativeCppClass]
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	public struct <unnamed-type-u>
	{
		// Token: 0x04000214 RID: 532
		[FieldOffset(0)]
		private int <alignment\u0020member>;

		// Token: 0x020000D0 RID: 208
		[NativeCppClass]
		[CLSCompliant(false)]
		[StructLayout(LayoutKind.Sequential, Size = 4)]
		public struct <unnamed-type-s>
		{
			// Token: 0x04000215 RID: 533
			private int <alignment\u0020member>;
		}
	}
}
