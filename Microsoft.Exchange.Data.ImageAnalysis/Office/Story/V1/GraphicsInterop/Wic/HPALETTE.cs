using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000026 RID: 38
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HPALETTE
	{
		// Token: 0x040000CB RID: 203
		public int fContext;

		// Token: 0x040000CC RID: 204
		public AnonymousUnionHandle u;
	}
}
