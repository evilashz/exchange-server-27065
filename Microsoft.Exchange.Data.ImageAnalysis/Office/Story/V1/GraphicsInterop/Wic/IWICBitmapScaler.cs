using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000034 RID: 52
	[Guid("00000302-A8F2-4877-BA0A-FD2B6645FB94")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICBitmapScaler : IWICBitmapSource
	{
		// Token: 0x06000189 RID: 393
		void GetSize(out int puiWidth, out int puiHeight);

		// Token: 0x0600018A RID: 394
		void GetPixelFormat(out Guid pPixelFormat);

		// Token: 0x0600018B RID: 395
		void GetResolution(out double pDpiX, out double pDpiY);

		// Token: 0x0600018C RID: 396
		void CopyPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x0600018D RID: 397
		void CopyPixels([In] ref WICRect prc, [In] int cbStride, [In] int cbBufferSize, IntPtr pbBuffer);

		// Token: 0x0600018E RID: 398
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pISource, [In] int uiWidth, [In] int uiHeight, [In] WICBitmapInterpolationMode mode);
	}
}
