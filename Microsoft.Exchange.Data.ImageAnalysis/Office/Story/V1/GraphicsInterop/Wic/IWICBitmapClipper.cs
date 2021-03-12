using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000029 RID: 41
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("E4FBCF03-223D-4E81-9333-D635556DD1B5")]
	internal interface IWICBitmapClipper : IWICBitmapSource
	{
		// Token: 0x06000109 RID: 265
		void GetSize(out int puiWidth, out int puiHeight);

		// Token: 0x0600010A RID: 266
		void GetPixelFormat(out Guid pPixelFormat);

		// Token: 0x0600010B RID: 267
		void GetResolution(out double pDpiX, out double pDpiY);

		// Token: 0x0600010C RID: 268
		void CopyPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x0600010D RID: 269
		void CopyPixels([In] ref WICRect prc, [In] int cbStride, [In] int cbBufferSize, IntPtr pbBuffer);

		// Token: 0x0600010E RID: 270
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pISource, [In] ref WICRect prc);
	}
}
