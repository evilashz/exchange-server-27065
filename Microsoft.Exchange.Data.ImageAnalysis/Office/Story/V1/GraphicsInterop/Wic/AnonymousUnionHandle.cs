using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000024 RID: 36
	[StructLayout(LayoutKind.Explicit, Pack = 8, Size = 8)]
	internal struct AnonymousUnionHandle
	{
		// Token: 0x040000C7 RID: 199
		[FieldOffset(0)]
		public int hInproc;

		// Token: 0x040000C8 RID: 200
		[FieldOffset(0)]
		public long hInproc64;
	}
}
