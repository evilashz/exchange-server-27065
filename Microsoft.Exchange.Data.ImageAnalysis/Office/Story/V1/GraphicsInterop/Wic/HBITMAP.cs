using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000025 RID: 37
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HBITMAP
	{
		// Token: 0x040000C9 RID: 201
		public int fContext;

		// Token: 0x040000CA RID: 202
		public AnonymousUnionHandle u;
	}
}
