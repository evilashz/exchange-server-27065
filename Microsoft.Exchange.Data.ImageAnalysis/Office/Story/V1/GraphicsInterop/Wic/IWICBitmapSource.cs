using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000027 RID: 39
	[Guid("00000120-A8F2-4877-BA0A-FD2B6645FB94")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICBitmapSource
	{
		// Token: 0x060000FC RID: 252
		void GetSize(out int puiWidth, out int puiHeight);

		// Token: 0x060000FD RID: 253
		void GetPixelFormat(out Guid pPixelFormat);

		// Token: 0x060000FE RID: 254
		void GetResolution(out double pDpiX, out double pDpiY);

		// Token: 0x060000FF RID: 255
		void CopyPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x06000100 RID: 256
		void CopyPixels([In] ref WICRect prc, [In] int cbStride, [In] int cbBufferSize, IntPtr pbBuffer);
	}
}
