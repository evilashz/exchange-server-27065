using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000028 RID: 40
	[Guid("00000121-A8F2-4877-BA0A-FD2B6645FB94")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICBitmap : IWICBitmapSource
	{
		// Token: 0x06000101 RID: 257
		void GetSize(out int puiWidth, out int puiHeight);

		// Token: 0x06000102 RID: 258
		void GetPixelFormat(out Guid pPixelFormat);

		// Token: 0x06000103 RID: 259
		void GetResolution(out double pDpiX, out double pDpiY);

		// Token: 0x06000104 RID: 260
		void CopyPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x06000105 RID: 261
		void CopyPixels([In] ref WICRect prc, [In] int cbStride, [In] int cbBufferSize, IntPtr pbBuffer);

		// Token: 0x06000106 RID: 262
		void Lock([In] ref WICRect prcLock, [In] WICBitmapLockFlags flags, [MarshalAs(UnmanagedType.Interface)] out IWICBitmapLock ppILock);

		// Token: 0x06000107 RID: 263
		void SetPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x06000108 RID: 264
		void SetResolution([In] double dpiX, [In] double dpiY);
	}
}
