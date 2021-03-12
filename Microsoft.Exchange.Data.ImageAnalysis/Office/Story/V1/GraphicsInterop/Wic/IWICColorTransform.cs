using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000036 RID: 54
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("B66F034F-D0E2-40AB-B436-6DE39E321A94")]
	internal interface IWICColorTransform : IWICBitmapSource
	{
		// Token: 0x06000195 RID: 405
		void GetSize(out int puiWidth, out int puiHeight);

		// Token: 0x06000196 RID: 406
		void GetPixelFormat(out Guid pPixelFormat);

		// Token: 0x06000197 RID: 407
		void GetResolution(out double pDpiX, out double pDpiY);

		// Token: 0x06000198 RID: 408
		void CopyPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x06000199 RID: 409
		void CopyPixels([In] ref WICRect prc, [In] int cbStride, [In] int cbBufferSize, IntPtr pbBuffer);

		// Token: 0x0600019A RID: 410
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIBitmapSource, [MarshalAs(UnmanagedType.Interface)] [In] IWICColorContext pIContextSource, [MarshalAs(UnmanagedType.Interface)] [In] IWICColorContext pIContextDest, [In] ref Guid pixelFmtDest);
	}
}
