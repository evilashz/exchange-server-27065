using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200025B RID: 603
	[ClassAccessLevel(AccessLevel.Implementation)]
	[StructLayout(LayoutKind.Explicit)]
	internal struct _PV
	{
		// Token: 0x040010A9 RID: 4265
		[FieldOffset(0)]
		internal short s;

		// Token: 0x040010AA RID: 4266
		[FieldOffset(0)]
		internal int i;

		// Token: 0x040010AB RID: 4267
		[FieldOffset(0)]
		internal long l;

		// Token: 0x040010AC RID: 4268
		[FieldOffset(0)]
		internal float f;

		// Token: 0x040010AD RID: 4269
		[FieldOffset(0)]
		internal double d;

		// Token: 0x040010AE RID: 4270
		[FieldOffset(0)]
		internal IntPtr intPtr;

		// Token: 0x040010AF RID: 4271
		[FieldOffset(0)]
		internal CountAndPtr cp;
	}
}
