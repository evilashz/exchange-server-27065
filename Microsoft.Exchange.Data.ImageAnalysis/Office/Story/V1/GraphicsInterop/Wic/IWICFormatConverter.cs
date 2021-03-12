using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000038 RID: 56
	[Guid("00000301-A8F2-4877-BA0A-FD2B6645FB94")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICFormatConverter : IWICBitmapSource
	{
		// Token: 0x0600019D RID: 413
		void GetSize(out int puiWidth, out int puiHeight);

		// Token: 0x0600019E RID: 414
		void GetPixelFormat(out Guid pPixelFormat);

		// Token: 0x0600019F RID: 415
		void GetResolution(out double pDpiX, out double pDpiY);

		// Token: 0x060001A0 RID: 416
		void CopyPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x060001A1 RID: 417
		void CopyPixels([In] ref WICRect prc, [In] int cbStride, [In] int cbBufferSize, IntPtr pbBuffer);

		// Token: 0x060001A2 RID: 418
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pISource, [In] ref Guid dstFormat, [In] WICBitmapDitherType dither, [MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette, [In] double alphaThresholdPercent, [In] WICBitmapPaletteType paletteTranslate);

		// Token: 0x060001A3 RID: 419
		void CanConvert([In] ref Guid srcPixelFormat, [In] ref Guid dstPixelFormat, out int pfCanConvert);
	}
}
