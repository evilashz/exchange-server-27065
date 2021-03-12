using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x0200004A RID: 74
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct WICRect
	{
		// Token: 0x04000189 RID: 393
		public int X;

		// Token: 0x0400018A RID: 394
		public int Y;

		// Token: 0x0400018B RID: 395
		public int Width;

		// Token: 0x0400018C RID: 396
		public int Height;
	}
}
