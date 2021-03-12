using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000033 RID: 51
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000123-A8F2-4877-BA0A-FD2B6645FB94")]
	internal interface IWICBitmapLock
	{
		// Token: 0x06000185 RID: 389
		void GetSize(out int puiWidth, out int puiHeight);

		// Token: 0x06000186 RID: 390
		void GetStride(out int pcbStride);

		// Token: 0x06000187 RID: 391
		void GetDataPointer(out int pcbBufferSize, out IntPtr ppbData);

		// Token: 0x06000188 RID: 392
		void GetPixelFormat(out Guid pPixelFormat);
	}
}
